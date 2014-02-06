using System;
using Microsoft.ServiceBus.Messaging;

namespace Microsoft.FluentMessaging
{
    public interface IMessageQueueSource
    {
        void StartMessagePump(Action<BrokeredMessage> messageProcessor);

        int MaxConcurrency { get; set; }
    }
}