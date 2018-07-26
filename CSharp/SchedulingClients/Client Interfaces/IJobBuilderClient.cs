using GAClients;
using SchedulingClients.JobBuilderServiceReference;
using System;
using System.Net;

namespace SchedulingClients
{
  public interface IJobBuilderClient : IClient
	{
		ServiceOperationResult TryCommit(int jobId, out bool success, int agentId = -1);

		ServiceOperationResult TryCreateJob(out JobData jobData);

		ServiceOperationResult TryCreateOrderedListTask(int parentTaskId, bool isFinalised, out int listTaskId);

		ServiceOperationResult TryCreateUnorderedListTask(int parentTaskId, out int listTaskId);

		ServiceOperationResult TryCreatePipelinedTask(int parentTaskId, bool isFinalised, out int listTaskId);

		ServiceOperationResult TryCreateServicingTask(int parentListTaskId, int nodeId, ServiceType serviceType, out int serviceTaskId, TimeSpan expectedDuration = default(TimeSpan));

		ServiceOperationResult TryCreateSleepingTask(int parentListTaskId, int nodeId, out int sleepingTaskId, TimeSpan expectedDuration = default(TimeSpan));

		ServiceOperationResult TryCreateMovingTask(int parentListTaskId, int nodeId, out int moveTaskId);

        ServiceOperationResult TryCreateAwaitingTask(int parentListTaskId, int nodeId, out int awaitTaskId);

		ServiceOperationResult TryIssueDirective(int taskId, int parameterId, byte value);

		ServiceOperationResult TryIssueDirective(int taskId, int parameterId, short value);

		ServiceOperationResult TryIssueDirective(int taskId, int parameterId, ushort value);

		ServiceOperationResult TryIssueDirective(int taskId, int parameterId, float value);

		ServiceOperationResult TryIssueDirective(int taskId, int parameterId, IPAddress value);

		ServiceOperationResult TryFinaliseTask(int taskId);

		ServiceOperationResult TryBeginEditingJob(int jobId);

		ServiceOperationResult TryFinishEditingJob(int jobId);

		ServiceOperationResult TryBeginEditingTask(int taskId);

		ServiceOperationResult TryFinishEditingTask(int taskId);
	}
}
