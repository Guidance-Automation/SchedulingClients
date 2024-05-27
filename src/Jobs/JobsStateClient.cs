using GAAPICommon.Enums;
using GAAPICommon.Messages;
using GAAPICommon.Services.Jobs;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;

namespace GAClients.SchedulingClients.Jobs;

/// <summary>
/// Client for interacting with the JobsState service.
/// </summary>
public class JobsStateClient : IJobsStateClient
{
    private bool _isDisposed;
    private CancellationTokenSource? _cts;
    private readonly JobsStateServiceProto.JobsStateServiceProtoClient _client;
    private readonly ILogger? _logger;

    /// <summary>
    /// The current state of jobs.
    /// </summary>
    public JobStateDto? JobsState { get; private set; }

    /// <summary>
    /// Event that is triggered when the jobs state is updated.
    /// </summary>
    public event Action<JobStateDto>? JobsStateUpdated;

    /// <summary>
    /// Initializes a new instance of the JobsStateClient class using an existing client instance.
    /// </summary>
    /// <param name="client">An existing instance of the JobsStateServiceProtoClient.</param>
    /// <param name="settings">Client settings.</param>
    /// <param name="logger">Logger for logging messages.</param>
    public JobsStateClient(JobsStateServiceProto.JobsStateServiceProtoClient client, ClientSettings settings, ILogger<JobsStateClient>? logger)
    {
        _client = client;
        _logger = logger;
        _logger?.LogInformation("[JobsStateClient] JobsStateClient created");
        if (settings.Subscribe)
            Task.Run(Subscribe);
    }

    /// <summary>
    /// Unsubscribe from job state updates.
    /// </summary>
    public void Unsubscribe()
    {
        _cts?.Cancel();
    }

    /// <summary>
    /// Subscribes to jobs state updates and raises the JobsStateUpdated event when an update is received.
    /// </summary>
    private async Task Subscribe()
    {
        _logger?.LogTrace("[JobsStateClient] Subscribe() started");
        _cts = new();
        while (!_cts.IsCancellationRequested)
        {
            try
            {
                JobsStateSubscribeRequest subscribeRequest = new();
                _logger?.LogDebug("[JobsStateClient] Sending JobsStateSubscribeRequest");
                using AsyncServerStreamingCall<JobStateDto> streamingCall = _client.Subscribe(subscribeRequest);

                await foreach (JobStateDto? jobStateDto in streamingCall.ResponseStream.ReadAllAsync(_cts.Token))
                {
                    _logger?.LogTrace("[JobsStateClient] Received JobStateDto: {JobStateDto}", jobStateDto);
                    JobsStateUpdated?.Invoke(jobStateDto);
                    JobsState = jobStateDto;
                }
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
            {
                _logger?.LogInformation("[JobsStateClient] Subscription cancelled");
                break;
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "[JobsStateClient] Exception during subscription. Retrying...");
                await Task.Delay(1000);
            }
        }
        _logger?.LogTrace("[JobsStateClient] Subscribe() ended");
    }

    /// <summary>
    /// Aborts all jobs.
    /// </summary>
    /// <returns>True if the operation succeeded, otherwise false.</returns>
    public bool AbortAllJobs()
    {
        _logger?.LogTrace("[JobsStateClient] AbortAllJobs() called");
        try
        {
            AbortAllJobsRequest request = new();
            _logger?.LogDebug("[JobsStateClient] Sending AbortAllJobsRequest");
            GAAPICommon.GenericResult response = _client.AbortAllJobs(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobsStateClient] AbortAllJobs() succeeded");
                return true;
            }
            else
            {
                _logger?.LogError("[JobsStateClient] Sending AbortAllJobs() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobsStateClient] Error aborting all jobs");
            return false;
        }
    }

