using System.Collections.Generic;
using Microsoft.ServiceBus.Messaging;

namespace Microsoft.FluentMessaging
{
    public interface IQueueProcessor
    {
        void ProcessMessages(IEnumerable<BrokeredMessage> messages, IMessageSink output);
    }
}
