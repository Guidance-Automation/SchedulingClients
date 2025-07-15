using GAAPICommon.Enums;
using GAAPICommon.Messages;
using GAAPICommon.Services.Jobs;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Guidance.SchedulingClients.Jobs;

/// <summary>
/// Client for interacting with the JobState service.
/// </summary>
public class JobStateClient : IJobStateClient
{
    private bool _isDisposed;
    private CancellationTokenSource? _cts;
    private readonly JobStateServiceProto.JobStateServiceProtoClient _client;
    private readonly ILogger? _logger;

    /// <summary>
    /// Event that is triggered when the job progress is updated.
    /// </summary>
    public event Action<JobProgressDto>? JobProgressUpdated;

    /// <summary>
    /// Initializes a new instance of the JobStateClient class using an existing client instance.
    /// </summary>
    /// <param name="client">An existing instance of the JobStateServiceProtoClient.</param>
    /// <param name="settings">Client settings.</param>
    /// <param name="logger">Logger for logging messages.</param>
    public JobStateClient(JobStateServiceProto.JobStateServiceProtoClient client, ClientSettings settings, ILogger<JobStateClient>? logger)
    {
        _client = client;
        _logger = logger;
        _logger?.LogInformation("[JobStateClient] JobStateClient created");
        if (settings.Subscribe)
            Task.Run(Subscribe);
    }

    /// <summary>
    /// Gets the current job summary for a specific agent.
    /// </summary>
    /// <param name="agentId">The ID of the agent.</param>
    /// <returns>The result containing the job summary.</returns>
    public JobSummaryDto? GetCurrentJobSummaryForAgentId(int agentId)
    {
        _logger?.LogTrace("[JobStateClient] GetCurrentJobSummaryForAgentId() called with {AgentId}", agentId);
        try
        {
            GetCurrentJobSummaryForAgentIdRequest request = new() { AgentId = agentId };
            _logger?.LogDebug("[JobStateClient] Sending GetCurrentJobSummaryForAgentIdRequest");
            GetCurrentJobSummaryForAgentIdResult response = _client.GetCurrentJobSummaryForAgentId(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobStateClient] GetCurrentJobSummaryForAgentId() succeeded");
                return response.JobSummary;
            }
            else
            {
                _logger?.LogError("[JobStateClient] Sending GetCurrentJobSummaryForAgentId() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobStateClient] Error getting current job summary for agent");
            return null;
        }
    }

