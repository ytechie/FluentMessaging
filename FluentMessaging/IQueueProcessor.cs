using System.Collections.Generic;
using Microsoft.ServiceBus.Messaging;

namespace FluentMessaging
{
    public interface IQueueProcessor
    {
        void ProcessMessages(IEnumerable<BrokeredMessage> messages, IMessageSink output);
    }
}
