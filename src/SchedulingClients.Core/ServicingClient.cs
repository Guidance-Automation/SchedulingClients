using BaseClients.Core;
using GAAPICommon.Architecture;
using SchedulingClients.Core.ServicingServiceReference;
using System;
using System.ServiceModel;

namespace SchedulingClients.Core
{
    internal class ServicingClient : AbstractCallbackClient<IServicingService>, IServicingClient
    {
        private ServicingServiceCallback callback = new ServicingServiceCallback();

        private bool isDisposed = false;

        public ServicingClient(Uri netTcpUri, TimeSpan heartbeat = default)
                    : base(netTcpUri, heartbeat)
        {
        }

        public event Action<ServiceStateDto> ServiceRequest
        {
            add { callback.ServiceRequest += value; }
            remove { callback.ServiceRequest -= value; }
        }

        public IServiceCallResult SetServiceComplete(int taskId)
        {
            Logger.Trace($"SetServiceComplete() taskId:{taskId}");
            return HandleAPICall(e => e.SetServiceComplete(taskId));
        }

        protected override void Dispose(bool isDisposing)
        {
            Logger.Debug("Dispose({0})", isDisposing);

            if (isDisposed)
                return;

            isDisposed = true;

            base.Dispose(isDisposing);
        }

        protected override void SetInstanceContext()
        {
            context = new InstanceContext(callback);
        }

        public IServiceCallResult<ServiceStateDto[]> GetOutstandingServiceRequests()
        {
            Logger.Trace($"GetOutstandingServiceRequests()");
            return HandleAPICall<ServiceStateDto[]>(e => e.GetOutstandingServiceRequests());
        }

        protected override void HandleSubscriptionHeartbeat(IServicingService channel, Guid key)
        {
            channel.SubscriptionHeartbeat(key);
        }

        protected override void HandleUnsubscribeHeartbeat(IServicingService channel, Guid key)
        {
            channel.UnsubscribeHeartbeat(key);
        }
    }
}