using GAAPICommon.Messages;

namespace GAClients.SchedulingClients.Jobs;

/// <summary>
/// For tracking individual jobs.
/// </summary>
public interface IJobStateClient : IDisposable
{
    /// <summary>
    /// Gets the summary for a specific job.
    /// </summary>
    /// <param name="jobId">Target job Id.</param>
    /// <returns>Job summary dto containing relevant data.</returns>
    public JobSummaryDto? GetJobSummary(int jobId);

    /// <summary>
    /// Gets the summary for a specific job.
    /// </summary>
    /// <param name="jobId">Target job Id.</param>
    /// <returns>Job summary dto containing relevant data.</returns>
    public Task<JobSummaryDto?> GetJobSummaryAsync(int jobId);

    /// <summary>
    /// Returns the summary for a job, via the id of one of its tasks.
    /// </summary>
    /// <param name="taskId">Target task Id.</param>
    /// <returns>Job summary dto containing relevant data.</returns>
    public JobSummaryDto? GetParentJobSummaryFromTaskId(int taskId);

    /// <summary>
    /// Returns the summary for a job, via the id of one of its tasks.
    /// </summary>
    /// <param name="taskId">Target task Id.</param>
    /// <returns>Job summary dto containing relevant data.</returns>
    public Task<JobSummaryDto?> GetParentJobSummaryFromTaskIdAsync(int taskId);

    /// <summary>
    /// Returns the summary for a job an agent is currently pursuing. 
    /// </summary>
    /// <param name="agentId">Target agent Id.</param>
    /// <returns>Job summary dto containing relevant data.</returns>
    public JobSummaryDto? GetCurrentJobSummaryForAgentId(int agentId);

    /// <summary>
    /// Returns the summary for a job an agent is currently pursuing. 
    /// </summary>
    /// <param name="agentId">Target agent Id.</param>
    /// <returns>Job summary dto containing relevant data.</returns>
    public Task<JobSummaryDto?> GetCurrentJobSummaryForAgentIdAsync(int agentId);

    /// <summary>
    /// Unsubscribe from the job updates.
    /// </summary>
    public void Unsubscribe();

    /// <summary>
    /// Fired whenever a job progress update is received. 
    /// </summary>
    public event Action<JobProgressDto>? JobProgressUpdated;
}