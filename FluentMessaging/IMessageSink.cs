using System.Collections.Generic;
using Microsoft.ServiceBus.Messaging;

namespace FluentMessaging
{
    public interface IMessageSink
    {
        void SendMessage(BrokeredMessage message);
        void SendMessages(IEnumerable<BrokeredMessage> messages, int batchSize);
    }
}
