using BaseClients.Architecture;
using GAAPICommon.Architecture;
using SchedulingClients.Core.JobBuilderServiceReference;
using System;
using System.Net;

namespace SchedulingClients.Core
{
    /// <summary>
    /// For creating and editing jobs in the system.
    /// </summary>
    public interface IJobBuilderClient : IClient
    {
        /// <summary>
        /// Commits a job - indicates a job is fully described and ready to be processed.
        /// </summary>
        /// <param name="jobId">Target job Id</param>
        /// <param name="agentId">Target agent to complete the job. Default (-1) indicates allocation handled by the scheduler.</param>
        /// <returns>Successful service call result on success</returns>
        IServiceCallResult CommitJob(int jobId, int agentId = -1);

        /// <summary>
        /// Creates a new job and places it in the editing state.
        /// </summary>
        /// <returns>JobDto containing job Id and root ordered list task Id</returns>
        IServiceCallResult<JobDto> CreateJob(JobPriority jobPriority = JobPriority.Normal);

        /// <summary>
        /// Creates a new ordered list task (subtasks must be completed in order).
        /// </summary>
        /// <param name="parentTaskId">Target parent task to attach to.</param>
        /// <returns>Id of newly created ordered list task.</returns>
        IServiceCallResult<int> CreateOrderedListTask(int parentTaskId);

        /// <summary>
        /// Creates a new unordered list task (subtasks may be completed in any order).
        /// </summary>
        /// <param name="parentTaskId">Target parent task to attach to.</param>
        /// <returns>Id of newly created unordered list task.</returns>
        IServiceCallResult<int> CreateUnorderedListTask(int parentTaskId);

        /// <summary>
        /// Creates a new atomic move list task (subtasks must be atomic moves and completed atomically).
        /// </summary>
        /// <param name="parentTaskId">Target parent task to attach to.</param>
        /// <returns>Id of newly created atomic move list task.</returns>
        IServiceCallResult<int> CreateAtomicMoveListTask(int parentTaskId);

        /// <summary>
        /// Creates a new servicing task.
        /// </summary>
        /// <param name="parentListTaskId">Target parent task to attach to.</param>
        /// <param name="nodeId">Target node for service.</param>
        /// <param name="serviceType">The type of service to be performed.</param>
        /// <param name="expectedDuration">An estimated duration of the task.</param>
        /// <returns>Id of newly created servicing task.</returns>
        IServiceCallResult<int> CreateServicingTask(int parentListTaskId, int nodeId, ServiceType serviceType, TimeSpan expectedDuration = default);

        /// <summary>
        /// Creates a new sleeping task. (Sleep at a node for a predetermined time).
        /// </summary>
        /// <param name="parentListTaskId">Target parent task to attach to.</param>
        /// <param name="nodeId">Target node for sleeping.</param>
        /// <param name="expectedDuration">Time to sleep for.</param>
        /// <returns>Id of newly created sleeping task.</returns>
        IServiceCallResult<int> CreateSleepingTask(int parentListTaskId, int nodeId, TimeSpan expectedDuration = default);

        /// <summary>
        /// Creates a new atomic move task (a sequence of move that must be completed in an atomic operation).
        /// </summary>
        /// <param name="parentAtomicMoveListTaskId">Target parent atomic move list task to attach to.</param>
        /// <param name="moveId">Move to follow.</param>
        /// <returns>Id of newly created atomic move task.</returns>
        IServiceCallResult<int> CreateAtomicMoveTask(int parentAtomicMoveListTaskId, int moveId);

        /// <summary>
        /// Creates a GoTo node task (travel to this node and perform no action).
        /// </summary>
        /// <param name="parentListTaskId">Target parent task to attach to.</param>
        /// <param name="nodeId">Target node Id.</param>
        /// <returns>Id of newly created GoTo node task.</returns>
        IServiceCallResult<int> CreateGoToNodeTask(int parentListTaskId, int nodeId);

        /// <summary>
        /// Creates a new Awaiting task (travel to this node and this task is only completed when a subsequent task is available).
        /// </summary>
        /// <param name="parentListTaskId">Target parent task to attach to.</param>
        /// <param name="nodeId">Target node Id.</param>
        /// <returns>Id of newly created awaiting task.</returns>
        IServiceCallResult<int> CreateAwaitingTask(int parentListTaskId, int nodeId);

        /// <summary>
        /// Issues a new Enum directive.
        /// </summary>
        /// <param name="taskId">Id of servicing task to issue to.</param>
        /// <param name="parameterAlias">Kingpin parameter to set.</param>
        /// <param name="value">Target value.</param>
        /// <returns>Successful service call result on success</returns>
        IServiceCallResult IssueEnumDirective(int taskId, string parameterAlias, byte value);

        /// <summary>
        /// Issues a new short directive.
        /// </summary>
        /// <param name="taskId">Id of servicing task to issue to.</param>
        /// <param name="parameterAlias">Kingpin parameter to set.</param>
        /// <param name="value">Target value.</param>
        /// <returns>Successful service call result on success</returns>
        IServiceCallResult IssueShortDirective(int taskId, string parameterAlias, short value);

        /// <summary>
        /// Issues a new unsigned short directive.
        /// </summary>
        /// <param name="taskId">Id of servicing task to issue to.</param>
        /// <param name="parameterAlias">Kingpin parameter to set.</param>
        /// <param name="value">Target value.</param>
        /// <returns>Successful service call result on success</returns>
        IServiceCallResult IssueUShortDirective(int taskId, string parameterAlias, ushort value);

        /// <summary>
        /// Issues a new float directive.
        /// </summary>
        /// <param name="taskId">Id of servicing task to issue to.</param>
        /// <param name="parameterAlias">Kingpin parameter to set.</param>
        /// <param name="value">Target value.</param>
        /// <returns>Successful service call result on success</returns>
        IServiceCallResult IssueFloatDirective(int taskId, string parameterAlias, float value);

        /// <summary>
        /// Issues a new IP address directive.
        /// </summary>
        /// <param name="taskId">Id of servicing task to issue to.</param>
        /// <param name="parameterAlias">Kingpin parameter to set.</param>
        /// <param name="value">Target value.</param>
        /// <returns>Successful service call result on success</returns>
        IServiceCallResult IssueIPAddressDirective(int taskId, string parameterAlias, IPAddress value);

        /// <summary>
        /// Begins editing an existing job.
        /// </summary>
        /// <param name="jobId">Target job Id.</param>
        /// <returns>True on success.</returns>
        IServiceCallResult<bool> BeginEditingJob(int jobId);

        /// <summary>
        /// Finishes editing an existing job, allowing the job to be progressed.
        /// </summary>
        /// <param name="jobId">Target job Id.</param>
        /// <returns>True on success.</returns>
        IServiceCallResult FinishEditingJob(int jobId);
    }
}