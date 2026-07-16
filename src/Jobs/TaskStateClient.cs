using GAAPICommon.Messages;
using GAAPICommon.Services.Jobs;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Guidance.SchedulingClients.Jobs;

public class TaskStateClient : ITaskStateClient
{
    private bool _isDisposed;
    private CancellationTokenSource? _cts;
    private readonly TaskStateServiceProto.TaskStateServiceProtoClient _client;
    private readonly ILogger? _logger;

    public event Action<TaskProgressDto>? TaskProgressUpdated;

    public TaskStateClient(TaskStateServiceProto.TaskStateServiceProtoClient client, ClientSettings settings, ILogger<TaskStateClient>? logger)
    {
        _client = client;
        _logger = logger;
        _logger?.LogInformationIfEnabled("[TaskStateClient] TaskStateClient created");
        if (settings.Subscribe)
            Task.Run(Subscribe);
    }

    /// <summary>
    /// Unsubscribe from the task updates.
    /// </summary>
    public void Unsubscribe()
    {
        _cts?.Cancel();
    }

    /// <summary>
    /// Subscribes to task state updates and raises the TaskProgressUpdated event when an update is received.
    /// </summary>
    private async Task Subscribe()
    {
        _logger?.LogTraceIfEnabled("[TaskStateClient] Subscribe() started");
        _cts = new();
        while (!_cts.IsCancellationRequested)
        {
            try
            {
                TaskStateSubscribeRequest request = new();
                _logger?.LogDebugIfEnabled("[TaskStateClient] Sending TaskStateSubscribeRequest");
                using AsyncServerStreamingCall<TaskProgressDto> streamingCall = _client.Subscribe(request);

                await foreach (TaskProgressDto? taskProgressDto in streamingCall.ResponseStream.ReadAllAsync(_cts.Token))
                {
                    _logger?.LogTraceIfEnabled("[TaskStateClient] Received TaskProgressDto: {TaskProgressDto}", taskProgressDto);
                    TaskProgressUpdated?.Invoke(taskProgressDto);
                }
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
            {
                _logger?.LogInformationIfEnabled("[TaskStateClient] Subscription cancelled");
                break;
            }
            catch (Exception ex)
            {
                _logger?.LogWarningIfEnabled(ex, "[TaskStateClient] Exception during subscription. Retrying...");
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
