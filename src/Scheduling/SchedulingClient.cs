using GAAPICommon.Messages;
using GAAPICommon.Services.Scheduling;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Guidance.SchedulingClients.Scheduling;

using GAAPICommon;

public class SchedulingClient : ISchedulingClient
{
    private bool _isDisposed;
    private CancellationTokenSource? _cts;
    private readonly SchedulingServiceProto.SchedulingServiceProtoClient _client;
    private readonly ILogger? _logger;

    public event Action<SchedulerStateDto>? SchedulerStateUpdated;

    public SchedulerStateDto? SchedulerState { get; private set; }

    public SchedulingClient(SchedulingServiceProto.SchedulingServiceProtoClient client, ClientSettings settings, ILogger<SchedulingClient>? logger)
    {
        _client = client;
        _logger = logger;
        _logger?.LogInformationIfEnabled("[SchedulingClient] SchedulingClient created");
        if (settings.Subscribe)
            Task.Run(Subscribe);
    }

    public ModuleInfoDto? GetModuleInfo()
        => Call(
            () => _client.GetModuleInfo(new SchedulingRequest()),
            nameof(GetModuleInfo));

    public Task<ModuleInfoDto?> GetModuleInfoAsync()
        => CallAsync(
            () => _client.GetModuleInfoAsync(new SchedulingRequest()),
            nameof(GetModuleInfoAsync));

    public SemVerDto? GetServiceVersion()
        => Call(
            () => _client.GetServiceVersion(new SchedulingRequest()),
            nameof(GetServiceVersion));

    public Task<SemVerDto?> GetServiceVersionAsync()
        => CallAsync(
            () => _client.GetServiceVersionAsync(new SchedulingRequest()),
            nameof(GetServiceVersionAsync));

    public bool RestartService()
        => CallResult(
            () => _client.RestartService(new SchedulingRequest()),
            response => response.ServiceCode,
            nameof(RestartService)) != null;

    public async Task<bool> RestartServiceAsync()
        => await CallResultAsync(
            () => _client.RestartServiceAsync(new SchedulingRequest()),
            response => response.ServiceCode,
            nameof(RestartServiceAsync)) != null;

    public SchedulerStateDto? GetSchedulerState()
        => Call(
            () => _client.GetSchedulerState(new SchedulingRequest()),
            nameof(GetSchedulerState));

    public Task<SchedulerStateDto?> GetSchedulerStateAsync()
        => CallAsync(
            () => _client.GetSchedulerStateAsync(new SchedulingRequest()),
            nameof(GetSchedulerStateAsync));

    public SchedulerConfigDto? GetSchedulerConfig()
        => Call(
            () => _client.GetSchedulerConfig(new SchedulingRequest()),
            nameof(GetSchedulerConfig));

    public Task<SchedulerConfigDto?> GetSchedulerConfigAsync()
        => CallAsync(
            () => _client.GetSchedulerConfigAsync(new SchedulingRequest()),
            nameof(GetSchedulerConfigAsync));

    public RuntimeSettingsResult? GetRuntimeSettings()
        => CallResult(
            () => _client.GetRuntimeSettings(new SchedulingRequest()),
            response => response.ServiceCode,
            nameof(GetRuntimeSettings));

    public Task<RuntimeSettingsResult?> GetRuntimeSettingsAsync()
        => CallResultAsync(
            () => _client.GetRuntimeSettingsAsync(new SchedulingRequest()),
            response => response.ServiceCode,
            nameof(GetRuntimeSettingsAsync));

    public RuntimeSettingsResult? SetRuntimeSettings(double maxJoinDistance, bool autoMigrate)
        => CallResult(
            () => _client.SetRuntimeSettings(new SetRuntimeSettingsRequest
            {
                MaxJoinDistance = maxJoinDistance,
                AutoMigrate = autoMigrate
            }),
            response => response.ServiceCode,
            nameof(SetRuntimeSettings));

