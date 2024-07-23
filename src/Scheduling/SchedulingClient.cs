using GAAPICommon.Messages;
using GAAPICommon.Services.Scheduling;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GAClients.SchedulingClients.Scheduling;

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
        _logger?.LogInformation("[SchedulingClient] SchedulingClient created");
        if (settings.Subscribe)
            Task.Run(Subscribe);
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
        _logger?.LogTrace("[SchedulingClient] Subscribe() started");
        _cts = new();
        while (!_cts.IsCancellationRequested)
        {
            try
            {
                SchedulingSubscribeRequest request = new();
                _logger?.LogDebug("[SchedulingClient] Sending SchedulingSubscribeRequest");
                using AsyncServerStreamingCall<SchedulerStateDto> streamingCall = _client.Subscribe(request);
                await foreach (SchedulerStateDto? schedulerStateDto in streamingCall.ResponseStream.ReadAllAsync(_cts.Token))
                {
                    _logger?.LogTrace("[SchedulingClient] Received SchedulerStateDto: {SchedulerStateDto}", schedulerStateDto);
                    SchedulerStateUpdated?.Invoke(schedulerStateDto);
                    SchedulerState = schedulerStateDto;
                }
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
            {
                _logger?.LogInformation("[SchedulingClient] Subscription cancelled");
                break;
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "[SchedulingClient] Exception during subscription. Retrying...");
                await Task.Delay(1000);
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
            _logger?.LogTrace("[TaskStateClient] Disposing resources");
            Unsubscribe();
            _cts?.Dispose();
        }

        _isDisposed = true;
        _logger?.LogInformation("[TaskStateClient] JobStateClient disposed");
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