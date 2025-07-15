using GAAPICommon.Enums;
using GAAPICommon.Messages;
using System.Net;

namespace Guidance.SchedulingClients.Jobs;

public interface IJobBuilderClient : IDisposable 
{
    /// <summary>
    /// Commits a job - indicates a job is fully described and ready to be processed.
    /// </summary>
    /// <param name="jobId">Target job Id</param>
    /// <param name="agentId">Target agent to complete the job. Default (-1) indicates allocation handled by the scheduler.</param>
    /// <returns>Successful service call result on success</returns>
    public bool CommitJob(int jobId, int agentId = -1);

    /// <summary>
    /// Commits a job - indicates a job is fully described and ready to be processed.
    /// </summary>
    /// <param name="jobId">Target job Id</param>
    /// <param name="agentId">Target agent to complete the job. Default (-1) indicates allocation handled by the scheduler.</param>
    /// <returns>Successful service call result on success</returns>
    public Task<bool> CommitJobAsync(int jobId, int agentId = -1);

    /// <summary>
    /// Creates a new job and places it in the editing state.
    /// </summary>
    /// <returns>JobDto containing job Id and root ordered list task Id</returns>
    public JobDto? CreateJob(JobPriority jobPriority = JobPriority.Normal);

    /// <summary>
    /// Creates a new job and places it in the editing state.
    /// </summary>
    /// <returns>JobDto containing job Id and root ordered list task Id</returns>
    public Task<JobDto?> CreateJobAsync(JobPriority jobPriority = JobPriority.Normal);

    /// <summary>
    /// Creates a new ordered list task (subtasks must be completed in order).
    /// </summary>
    /// <param name="parentTaskId">Target parent task to attach to.</param>
    /// <returns>Id of newly created ordered list task.</returns>
    public int CreateOrderedListTask(int parentTaskId);

    /// <summary>
    /// Creates a new ordered list task (subtasks must be completed in order).
    /// </summary>
    /// <param name="parentTaskId">Target parent task to attach to.</param>
    /// <returns>Id of newly created ordered list task.</returns>
    public Task<int> CreateOrderedListTaskAsync(int parentTaskId);

    /// <summary>
    /// Creates a new unordered list task (subtasks may be completed in any order).
    /// </summary>
    /// <param name="parentTaskId">Target parent task to attach to.</param>
    /// <returns>Id of newly created unordered list task.</returns>
    public int CreateUnorderedListTask(int parentTaskId);

    /// <summary>
    /// Creates a new unordered list task (subtasks may be completed in any order).
    /// </summary>
    /// <param name="parentTaskId">Target parent task to attach to.</param>
    /// <returns>Id of newly created unordered list task.</returns>
    public Task<int> CreateUnorderedListTaskAsync(int parentTaskId);

    /// <summary>
    /// Creates a new atomic move list task (subtasks must be atomic moves and completed atomically).
    /// </summary>
    /// <param name="parentTaskId">Target parent task to attach to.</param>
    /// <returns>Id of newly created atomic move list task.</returns>
    public int CreateAtomicMoveListTask(int parentTaskId);

    /// <summary>
    /// Creates a new atomic move list task (subtasks must be atomic moves and completed atomically).
    /// </summary>
    /// <param name="parentTaskId">Target parent task to attach to.</param>
    /// <returns>Id of newly created atomic move list task.</returns>
    public Task<int> CreateAtomicMoveListTaskAsync(int parentTaskId);

    /// <summary>
    /// Creates a new servicing task.
    /// </summary>
    /// <param name="parentListTaskId">Target parent task to attach to.</param>
    /// <param name="nodeId">Target node for service.</param>
    /// <param name="serviceType">The type of service to be performed.</param>
    /// <param name="expectedDuration">An estimated duration of the task.</param>
    /// <returns>Id of newly created servicing task.</returns>
    public int CreateServicingTask(int parentListTaskId, int nodeId, ServiceType serviceType, TimeSpan expectedDuration = default);

    /// <summary>
    /// Creates a new servicing task.
    /// </summary>
    /// <param name="parentListTaskId">Target parent task to attach to.</param>
    /// <param name="nodeId">Target node for service.</param>
    /// <param name="serviceType">The type of service to be performed.</param>
    /// <param name="expectedDuration">An estimated duration of the task.</param>
    /// <returns>Id of newly created servicing task.</returns>
    public Task<int> CreateServicingTaskAsync(int parentListTaskId, int nodeId, ServiceType serviceType, TimeSpan expectedDuration = default);

