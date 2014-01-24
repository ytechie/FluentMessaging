using Microsoft.ServiceBus.Messaging;
using System;

namespace FluentMessaging
{
    public class TopicSubscriptionSource : IMessageQueueSource
    {
        //public string ConnectionString { get; set; }
        //public string QueueName { get; set; }

        private readonly string _connectionString;
        private readonly string _queueName;
        private readonly string _subscriptionName;

        private SubscriptionClient _subscriptionClient;

        public int MaxConcurrency { get; set; }

        public TopicSubscriptionSource(string connectionString, string queueName, string subscriptionName)
        {
            _connectionString = connectionString;
            _queueName = queueName;
            _subscriptionName = subscriptionName;
        }

        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }

        public string QueueName
        {
            get
            {
                return _queueName;
            }
        }

        public string SubscriptionName
        {
            get
            {
                return _subscriptionName;
            }
        }

        public SubscriptionClient SubscriptionClient
        {
            get
            {
                if (_subscriptionClient == null)
                    _subscriptionClient = SubscriptionClient.CreateFromConnectionString(
                        _connectionString, _queueName, _subscriptionName, ReceiveMode.ReceiveAndDelete);

                return _subscriptionClient;
            }
        }

        public void StartMessagePump(Action<BrokeredMessage> messageProcessor)
        {
            var options = new OnMessageOptions { AutoComplete = true };
            SubscriptionClient.OnMessage(messageProcessor);
        }
    }
}
