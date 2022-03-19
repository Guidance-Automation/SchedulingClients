using BaseClients.Core;
using SchedulingClients.Core.TaskStateServiceReference;
using System;
using System.ServiceModel;

namespace SchedulingClients.Core
{
    internal class TaskStateClient : AbstractCallbackClient<ITaskStateService>, ITaskStateClient
    {
        private TaskStateServiceCallback callback = new TaskStateServiceCallback();

        public TaskStateClient(Uri netTcpUri, TimeSpan heartbeat = default)
            : base(netTcpUri, heartbeat)
        {
        }

        public event Action<TaskProgressDto> TaskProgressUpdated
        {
            add { callback.TaskProgressUpdated += value; }
            remove { callback.TaskProgressUpdated -= value; }
        }

        private bool isDisposed = false;

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposed)
                return;

            isDisposed = true;

            base.Dispose(isDisposing);
        }

        protected override void SetInstanceContext()
        {
            context = new InstanceContext(callback);
        }

        protected override void HandleSubscriptionHeartbeat(ITaskStateService channel, Guid key)
        {
            channel.SubscriptionHeartbeat(key);
        }

        protected override void HandleUnsubscribeHeartbeat(ITaskStateService channel, Guid key)
        {
            channel.UnsubscribeHeartbeat(key);
        }
    }
}