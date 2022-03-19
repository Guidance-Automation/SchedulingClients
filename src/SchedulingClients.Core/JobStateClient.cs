using BaseClients.Core;
using GAAPICommon.Architecture;
using GAAPICommon.Core.Dtos;
using SchedulingClients.Core.JobStateServiceReference;
using System;
using System.ServiceModel;

namespace SchedulingClients.Core
{
    internal class JobStateClient : AbstractCallbackClient<IJobStateService>, IJobStateClient
    {
        private JobStateServiceCallback callback = new JobStateServiceCallback();

        private bool isDisposed = false;

        public JobStateClient(Uri netTcpUri, TimeSpan heartbeat = default)
            : base(netTcpUri, heartbeat)
        {
        }

        public event Action<JobProgressDto> JobProgressUpdated
        {
            add { callback.JobProgressUpdated += value; }
            remove { callback.JobProgressUpdated -= value; }
        }

        public IServiceCallResult<JobSummaryDto> GetJobSummary(int jobId)
        {
            Logger.Trace($"GetJobSummary() jobId:{jobId}");
            return HandleAPICall<JobSummaryDto>(e => e.GetJobSummary(jobId));
        }

        public IServiceCallResult<JobSummaryDto> GetParentJobSummaryFromTaskId(int taskId)
        {
            Logger.Trace($"GetParentJobSummaryFromTaskId() taskId:{taskId}");
            return HandleAPICall<JobSummaryDto>(e => e.GetParentJobSummaryFromTaskId(taskId));
        }

        public IServiceCallResult<JobSummaryDto> GetCurrentJobSummaryForAgentId(int agentId)
        {
            Logger.Trace($"GetParentJobSummaryFromTaskId() agentId:{agentId}");
            return HandleAPICall<JobSummaryDto>(e => e.GetCurrentJobSummaryForAgentId(agentId));
        }

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

        protected override void HandleSubscriptionHeartbeat(IJobStateService channel, Guid key)
        {
            channel.SubscriptionHeartbeat(key);
        }

        protected override void HandleUnsubscribeHeartbeat(IJobStateService channel, Guid key)
        {
            channel.UnsubscribeHeartbeat(key);
        }
    }
}