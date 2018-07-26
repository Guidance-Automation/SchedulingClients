using GAClients;
using SchedulingClients.JobStateServiceReference;
using System;

namespace SchedulingClients
{
    public interface IJobStateClient : ICallbackClient
	{
        ServiceOperationResult TryGetJobSummary(int jobId, out JobSummaryData jobSummaryData);

        ServiceOperationResult TryGetParentJobSummaryFromTaskId(int taskId, out JobSummaryData jobSummaryData);

        ServiceOperationResult TryGetCurrentJobSummaryForAgentId(int agentId, out JobSummaryData jobSummaryData);

        event Action<JobProgressData> JobProgressUpdated;
    }
}
