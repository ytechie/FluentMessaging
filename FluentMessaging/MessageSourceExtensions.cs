using log4net;
using Microsoft.Practices.ServiceLocation;
using System.IO;
using System.Reactive.Subjects;
using System.Reflection;

namespace FluentMessaging
{
    public static class MessageSourceExtensions
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static IMessageQueueSource WithMaxConcurrency(this IMessageQueueSource queueSource, int threads)
        {
            queueSource.MaxConcurrency = threads;
            return queueSource;
        }

        public static ProcessorWrapper ProcessWith<TProcessorType>(this IMessageQueueSource queueSource) where TProcessorType : IQueueProcessor
        {
            var queueProcessor = (IQueueProcessor)ServiceLocator.Current.GetInstance(typeof(TProcessorType));

            return queueSource.ProcessWith(queueProcessor);
        }

        public static ProcessorWrapper ProcessWith(this IMessageQueueSource queueSource, IQueueProcessor queueProcessor)
        {
            var messageQueueProcessor = new ProcessorWrapper(queueSource, queueProcessor);

            return messageQueueProcessor;
        }

        //This is a shortcut method that should wrap a transformer
        public static Subject<TOutput> OutputToReactive<TOutput>(this IMessageQueueSource queueSource, ISerializer<TOutput> serializer)
        {
            var rx = new Subject<TOutput>();
            queueSource.StartMessagePump(message =>
            {
                var stream = message.GetBody<Stream>();
                var obj = serializer.Deserialize(stream);
                rx.OnNext(obj);
            });

            return rx;
        }
    }
}
