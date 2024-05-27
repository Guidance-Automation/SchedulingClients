using GAAPICommon.Messages;

namespace GAClients.SchedulingClients.Jobs;

public interface IJobsStateClient : IDisposable
{
    /// <summary>
    /// Aborts all jobs in the system.
    /// </summary>
    /// <returns>Successful service call result on success.</returns>
    public bool AbortAllJobs();

    /// <summary>
    /// Aborts all jobs in the system.
    /// </summary>
    /// <returns>Successful service call result on success.</returns>
    public Task<bool> AbortAllJobsAsync();

    /// <summary>
    /// Aborts all jobs for a specific agent.
    /// </summary>
    /// <param name="agentId">Target agent Id.</param>
    /// <returns>Successful service call result on success.</returns>
    public bool AbortAllJobsForAgent(int agentId);

    /// <summary>
    /// Aborts all jobs for a specific agent.
    /// </summary>
    /// <param name="agentId">Target agent Id.</param>
    /// <returns>Successful service call result on success.</returns>
    public Task<bool> AbortAllJobsForAgentAsync(int agentId);

    /// <summary>
    /// Aborts a specific job.
    /// </summary>
    /// <param name="jobId">Target job Id.</param>
    /// <param name="note">Reason for abortion.</param>
    /// <returns>Successful service call result on success.</returns>
    public bool AbortJob(int jobId, string note);

    /// <summary>
    /// Aborts a specific job.
    /// </summary>
    /// <param name="jobId">Target job Id.</param>
    /// <param name="note">Reason for abortion.</param>
    /// <returns>Successful service call result on success.</returns>
    public Task<bool> AbortJobAsync(int jobId, string note);

    /// <summary>
    /// Aborts a specific task.
    /// </summary>
    /// <param name="taskId">Target task Id.</param>
    /// <returns>Successful service call result on success.</returns>
    public bool AbortTask(int taskId);

    /// <summary>
    /// Aborts a specific task.
    /// </summary>
    /// <param name="taskId">Target task Id.</param>
    /// <returns>Successful service call result on success.</returns>
    public Task<bool> AbortTaskAsync(int taskId);

    /// <summary>
    /// Gets the active job Ids for a specific agent.
    /// </summary>
    /// <param name="agentId">Target agent Id.</param>
    /// <returns>Array of active job Ids.</returns>
    public IEnumerable<int>? GetActiveJobIdsForAgent(int agentId);

    /// <summary>
    /// Gets the active job Ids for a specific agent.
    /// </summary>
    /// <param name="agentId">Target agent Id.</param>
    /// <returns>Array of active job Ids.</returns>
    public Task<IEnumerable<int>?> GetActiveJobIdsForAgentAsync(int agentId);

    /// <summary>
    /// A summary of the current state of all jobs in the system.
    /// </summary>
    public JobStateDto? JobsState { get; }

    /// <summary>
    /// Fired whenever the jobs state summary is updated.
    /// </summary>
    public event Action<JobStateDto>? JobsStateUpdated;
}