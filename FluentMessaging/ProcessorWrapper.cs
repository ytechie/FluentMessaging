namespace Microsoft.FluentMessaging
{
    public class ProcessorWrapper
    {
        public IMessageQueueSource QueueSource { get; set; }

        public IQueueProcessor QueueProcessor { get; set; }

        public ProcessorWrapper(IMessageQueueSource queueSource, IQueueProcessor queueProcessor)
        {
            QueueSource = queueSource;
            QueueProcessor = queueProcessor;
        }
    }
}