    /// <summary>
    /// Creates a new sleeping task. (Sleep at a node for a predetermined time).
    /// </summary>
    /// <param name="parentListTaskId">Target parent task to attach to.</param>
    /// <param name="nodeId">Target node for sleeping.</param>
    /// <param name="expectedDuration">Time to sleep for.</param>
    /// <returns>Id of newly created sleeping task.</returns>
    public int CreateSleepingTask(int parentListTaskId, int nodeId, TimeSpan expectedDuration = default);

    /// <summary>
    /// Creates a new sleeping task. (Sleep at a node for a predetermined time).
    /// </summary>
    /// <param name="parentListTaskId">Target parent task to attach to.</param>
    /// <param name="nodeId">Target node for sleeping.</param>
    /// <param name="expectedDuration">Time to sleep for.</param>
    /// <returns>Id of newly created sleeping task.</returns>
    public Task<int> CreateSleepingTaskAsync(int parentListTaskId, int nodeId, TimeSpan expectedDuration = default);

    /// <summary>
    /// Creates a new atomic move task (a sequence of move that must be completed in an atomic operation).
    /// </summary>
    /// <param name="parentAtomicMoveListTaskId">Target parent atomic move list task to attach to.</param>
    /// <param name="moveId">Move to follow.</param>
    /// <returns>Id of newly created atomic move task.</returns>
    public int CreateAtomicMoveTask(int parentAtomicMoveListTaskId, int moveId);

    /// <summary>
    /// Creates a new atomic move task (a sequence of move that must be completed in an atomic operation).
    /// </summary>
    /// <param name="parentAtomicMoveListTaskId">Target parent atomic move list task to attach to.</param>
    /// <param name="moveId">Move to follow.</param>
    /// <returns>Id of newly created atomic move task.</returns>
    public Task<int> CreateAtomicMoveTaskAsync(int parentAtomicMoveListTaskId, int moveId);

    /// <summary>
    /// Creates a GoTo node task (travel to this node and perform no action).
    /// </summary>
    /// <param name="parentListTaskId">Target parent task to attach to.</param>
    /// <param name="nodeId">Target node Id.</param>
    /// <returns>Id of newly created GoTo node task.</returns>
    public int CreateGoToNodeTask(int parentListTaskId, int nodeId);

    /// <summary>
    /// Creates a GoTo node task (travel to this node and perform no action).
    /// </summary>
    /// <param name="parentListTaskId">Target parent task to attach to.</param>
    /// <param name="nodeId">Target node Id.</param>
    /// <returns>Id of newly created GoTo node task.</returns>
    public Task<int> CreateGoToNodeTaskAsync(int parentListTaskId, int nodeId);

    /// <summary>
    /// Creates a new Awaiting task (travel to this node and this task is only completed when a subsequent task is available).
    /// </summary>
    /// <param name="parentListTaskId">Target parent task to attach to.</param>
    /// <param name="nodeId">Target node Id.</param>
    /// <returns>Id of newly created awaiting task.</returns>
    public int CreateAwaitingTask(int parentListTaskId, int nodeId);

    /// <summary>
    /// Creates a new Awaiting task (travel to this node and this task is only completed when a subsequent task is available).
    /// </summary>
    /// <param name="parentListTaskId">Target parent task to attach to.</param>
    /// <param name="nodeId">Target node Id.</param>
    /// <returns>Id of newly created awaiting task.</returns>
    public Task<int> CreateAwaitingTaskAsync(int parentListTaskId, int nodeId);

    /// <summary>
    /// Issues a new Enum directive.
    /// </summary>
    /// <param name="taskId">Id of servicing task to issue to.</param>
    /// <param name="parameterAlias">Kingpin parameter to set.</param>
    /// <param name="value">Target value.</param>
    /// <returns>Successful service call result on success</returns>
    public bool IssueEnumDirective(int taskId, string parameterAlias, byte value);

    /// <summary>
    /// Issues a new Enum directive.
    /// </summary>
    /// <param name="taskId">Id of servicing task to issue to.</param>
    /// <param name="parameterAlias">Kingpin parameter to set.</param>
    /// <param name="value">Target value.</param>
    /// <returns>Successful service call result on success</returns>
    public Task<bool> IssueEnumDirectiveAsync(int taskId, string parameterAlias, byte value);

