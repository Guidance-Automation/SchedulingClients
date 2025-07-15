using GAAPICommon.Messages;

namespace Guidance.SchedulingClients.Scheduling;

public interface ISchedulingClient : IDisposable
{
    /// <summary>
    /// Fired whenever the scheduler state is updated.
    /// </summary>
    public event Action<SchedulerStateDto> SchedulerStateUpdated;

    /// <summary>
    /// The current state of the scheduler.
    /// </summary>
    public SchedulerStateDto? SchedulerState { get; }

    /// <summary>
    /// Unsubscribe from scheduler state updates.
    /// </summary>
    public void Unsubscribe();
}