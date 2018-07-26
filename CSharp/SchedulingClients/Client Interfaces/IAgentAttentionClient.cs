using SchedulingClients.AgentAttentionServiceReference;
using GAClients;
using System;

namespace SchedulingClients
{
    public interface IAgentAttentionClient : ICallbackClient
    { 
        event Action<AgentAttentionData[]> AgentAttentionChange;
    } 
}
