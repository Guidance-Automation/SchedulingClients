﻿using System;
using System.Collections.Generic;
using SchedulingClients.FleetManagerServiceReference;
using System.ServiceModel;
using System.Net;
using GAClients;
using System.Xml.Linq;

namespace SchedulingClients
{
    public class FleetManagerClient : AbstractCallbackClient<IFleetManagerService>
    {
        private FleetManagerServiceCallback callback = new FleetManagerServiceCallback();

        private bool isDisposed = false;

        private TimeSpan heartbeat;

        private Queue<byte[]> buffer = new Queue<byte[]>();

        /// <summary>
        /// Creates a new FleetManagerClient
        /// </summary>
        /// <param name="netTcpUri">net.tcp address of the job builder service</param>
        public FleetManagerClient(Uri netTcpUri, TimeSpan heartbeat = default(TimeSpan))
            : base(netTcpUri)
        {
            this.heartbeat = heartbeat < TimeSpan.FromMilliseconds(1000) ? TimeSpan.FromMilliseconds(1000) : heartbeat;
            this.callback.FleetStateUpdate += Callback_FleetStateUpdate;
        }

        private void Callback_FleetStateUpdate(FleetState fleetState)
        {
            LastFleetStateReceived = fleetState;
        }

        public TimeSpan Heartbeat { get { return heartbeat; } }

        ~FleetManagerClient()
        {
            Dispose(false);
        }

        protected override void HeartbeatThread()
        {
            Logger.Debug("HeartbeatThread()");

            ChannelFactory<IFleetManagerService> channelFactory = CreateChannelFactory();
            IFleetManagerService fleetManagerService = channelFactory.CreateChannel();

            bool? exceptionCaught;

            while (!Terminate)
            {
                exceptionCaught = null;

                try
                {
                    Logger.Trace("SubscriptionHeartbeat({0})", Key);
                    fleetManagerService.SubscriptionHeartbeat(Key);
                    IsConnected = true;
                    exceptionCaught = false;
                }
                catch (EndpointNotFoundException)
                {
                    Logger.Warn("HeartbeatThread - EndpointNotFoundException. Is the server running?");
                    exceptionCaught = true;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    exceptionCaught = true;
                }

                if (exceptionCaught == true)
                {
                    channelFactory.Abort();
                    IsConnected = false;

                    channelFactory = CreateChannelFactory(); // Create a new channel as this one is dead
                    fleetManagerService = channelFactory.CreateChannel();
                }

                heartbeatReset.WaitOne(Heartbeat);
            }

            Logger.Debug("HeartbeatThread exit");
        }

        protected override void SetInstanceContext()
        {
            this.context = new InstanceContext(this.callback);
        }

        private FleetState lastFleetStateReceived = null;

