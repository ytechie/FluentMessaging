using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.FluentMessaging
{
    [TestClass]
    public class MessageQueueSourceTests
    {
        [TestMethod]
        public void FromQueue_ParamtersPersist()
        {
            var source = QueueFramework.FromQueue("a", "b");

            Assert.AreEqual("a", source.ConnectionString);
            Assert.AreEqual("b", source.QueueName);
        }

        [TestMethod]
        public void FromTopicSubscription_ParamtersPersist()
        {
            var source = QueueFramework.FromTopicSubscription("a", "b", "c");

            Assert.AreEqual("a", source.ConnectionString);
            Assert.AreEqual("b", source.QueueName);
            Assert.AreEqual("c", source.SubscriptionName);
        }

        [TestMethod]
        public void MaxConcurrency_ShouldNotBeZero()
        {
            var source = QueueFramework.FromQueue("a", "b");

            Assert.AreNotEqual(0, source.MaxConcurrency);
        }
    }
}
