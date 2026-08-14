using GAAPICommon.Messages;

namespace Guidance.SchedulingClients.Scheduling;

using GAAPICommon.Services.Scheduling;

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

    public ModuleInfoDto? GetModuleInfo();
    public Task<ModuleInfoDto?> GetModuleInfoAsync();
    public SemVerDto? GetServiceVersion();
    public Task<SemVerDto?> GetServiceVersionAsync();
    public bool RestartService();
    public Task<bool> RestartServiceAsync();
    public SchedulerStateDto? GetSchedulerState();
    public Task<SchedulerStateDto?> GetSchedulerStateAsync();
    public SchedulerConfigDto? GetSchedulerConfig();
    public Task<SchedulerConfigDto?> GetSchedulerConfigAsync();
    public RuntimeSettingsResult? GetRuntimeSettings();
    public Task<RuntimeSettingsResult?> GetRuntimeSettingsAsync();
    public RuntimeSettingsResult? SetRuntimeSettings(double maxJoinDistance, bool autoMigrate);
    public Task<RuntimeSettingsResult?> SetRuntimeSettingsAsync(double maxJoinDistance, bool autoMigrate);
    public JobCostingsResult? GetJobCostings();
    public Task<JobCostingsResult?> GetJobCostingsAsync();
    public JobCostingsResult? SetJobCostings(
        double baseCostSeconds,
        double costScalingThresholdSeconds,
        double lambda,
        bool isEnabled);
    public Task<JobCostingsResult?> SetJobCostingsAsync(
        double baseCostSeconds,
        double costScalingThresholdSeconds,
        double lambda,
        bool isEnabled);
    public JobCostingsResult? RestoreJobCostingDefaults();
    public Task<JobCostingsResult?> RestoreJobCostingDefaultsAsync();
    public ComponentManagerStatusDto? GetComponentManagerStatus();
    public Task<ComponentManagerStatusDto?> GetComponentManagerStatusAsync();
    public string? GetComponentManagerFailureReason();
    public Task<string?> GetComponentManagerFailureReasonAsync();
    public bool SetSpotManagement(bool value);
    public Task<bool> SetSpotManagementAsync(bool value);
    public bool SetAttractMode(bool value);
    public Task<bool> SetAttractModeAsync(bool value);
    public bool SetWaypointItineraryBuffer(int value);
    public Task<bool> SetWaypointItineraryBufferAsync(int value);
    public bool SetImmediateChargeLevel(int value);
    public Task<bool> SetImmediateChargeLevelAsync(int value);
    public bool SetChargeDowngradeLevel(int value);
    public Task<bool> SetChargeDowngradeLevelAsync(int value);
    public bool SetMaximumChargeLevel(int value);
    public Task<bool> SetMaximumChargeLevelAsync(int value);
    public bool SetIdleTimeout(TimeSpan value);
    public Task<bool> SetIdleTimeoutAsync(TimeSpan value);

    /// <summary>
    /// Unsubscribe from scheduler state updates.
    /// </summary>
    public void Unsubscribe();
}
