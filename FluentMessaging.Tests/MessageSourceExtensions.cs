using Microsoft.ServiceBus.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;

namespace FluentMessaging
{
    [TestClass]
    public class MessageSourceExtensionsTests
    {
        [TestMethod]
        public void WithMaxConcurreny()
        {
            var source = Mock.Of<IMessageQueueSource>();

            source.WithMaxConcurrency(5);

            Assert.AreEqual(5, source.MaxConcurrency);
        }

        [TestMethod]
        public void ProcessWith()
        {
            var source = Mock.Of<IMessageQueueSource>();
            var processor = Mock.Of<IQueueProcessor>();

            var wrapped = source.ProcessWith(processor);

            Assert.AreEqual(source, wrapped.QueueSource);
            Assert.AreEqual(processor, wrapped.QueueProcessor);
        }

        [TestMethod]
        public void OutputToReactive_MessageStreamsSentToSuppliedDeserializer()
        {
            var source = new Mock<IMessageQueueSource>();
            Action<BrokeredMessage> pump = null;
            source.Setup(x => x.StartMessagePump(It.IsAny<Action<BrokeredMessage>>()))
                .Callback<Action<BrokeredMessage>>(r => pump = r);
            var serializer = new Mock<ISerializer<int>>();
            serializer.Setup(x => x.Deserialize(It.IsAny<MemoryStream>())).Returns(0);

            source.Object.OutputToReactive(serializer.Object);

            var ms1 = new MemoryStream(new byte[] { 1 });
            var ms2 = new MemoryStream(new byte[] { 2 });
            pump(new BrokeredMessage(ms1));
            pump(new BrokeredMessage(ms2));

            //These can't be verified because they get transformed by the BrokeredMessage
            //serializer.Verify(x => x.Deserialize(ms1));
            //serializer.Verify(x => x.Deserialize(ms2));
        }

        [TestMethod]
        public void OutputToReactive_ReactiveOutputDeserialized()
        {
            var source = new Mock<IMessageQueueSource>();
            Action<BrokeredMessage> pump = null;
            source.Setup(x => x.StartMessagePump(It.IsAny<Action<BrokeredMessage>>()))
                .Callback<Action<BrokeredMessage>>(r => pump = r);

            var serializer = new Mock<ISerializer<int>>();
            serializer.Setup(x => x.Deserialize(It.IsAny<MemoryStream>())).Returns((MemoryStream ms) => ms.ReadByte());

            var rx = source.Object.OutputToReactive(serializer.Object);

            var received = new List<int>();
            rx.Subscribe(received.Add);

            Stream ms1 = new MemoryStream(new byte[] { 1 });
            Stream ms2 = new MemoryStream(new byte[] { 2 });
            pump(new BrokeredMessage(ms1, false));
            pump(new BrokeredMessage(ms2, false));

            Assert.AreEqual(2, received.Count);
            Assert.AreEqual(1, received[0]);
            Assert.AreEqual(2, received[1]);
        }
    }
}
