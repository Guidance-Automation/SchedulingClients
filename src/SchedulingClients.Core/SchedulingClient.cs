using BaseClients.Core;
using GAAPICommon.Core.Dtos;
using GACore.Extensions;
using SchedulingClients.Core.SchedulingServiceReference;
using System;
using System.ServiceModel;

namespace SchedulingClients.Core
{
    public class SchedulingClient : AbstractCallbackClient<ISchedulingService>, ISchedulingClient
    {
        private SchedulingServiceCallback callback = new SchedulingServiceCallback();

        private bool isDisposed = false;

        public SchedulingClient(Uri netTcpUri, TimeSpan heartbeat = default)
            : base(netTcpUri, heartbeat)
        {
            callback.Updated += Callback_Updated;
        }

        private void Callback_Updated(SchedulerStateDto schedulerState)
        {
            SchedulerState = schedulerState;
        }

        private SchedulerStateDto schedulerState = null;

        public SchedulerStateDto SchedulerState
        {
            get { return schedulerState; }
            set
            {
                if (schedulerState == null || value.Cycle.IsCurrentByteTickLarger(schedulerState.Cycle))
                    schedulerState = value;
            }
        }
        

        public event Action<SchedulerStateDto> Updated
        {
            add { callback.Updated += value; }
            remove { callback.Updated -= value; }
        }

        protected override void Dispose(bool isDisposing)
        {
            Logger.Debug("Dispose({0})", isDisposing);

            if (isDisposed)
                return;

            if (isDisposing)
                callback.Updated -= Callback_Updated;

            isDisposed = true;

            base.Dispose(isDisposing);
        }

        protected override void HandleSubscriptionHeartbeat(ISchedulingService channel, Guid key)
        {
            channel.SubscriptionHeartbeat(key);
        }

        protected override void HandleUnsubscribeHeartbeat(ISchedulingService channel, Guid key)
        {
            channel.UnsubscribeHeartbeat(key);
        }

        protected override void SetInstanceContext()
        {
            context = new InstanceContext(callback);
        }
    }
}
