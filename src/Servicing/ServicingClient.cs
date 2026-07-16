using GAAPICommon;
using GAAPICommon.Enums;
using GAAPICommon.Messages;
using GAAPICommon.Services.Servicing;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Guidance.SchedulingClients.Servicing;

/// <summary>
/// Client for interacting with the Servicing service.
/// </summary>
public class ServicingClient : IServicingClient
{
    private bool _isDisposed;
    private CancellationTokenSource? _cts;
    private readonly ServicingServiceProto.ServicingServiceProtoClient _client;
    private readonly ILogger? _logger;

    /// <summary>
    /// Event that is triggered when a service request is received.
    /// </summary>
    public event Action<ServiceStateDto>? ServiceRequest;

    /// <summary>
    /// Initializes a new instance of the ServicingClient class using an existing client instance.
    /// </summary>
    /// <param name="client">An existing instance of the ServicingServiceProtoClient.</param>
    /// <param name="settings">Client settings.</param>
    /// <param name="logger">Logger for logging messages.</param>
    public ServicingClient(ServicingServiceProto.ServicingServiceProtoClient client, ClientSettings settings, ILogger<ServicingClient>? logger)
    {
        _client = client;
        _logger = logger;
        _logger?.LogInformationIfEnabled("[ServicingClient] ServicingClient created");
        if (settings.Subscribe)
            Task.Run(Subscribe);
    }

    /// <summary>
    /// Gets outstanding service requests.
    /// </summary>
    /// <returns>The outstanding service requests, or null if an error occurred.</returns>
    public IEnumerable<ServiceStateDto>? GetOutstandingServiceRequests()
    {
        _logger?.LogTraceIfEnabled("[ServicingClient] GetOutstandingServiceRequests() called");
        try
        {
            GetOutstandingServiceRequestsRequest request = new();
            _logger?.LogDebugIfEnabled("[ServicingClient] Sending GetOutstandingServiceRequestsRequest");
            GetOutstandingServiceRequestsResult response = _client.GetOutstandingServiceRequests(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformationIfEnabled("[ServicingClient] GetOutstandingServiceRequests() succeeded");
                return response.ServiceStates;
            }
            else
            {
                _logger?.LogErrorIfEnabled("[ServicingClient] GetOutstandingServiceRequests() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogErrorIfEnabled(ex, "[ServicingClient] Error getting outstanding service requests");
            return null;
        }
    }

    /// <summary>
    /// Gets outstanding service requests asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the outstanding service requests, or null if an error occurred.</returns>
    public async Task<IEnumerable<ServiceStateDto>?> GetOutstandingServiceRequestsAsync()
    {
        _logger?.LogTraceIfEnabled("[ServicingClient] GetOutstandingServiceRequestsAsync() called");
        try
        {
            GetOutstandingServiceRequestsRequest request = new();
            _logger?.LogDebugIfEnabled("[ServicingClient] Sending GetOutstandingServiceRequestsRequest");
            GetOutstandingServiceRequestsResult response = await _client.GetOutstandingServiceRequestsAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformationIfEnabled("[ServicingClient] GetOutstandingServiceRequestsAsync() succeeded");
                return response.ServiceStates;
            }
            else
            {
                _logger?.LogErrorIfEnabled("[ServicingClient] GetOutstandingServiceRequestsAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogErrorIfEnabled(ex, "[ServicingClient] Error getting outstanding service requests");
            return null;
        }
    }

    /// <summary>
    /// Sets a service task as complete.
    /// </summary>
    /// <param name="taskId">The ID of the task.</param>
    /// <returns>True if the operation succeeded, otherwise false.</returns>
    public bool SetServiceComplete(int taskId)
    {
        _logger?.LogTraceIfEnabled("[ServicingClient] SetServiceComplete() called with {TaskId}", taskId);
        try
        {
            SetServiceCompleteRequest request = new() { TaskId = taskId };
            _logger?.LogDebugIfEnabled("[ServicingClient] Sending SetServiceCompleteRequest");
            GenericResult response = _client.SetServiceComplete(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformationIfEnabled("[ServicingClient] SetServiceComplete() succeeded");
                return true;
            }
            else
            {
                _logger?.LogErrorIfEnabled("[ServicingClient] SetServiceComplete() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogErrorIfEnabled(ex, "[ServicingClient] Error setting service complete");
            return false;
        }
    }

    /// <summary>
    /// Sets a service task as complete asynchronously.
    /// </summary>
    /// <param name="taskId">The ID of the task.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains true if the operation succeeded, otherwise false.</returns>
    public async Task<bool> SetServiceCompleteAsync(int taskId)
    {
        _logger?.LogTraceIfEnabled("[ServicingClient] SetServiceCompleteAsync() called with {TaskId}", taskId);
        try
        {
            SetServiceCompleteRequest request = new() { TaskId = taskId };
            _logger?.LogDebugIfEnabled("[ServicingClient] Sending SetServiceCompleteRequest");
            GenericResult response = await _client.SetServiceCompleteAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformationIfEnabled("[ServicingClient] SetServiceCompleteAsync() succeeded");
                return true;
            }
            else
            {
                _logger?.LogErrorIfEnabled("[ServicingClient] SetServiceCompleteAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogErrorIfEnabled(ex, "[ServicingClient] Error setting service complete");
            return false;
        }
    }

    /// <summary>
    /// Unsubscribes from the service updates.
    /// </summary>
    public void Unsubscribe()
    {
        _logger?.LogInformationIfEnabled("[ServicingClient] Unsubscribing from service updates");
        _cts?.Cancel();
    }

    /// <summary>
    /// Subscribes to service updates and raises the ServiceRequest event when an update is received.
    /// </summary>
    private async Task Subscribe()
    {
        _logger?.LogTraceIfEnabled("[ServicingClient] Subscribe() started");
        _cts = new();
        while (!_cts.IsCancellationRequested)
        {
            try
            {
                ServicingSubscribeRequest request = new();
                _logger?.LogDebugIfEnabled("[ServicingClient] Sending ServicingSubscribeRequest");
                using AsyncServerStreamingCall<ServiceStateDto> streamingCall = _client.Subscribe(request);

                await foreach (ServiceStateDto? serviceStateDto in streamingCall.ResponseStream.ReadAllAsync(_cts.Token))
                {
                    _logger?.LogTraceIfEnabled("[ServicingClient] Received ServiceStateDto: {ServiceStateDto}", serviceStateDto);
                    ServiceRequest?.Invoke(serviceStateDto);
                }
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
            {
                _logger?.LogInformationIfEnabled("[ServicingClient] Subscription cancelled");
                break;
            }
            catch (Exception ex)
            {
                _logger?.LogWarningIfEnabled(ex, "[ServicingClient] Exception during subscription. Retrying...");
                await Task.Delay(1000);
            }
        }
        _logger?.LogTraceIfEnabled("[ServicingClient] Subscribe() ended");
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
            _logger?.LogTraceIfEnabled("[ServicingClient] Disposing resources");
            Unsubscribe();
            _cts?.Dispose();
        }

        _isDisposed = true;
        _logger?.LogInformationIfEnabled("[ServicingClient] ServicingClient disposed");
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