    /// <summary>
    /// Issues a new short directive.
    /// </summary>
    /// <param name="taskId">Id of servicing task to issue to.</param>
    /// <param name="parameterAlias">Kingpin parameter to set.</param>
    /// <param name="value">Target value.</param>
    /// <returns>Successful service call result on success</returns>
    public bool IssueShortDirective(int taskId, string parameterAlias, short value);

    /// <summary>
    /// Issues a new short directive.
    /// </summary>
    /// <param name="taskId">Id of servicing task to issue to.</param>
    /// <param name="parameterAlias">Kingpin parameter to set.</param>
    /// <param name="value">Target value.</param>
    /// <returns>Successful service call result on success</returns>
    public Task<bool> IssueShortDirectiveAsync(int taskId, string parameterAlias, short value);

    /// <summary>
    /// Issues a new unsigned short directive.
    /// </summary>
    /// <param name="taskId">Id of servicing task to issue to.</param>
    /// <param name="parameterAlias">Kingpin parameter to set.</param>
    /// <param name="value">Target value.</param>
    /// <returns>Successful service call result on success</returns>
    public bool IssueUShortDirective(int taskId, string parameterAlias, ushort value);

    /// <summary>
    /// Issues a new unsigned short directive.
    /// </summary>
    /// <param name="taskId">Id of servicing task to issue to.</param>
    /// <param name="parameterAlias">Kingpin parameter to set.</param>
    /// <param name="value">Target value.</param>
    /// <returns>Successful service call result on success</returns>
    public Task<bool> IssueUShortDirectiveAsync(int taskId, string parameterAlias, ushort value);

    /// <summary>
    /// Issues a new float directive.
    /// </summary>
    /// <param name="taskId">Id of servicing task to issue to.</param>
    /// <param name="parameterAlias">Kingpin parameter to set.</param>
    /// <param name="value">Target value.</param>
    /// <returns>Successful service call result on success</returns>
    public bool IssueFloatDirective(int taskId, string parameterAlias, float value);

    /// <summary>
    /// Issues a new float directive.
    /// </summary>
    /// <param name="taskId">Id of servicing task to issue to.</param>
    /// <param name="parameterAlias">Kingpin parameter to set.</param>
    /// <param name="value">Target value.</param>
    /// <returns>Successful service call result on success</returns>
    public Task<bool> IssueFloatDirectiveAsync(int taskId, string parameterAlias, float value);

    /// <summary>
    /// Issues a new IP address directive.
    /// </summary>
    /// <param name="taskId">Id of servicing task to issue to.</param>
    /// <param name="parameterAlias">Kingpin parameter to set.</param>
    /// <param name="value">Target value.</param>
    /// <returns>Successful service call result on success</returns>
    public bool IssueIPAddressDirective(int taskId, string parameterAlias, IPAddress value);

    /// <summary>
    /// Issues a new IP address directive.
    /// </summary>
    /// <param name="taskId">Id of servicing task to issue to.</param>
    /// <param name="parameterAlias">Kingpin parameter to set.</param>
    /// <param name="value">Target value.</param>
    /// <returns>Successful service call result on success</returns>
    public Task<bool> IssueIPAddressDirectiveAsync(int taskId, string parameterAlias, IPAddress value);

    /// <summary>
    /// Begins editing an existing job.
    /// </summary>
    /// <param name="jobId">Target job Id.</param>
    /// <returns>True on success.</returns>
    public bool BeginEditingJob(int jobId);

    /// <summary>
    /// Begins editing an existing job.
    /// </summary>
    /// <param name="jobId">Target job Id.</param>
    /// <returns>True on success.</returns>
    public Task<bool> BeginEditingJobAsync(int jobId);

    /// <summary>
    /// Finishes editing an existing job, allowing the job to be progressed.
    /// </summary>
    /// <param name="jobId">Target job Id.</param>
    /// <returns>True on success.</returns>
    public bool FinishEditingJob(int jobId);

    /// <summary>
    /// Finishes editing an existing job, allowing the job to be progressed.
    /// </summary>
    /// <param name="jobId">Target job Id.</param>
    /// <returns>True on success.</returns>
    public Task<bool> FinishEditingJobAsync(int jobId);
}