using GAAPICommon;
using GAAPICommon.Enums;
using GAAPICommon.Messages;
using GAAPICommon.Services.Agents;
using Microsoft.Extensions.Logging;

namespace GAClients.SchedulingClients.Agents;

/// <summary>
/// Client for interacting with the Agent service.
/// </summary>
public class AgentClient : IAgentClient
{
    private bool _isDisposed;
    private readonly AgentServiceProto.AgentServiceProtoClient _client;
    private readonly ILogger? _logger;

    /// <summary>
    /// Initializes a new instance of the AgentClient class using an existing client instance.
    /// Intended for use with dependency injection.
    /// </summary>
    /// <param name="client">An existing instance of the AgentServiceProtoClient.</param>
    /// <param name="logger">Logger for logging messages.</param>
    public AgentClient(AgentServiceProto.AgentServiceProtoClient client, ILogger<AgentClient>? logger)
    {
        _client = client;
        _logger = logger;
        _logger?.LogInformation("[AgentClient] AgentClient created with existing client instance");
    }

    /// <summary>
    /// Gets all agents.
    /// </summary>
    /// <returns>The result containing all agents data.</returns>
    public IEnumerable<AgentDto>? GetAllAgents()
    {
        _logger?.LogTrace("[AgentClient] GetAllAgents() called");
        try
        {
            GetAllAgentDataRequest request = new();
            _logger?.LogDebug("[AgentClient] Sending GetAllAgentDataRequest");
            GetAllAgentDataResult response = _client.GetAllAgentData(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("GetAllAgents() succeeded");
                return response.Agents;
            }
            else
            {
                _logger?.LogError("[AgentClient] Sending GetAllAgents() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[AgentClient] Error getting all agents");
            throw;
        }
    }

    /// <summary>
    /// Gets all agents asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains all agents data.</returns>
    public async Task<IEnumerable<AgentDto>?> GetAllAgentsAsync()
    {
        _logger?.LogTrace("[AgentClient] GetAllAgentsAsync() called");
        try
        {
            GetAllAgentDataRequest request = new();
            _logger?.LogDebug("[AgentClient] Sending GetAllAgentDataRequest");
            GetAllAgentDataResult response = await _client.GetAllAgentDataAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("GetAllAgentsAsync() succeeded");
                return response.Agents;
            }
            else
            {
                _logger?.LogError("[AgentClient] Sending GetAllAgentsAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[AgentClient] Error getting all agents");
            throw;
        }
    }

    /// <summary>
    /// Gets all agents in a specific lifetime state.
    /// </summary>
    /// <param name="agentLifetimeState">The lifetime state to filter agents.</param>
    /// <returns>The result containing agents in the specified lifetime state.</returns>
    public IEnumerable<AgentDto>? GetAllAgentsInLifetimeState(AgentLifetimeState agentLifetimeState)
    {
        _logger?.LogTrace("[AgentClient] GetAllAgentsInLifetimeState() called with {AgentLifetimeState}", agentLifetimeState);
        try
        {
            GetAllAgentsInLifetimeStateRequest request = new()
            {
                AgentLifetimeState = agentLifetimeState
            };
            _logger?.LogDebug("[AgentClient] Sending GetAllAgentsInLifetimeStateRequest");
            GetAllAgentsInLifetimeStateResult response = _client.GetAllAgentsInLifetimeState(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("GetAllAgentsInLifetimeState() succeeded");
                return response.Agents;
            }
            else
            {
                _logger?.LogError("[AgentClient] Sending GetAllAgentsInLifetimeState() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[AgentClient] Error getting agents in lifetime state");
            throw;
        }
    }

    /// <summary>
    /// Gets all agents in a specific lifetime state asynchronously.
    /// </summary>
    /// <param name="agentLifetimeState">The lifetime state to filter agents.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains agents in the specified lifetime state.</returns>
    public async Task<IEnumerable<AgentDto>?> GetAllAgentsInLifetimeStateAsync(AgentLifetimeState agentLifetimeState)
    {
        _logger?.LogTrace("[AgentClient] GetAllAgentsInLifetimeStateAsync() called with {AgentLifetimeState}", agentLifetimeState);
        try
        {
            GetAllAgentsInLifetimeStateRequest request = new()
            {
                AgentLifetimeState = agentLifetimeState
            };
            _logger?.LogDebug("[AgentClient] Sending GetAllAgentsInLifetimeStateRequest");
            GetAllAgentsInLifetimeStateResult response = await _client.GetAllAgentsInLifetimeStateAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("GetAllAgentsInLifetimeStateAsync() succeeded");
                return response.Agents;
            }
            else
            {
                _logger?.LogError("[AgentClient] Sending GetAllAgentsInLifetimeStateAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[AgentClient] Error getting agents in lifetime state");
            return null;
        }
    }

    /// <summary>
    /// Sets the lifetime state of an agent.
    /// </summary>
    /// <param name="agentId">The ID of the agent.</param>
    /// <param name="desiredState">The desired lifetime state.</param>
    /// <returns>The result of the operation.</returns>
    public bool SetAgentLifetimeState(int agentId, AgentLifetimeState desiredState)
    {
        _logger?.LogTrace("[AgentClient] SetAgentLifetimeState() called with {AgentId} and {DesiredState}", agentId, desiredState);
        try
        {
            SetAgentLifetimeStateRequest request = new()
            {
                AgentId = agentId,
                AgentLifetimeState = desiredState
            };
            _logger?.LogDebug("[AgentClient] Sending SetAgentLifetimeStateRequest");
            GenericResult response = _client.SetAgentLifetimeState(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("SetAgentLifetimeState() succeeded");
                return true;
            }
            else
            {
                _logger?.LogError("[AgentClient] Sending SetAgentLifetimeState() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[AgentClient] Error setting agent lifetime state");
            throw;
        }
    }

    /// <summary>
    /// Sets the lifetime state of an agent asynchronously.
    /// </summary>
    /// <param name="agentId">The ID of the agent.</param>
    /// <param name="desiredState">The desired lifetime state.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the operation.</returns>
    public async Task<bool> SetAgentLifetimeStateAsync(int agentId, AgentLifetimeState desiredState)
    {
        _logger?.LogTrace("[AgentClient] SetAgentLifetimeStateAsync() called with {AgentId} and {DesiredState}", agentId, desiredState);
        try
        {
            SetAgentLifetimeStateRequest request = new()
            {
                AgentId = agentId,
                AgentLifetimeState = desiredState
            };
            _logger?.LogDebug("[AgentClient] Sending SetAgentLifetimeStateRequest");
            GenericResult response = await _client.SetAgentLifetimeStateAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformation("SetAgentLifetimeStateAsync() succeeded");
                return true;
            }
            else
            {
                _logger?.LogError("[AgentClient] Sending SetAgentLifetimeStateAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "[AgentClient] Error setting agent lifetime state");
            return false;
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
            _logger?.LogTrace("[AgentClient] Disposing resources");
        }

        _isDisposed = true;
        _logger?.LogInformation("[AgentClient] AgentClient disposed");
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