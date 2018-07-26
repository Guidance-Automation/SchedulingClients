using GAClients;
using SchedulingClients.Client_Interfaces;
using SchedulingClients.TaskStateServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingClients
{
    internal class TaskStateClient : AbstractCallbackClient<ITaskStateService>, ITaskStateClient
    {
        private TaskStateServiceCallback callback = new TaskStateServiceCallback();

        private TimeSpan heartbeat;

        public TaskStateClient(Uri netTcpUri, TimeSpan hearbeat = default(TimeSpan))
            : base(netTcpUri)
        {
            this.heartbeat = heartbeat < TimeSpan.FromMilliseconds(1000) ? TimeSpan.FromMilliseconds(1000) : heartbeat;
        }

        public event Action<TaskProgressData> TaskProgressUpdated
        {
            add { callback.TaskProgressUpdated += value; }
            remove { callback.TaskProgressUpdated -= value; }
        }

        /// <summary>
        /// Heartbeat time
        /// </summary>
        public TimeSpan Heartbeat { get { return heartbeat; } }

        private bool isDisposed = false;

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposed)
            {
                return;
            }

            isDisposed = true;

            base.Dispose(isDisposing);
        }

        protected override void HeartbeatThread()
        {
            Logger.Debug("HeartbeatThread()");

            ChannelFactory<ITaskStateService> channelFactory = CreateChannelFactory();
            ITaskStateService taskStateService = channelFactory.CreateChannel();

            bool? exceptionCaught;

            while (!Terminate)
            {
                exceptionCaught = null;

                try
                {
                    Logger.Trace("SubscriptionHeartbeat({0})", Key);
                    taskStateService.SubscriptionHeartbeat(Key);
                    IsConnected = true;
                    exceptionCaught = false;
                }
                catch (EndpointNotFoundException)
                {
                    Logger.Warn("HeartbeatThread - EndpointNotFoundException. Is the server running?");
                    exceptionCaught = true;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    exceptionCaught = true;
                }

                if (exceptionCaught == true)
                {
                    channelFactory.Abort();
                    IsConnected = false;

                    channelFactory = CreateChannelFactory(); // Create a new channel as this one is dead
                    taskStateService = channelFactory.CreateChannel();
                }

                heartbeatReset.WaitOne(Heartbeat);
            }

            Logger.Debug("HeartbeatThread exit");
        }


        protected override void SetInstanceContext()
        {
            this.context = new InstanceContext(this.callback);
        }

    }
}
