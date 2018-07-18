﻿using GAClients;
using SchedulingClients.FleetManagerServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SchedulingClients
{
	public interface IFleetManagerClient : ICallbackClient
	{
		ServiceOperationResult TryRequestFreeze(out bool success);

		ServiceOperationResult TryGetKingpinDescription(IPAddress ipAddress, out XDocument xDocument);

		ServiceOperationResult TryCommitExtendedWaypoints(IPAddress ipAddress, int instructionId, BaseMovementType baseMovementType, byte[] extendedWaypoints, out bool success);

		ServiceOperationResult TryRequestUnfreeze(out bool success);

		ServiceOperationResult TryCreateVirtualVehicle(IPAddress ipAddress, PoseData pose, out bool success);

		ServiceOperationResult TryRemoveVehicle(IPAddress ipAddress, out bool success);

		ServiceOperationResult TrySetPose(IPAddress ipAddress, PoseData pose, out bool success);

        FleetState FleetState { get; }

    }
}