    /// <summary>
    /// Aborts all jobs asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains true if the operation succeeded, otherwise false.</returns>
    public async Task<bool> AbortAllJobsAsync()
    {
        _logger?.LogTrace("[JobsStateClient] AbortAllJobsAsync() called");
        try
        {
            AbortAllJobsRequest request = new();
            _logger?.LogDebug("[JobsStateClient] Sending AbortAllJobsRequest");
            GAAPICommon.GenericResult response = await _client.AbortAllJobsAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobsStateClient] AbortAllJobsAsync() succeeded");
                return true;
            }
            else
            {
                _logger?.LogError("[JobsStateClient] Sending AbortAllJobsAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobsStateClient] Error aborting all jobs");
            return false;
        }
    }

    /// <summary>
    /// Aborts all jobs for a specific agent.
    /// </summary>
    /// <param name="agentId">The ID of the agent.</param>
    /// <returns>True if the operation succeeded, otherwise false.</returns>
    public bool AbortAllJobsForAgent(int agentId)
    {
        _logger?.LogTrace("[JobsStateClient] AbortAllJobsForAgent() called with {AgentId}", agentId);
        try
        {
            AbortAllJobsForAgentRequest request = new() { AgentId = agentId };
            _logger?.LogDebug("[JobsStateClient] Sending AbortAllJobsForAgentRequest");
            GAAPICommon.GenericResult response = _client.AbortAllJobsForAgent(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobsStateClient] AbortAllJobsForAgent() succeeded");
                return true;
            }
            else
            {
                _logger?.LogError("[JobsStateClient] Sending AbortAllJobsForAgent() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobsStateClient] Error aborting all jobs for agent");
            return false;
        }
    }

    /// <summary>
    /// Aborts all jobs for a specific agent asynchronously.
    /// </summary>
    /// <param name="agentId">The ID of the agent.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains true if the operation succeeded, otherwise false.</returns>
    public async Task<bool> AbortAllJobsForAgentAsync(int agentId)
    {
        _logger?.LogTrace("[JobsStateClient] AbortAllJobsForAgentAsync() called with {AgentId}", agentId);
        try
        {
            AbortAllJobsForAgentRequest request = new() { AgentId = agentId };
            _logger?.LogDebug("[JobsStateClient] Sending AbortAllJobsForAgentRequest");
            GAAPICommon.GenericResult response = await _client.AbortAllJobsForAgentAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobsStateClient] AbortAllJobsForAgentAsync() succeeded");
                return true;
            }
            else
            {
                _logger?.LogError("[JobsStateClient] Sending AbortAllJobsForAgentAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobsStateClient] Error aborting all jobs for agent");
            return false;
        }
    }

    /// <summary>
    /// Aborts a specific job.
    /// </summary>
    /// <param name="jobId">The ID of the job.</param>
    /// <param name="note">The note explaining the reason for aborting the job.</param>
    /// <returns>True if the operation succeeded, otherwise false.</returns>
    public bool AbortJob(int jobId, string note)
    {
        _logger?.LogTrace("[JobsStateClient] AbortJob() called with {JobId} and {Note}", jobId, note);
        try
        {
            AbortJobRequest request = new() { JobId = jobId, Note = note };
            _logger?.LogDebug("[JobsStateClient] Sending AbortJobRequest");
            GAAPICommon.GenericResult response = _client.AbortJob(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobsStateClient] AbortJob() succeeded");
                return true;
            }
            else
            {
                _logger?.LogError("[JobsStateClient] Sending AbortJob() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobsStateClient] Error aborting job");
            return false;
        }
    }

    /// <summary>
    /// Aborts a specific job asynchronously.
    /// </summary>
    /// <param name="jobId">The ID of the job.</param>
    /// <param name="note">The note explaining the reason for aborting the job.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains true if the operation succeeded, otherwise false.</returns>
    public async Task<bool> AbortJobAsync(int jobId, string note)
    {
        _logger?.LogTrace("[JobsStateClient] AbortJobAsync() called with {JobId} and {Note}", jobId, note);
        try
        {
            AbortJobRequest request = new() { JobId = jobId, Note = note };
            _logger?.LogDebug("[JobsStateClient] Sending AbortJobRequest");
            GAAPICommon.GenericResult response = await _client.AbortJobAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobsStateClient] AbortJobAsync() succeeded");
                return true;
            }
            else
            {
                _logger?.LogError("[JobsStateClient] Sending AbortJobAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobsStateClient] Error aborting job");
            return false;
        }
    }

    /// <summary>
    /// Aborts a specific task.
    /// </summary>
    /// <param name="taskId">The ID of the task.</param>
    /// <returns>True if the operation succeeded, otherwise false.</returns>
    public bool AbortTask(int taskId)
    {
        _logger?.LogTrace("[JobsStateClient] AbortTask() called with {TaskId}", taskId);
        try
        {
            AbortTaskRequest request = new() { TaskId = taskId };
            _logger?.LogDebug("[JobsStateClient] Sending AbortTaskRequest");
            GAAPICommon.GenericResult response = _client.AbortTask(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobsStateClient] AbortTask() succeeded");
                return true;
            }
            else
            {
                _logger?.LogError("[JobsStateClient] Sending AbortTask() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobsStateClient] Error aborting task");
            return false;
        }
    }

    /// <summary>
    /// Aborts a specific task asynchronously.
    /// </summary>
    /// <param name="taskId">The ID of the task.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains true if the operation succeeded, otherwise false.</returns>
    public async Task<bool> AbortTaskAsync(int taskId)
    {
        _logger?.LogTrace("[JobsStateClient] AbortTaskAsync() called with {TaskId}", taskId);
        try
        {
            AbortTaskRequest request = new() { TaskId = taskId };
            _logger?.LogDebug("[JobsStateClient] Sending AbortTaskRequest");
            GAAPICommon.GenericResult response = await _client.AbortTaskAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobsStateClient] AbortTaskAsync() succeeded");
                return true;
            }
            else
            {
                _logger?.LogError("[JobsStateClient] Sending AbortTaskAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobsStateClient] Error aborting task");
            return false;
        }
    }

    /// <summary>
    /// Gets active job IDs for a specific agent.
    /// </summary>
    /// <param name="agentId">The ID of the agent.</param>
    /// <returns>The result containing the active job IDs.</returns>
    public IEnumerable<int>? GetActiveJobIdsForAgent(int agentId)
    {
        _logger?.LogTrace("[JobsStateClient] GetActiveJobIdsForAgent() called with {AgentId}", agentId);
        try
        {
            GetActiveJobIdsForAgentRequest request = new() { AgentId = agentId };
            _logger?.LogDebug("[JobsStateClient] Sending GetActiveJobIdsForAgentRequest");
            GetActiveJobIdsForAgentResult response = _client.GetActiveJobIdsForAgent(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobsStateClient] GetActiveJobIdsForAgent() succeeded");
                return response.ActiveJobs;
            }
            else
            {
                _logger?.LogError("[JobsStateClient] Sending GetActiveJobIdsForAgent() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobsStateClient] Error getting active job IDs for agent");
            return null;
        }
    }

    /// <summary>
    /// Gets active job IDs for a specific agent asynchronously.
    /// </summary>
    /// <param name="agentId">The ID of the agent.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the active job IDs.</returns>
    public async Task<IEnumerable<int>?> GetActiveJobIdsForAgentAsync(int agentId)
    {
        _logger?.LogTrace("[JobsStateClient] GetActiveJobIdsForAgentAsync() called with {AgentId}", agentId);
        try
        {
            GetActiveJobIdsForAgentRequest request = new() { AgentId = agentId };
            _logger?.LogDebug("[JobsStateClient] Sending GetActiveJobIdsForAgentRequest");
            GetActiveJobIdsForAgentResult response = await _client.GetActiveJobIdsForAgentAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobsStateClient] GetActiveJobIdsForAgentAsync() succeeded");
                return response.ActiveJobs;
            }
            else
            {
                _logger?.LogError("[JobsStateClient] Sending GetActiveJobIdsForAgentAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobsStateClient] Error getting active job IDs for agent");
            return null;
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
            _logger?.LogTrace("[JobsStateClient] Disposing resources");
            Unsubscribe();
            _cts?.Dispose();
        }

        _isDisposed = true;
        _logger?.LogInformation("[JobsStateClient] JobsStateClient disposed");
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
