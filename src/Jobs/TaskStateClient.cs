using GAAPICommon.Messages;
using GAAPICommon.Services.Jobs;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GAClients.SchedulingClients.Jobs;

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
        _logger?.LogInformation("[TaskStateClient] TaskStateClient created");
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
        _logger?.LogTrace("[TaskStateClient] Subscribe() started");
        _cts = new();
        while (!_cts.IsCancellationRequested)
        {
            try
            {
                TaskStateSubscribeRequest request = new();
                _logger?.LogDebug("[TaskStateClient] Sending TaskStateSubscribeRequest");
                using AsyncServerStreamingCall<TaskProgressDto> streamingCall = _client.Subscribe(request);

                await foreach (TaskProgressDto? taskProgressDto in streamingCall.ResponseStream.ReadAllAsync(_cts.Token))
                {
                    _logger?.LogTrace("[TaskStateClient] Received TaskProgressDto: {TaskProgressDto}", taskProgressDto);
                    TaskProgressUpdated?.Invoke(taskProgressDto);
                }
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
            {
                _logger?.LogInformation("[TaskStateClient] Subscription cancelled");
                break;
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "[TaskStateClient] Exception during subscription. Retrying...");
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