using BaseClients.Architecture;
using GAAPICommon.Core.Dtos;
using SchedulingClients.Core.SchedulingServiceReference;
using System;

namespace SchedulingClients.Core
{
    /// <summary>
    /// Monitors state of the scheduler.
    /// </summary>
    public interface ISchedulingClient : ICallbackClient
    {
        /// <summary>
        /// Fired whenever the scheduler state is updated.
        /// </summary>
        event Action<SchedulerStateDto> Updated;

        /// <summary>
        /// Fired whenever SpotManager is enabled or disabled, or spot bookings change.
        /// </summary>
        event Action<SchedulerStateDto> SpotManagerChanged;

        /// <summary>
        /// The current state of the scheduler.
        /// </summary>
        SchedulerStateDto SchedulerState { get; }
    }
}
