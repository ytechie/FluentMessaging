using System;
using System.Collections.Generic;
using System.Reflection;
using log4net;
using Microsoft.ServiceBus.Messaging;

namespace FluentMessaging
{
    public class QueueOutput : IMessageSink
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly QueueClient _output;

        public QueueOutput(QueueClient outputQueue)
        {
            _output = outputQueue;
        }

        public void SendMessage(BrokeredMessage message)
        {
            try
            {
                _output.Send(message);
            }
            catch (Exception ex)
            {
                Log.Error("Error sending service bus message", ex);
                throw;
            }
        }

        public void SendMessages(IEnumerable<BrokeredMessage> messages, int batchSize)
        {
            var batch = new List<BrokeredMessage>();
            foreach (var message in messages)
            {
                batch.Add(message);
                if (batch.Count == batchSize)
                {
                    //Flush the batch
                    _output.SendBatch(batch);
                    batch.Clear();
                }
            }

            Log.DebugFormat("Batched {0} messages to send", batch.Count);
            try
            {
                //Send the remainder
                _output.SendBatch(batch);
            }
            catch (Exception ex)
            {
                Log.Error("Error sending service bus messages in a batch", ex);
                throw;
            }
        }
    }
}
