using Microsoft.ServiceBus.Messaging;
using System;

namespace FluentMessaging
{
    public class TopicSubscriptionSource : IMessageQueueSource
    {
        public string ConnectionString { get; set; }
        public string QueueName { get; set; }

        public string SubscriptionName { get; set; }

        public int MaxConcurrency { get; set; }

        public void StartMessagePump(Action<BrokeredMessage> messageProcessor)
        {
            var subClient = SubscriptionClient.CreateFromConnectionString(ConnectionString, QueueName, SubscriptionName, ReceiveMode.ReceiveAndDelete);

            var options = new OnMessageOptions { AutoComplete = true };
            subClient.OnMessage(messageProcessor);

        }
    }
}