    public Task<RuntimeSettingsResult?> SetRuntimeSettingsAsync(double maxJoinDistance, bool autoMigrate)
        => CallResultAsync(
            () => _client.SetRuntimeSettingsAsync(new SetRuntimeSettingsRequest
            {
                MaxJoinDistance = maxJoinDistance,
                AutoMigrate = autoMigrate
            }),
            response => response.ServiceCode,
            nameof(SetRuntimeSettingsAsync));

    public JobCostingsResult? GetJobCostings()
        => CallResult(
            () => _client.GetJobCostings(new SchedulingRequest()),
            response => response.ServiceCode,
            nameof(GetJobCostings));

    public Task<JobCostingsResult?> GetJobCostingsAsync()
        => CallResultAsync(
            () => _client.GetJobCostingsAsync(new SchedulingRequest()),
            response => response.ServiceCode,
            nameof(GetJobCostingsAsync));

    public JobCostingsResult? SetJobCostings(
        double baseCostSeconds,
        double costScalingThresholdSeconds,
        double lambda,
        bool isEnabled)
        => CallResult(
            () => _client.SetJobCostings(CreateJobCostingsRequest(
                baseCostSeconds,
                costScalingThresholdSeconds,
                lambda,
                isEnabled)),
            response => response.ServiceCode,
            nameof(SetJobCostings));

    public Task<JobCostingsResult?> SetJobCostingsAsync(
        double baseCostSeconds,
        double costScalingThresholdSeconds,
        double lambda,
        bool isEnabled)
        => CallResultAsync(
            () => _client.SetJobCostingsAsync(CreateJobCostingsRequest(
                baseCostSeconds,
                costScalingThresholdSeconds,
                lambda,
                isEnabled)),
            response => response.ServiceCode,
            nameof(SetJobCostingsAsync));

    public JobCostingsResult? RestoreJobCostingDefaults()
        => CallResult(
            () => _client.RestoreJobCostingDefaults(new SchedulingRequest()),
            response => response.ServiceCode,
            nameof(RestoreJobCostingDefaults));

    public Task<JobCostingsResult?> RestoreJobCostingDefaultsAsync()
        => CallResultAsync(
            () => _client.RestoreJobCostingDefaultsAsync(new SchedulingRequest()),
            response => response.ServiceCode,
            nameof(RestoreJobCostingDefaultsAsync));

    public ComponentManagerStatusDto? GetComponentManagerStatus()
        => Call(
            () => _client.GetComponentManagerStatus(new SchedulingRequest()),
            nameof(GetComponentManagerStatus));

    public Task<ComponentManagerStatusDto?> GetComponentManagerStatusAsync()
        => CallAsync(
            () => _client.GetComponentManagerStatusAsync(new SchedulingRequest()),
            nameof(GetComponentManagerStatusAsync));

    public string? GetComponentManagerFailureReason()
        => CallResult(
            () => _client.GetComponentManagerFailureReason(new SchedulingRequest()),
            response => response.ServiceCode,
            nameof(GetComponentManagerFailureReason))?.Value;

    public async Task<string?> GetComponentManagerFailureReasonAsync()
        => (await CallResultAsync(
            () => _client.GetComponentManagerFailureReasonAsync(new SchedulingRequest()),
            response => response.ServiceCode,
            nameof(GetComponentManagerFailureReasonAsync)))?.Value;

    public bool SetSpotManagement(bool value)
        => SetBoolean(value, request => _client.SetSpotManagement(request), nameof(SetSpotManagement));

    public Task<bool> SetSpotManagementAsync(bool value)
        => SetBooleanAsync(value, request => _client.SetSpotManagementAsync(request), nameof(SetSpotManagementAsync));

    public bool SetAttractMode(bool value)
        => SetBoolean(value, request => _client.SetAttractMode(request), nameof(SetAttractMode));

    public Task<bool> SetAttractModeAsync(bool value)
        => SetBooleanAsync(value, request => _client.SetAttractModeAsync(request), nameof(SetAttractModeAsync));

