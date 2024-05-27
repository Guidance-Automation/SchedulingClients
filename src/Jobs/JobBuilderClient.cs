using GAAPICommon;
using GAAPICommon.Enums;
using GAAPICommon.Messages;
using GAAPICommon.Services.Jobs;
using Microsoft.Extensions.Logging;
using System.Net;

namespace GAClients.SchedulingClients.Jobs;

/// <summary>
/// Client for interacting with the JobBuilder service.
/// </summary>
public class JobBuilderClient : IJobBuilderClient
{
    private bool _isDisposed;
    private readonly JobBuilderServiceProto.JobBuilderServiceProtoClient _client;
    private readonly ILogger? _logger;

    /// <summary>
    /// Initializes a new instance of the JobBuilderClient class using an existing client instance.
    /// Intended for use with dependancy injection.
    /// </summary>
    /// <param name="client">An existing instance of the JobBuilderServiceProtoClient.</param>
    /// <param name="logger">Logger for logging messages.</param>
    public JobBuilderClient(JobBuilderServiceProto.JobBuilderServiceProtoClient client, ILogger<JobBuilderClient>? logger)
    {
        _client = client;
        _logger = logger;
        _logger?.LogInformation("[JobBuilderClient] JobBuilderClient created with existing client instance");
    }

    /// <summary>
    /// Begins editing a job.
    /// </summary>
    /// <param name="jobId">The ID of the job to edit.</param>
    /// <returns>True if the operation succeeded, otherwise false.</returns>
    public bool BeginEditingJob(int jobId)
    {
        _logger?.LogTrace("[JobBuilderClient] BeginEditingJob() called with {JobId}", jobId);
        try
        {
            EditingJobRequest request = new() { JobId = jobId };
            _logger?.LogDebug("[JobBuilderClient] Sending BeginEditingJobRequest");
            BoolResult response = _client.BeginEditingJob(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] BeginEditingJob() succeeded");
                return response.Value;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending BeginEditingJob() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error beginning editing job");
            throw;
        }
    }

