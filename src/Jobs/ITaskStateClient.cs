using GAAPICommon.Messages;

namespace GAClients.SchedulingClients.Jobs;

/// <summary>
/// Monitors task progress.
/// </summary>
public interface ITaskStateClient : IDisposable
{
    /// <summary>
    /// Fired whenever a task progresses its state. 
    /// </summary>
    public event Action<TaskProgressDto>? TaskProgressUpdated;

    /// <summary>
    /// Unsubscribe from the task updates.
    /// </summary>
    public void Unsubscribe();
}