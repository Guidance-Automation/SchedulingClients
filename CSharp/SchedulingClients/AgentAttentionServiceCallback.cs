using SchedulingClients.AgentAttentionServiceReference;
using System;

namespace SchedulingClients
{
    public class AgentAttentionServiceCallback : IAgentAttentionServiceCallback
	{
		public AgentAttentionServiceCallback()
		{
		}

		public event Action<AgentAttentionData[]> AgentAttentionChange;

		public void OnCallback(AgentAttentionData[] callbackObject)
		{
			Action<AgentAttentionData[]> handlers = AgentAttentionChange;

			if (handlers != null)
			{
				foreach (Action<AgentAttentionData[]> handler in handlers.GetInvocationList())
				{
					handler.BeginInvoke(callbackObject, null, null);
				}
			}
		}
	}
}