    /// <summary>
    /// Gets the current job summary for a specific agent asynchronously.
    /// </summary>
    /// <param name="agentId">The ID of the agent.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the job summary.</returns>
    public async Task<JobSummaryDto?> GetCurrentJobSummaryForAgentIdAsync(int agentId)
    {
        _logger?.LogTrace("[JobStateClient] GetCurrentJobSummaryForAgentIdAsync() called with {AgentId}", agentId);
        try
        {
            GetCurrentJobSummaryForAgentIdRequest request = new() { AgentId = agentId };
            _logger?.LogDebug("[JobStateClient] Sending GetCurrentJobSummaryForAgentIdRequest");
            GetCurrentJobSummaryForAgentIdResult response = await _client.GetCurrentJobSummaryForAgentIdAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobStateClient] GetCurrentJobSummaryForAgentIdAsync() succeeded");
                return response.JobSummary;
            }
            else
            {
                _logger?.LogError("[JobStateClient] Sending GetCurrentJobSummaryForAgentIdAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobStateClient] Error getting current job summary for agent");
            return null;
        }
    }

    /// <summary>
    /// Gets the job summary for a specific job.
    /// </summary>
    /// <param name="jobId">The ID of the job.</param>
    /// <returns>The result containing the job summary.</returns>
    public JobSummaryDto? GetJobSummary(int jobId)
    {
        _logger?.LogTrace("[JobStateClient] GetJobSummary() called with {JobId}", jobId);
        try
        {
            GetJobSummaryRequest request = new() { JobId = jobId };
            _logger?.LogDebug("[JobStateClient] Sending GetJobSummaryRequest");
            GetJobSummaryResult response = _client.GetJobSummary(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobStateClient] GetJobSummary() succeeded");
                return response.JobSummary;
            }
            else
            {
                _logger?.LogError("[JobStateClient] Sending GetJobSummary() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobStateClient] Error getting job summary");
            return null;
        }
    }

    /// <summary>
    /// Gets the job summary for a specific job asynchronously.
    /// </summary>
    /// <param name="jobId">The ID of the job.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the job summary.</returns>
    public async Task<JobSummaryDto?> GetJobSummaryAsync(int jobId)
    {
        _logger?.LogTrace("[JobStateClient] GetJobSummaryAsync() called with {JobId}", jobId);
        try
        {
            GetJobSummaryRequest request = new() { JobId = jobId };
            _logger?.LogDebug("[JobStateClient] Sending GetJobSummaryRequest");
            GetJobSummaryResult response = await _client.GetJobSummaryAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobStateClient] GetJobSummaryAsync() succeeded");
                return response.JobSummary;
            }
            else
            {
                _logger?.LogError("[JobStateClient] Sending GetJobSummaryAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobStateClient] Error getting job summary");
            return null;
        }
    }

    /// <summary>
    /// Gets the parent job summary for a specific task.
    /// </summary>
    /// <param name="taskId">The ID of the task.</param>
    /// <returns>The result containing the parent job summary.</returns>
    public JobSummaryDto? GetParentJobSummaryFromTaskId(int taskId)
    {
        _logger?.LogTrace("[JobStateClient] GetParentJobSummaryFromTaskId() called with {TaskId}", taskId);
        try
        {
            GetParentJobSummaryFromTaskIdRequest request = new() { TaskId = taskId };
            _logger?.LogDebug("[JobStateClient] Sending GetParentJobSummaryFromTaskIdRequest");
            GetParentJobSummaryFromTaskIdResult response = _client.GetParentJobSummaryFromTaskId(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobStateClient] GetParentJobSummaryFromTaskId() succeeded");
                return response.JobSummary;
            }
            else
            {
                _logger?.LogError("[JobStateClient] Sending GetParentJobSummaryFromTaskId() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobStateClient] Error getting parent job summary from task ID");
            return null;
        }
    }

    /// <summary>
    /// Gets the parent job summary for a specific task asynchronously.
    /// </summary>
    /// <param name="taskId">The ID of the task.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the parent job summary.</returns>
    public async Task<JobSummaryDto?> GetParentJobSummaryFromTaskIdAsync(int taskId)
    {
        _logger?.LogTrace("[JobStateClient] GetParentJobSummaryFromTaskIdAsync() called with {TaskId}", taskId);
        try
        {
            GetParentJobSummaryFromTaskIdRequest request = new() { TaskId = taskId };
            _logger?.LogDebug("[JobStateClient] Sending GetParentJobSummaryFromTaskIdRequest");
            GetParentJobSummaryFromTaskIdResult response = await _client.GetParentJobSummaryFromTaskIdAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobStateClient] GetParentJobSummaryFromTaskIdAsync() succeeded");
                return response.JobSummary;
            }
            else
            {
                _logger?.LogError("[JobStateClient] Sending GetParentJobSummaryFromTaskIdAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobStateClient] Error getting parent job summary from task ID");
            return null;
        }
    }

    /// <summary>
    /// Unsubscribe from the job updates.
    /// </summary>
    public void Unsubscribe()
    {
        _cts?.Cancel();
    }

    /// <summary>
    /// Subscribes to job state updates and raises the JobProgressUpdated event when an update is received.
    /// </summary>
    private async Task Subscribe()
    {
        _logger?.LogTrace("[JobStateClient] Subscribe() started");
        _cts = new();
        while (!_cts.IsCancellationRequested)
        {
            try
            {
                JobStateSubscribeRequest request = new();
                _logger?.LogDebug("[JobStateClient] Sending JobStateSubscribeRequest");
                using AsyncServerStreamingCall<JobProgressDto> streamingCall = _client.Subscribe(request);

                await foreach (JobProgressDto? jobProgressDto in streamingCall.ResponseStream.ReadAllAsync(_cts.Token))
                {
                    _logger?.LogTrace("[JobStateClient] Received JobProgressDto: {JobProgressDto}", jobProgressDto);
                    JobProgressUpdated?.Invoke(jobProgressDto);
                }
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
            {
                _logger?.LogInformation("[JobStateClient] Subscription cancelled");
                break;
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "[JobStateClient] Exception during subscription. Retrying...");
                await Task.Delay(1000);
            }
        }
        _logger?.LogTrace("[JobStateClient] Subscribe() ended");
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
            _logger?.LogTrace("[JobStateClient] Disposing resources");
            Unsubscribe();
            _cts?.Dispose();
        }

        _isDisposed = true;
        _logger?.LogInformation("[JobStateClient] JobStateClient disposed");
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