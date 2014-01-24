namespace FluentMessaging
{
    public static class QueueFramework
    {
        public const int DefaultMaxConcurrency = 1;

        public static MessageQueueSource FromQueue(string connectionString, string queueName)
        {
            var source = new MessageQueueSource
            {
                ConnectionString = connectionString,
                QueueName = queueName,
                MaxConcurrency = DefaultMaxConcurrency
            };

            return source;
        }

        public static TopicSubscriptionSource FromTopicSubscription(string connectionString, string queueName, string subscription)
        {
            var source = new TopicSubscriptionSource(connectionString, queueName, subscription);

            return source;
        }
    }
}