    public bool SetWaypointItineraryBuffer(int value)
        => SetInteger(value, request => _client.SetWaypointItineraryBuffer(request), nameof(SetWaypointItineraryBuffer));

    public Task<bool> SetWaypointItineraryBufferAsync(int value)
        => SetIntegerAsync(value, request => _client.SetWaypointItineraryBufferAsync(request), nameof(SetWaypointItineraryBufferAsync));

    public bool SetImmediateChargeLevel(int value)
        => SetInteger(value, request => _client.SetImmediateChargeLevel(request), nameof(SetImmediateChargeLevel));

    public Task<bool> SetImmediateChargeLevelAsync(int value)
        => SetIntegerAsync(value, request => _client.SetImmediateChargeLevelAsync(request), nameof(SetImmediateChargeLevelAsync));

    public bool SetChargeDowngradeLevel(int value)
        => SetInteger(value, request => _client.SetChargeDowngradeLevel(request), nameof(SetChargeDowngradeLevel));

    public Task<bool> SetChargeDowngradeLevelAsync(int value)
        => SetIntegerAsync(value, request => _client.SetChargeDowngradeLevelAsync(request), nameof(SetChargeDowngradeLevelAsync));

    public bool SetMaximumChargeLevel(int value)
        => SetInteger(value, request => _client.SetMaximumChargeLevel(request), nameof(SetMaximumChargeLevel));

    public Task<bool> SetMaximumChargeLevelAsync(int value)
        => SetIntegerAsync(value, request => _client.SetMaximumChargeLevelAsync(request), nameof(SetMaximumChargeLevelAsync));

    public bool SetIdleTimeout(TimeSpan value)
        => CallResult(
            () => _client.SetIdleTimeout(new DurationValueRequest
            {
                Value = Google.Protobuf.WellKnownTypes.Duration.FromTimeSpan(value)
            }),
            response => response.ServiceCode,
            nameof(SetIdleTimeout)) != null;

    public async Task<bool> SetIdleTimeoutAsync(TimeSpan value)
        => await CallResultAsync(
            () => _client.SetIdleTimeoutAsync(new DurationValueRequest
            {
                Value = Google.Protobuf.WellKnownTypes.Duration.FromTimeSpan(value)
            }),
            response => response.ServiceCode,
            nameof(SetIdleTimeoutAsync)) != null;

    private bool SetBoolean(
        bool value,
        Func<BoolValueRequest, GenericResult> call,
        string operation)
        => CallResult(
            () => call(new BoolValueRequest { Value = value }),
            response => response.ServiceCode,
            operation) != null;

    private async Task<bool> SetBooleanAsync(
        bool value,
        Func<BoolValueRequest, AsyncUnaryCall<GenericResult>> call,
        string operation)
        => await CallResultAsync(
            () => call(new BoolValueRequest { Value = value }),
            response => response.ServiceCode,
            operation) != null;

    private bool SetInteger(
        int value,
        Func<Int32ValueRequest, GenericResult> call,
        string operation)
        => CallResult(
            () => call(new Int32ValueRequest { Value = value }),
            response => response.ServiceCode,
            operation) != null;

    private async Task<bool> SetIntegerAsync(
        int value,
        Func<Int32ValueRequest, AsyncUnaryCall<GenericResult>> call,
        string operation)
        => await CallResultAsync(
            () => call(new Int32ValueRequest { Value = value }),
            response => response.ServiceCode,
            operation) != null;

    private static SetJobCostingsRequest CreateJobCostingsRequest(
        double baseCostSeconds,
        double costScalingThresholdSeconds,
        double lambda,
        bool isEnabled)
        => new()
        {
            BaseCostSeconds = baseCostSeconds,
            CostScalingThresholdSeconds = costScalingThresholdSeconds,
            Lambda = lambda,
            IsEnabled = isEnabled
        };

