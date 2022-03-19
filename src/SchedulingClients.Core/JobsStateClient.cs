using BaseClients.Core;
using GAAPICommon.Architecture;
using MoreLinq;
using SchedulingClients.Core.JobsStateServiceReference;
using System;
using System.Linq;
using System.ServiceModel;

namespace SchedulingClients.Core
{
    internal class JobsStateClient : AbstractCallbackClient<IJobsStateService>, IJobsStateClient
    {
        private JobsStateServiceCallback callback = new JobsStateServiceCallback();

        private bool isDisposed = false;

        private JobsStateDto jobsStateDto = null;

        public JobsStateClient(Uri netTcpUri, TimeSpan heartbeat = default)
                    : base(netTcpUri, heartbeat)
        {
            callback.JobsStateChange += Callback_JobsStateChange;
        }

        public event Action<JobsStateDto> JobsStateUpdated;

        public JobsStateDto JobsState
        {
            get { return jobsStateDto; }

            private set
            {
                if (jobsStateDto != value)
                {
                    jobsStateDto = value;
                    OnJobsStateUpdated(jobsStateDto);
                }
            }
        }

        private void OnJobsStateUpdated(JobsStateDto jobsStateDto)
        {
            Action<JobsStateDto> handlers = JobsStateUpdated;

            handlers?
                   .GetInvocationList()
                   .Cast<Action<JobsStateDto>>()
                   .ForEach(e => e.BeginInvoke(jobsStateDto, null, null));
        }

        public IServiceCallResult AbortAllJobs()
        {
            Logger.Trace("AbortAllJobs()");
            return HandleAPICall(e => e.AbortAllJobs());
        }

        public IServiceCallResult AbortAllJobsForAgent(int agentId)
        {
            Logger.Trace($"AbortAllJobsForAgent() agentId:{agentId}");
            return HandleAPICall(e => e.AbortAllJobsForAgent(agentId));
        }

        public IServiceCallResult AbortJob(int jobId, string note)
        {
            if (string.IsNullOrEmpty(note))
                note = "Unspecified";

            Logger.Trace($"AbortJob() jobId:{jobId} note:{note}");
            return HandleAPICall(e => e.AbortJob(jobId, note));
        }

        public IServiceCallResult AbortTask(int taskId)
        {
            Logger.Trace($"AbortTask() taskId:{taskId}");
            return HandleAPICall(e => e.AbortTask(taskId));
        }

        public IServiceCallResult<int[]> GetActiveJobIdsForAgent(int agentId)
        {
            Logger.Trace($"GetActiveJobIdsForAgent() agentId:{agentId}");
            return HandleAPICall<int[]>(e => e.GetActiveJobIdsForAgent(agentId));
        }

        protected override void Dispose(bool isDisposing)
        {
            Logger.Debug("Dispose({0})", isDisposing);

            if (isDisposed)
                return;

            callback.JobsStateChange -= Callback_JobsStateChange;
            isDisposed = true;

            base.Dispose(isDisposing);
        }

        protected override void SetInstanceContext()
        {
            context = new InstanceContext(callback);
        }

        private void Callback_JobsStateChange(JobsStateDto newJobsStateDto)
        {
            JobsState = newJobsStateDto;
        }

        protected override void HandleSubscriptionHeartbeat(IJobsStateService channel, Guid key)
        {
            channel.SubscriptionHeartbeat(key);
        }

        protected override void HandleUnsubscribeHeartbeat(IJobsStateService channel, Guid key)
        {
            channel.UnsubscribeHeartbeat(key);
        }
    }
}