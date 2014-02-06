using System.Collections.Generic;
using Microsoft.ServiceBus.Messaging;

namespace Microsoft.FluentMessaging
{
    public static class ProcessorWrapperExtensions
    {
        public static void OutputToQueue(this ProcessorWrapper queueProcessor, string connectionString, string queueName)
        {
            var destQueue = QueueClient.CreateFromConnectionString(connectionString, queueName);
            var sender = new QueueOutput(destQueue);

            var processor = queueProcessor.QueueProcessor;
            
            queueProcessor.QueueSource.StartMessagePump(message =>
            {
                processor.ProcessMessages(new List<BrokeredMessage> { message }, sender);
            });
        }

        public static void OutputToTopic(this ProcessorWrapper queueProcessor, string connectionString, string queueName)
        {
            var destTopic = TopicClient.CreateFromConnectionString(connectionString, queueName);
            var sender = new TopicOutput(destTopic);

            var processor = queueProcessor.QueueProcessor;

            queueProcessor.QueueSource.StartMessagePump(message =>
            {
                processor.ProcessMessages(new List<BrokeredMessage> { message }, sender);
            });
        }
    }
}