    private TResponse? Call<TResponse>(Func<TResponse> call, string operation)
        where TResponse : class
    {
        try
        {
            return call();
        }
        catch (Exception ex)
        {
            _logger?.LogErrorIfEnabled(ex, "[SchedulingClient] {Operation} failed", operation);
            return null;
        }
    }

    private async Task<TResponse?> CallAsync<TResponse>(
        Func<AsyncUnaryCall<TResponse>> call,
        string operation)
        where TResponse : class
    {
        try
        {
            return await call();
        }
        catch (Exception ex)
        {
            _logger?.LogErrorIfEnabled(ex, "[SchedulingClient] {Operation} failed", operation);
            return null;
        }
    }

    private TResponse? CallResult<TResponse>(
        Func<TResponse> call,
        Func<TResponse, int> serviceCode,
        string operation)
        where TResponse : class
    {
        TResponse? response = Call(call, operation);
        if (response == null)
            return null;

        int code = serviceCode(response);
        if (code == (int)GAAPICommon.Enums.ServiceCode.NoError)
            return response;

        _logger?.LogErrorIfEnabled(
            "[SchedulingClient] {Operation} failed with {ServiceCode}",
            operation,
            code);
        return null;
    }

    private async Task<TResponse?> CallResultAsync<TResponse>(
        Func<AsyncUnaryCall<TResponse>> call,
        Func<TResponse, int> serviceCode,
        string operation)
        where TResponse : class
    {
        TResponse? response = await CallAsync(call, operation);
        if (response == null)
            return null;

        int code = serviceCode(response);
        if (code == (int)GAAPICommon.Enums.ServiceCode.NoError)
            return response;

        _logger?.LogErrorIfEnabled(
            "[SchedulingClient] {Operation} failed with {ServiceCode}",
            operation,
            code);
        return null;
    }

    /// <summary>
    /// Unsubscribe from scheduler state updates.
    /// </summary>
    public void Unsubscribe()
    {
        _cts?.Cancel();
    }

    private async Task Subscribe()
    {
        _logger?.LogTraceIfEnabled("[SchedulingClient] Subscribe() started");
        _cts = new();
        while (!_cts.IsCancellationRequested)
        {
            try
            {
                SchedulingSubscribeRequest request = new();
                _logger?.LogDebugIfEnabled("[SchedulingClient] Sending SchedulingSubscribeRequest");
                using AsyncServerStreamingCall<SchedulerStateDto> streamingCall = _client.Subscribe(request);
                await foreach (SchedulerStateDto? schedulerStateDto in streamingCall.ResponseStream.ReadAllAsync(_cts.Token))
                {
                    _logger?.LogTraceIfEnabled("[SchedulingClient] Received SchedulerStateDto: {SchedulerStateDto}", schedulerStateDto);
                    SubscriptionCallbackDispatcher.Invoke(
                        SchedulerStateUpdated,
                        schedulerStateDto,
                        _logger,
                        nameof(SchedulingClient));
                    SchedulerState = schedulerStateDto;
                }
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
            {
                _logger?.LogInformationIfEnabled("[SchedulingClient] Subscription cancelled");
                break;
            }
            catch (Exception ex)
            {
                _logger?.LogWarningIfEnabled(ex, "[SchedulingClient] Exception during subscription. Retrying...");
                await Task.Delay(1000, _cts.Token);
            }
        }
    }

    /// <summary>
    /// Disposes of the client resources.
    /// </summary>
    /// <param name="disposing">Indicates whether the method is called from the Dispose method or from a finalizer.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed)
            return;

        if (disposing)
        {
            _logger?.LogTraceIfEnabled("[TaskStateClient] Disposing resources");
            Unsubscribe();
            _cts?.Dispose();
        }

        _isDisposed = true;
        _logger?.LogInformationIfEnabled("[TaskStateClient] JobStateClient disposed");
    }

    /// <summary>
    /// Disposes of the client, releasing all managed resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