    /// <summary>
    /// Begins editing a job asynchronously.
    /// </summary>
    /// <param name="jobId">The ID of the job to edit.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains true if the operation succeeded, otherwise false.</returns>
    public async Task<bool> BeginEditingJobAsync(int jobId)
    {
        _logger?.LogTrace("[JobBuilderClient] BeginEditingJobAsync() called with {JobId}", jobId);
        try
        {
            EditingJobRequest request = new() { JobId = jobId };
            _logger?.LogDebug("[JobBuilderClient] Sending BeginEditingJobRequest");
            BoolResult response = await _client.BeginEditingJobAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] BeginEditingJobAsync() succeeded");
                return response.Value;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending BeginEditingJobAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error beginning editing job");
            throw;
        }
    }

    /// <summary>
    /// Commits a job.
    /// </summary>
    /// <param name="jobId">The ID of the job to commit.</param>
    /// <param name="agentId">The ID of the agent to commit the job to (optional).</param>
    /// <returns>True if the operation succeeded, otherwise false.</returns>
    public bool CommitJob(int jobId, int agentId = -1)
    {
        _logger?.LogTrace("[JobBuilderClient] CommitJob() called with {JobId} and {AgentId}", jobId, agentId);
        try
        {
            CommitJobRequest request = new() { JobId = jobId, AgentId = agentId };
            _logger?.LogDebug("[JobBuilderClient] Sending CommitJobRequest");
            GenericResult response = _client.CommitJob(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] CommitJob() succeeded");
                return true;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending CommitJob() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error committing job");
            throw;
        }
    }

    /// <summary>
    /// Commits a job asynchronously.
    /// </summary>
    /// <param name="jobId">The ID of the job to commit.</param>
    /// <param name="agentId">The ID of the agent to commit the job to (optional).</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains true if the operation succeeded, otherwise false.</returns>
    public async Task<bool> CommitJobAsync(int jobId, int agentId = -1)
    {
        _logger?.LogTrace("[JobBuilderClient] CommitJobAsync() called with {JobId} and {AgentId}", jobId, agentId);
        try
        {
            CommitJobRequest request = new() { JobId = jobId, AgentId = agentId };
            _logger?.LogDebug("[JobBuilderClient] Sending CommitJobRequest");
            GenericResult response = await _client.CommitJobAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] CommitJobAsync() succeeded");
                return true;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending CommitJobAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error committing job");
            throw;
        }
    }

    /// <summary>
    /// Creates an atomic move list task.
    /// </summary>
    /// <param name="parentTaskId">The ID of the parent task.</param>
    /// <returns>The new task ID if the operation succeeded, otherwise -1.</returns>
    public int CreateAtomicMoveListTask(int parentTaskId)
    {
        _logger?.LogTrace("[JobBuilderClient] CreateAtomicMoveListTask() called with {ParentTaskId}", parentTaskId);
        try
        {
            CreateAtomicMoveListTaskRequest request = new() { ParentTaskId = parentTaskId };
            _logger?.LogDebug("[JobBuilderClient] Sending CreateAtomicMoveListTaskRequest");
            Int32Result response = _client.CreateAtomicMoveListTask(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] CreateAtomicMoveListTask() succeeded");
                return response.Value;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending CreateAtomicMoveListTask() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return -1;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error creating atomic move list task");
            throw;
        }
    }

    /// <summary>
    /// Creates an atomic move list task asynchronously.
    /// </summary>
    /// <param name="parentTaskId">The ID of the parent task.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the new task ID if the operation succeeded, otherwise -1.</returns>
    public async Task<int> CreateAtomicMoveListTaskAsync(int parentTaskId)
    {
        _logger?.LogTrace("[JobBuilderClient] CreateAtomicMoveListTaskAsync() called with {ParentTaskId}", parentTaskId);
        try
        {
            CreateAtomicMoveListTaskRequest request = new() { ParentTaskId = parentTaskId };
            _logger?.LogDebug("[JobBuilderClient] Sending CreateAtomicMoveListTaskRequest");
            Int32Result response = await _client.CreateAtomicMoveListTaskAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] CreateAtomicMoveListTaskAsync() succeeded");
                return response.Value;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending CreateAtomicMoveListTaskAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return -1;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error creating atomic move list task");
            throw;
        }
    }

    /// <summary>
    /// Creates an atomic move task.
    /// </summary>
    /// <param name="parentAtomicMoveListTaskId">The ID of the parent atomic move list task.</param>
    /// <param name="moveId">The ID of the move.</param>
    /// <returns>The new task ID if the operation succeeded, otherwise -1.</returns>
    public int CreateAtomicMoveTask(int parentAtomicMoveListTaskId, int moveId)
    {
        _logger?.LogTrace("[JobBuilderClient] CreateAtomicMoveTask() called with {ParentAtomicMoveListTaskId} and {MoveId}", parentAtomicMoveListTaskId, moveId);
        try
        {
            CreateAtomicMoveTaskRequest request = new() { ParentAtomicMoveListTaskId = parentAtomicMoveListTaskId, MoveId = moveId };
            _logger?.LogDebug("[JobBuilderClient] Sending CreateAtomicMoveTaskRequest");
            Int32Result response = _client.CreateAtomicMoveTask(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] CreateAtomicMoveTask() succeeded");
                return response.Value;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending CreateAtomicMoveTask() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return -1;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error creating atomic move task");
            throw;
        }
    }

    /// <summary>
    /// Creates an atomic move task asynchronously.
    /// </summary>
    /// <param name="parentAtomicMoveListTaskId">The ID of the parent atomic move list task.</param>
    /// <param name="moveId">The ID of the move.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the new task ID if the operation succeeded, otherwise -1.</returns>
    public async Task<int> CreateAtomicMoveTaskAsync(int parentAtomicMoveListTaskId, int moveId)
    {
        _logger?.LogTrace("[JobBuilderClient] CreateAtomicMoveTaskAsync() called with {ParentAtomicMoveListTaskId} and {MoveId}", parentAtomicMoveListTaskId, moveId);
        try
        {
            CreateAtomicMoveTaskRequest request = new() { ParentAtomicMoveListTaskId = parentAtomicMoveListTaskId, MoveId = moveId };
            _logger?.LogDebug("[JobBuilderClient] Sending CreateAtomicMoveTaskRequest");
            Int32Result response = await _client.CreateAtomicMoveTaskAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] CreateAtomicMoveTaskAsync() succeeded");
                return response.Value;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending CreateAtomicMoveTaskAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return -1;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error creating atomic move task");
            throw;
        }
    }

    /// <summary>
    /// Creates an awaiting task.
    /// </summary>
    /// <param name="parentListTaskId">The ID of the parent task list.</param>
    /// <param name="nodeId">The ID of the node.</param>
    /// <returns>The new task ID if the operation succeeded, otherwise -1.</returns>
    public int CreateAwaitingTask(int parentListTaskId, int nodeId)
    {
        _logger?.LogTrace("[JobBuilderClient] CreateAwaitingTask() called with {ParentListTaskId} and {NodeId}", parentListTaskId, nodeId);
        try
        {
            CreateAwaitingTaskRequest request = new() { ParentTaskId = parentListTaskId, NodeId = nodeId };
            _logger?.LogDebug("[JobBuilderClient] Sending CreateAwaitingTaskRequest");
            Int32Result response = _client.CreateAwaitingTask(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] CreateAwaitingTask() succeeded");
                return response.Value;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending CreateAwaitingTask() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return -1;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error creating awaiting task");
            throw;
        }
    }

    /// <summary>
    /// Creates an awaiting task asynchronously.
    /// </summary>
    /// <param name="parentListTaskId">The ID of the parent task list.</param>
    /// <param name="nodeId">The ID of the node.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the new task ID if the operation succeeded, otherwise -1.</returns>
    public async Task<int> CreateAwaitingTaskAsync(int parentListTaskId, int nodeId)
    {
        _logger?.LogTrace("[JobBuilderClient] CreateAwaitingTaskAsync() called with {ParentListTaskId} and {NodeId}", parentListTaskId, nodeId);
        try
        {
            CreateAwaitingTaskRequest request = new() { ParentTaskId = parentListTaskId, NodeId = nodeId };
            _logger?.LogDebug("[JobBuilderClient] Sending CreateAwaitingTaskRequest");
            Int32Result response = await _client.CreateAwaitingTaskAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] CreateAwaitingTaskAsync() succeeded");
                return response.Value;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending CreateAwaitingTaskAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return -1;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error creating awaiting task");
            throw;
        }
    }

    /// <summary>
    /// Creates a go-to-node task.
    /// </summary>
    /// <param name="parentListTaskId">The ID of the parent task list.</param>
    /// <param name="nodeId">The ID of the node to go to.</param>
    /// <returns>The new task ID if the operation succeeded, otherwise -1.</returns>
    public int CreateGoToNodeTask(int parentListTaskId, int nodeId)
    {
        _logger?.LogTrace("[JobBuilderClient] CreateGoToNodeTask() called with {ParentListTaskId} and {NodeId}", parentListTaskId, nodeId);
        try
        {
            CreateGoToNodeTaskRequest request = new() { ParentTaskId = parentListTaskId, NodeId = nodeId };
            _logger?.LogDebug("[JobBuilderClient] Sending CreateGoToNodeTaskRequest");
            Int32Result response = _client.CreateGoToNodeTask(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] CreateGoToNodeTask() succeeded");
                return response.Value;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending CreateGoToNodeTask() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return -1;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error creating go-to-node task");
            throw;
        }
    }

    /// <summary>
    /// Creates a go-to-node task asynchronously.
    /// </summary>
    /// <param name="parentListTaskId">The ID of the parent task list.</param>
    /// <param name="nodeId">The ID of the node to go to.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the new task ID if the operation succeeded, otherwise -1.</returns>
    public async Task<int> CreateGoToNodeTaskAsync(int parentListTaskId, int nodeId)
    {
        _logger?.LogTrace("[JobBuilderClient] CreateGoToNodeTaskAsync() called with {ParentListTaskId} and {NodeId}", parentListTaskId, nodeId);
        try
        {
            CreateGoToNodeTaskRequest request = new() { ParentTaskId = parentListTaskId, NodeId = nodeId };
            _logger?.LogDebug("[JobBuilderClient] Sending CreateGoToNodeTaskRequest");
            Int32Result response = await _client.CreateGoToNodeTaskAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] CreateGoToNodeTaskAsync() succeeded");
                return response.Value;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending CreateGoToNodeTaskAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return -1;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error creating go-to-node task");
            throw;
        }
    }

    /// <summary>
    /// Creates a job with the specified priority.
    /// </summary>
    /// <param name="jobPriority">The priority of the job.</param>
    /// <returns>The new job information if the operation succeeded, otherwise null.</returns>
    public JobDto? CreateJob(JobPriority jobPriority = JobPriority.Normal)
    {
        _logger?.LogTrace("[JobBuilderClient] CreateJob() called with {JobPriority}", jobPriority);
        try
        {
            CreateJobRequest request = new() { JobPriority = jobPriority };
            _logger?.LogDebug("[JobBuilderClient] Sending CreateJobRequest");
            CreateJobResult response = _client.CreateJob(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] CreateJob() succeeded");
                return response.Job;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending CreateJob() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error creating job");
            throw;
        }
    }

    /// <summary>
    /// Creates a job with the specified priority asynchronously.
    /// </summary>
    /// <param name="jobPriority">The priority of the job.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the new job information if the operation succeeded, otherwise null.</returns>
    public async Task<JobDto?> CreateJobAsync(JobPriority jobPriority = JobPriority.Normal)
    {
        _logger?.LogTrace("[JobBuilderClient] CreateJobAsync() called with {JobPriority}", jobPriority);
        try
        {
            CreateJobRequest request = new() { JobPriority = jobPriority };
            _logger?.LogDebug("[JobBuilderClient] Sending CreateJobRequest");
            CreateJobResult response = await _client.CreateJobAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] CreateJobAsync() succeeded");
                return response.Job;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending CreateJobAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error creating job");
            throw;
        }
    }

    /// <summary>
    /// Creates an ordered list task.
    /// </summary>
    /// <param name="parentTaskId">The ID of the parent task.</param>
    /// <returns>The new task ID if the operation succeeded, otherwise -1.</returns>
    public int CreateOrderedListTask(int parentTaskId)
    {
        _logger?.LogTrace("[JobBuilderClient] CreateOrderedListTask() called with {ParentTaskId}", parentTaskId);
        try
        {
            CreateOrderedListTaskRequest request = new() { ParentTaskId = parentTaskId };
            _logger?.LogDebug("[JobBuilderClient] Sending CreateOrderedListTaskRequest");
            Int32Result response = _client.CreateOrderedListTask(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] CreateOrderedListTask() succeeded");
                return response.Value;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending CreateOrderedListTask() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return -1;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error creating ordered list task");
            throw;
        }
    }

    /// <summary>
    /// Creates an ordered list task asynchronously.
    /// </summary>
    /// <param name="parentTaskId">The ID of the parent task.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the new task ID if the operation succeeded, otherwise -1.</returns>
    public async Task<int> CreateOrderedListTaskAsync(int parentTaskId)
    {
        _logger?.LogTrace("[JobBuilderClient] CreateOrderedListTaskAsync() called with {ParentTaskId}", parentTaskId);
        try
        {
            CreateOrderedListTaskRequest request = new() { ParentTaskId = parentTaskId };
            _logger?.LogDebug("[JobBuilderClient] Sending CreateOrderedListTaskRequest");
            Int32Result response = await _client.CreateOrderedListTaskAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] CreateOrderedListTaskAsync() succeeded");
                return response.Value;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending CreateOrderedListTaskAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return -1;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error creating ordered list task");
            throw;
        }
    }

    /// <summary>
    /// Creates a servicing task.
    /// </summary>
    /// <param name="parentListTaskId">The ID of the parent task list.</param>
    /// <param name="nodeId">The ID of the node.</param>
    /// <param name="serviceType">The type of service.</param>
    /// <param name="expectedDuration">The expected duration of the service.</param>
    /// <returns>The new task ID if the operation succeeded, otherwise -1.</returns>
    public int CreateServicingTask(int parentListTaskId, int nodeId, ServiceType serviceType, TimeSpan expectedDuration = default)
    {
        _logger?.LogTrace("[JobBuilderClient] CreateServicingTask() called with {ParentListTaskId}, {NodeId}, {ServiceType}, and {ExpectedDuration}", parentListTaskId, nodeId, serviceType, expectedDuration);
        try
        {
            CreateServicingTaskRequest request = new()
            {
                ParentTaskId = parentListTaskId,
                NodeId = nodeId,
                ServiceType = serviceType,
                ExpectedDuration = Google.Protobuf.WellKnownTypes.Duration.FromTimeSpan(expectedDuration)
            };
            _logger?.LogDebug("[JobBuilderClient] Sending CreateServicingTaskRequest");
            Int32Result response = _client.CreateServicingTask(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] CreateServicingTask() succeeded");
                return response.Value;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending CreateServicingTask() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return -1;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error creating servicing task");
            throw;
        }
    }

    /// <summary>
    /// Creates a servicing task asynchronously.
    /// </summary>
    /// <param name="parentListTaskId">The ID of the parent task list.</param>
    /// <param name="nodeId">The ID of the node.</param>
    /// <param name="serviceType">The type of service.</param>
    /// <param name="expectedDuration">The expected duration of the service.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the new task ID if the operation succeeded, otherwise -1.</returns>
    public async Task<int> CreateServicingTaskAsync(int parentListTaskId, int nodeId, ServiceType serviceType, TimeSpan expectedDuration = default)
    {
        _logger?.LogTrace("[JobBuilderClient] CreateServicingTaskAsync() called with {ParentListTaskId}, {NodeId}, {ServiceType}, and {ExpectedDuration}", parentListTaskId, nodeId, serviceType, expectedDuration);
        try
        {
            CreateServicingTaskRequest request = new()
            {
                ParentTaskId = parentListTaskId,
                NodeId = nodeId,
                ServiceType = serviceType,
                ExpectedDuration = Google.Protobuf.WellKnownTypes.Duration.FromTimeSpan(expectedDuration)
            };
            _logger?.LogDebug("[JobBuilderClient] Sending CreateServicingTaskRequest");
            Int32Result response = await _client.CreateServicingTaskAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] CreateServicingTaskAsync() succeeded");
                return response.Value;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending CreateServicingTaskAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return -1;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error creating servicing task");
            throw;
        }
    }

    /// <summary>
    /// Creates a sleeping task.
    /// </summary>
    /// <param name="parentListTaskId">The ID of the parent task list.</param>
    /// <param name="nodeId">The ID of the node.</param>
    /// <param name="expectedDuration">The expected duration of the sleeping task.</param>
    /// <returns>The new task ID if the operation succeeded, otherwise -1.</returns>
    public int CreateSleepingTask(int parentListTaskId, int nodeId, TimeSpan expectedDuration = default)
    {
        _logger?.LogTrace("[JobBuilderClient] CreateSleepingTask() called with {ParentListTaskId}, {NodeId}, and {ExpectedDuration}", parentListTaskId, nodeId, expectedDuration);
        try
        {
            CreateSleepingTaskRequest request = new()
            {
                ParentTaskId = parentListTaskId,
                NodeId = nodeId,
                ExpectedDuration = Google.Protobuf.WellKnownTypes.Duration.FromTimeSpan(expectedDuration)
            };
            _logger?.LogDebug("[JobBuilderClient] Sending CreateSleepingTaskRequest");
            Int32Result response = _client.CreateSleepingTask(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] CreateSleepingTask() succeeded");
                return response.Value;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending CreateSleepingTask() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return -1;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error creating sleeping task");
            throw;
        }
    }

    /// <summary>
    /// Creates a sleeping task asynchronously.
    /// </summary>
    /// <param name="parentListTaskId">The ID of the parent task list.</param>
    /// <param name="nodeId">The ID of the node.</param>
    /// <param name="expectedDuration">The expected duration of the sleeping task.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the new task ID if the operation succeeded, otherwise -1.</returns>
    public async Task<int> CreateSleepingTaskAsync(int parentListTaskId, int nodeId, TimeSpan expectedDuration = default)
    {
        _logger?.LogTrace("[JobBuilderClient] CreateSleepingTaskAsync() called with {ParentListTaskId}, {NodeId}, and {ExpectedDuration}", parentListTaskId, nodeId, expectedDuration);
        try
        {
            CreateSleepingTaskRequest request = new()
            {
                ParentTaskId = parentListTaskId,
                NodeId = nodeId,
                ExpectedDuration = Google.Protobuf.WellKnownTypes.Duration.FromTimeSpan(expectedDuration)
            };
            _logger?.LogDebug("[JobBuilderClient] Sending CreateSleepingTaskRequest");
            Int32Result response = await _client.CreateSleepingTaskAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] CreateSleepingTaskAsync() succeeded");
                return response.Value;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending CreateSleepingTaskAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return -1;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error creating sleeping task");
            throw;
        }
    }

    /// <summary>
    /// Creates an unordered list task.
    /// </summary>
    /// <param name="parentTaskId">The ID of the parent task.</param>
    /// <returns>The new task ID if the operation succeeded, otherwise -1.</returns>
    public int CreateUnorderedListTask(int parentTaskId)
    {
        _logger?.LogTrace("[JobBuilderClient] CreateUnorderedListTask() called with {ParentTaskId}", parentTaskId);
        try
        {
            CreateUnorderedListTaskRequest request = new() { ParentTaskId = parentTaskId };
            _logger?.LogDebug("[JobBuilderClient] Sending CreateUnorderedListTaskRequest");
            Int32Result response = _client.CreateUnorderedListTask(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] CreateUnorderedListTask() succeeded");
                return response.Value;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending CreateUnorderedListTask() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return -1;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error creating unordered list task");
            throw;
        }
    }

    /// <summary>
    /// Creates an unordered list task asynchronously.
    /// </summary>
    /// <param name="parentTaskId">The ID of the parent task.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the new task ID if the operation succeeded, otherwise -1.</returns>
    public async Task<int> CreateUnorderedListTaskAsync(int parentTaskId)
    {
        _logger?.LogTrace("[JobBuilderClient] CreateUnorderedListTaskAsync() called with {ParentTaskId}", parentTaskId);
        try
        {
            CreateUnorderedListTaskRequest request = new() { ParentTaskId = parentTaskId };
            _logger?.LogDebug("[JobBuilderClient] Sending CreateUnorderedListTaskRequest");
            Int32Result response = await _client.CreateUnorderedListTaskAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] CreateUnorderedListTaskAsync() succeeded");
                return response.Value;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending CreateUnorderedListTaskAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return -1;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error creating unordered list task");
            throw;
        }
    }

    /// <summary>
    /// Finishes editing a job.
    /// </summary>
    /// <param name="jobId">The ID of the job to finish editing.</param>
    /// <returns>True if the operation succeeded, otherwise false.</returns>
    public bool FinishEditingJob(int jobId)
    {
        _logger?.LogTrace("[JobBuilderClient] FinishEditingJob() called with {JobId}", jobId);
        try
        {
            FinishEditingJobRequest request = new() { JobId = jobId };
            _logger?.LogDebug("[JobBuilderClient] Sending FinishEditingJobRequest");
            GenericResult response = _client.FinishEditingJob(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] FinishEditingJob() succeeded");
                return true;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending FinishEditingJob() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error finishing editing job");
            throw;
        }
    }

    /// <summary>
    /// Finishes editing a job asynchronously.
    /// </summary>
    /// <param name="jobId">The ID of the job to finish editing.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains true if the operation succeeded, otherwise false.</returns>
    public async Task<bool> FinishEditingJobAsync(int jobId)
    {
        _logger?.LogTrace("[JobBuilderClient] FinishEditingJobAsync() called with {JobId}", jobId);
        try
        {
            FinishEditingJobRequest request = new() { JobId = jobId };
            _logger?.LogDebug("[JobBuilderClient] Sending FinishEditingJobRequest");
            GenericResult response = await _client.FinishEditingJobAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] FinishEditingJobAsync() succeeded");
                return true;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending FinishEditingJobAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error finishing editing job");
            throw;
        }
    }

    /// <summary>
    /// Issues an enum directive to a task.
    /// </summary>
    /// <param name="taskId">The ID of the task.</param>
    /// <param name="parameterAlias">The alias of the parameter.</param>
    /// <param name="value">The value of the directive.</param>
    /// <returns>True if the operation succeeded, otherwise false.</returns>
    public bool IssueEnumDirective(int taskId, string parameterAlias, byte value)
    {
        _logger?.LogTrace("[JobBuilderClient] IssueEnumDirective() called with {TaskId}, {ParameterAlias}, and {Value}", taskId, parameterAlias, value);
        try
        {
            IssueIntDirectiveRequest request = new() { TaskId = taskId, Alias = parameterAlias, Value = value };
            _logger?.LogDebug("[JobBuilderClient] Sending IssueEnumDirectiveRequest");
            GenericResult response = _client.IssueEnumDirective(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] IssueEnumDirective() succeeded");
                return true;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending IssueEnumDirective() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error issuing enum directive");
            throw;
        }
    }

    /// <summary>
    /// Issues an enum directive to a task asynchronously.
    /// </summary>
    /// <param name="taskId">The ID of the task.</param>
    /// <param name="parameterAlias">The alias of the parameter.</param>
    /// <param name="value">The value of the directive.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains true if the operation succeeded, otherwise false.</returns>
    public async Task<bool> IssueEnumDirectiveAsync(int taskId, string parameterAlias, byte value)
    {
        _logger?.LogTrace("[JobBuilderClient] IssueEnumDirectiveAsync() called with {TaskId}, {ParameterAlias}, and {Value}", taskId, parameterAlias, value);
        try
        {
            IssueIntDirectiveRequest request = new() { TaskId = taskId, Alias = parameterAlias, Value = value };
            _logger?.LogDebug("[JobBuilderClient] Sending IssueEnumDirectiveRequest");
            GenericResult response = await _client.IssueEnumDirectiveAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] IssueEnumDirectiveAsync() succeeded");
                return true;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending IssueEnumDirectiveAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error issuing enum directive");
            throw;
        }
    }

    /// <summary>
    /// Issues a float directive to a task.
    /// </summary>
    /// <param name="taskId">The ID of the task.</param>
    /// <param name="parameterAlias">The alias of the parameter.</param>
    /// <param name="value">The value of the directive.</param>
    /// <returns>True if the operation succeeded, otherwise false.</returns>
    public bool IssueFloatDirective(int taskId, string parameterAlias, float value)
    {
        _logger?.LogTrace("[JobBuilderClient] IssueFloatDirective() called with {TaskId}, {ParameterAlias}, and {Value}", taskId, parameterAlias, value);
        try
        {
            IssueFloatDirectiveRequest request = new() { TaskId = taskId, Alias = parameterAlias, Value = value };
            _logger?.LogDebug("[JobBuilderClient] Sending IssueFloatDirectiveRequest");
            GenericResult response = _client.IssueFloatDirective(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] IssueFloatDirective() succeeded");
                return true;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending IssueFloatDirective() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error issuing float directive");
            throw;
        }
    }

    /// <summary>
    /// Issues a float directive to a task asynchronously.
    /// </summary>
    /// <param name="taskId">The ID of the task.</param>
    /// <param name="parameterAlias">The alias of the parameter.</param>
    /// <param name="value">The value of the directive.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains true if the operation succeeded, otherwise false.</returns>
    public async Task<bool> IssueFloatDirectiveAsync(int taskId, string parameterAlias, float value)
    {
        _logger?.LogTrace("[JobBuilderClient] IssueFloatDirectiveAsync() called with {TaskId}, {ParameterAlias}, and {Value}", taskId, parameterAlias, value);
        try
        {
            IssueFloatDirectiveRequest request = new() { TaskId = taskId, Alias = parameterAlias, Value = value };
            _logger?.LogDebug("[JobBuilderClient] Sending IssueFloatDirectiveRequest");
            GenericResult response = await _client.IssueFloatDirectiveAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] IssueFloatDirectiveAsync() succeeded");
                return true;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending IssueFloatDirectiveAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error issuing float directive");
            throw;
        }
    }

    /// <summary>
    /// Issues an IP address directive to a task.
    /// </summary>
    /// <param name="taskId">The ID of the task.</param>
    /// <param name="parameterAlias">The alias of the parameter.</param>
    /// <param name="value">The value of the directive.</param>
    /// <returns>True if the operation succeeded, otherwise false.</returns>
    public bool IssueIPAddressDirective(int taskId, string parameterAlias, IPAddress value)
    {
        _logger?.LogTrace("[JobBuilderClient] IssueIPAddressDirective() called with {TaskId}, {ParameterAlias}, and {Value}", taskId, parameterAlias, value);
        try
        {
            IssueIPAddressDirectiveRequest request = new() { TaskId = taskId, Alias = parameterAlias, Value = value.ToString() };
            _logger?.LogDebug("[JobBuilderClient] Sending IssueIPAddressDirectiveRequest");
            GenericResult response = _client.IssueIPAddressDirective(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] IssueIPAddressDirective() succeeded");
                return true;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending IssueIPAddressDirective() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error issuing IP address directive");
            throw;
        }
    }

    /// <summary>
    /// Issues an IP address directive to a task asynchronously.
    /// </summary>
    /// <param name="taskId">The ID of the task.</param>
    /// <param name="parameterAlias">The alias of the parameter.</param>
    /// <param name="value">The value of the directive.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains true if the operation succeeded, otherwise false.</returns>
    public async Task<bool> IssueIPAddressDirectiveAsync(int taskId, string parameterAlias, IPAddress value)
    {
        _logger?.LogTrace("[JobBuilderClient] IssueIPAddressDirectiveAsync() called with {TaskId}, {ParameterAlias}, and {Value}", taskId, parameterAlias, value);
        try
        {
            IssueIPAddressDirectiveRequest request = new() { TaskId = taskId, Alias = parameterAlias, Value = value.ToString() };
            _logger?.LogDebug("[JobBuilderClient] Sending IssueIPAddressDirectiveRequest");
            GenericResult response = await _client.IssueIPAddressDirectiveAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] IssueIPAddressDirectiveAsync() succeeded");
                return true;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending IssueIPAddressDirectiveAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error issuing IP address directive");
            throw;
        }
    }

    /// <summary>
    /// Issues a short directive to a task.
    /// </summary>
    /// <param name="taskId">The ID of the task.</param>
    /// <param name="parameterAlias">The alias of the parameter.</param>
    /// <param name="value">The value of the directive.</param>
    /// <returns>True if the operation succeeded, otherwise false.</returns>
    public bool IssueShortDirective(int taskId, string parameterAlias, short value)
    {
        _logger?.LogTrace("[JobBuilderClient] IssueShortDirective() called with {TaskId}, {ParameterAlias}, and {Value}", taskId, parameterAlias, value);
        try
        {
            IssueIntDirectiveRequest request = new() { TaskId = taskId, Alias = parameterAlias, Value = value };
            _logger?.LogDebug("[JobBuilderClient] Sending IssueShortDirectiveRequest");
            GenericResult response = _client.IssueShortDirective(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] IssueShortDirective() succeeded");
                return true;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending IssueShortDirective() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error issuing short directive");
            throw;
        }
    }

    /// <summary>
    /// Issues a short directive to a task asynchronously.
    /// </summary>
    /// <param name="taskId">The ID of the task.</param>
    /// <param name="parameterAlias">The alias of the parameter.</param>
    /// <param name="value">The value of the directive.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains true if the operation succeeded, otherwise false.</returns>
    public async Task<bool> IssueShortDirectiveAsync(int taskId, string parameterAlias, short value)
    {
        _logger?.LogTrace("[JobBuilderClient] IssueShortDirectiveAsync() called with {TaskId}, {ParameterAlias}, and {Value}", taskId, parameterAlias, value);
        try
        {
            IssueIntDirectiveRequest request = new() { TaskId = taskId, Alias = parameterAlias, Value = value };
            _logger?.LogDebug("[JobBuilderClient] Sending IssueShortDirectiveRequest");
            GenericResult response = await _client.IssueShortDirectiveAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] IssueShortDirectiveAsync() succeeded");
                return true;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending IssueShortDirectiveAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error issuing short directive");
            throw;
        }
    }

    /// <summary>
    /// Issues an unsigned short directive to a task.
    /// </summary>
    /// <param name="taskId">The ID of the task.</param>
    /// <param name="parameterAlias">The alias of the parameter.</param>
    /// <param name="value">The value of the directive.</param>
    /// <returns>True if the operation succeeded, otherwise false.</returns>
    public bool IssueUShortDirective(int taskId, string parameterAlias, ushort value)
    {
        _logger?.LogTrace("[JobBuilderClient] IssueUShortDirective() called with {TaskId}, {ParameterAlias}, and {Value}", taskId, parameterAlias, value);
        try
        {
            IssueIntDirectiveRequest request = new() { TaskId = taskId, Alias = parameterAlias, Value = value };
            _logger?.LogDebug("[JobBuilderClient] Sending IssueUShortDirectiveRequest");
            GenericResult response = _client.IssueUShortDirective(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] IssueUShortDirective() succeeded");
                return true;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending IssueUShortDirective() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error issuing unsigned short directive");
            throw;
        }
    }

    /// <summary>
    /// Issues an unsigned short directive to a task asynchronously.
    /// </summary>
    /// <param name="taskId">The ID of the task.</param>
    /// <param name="parameterAlias">The alias of the parameter.</param>
    /// <param name="value">The value of the directive.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains true if the operation succeeded, otherwise false.</returns>
    public async Task<bool> IssueUShortDirectiveAsync(int taskId, string parameterAlias, ushort value)
    {
        _logger?.LogTrace("[JobBuilderClient] IssueUShortDirectiveAsync() called with {TaskId}, {ParameterAlias}, and {Value}", taskId, parameterAlias, value);
        try
        {
            IssueIntDirectiveRequest request = new() { TaskId = taskId, Alias = parameterAlias, Value = value };
            _logger?.LogDebug("[JobBuilderClient] Sending IssueUShortDirectiveRequest");
            GenericResult response = await _client.IssueUShortDirectiveAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("[JobBuilderClient] IssueUShortDirectiveAsync() succeeded");
                return true;
            }
            else
            {
                _logger?.LogError("[JobBuilderClient] Sending IssueUShortDirectiveAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[JobBuilderClient] Error issuing unsigned short directive");
            throw;
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
            _logger?.LogTrace("[JobBuilderClient] Disposing resources");
        }

        _isDisposed = true;
        _logger?.LogInformation("[JobBuilderClient] FleetManagerClient disposed");
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