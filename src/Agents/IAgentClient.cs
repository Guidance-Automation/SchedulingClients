using GAAPICommon.Enums;
using GAAPICommon.Messages;

namespace GAClients.SchedulingClients.Agents;

public interface IAgentClient : IDisposable
{
    /// <summary>
    /// Gets all agents known to the scheduler. 
    /// </summary>
    /// <returns>Array of agent dtos</returns>
    public IEnumerable<AgentDto>? GetAllAgents();

    /// <summary>
    /// Gets all agents known to the scheduler asynchronously.
    /// </summary>
    /// <returns>Array of agent dtos</returns>
    public Task<IEnumerable<AgentDto>?> GetAllAgentsAsync();

    /// <summary>
    /// Gets all agents known to the scheduler in a given lifetime state.
    /// </summary>
    /// <param name="agentLifetimeState">Target lifetime state</param>
    /// <returns>Array of agent dtos</returns>
    public IEnumerable<AgentDto>? GetAllAgentsInLifetimeState(AgentLifetimeState agentLifetimeState);

    /// <summary>
    /// Gets all agents known to the scheduler in a given lifetime state asynchronously.
    /// </summary>
    /// <param name="agentLifetimeState">Target lifetime state</param>
    /// <returns>Array of agent dtos</returns>
    public Task<IEnumerable<AgentDto>?> GetAllAgentsInLifetimeStateAsync(AgentLifetimeState agentLifetimeState);

    /// <summary>
    /// Sets the lifetime state of an agent.
    /// </summary>
    /// <param name="agentId">Target agent Id</param>
    /// <param name="desiredState">Target state</param>
    /// <returns>Successful service call result on success</returns>
    public bool SetAgentLifetimeState(int agentId, AgentLifetimeState desiredState);

    /// <summary>
    /// Sets the lifetime state of an agent asynchronously.
    /// </summary>
    /// <param name="agentId">Target agent Id</param>
    /// <param name="desiredState">Target state</param>
    /// <returns>Successful service call result on success</returns>
    public Task<bool> SetAgentLifetimeStateAsync(int agentId, AgentLifetimeState desiredState);
}