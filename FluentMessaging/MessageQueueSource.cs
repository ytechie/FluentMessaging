using System;
using Microsoft.ServiceBus.Messaging;

namespace FluentMessaging
{
    public class MessageQueueSource : IMessageQueueSource
    {
        public string ConnectionString { get; set; }
        public string QueueName { get; set; }

        public int MaxConcurrency { get; set; }

        public void StartMessagePump(Action<BrokeredMessage> messageProcessor)
        {
            var sourceQueue = QueueClient.CreateFromConnectionString(ConnectionString, QueueName);

            sourceQueue.OnMessage(messageProcessor);
        }
    }
}