        public FleetState LastFleetStateReceived
        {
            get { return lastFleetStateReceived; }
            set
            {
                if (lastFleetStateReceived == null || value.Tick.IsCurrentByteTickLarger(lastFleetStateReceived.Tick))
                {
                    lastFleetStateReceived = value;
                    OnNotifyPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Requests fleet manager freeze
        /// </summary>
        /// <param name="success">True if frozen</param>
        /// <returns>ServiceOperationResult</returns>
        public ServiceOperationResult TryRequestFreeze(out bool success)
        {
            Logger.Info("TryRequestFreeze()");

            try
            {
                var result = RequestFreeze();
                success = result.Item1;
                return ServiceOperationResultFactory.FromFleetManagerServiceCallData(result.Item2);
            }
            catch (Exception ex)
            {
                success = false;
                return HandleClientException(ex);
            }
        }

        /// <summary>
        /// Get xDocument of kingpin description
        /// </summary>
        /// <param name="ipAddress">IPAddress</param>
        /// <param name="xDocument">kingpin.xml xDocument</param>
        /// <returns>ServiceOperationResult</returns>
        public ServiceOperationResult TryGetKingpinDescription(IPAddress ipAddress, out XDocument xDocument)
        {
            Logger.Info("TryGetKingpinDescription()");

            try
            {
                var result = GetKingpinDescription(ipAddress);
                XElement xElement = result.Item1;
                xDocument = new XDocument(xElement);

                return ServiceOperationResultFactory.FromFleetManagerServiceCallData(result.Item2);
            }
            catch (Exception ex)
            {
                xDocument = null;
                return HandleClientException(ex);
            }
        }

        /// <summary>
        /// Commit extended waypoints to a kingpin
        /// </summary>
        /// <returns>ServiceOperationResult</returns>
        public ServiceOperationResult TryCommitExtendedWaypoints(IPAddress ipAddress, int instructionId, byte[] extendedWaypoints,  out bool success)
        {
            Logger.Info("TryCommitExtendedWaypoints()");

            try
            {
                Tuple<bool,ServiceCallData> result = CommitExtendedWaypoints(ipAddress, instructionId, extendedWaypoints);
                success = result.Item1;
                return ServiceOperationResultFactory.FromFleetManagerServiceCallData(result.Item2);
            }
            catch (Exception ex)
            {
                success = false;
                return HandleClientException(ex);
            }
        }

   

        /// <summary>
        /// Requests fleet manager unfreeze
        /// </summary>
        /// <param name="success">True if unfrozen</param>
        /// <returns>ServiceOperationResult</returns>
        public ServiceOperationResult TryRequestUnfreeze(out bool success)
        {
            Logger.Info("TryRequestUnfreeze()");

            try
            {
                var result = RequestUnfreeze();
                success = result.Item1;
                return ServiceOperationResultFactory.FromFleetManagerServiceCallData(result.Item2);
            }
            catch (Exception ex)
            {
                success = false;
                return HandleClientException(ex);
            }
        }

        public ServiceOperationResult TryCreateVirtualVehicle(IPAddress ipAddress, PoseData pose, out bool success)
        {
            Logger.Info("TryCreateVirtualVehicle()");

            try
            {
                var result = CreateVirtualVehicle(ipAddress, pose);
                success = result.Item1;
                return ServiceOperationResultFactory.FromFleetManagerServiceCallData(result.Item2);
            }
            catch (Exception ex)
            {
                success = false;
                return HandleClientException(ex);
            }
        }

        public ServiceOperationResult TryRemoveVehicle(IPAddress ipAddress, out bool success)
        {
            Logger.Info("TryRemoveVehicle()");

            try
            {
                var result = RemoveVehicle(ipAddress);
                success = result.Item1;
                return ServiceOperationResultFactory.FromFleetManagerServiceCallData(result.Item2);
            }
            catch (Exception ex)
            {
                success = false;
                return HandleClientException(ex);
            }
        }

        public ServiceOperationResult TrySetPose(IPAddress ipAddress, PoseData pose, out bool success)
        {
            Logger.Info("TryCreateVirtualVehicle()");

            try
            {
                var result = SetPose(ipAddress, pose);
                success = result.Item1;
                return ServiceOperationResultFactory.FromFleetManagerServiceCallData(result.Item2);
            }
            catch (Exception ex)
            {
                success = false;
                return HandleClientException(ex);
            }
        }

        protected override void Dispose(bool isDisposing)
        {
            Logger.Debug("Dispose({0})", isDisposing);

            if (isDisposed)
            {
                return;
            }

            if (isDisposing)
            {
                this.callback.FleetStateUpdate -= Callback_FleetStateUpdate;
            }

            isDisposed = true;
        }

        private Tuple<XElement,ServiceCallData> GetKingpinDescription(IPAddress ipAddress)
        {
            Logger.Debug("GetKingpinDescription()");

            if (isDisposed)
            {
                throw new ObjectDisposedException("FleetManagerClient");
            }

            Tuple<XElement, ServiceCallData> result;

            using (ChannelFactory<IFleetManagerService> channelFactory = CreateChannelFactory())
            {
                IFleetManagerService channel = channelFactory.CreateChannel();
                result = channel.GetKingpinDescription(ipAddress);
                channelFactory.Close();
            }

            return result;
        }

        private Tuple<bool, ServiceCallData> RequestFreeze()
        {
            Logger.Debug("RequestFreeze()");

            if (isDisposed)
            {
                throw new ObjectDisposedException("FleetManagerClient");
            }

            Tuple<bool, ServiceCallData> result;

            using (ChannelFactory<IFleetManagerService> channelFactory = CreateChannelFactory())
            {
                IFleetManagerService channel = channelFactory.CreateChannel();
                result = channel.RequestFreeze();
                channelFactory.Close();
            }

            return result;
        }

        private Tuple<bool,ServiceCallData> CommitExtendedWaypoints(IPAddress ipAddress, int instructionId, byte[] extendedWaypoints)
        {
            if (isDisposed)
            {
                throw new ObjectDisposedException("FleetManagerClient");
            }

            Tuple<bool, ServiceCallData> result;

            using (ChannelFactory<IFleetManagerService> channelFactory = CreateChannelFactory())
            {
                IFleetManagerService channel = channelFactory.CreateChannel();
                result = channel.CommitExtendedWaypoints(ipAddress, instructionId, extendedWaypoints);
                channelFactory.Close();
            }

            return result;
        }

        private Tuple<bool, ServiceCallData> RequestUnfreeze()
        {
            Logger.Debug("RequestUnfreeze()");

            if (isDisposed)
            {
                throw new ObjectDisposedException("FleetManagerClient");
            }

            Tuple<bool, ServiceCallData> result;

            using (ChannelFactory<IFleetManagerService> channelFactory = CreateChannelFactory())
            {
                IFleetManagerService channel = channelFactory.CreateChannel();
                result = channel.RequestUnfreeze();
                channelFactory.Close();
            }

            return result;
        }

        private Tuple<bool, ServiceCallData> CreateVirtualVehicle(IPAddress ipAddress, PoseData pose)
        {
            Logger.Debug("CreateVirtualVehicle()");

            if (isDisposed)
            {
                throw new ObjectDisposedException("FleetManagerClient");
            }

            Tuple<bool, ServiceCallData> result;

            using (ChannelFactory<IFleetManagerService> channelFactory = CreateChannelFactory())
            {
                IFleetManagerService channel = channelFactory.CreateChannel();
                result = channel.CreateVirtualVehicle(ipAddress, pose);
                channelFactory.Close();
            }

            return result;
        }

        private Tuple<bool, ServiceCallData> RemoveVehicle(IPAddress ipAddress)
        {
            Logger.Debug("RemoveVehicle()");

            if (isDisposed)
            {
                throw new ObjectDisposedException("FleetManagerClient");
            }

            Tuple<bool, ServiceCallData> result;

            using (ChannelFactory<IFleetManagerService> channelFactory = CreateChannelFactory())
            {
                IFleetManagerService channel = channelFactory.CreateChannel();
                result = channel.RemoveVehicle(ipAddress);
                channelFactory.Close();
            }

            return result;
        }

        private Tuple<bool, ServiceCallData> SetPose(IPAddress ipAddress, PoseData pose)
        {
            Logger.Debug("SetPose()");

            if (isDisposed)
            {
                throw new ObjectDisposedException("FleetManagerClient");
            }

            Tuple<bool, ServiceCallData> result;

            using (ChannelFactory<IFleetManagerService> channelFactory = CreateChannelFactory())
            {
                IFleetManagerService channel = channelFactory.CreateChannel();
                result = channel.SetPose(ipAddress, pose);
                channelFactory.Close();
            }

            return result;
        }
    }
}