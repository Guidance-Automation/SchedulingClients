﻿using GAClients;
using SchedulingClients.AgentBatteryStatusServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingClients
{
	public interface IAgentBatteryStatusClient
	{
		ServiceOperationResult TryGetAllAgentData(out IEnumerable<AgentBatteryStatusData> agentBatteryStatusDatas);
	}
}
