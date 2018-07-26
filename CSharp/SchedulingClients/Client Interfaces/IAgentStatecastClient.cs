using GAClients;
using SchedulingClients.AgentStateCastServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingClients
{
	public interface IAgentStatecastClient : IClient
	{
		ServiceOperationResult TryGetEnumStatecastValue(int agentId, string parameterAlias, out byte parameterValue);

		ServiceOperationResult TryGetFloatStatecastValue(int agentId, string parameterAlias, out float parameterValue);

		ServiceOperationResult TryGetShortStatecastValue(int agentId, string parameterAlias, out short parameterValue);

		ServiceOperationResult TryGetUShortStatecastValue(int agentId, string parameterAlias, out ushort parameterValue);

        ServiceOperationResult TryGetUIntegerStatecastValue(int agentId, string parameterAlias, out uint parameterValue);

        ServiceOperationResult TryGetIntegerStatecastValue(int agentId, string parameterAlias, out int parameterValue);

        ServiceOperationResult TryGetIPAddressStatecastValue(int agentId, string parameterAlias, out IPAddress parameterValue);

		ServiceOperationResult TryGetStatecastDescription(int agentId, out CastType statecastDescription);
	}
}
