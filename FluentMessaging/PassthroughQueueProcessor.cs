using System.Collections.Generic;
using Microsoft.ServiceBus.Messaging;

namespace FluentMessaging
{
    public class PassthroughQueueProcessor : IQueueProcessor
    {
        public void ProcessMessages(IEnumerable<BrokeredMessage> messages, IMessageSink output)
        {
            foreach (var message in messages)
            {
                var sendMessage = message.Clone();

                output.SendMessage(sendMessage);
            }
        }
    }
}
