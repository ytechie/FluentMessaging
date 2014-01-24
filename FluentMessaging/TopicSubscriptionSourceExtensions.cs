using System;
using Microsoft.ServiceBus.Messaging;

namespace FluentMessaging
{
    public static class TopicSubscriptionSourceExtensions
    {
        public static TopicSubscriptionSource WithOptions(this TopicSubscriptionSource topicSubscriptionSource, Action<SubscriptionClient> action)
        {
            action(topicSubscriptionSource.SubscriptionClient);
            return topicSubscriptionSource;
        }

        public static TopicSubscriptionSource WithPrefetchCount(this TopicSubscriptionSource topicSubscriptionSource, int prefetchCount)
        {
            return topicSubscriptionSource.WithOptions(x => x.PrefetchCount = prefetchCount);
        }
    }
}
