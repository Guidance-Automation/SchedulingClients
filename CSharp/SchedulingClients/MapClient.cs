using SchedulingClients.MapServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using NLog;
using BaseClients;

namespace SchedulingClients
{
    internal class MapClient : AbstractCallbackClient<IMapService>, IMapClient
    {
        private bool isDisposed = false;

        private TimeSpan heartbeat;

        private MapServiceCallback callback = new MapServiceCallback();

        public static TimeSpan MinimumHeartbeat => TimeSpan.FromMilliseconds(10000);

        /// <summary>
        /// Creates a new MapClient
        /// </summary>
        /// <param name="netTcpUri">net.tcp address of the map service</param>
        public MapClient(Uri netTcpUri, TimeSpan heartbeat = default(TimeSpan))
                    : base(netTcpUri)
        {
            this.heartbeat = heartbeat < MinimumHeartbeat ? MinimumHeartbeat : heartbeat;
            callback.OccupyingMandateProgressChange += Callback_OccupyingMandateProgressChange;
        }

        private OccupyingMandateProgressData occupyingMandateProgressData = null;

        /// <summary>
        /// The current state of progress of the occupying mandate
        /// </summary>
        public OccupyingMandateProgressData OccupyingMandateProgressData
        {
            get { return occupyingMandateProgressData; }

            private set
            {
                if (occupyingMandateProgressData != value)
                {
                    occupyingMandateProgressData = value;
                    OnNotifyPropertyChanged();
                }
            }
        }

        private void Callback_OccupyingMandateProgressChange(OccupyingMandateProgressData newProgressData)
        {
            OccupyingMandateProgressData = newProgressData;
        }

        protected override void SetInstanceContext()
        {
            this.context = new InstanceContext(this.callback);
        }

        /// <summary>
        /// Gets all move data
        /// </summary>
        /// <param name="moveData">All moves in the roadmap</param>
        /// <returns>ServiceOperationResult</returns>
        public ServiceOperationResult TryGetAllMoveData(out IEnumerable<MoveData> moveData)
        {
            Logger.Info("TryGetAllMoveData()");

            try
            {
                var result = GetAllMoveData();
                moveData = result.Item1;
                return ServiceOperationResultFactory.FromMapServiceCallData(result.Item2);
            }
            catch (Exception ex)
            {
                moveData = Enumerable.Empty<MoveData>();
                return HandleClientException(ex);
            }
        }

        /// <summary>
        /// Gets all node data
        /// </summary>
        /// <param name="nodeData">All nodes in the roadmap</param>
        /// <returns>ServiceOperationResult</returns>
        public ServiceOperationResult TryGetAllNodeData(out IEnumerable<NodeData> nodeData)
        {
            Logger.Info("TryGetAllNodeData()");

            try
            {
                var result = GetAllNodeData();
                nodeData = result.Item1;
                return ServiceOperationResultFactory.FromMapServiceCallData(result.Item2);
            }
            catch (Exception ex)
            {
                nodeData = Enumerable.Empty<NodeData>();
                return HandleClientException(ex);
            }
        }

        /// <summary>
        /// Hearbeat time
        /// </summary>
        public TimeSpan Heartbeat => heartbeat;

        protected override void HeartbeatThread()
        {
            Logger.Debug("HeartbeatThread()");

            ChannelFactory<IMapService> channelFactory = CreateChannelFactory();
            IMapService mapsStateService = channelFactory.CreateChannel();

            bool? exceptionCaught;

            while (!Terminate)
            {
                exceptionCaught = null;

                if (OccupyingMandateProgressData == null)
                {
                    OccupyingMandateProgressData refreshed;
                    if (TryGetOccupyingMandateProgressData(out refreshed).IsSuccessfull) OccupyingMandateProgressData = refreshed;
                }

                try
                {
                    Logger.Trace("SubscriptionHeartbeat({0})", Key);
                    mapsStateService.SubscriptionHeartbeat(Key);
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
                    OccupyingMandateProgressData = null;

                    channelFactory = CreateChannelFactory(); // Create a new channel as this one is dead
                    mapsStateService = channelFactory.CreateChannel();
                }

                heartbeatReset.WaitOne(Heartbeat);
            }

            Logger.Debug("HeartbeatThread exit");
        }

        /// <summary>
        /// Gets all parameter data
        /// </summary>
        /// <param name="parameterData">All parameters in the map</param>
        /// <returns>ServiceOperationResult</returns>
        public ServiceOperationResult TryGetAllParameterData(out IEnumerable<ParameterData> parameterData)
        {
            Logger.Info("TryGetAllParameterData()");

            try
            {
                var result = GetAllParameterData();
                parameterData = result.Item1;
                return ServiceOperationResultFactory.FromMapServiceCallData(result.Item2);
            }
            catch (Exception ex)
            {
                parameterData = Enumerable.Empty<ParameterData>();
                return HandleClientException(ex);
            }
        }

        /// <summary>
        /// Gets the trajectory of a specific move
        /// </summary>
        /// <param name="moveId">Id of the move</param>
        /// <param name="waypointData">Waypoints for this move</param>
        /// <returns>ServiceOperationResult</returns>
        public ServiceOperationResult TryGetTrajectory(int moveId, out IEnumerable<WaypointData> waypointData)
        {
            Logger.Info("TryGetTrajectory({0})", moveId);

            try
            {
                var result = GetTrajectory(moveId);
                waypointData = result.Item1;
                return ServiceOperationResultFactory.FromMapServiceCallData(result.Item2);
            }
            catch (Exception ex)
            {
                waypointData = null;
                return HandleClientException(ex);
            }
        }

        public ServiceOperationResult TryGetOccupyingMandateProgressData(out OccupyingMandateProgressData occupyingMandateProgressData)
        {
            Logger.Info("TryGetOccupyingMandateProgressData()");

            try
            {
                var result = GetOccupyingMandateProgressData();
                occupyingMandateProgressData = result.Item1;
                return ServiceOperationResultFactory.FromMapServiceCallData(result.Item2);
            }
            catch(Exception ex)
            {
                occupyingMandateProgressData = null;
                return HandleClientException(ex);
            }
        }

        /// <summary>
        /// Attempts to remotely occupy an area of the map
        /// </summary>
        /// <param name="mapItemIds">Map Items to occupy off</param>
        /// <param name="timeout">Length of time to wait before abandoning the occupation attempt</param>
        /// <returns></returns>
        public ServiceOperationResult TrySetOccupyingMandate(HashSet<int> mapItemIds, TimeSpan timeout)
        {
            Logger.Info("TrySetOccupyingMandate()");

            try
            {
                ServiceCallData result = SetOccupyingMandate(mapItemIds, timeout);   
                return ServiceOperationResultFactory.FromMapServiceCallData(result);
            }
            catch (Exception ex)
            {
                return HandleClientException(ex);
            }
        }

        /// <summary>
        /// Clears a previously occupied area of the map
        /// </summary>
        public ServiceOperationResult TryClearOccupyingMandate()
        {
            Logger.Info("TryClearBlockingMandate()");

            try
            {
                ServiceCallData result = ClearOccupyingMandate();
                return ServiceOperationResultFactory.FromMapServiceCallData(result);
            }
            catch (Exception ex)
            {
                return HandleClientException(ex);
            }
        }

        /// <summary>
        /// Attempts to get all map items set into maintenance mode.
        /// </summary>
        /// <param name="mapItemsInMaintenance">Map Item Ids in maintenance.</param>
        public ServiceOperationResult TryGetAllMaintenanceItems(out IEnumerable<MapItemData> mapItemsInMaintenance)
        {
            Logger.Info("TryGetAllMaintenanceItems()");

            try
            {
                Tuple<MapItemData[], ServiceCallData> result = GetItemsInMaintenance();
                mapItemsInMaintenance = result.Item1;
                return ServiceOperationResultFactory.FromMapServiceCallData(result.Item2);
            }
            catch (Exception ex)
            {
                mapItemsInMaintenance = Enumerable.Empty<MapItemData>();
                return HandleClientException(ex);
            }
        }

        /// <summary>
        /// Attempts to set all given items as in maintenance.
        /// </summary>
        /// <param name="mapItemIds">Map items to add into maintenance mode.</param>
        public ServiceOperationResult TrySetInMaintenance(HashSet<int> mapItemIds)
        {
            Logger.Info("TrySetInMaintenance()");

            try
            {
                ServiceCallData result = SetInMaintenance(mapItemIds);
                return ServiceOperationResultFactory.FromMapServiceCallData(result);
            }
            catch (Exception ex)
            {
                return HandleClientException(ex);
            }
        }

        /// <summary>
        /// Attempts to remove all given items from in maintenance.
        /// </summary>
        /// <param name="mapItemIds">Map items to remove from maintenance mode.</param>
        public ServiceOperationResult TryRemoveFromMaintenance(HashSet<int> mapItemIds)
        {
            Logger.Info("TryRemoveFromMaintenance()");

            try
            {
                ServiceCallData result = RemoveFromMaintenance(mapItemIds);
                return ServiceOperationResultFactory.FromMapServiceCallData(result);
            }
            catch (Exception ex)
            {
                return HandleClientException(ex);
            }
        }

        protected override void Dispose(bool isDisposing)
        {
            Logger.Debug("Dispose({0})", isDisposing);

            if (isDisposed) return;

            callback.OccupyingMandateProgressChange += Callback_OccupyingMandateProgressChange;
   
            base.Dispose(isDisposing);
        }

        private Tuple<MoveData[], ServiceCallData> GetAllMoveData()
        {
            Logger.Debug("GetAllMoveData()");

            if (isDisposed)
            {
                throw new ObjectDisposedException("RoadmapClient");
            }

            Tuple<MoveData[], ServiceCallData> result;

            using (ChannelFactory<IMapService> channelFactory = CreateChannelFactory())
            {
                IMapService channel = channelFactory.CreateChannel();
                result = channel.GetAllMoveData();
                channelFactory.Close();
            }

            return result;
        }

        private Tuple<NodeData[], ServiceCallData> GetAllNodeData()
        {
            Logger.Debug("GetAllNodeData()");

            if (isDisposed)
            {
                throw new ObjectDisposedException("RoadmapClient");
            }

            Tuple<NodeData[], ServiceCallData> result;

            using (ChannelFactory<IMapService> channelFactory = CreateChannelFactory())
            {
                IMapService channel = channelFactory.CreateChannel();
                result = channel.GetAllNodeData();
                channelFactory.Close();
            }

            return result;
        }

        private Tuple<ParameterData[], ServiceCallData> GetAllParameterData()
        {
            Logger.Debug("GetAllParameterData()");

            if (isDisposed)
            {
                throw new ObjectDisposedException("RoadmapClient");
            }

            Tuple<ParameterData[], ServiceCallData> result;

            using (ChannelFactory<IMapService> channelFactory = CreateChannelFactory())
            {
                IMapService channel = channelFactory.CreateChannel();
                result = channel.GetAllParameterData();
                channelFactory.Close();
            }

            return result;
        }

        private Tuple<WaypointData[], ServiceCallData> GetTrajectory(int moveId)
        {
            Logger.Debug("GetTrajectory({0})", moveId);

            if (isDisposed)
            {
                throw new ObjectDisposedException("MapClient");
            }

            Tuple<WaypointData[], ServiceCallData> result;

            using (ChannelFactory<IMapService> channelFactory = CreateChannelFactory())
            {
                IMapService channel = channelFactory.CreateChannel();
                result = channel.GetTrajectory(moveId);
                channelFactory.Close();
            }

            return result;
        }

        private Tuple<OccupyingMandateProgressData, ServiceCallData> GetOccupyingMandateProgressData()
        {
            Logger.Debug("GetOccupyingMandateProgressData()");

            if (isDisposed) throw new ObjectDisposedException("MapClient");

            Tuple<OccupyingMandateProgressData, ServiceCallData> result;

            using (ChannelFactory<IMapService> channelFactory = CreateChannelFactory())
            {
                IMapService channel = channelFactory.CreateChannel();
                result = channel.GetOccupyingMandateProgressData();
                channelFactory.Close();
            }

            return result;
        }

        private ServiceCallData SetOccupyingMandate(HashSet<int> mapItemIds, TimeSpan timeout)
        {
            Logger.Debug("SetOccupyingMandate()");

            if (isDisposed) throw new ObjectDisposedException("MapClient");

            ServiceCallData result;

            using (ChannelFactory<IMapService> channelFactory = CreateChannelFactory())
            {
                IMapService channel = channelFactory.CreateChannel();
                result = channel.SetOccupyingMandate(mapItemIds.ToArray(), timeout);
                channelFactory.Close();
            }

            return result;
        }

        private ServiceCallData ClearOccupyingMandate()
        {
            Logger.Debug("ClearOccupyingMandate()");

            if (isDisposed) throw new ObjectDisposedException("MapClient");

            ServiceCallData result;

            using (ChannelFactory<IMapService> channelFactory = CreateChannelFactory())
            {
                IMapService channel = channelFactory.CreateChannel();
                result = channel.ClearOccupyingMandate();
                channelFactory.Close();
            }

            return result;
        }

        private Tuple<MapItemData[], ServiceCallData> GetItemsInMaintenance()
        {
            Logger.Debug("GetItemsInMaintenance()");

            if (isDisposed) throw new ObjectDisposedException("MapClient");

            using (ChannelFactory<IMapService> channelFactory = CreateChannelFactory())
            {
                IMapService channel = channelFactory.CreateChannel();
                return channel.GetMapItemsInMaintenance();
            }
        }

        private ServiceCallData SetInMaintenance(HashSet<int> mapItemIds)
        {
            Logger.Debug("SetInMaintenance()");

            if (isDisposed) throw new ObjectDisposedException("MapClient");

            using (ChannelFactory<IMapService> channelFactory = CreateChannelFactory())
            {
                IMapService channel = channelFactory.CreateChannel();
                return channel.SetItemsInMaintenance(mapItemIds.ToArray());
            }
        }

        private ServiceCallData RemoveFromMaintenance(HashSet<int> mapItemIds)
        {
            Logger.Debug("RemoveFromMaintenance()");

            if (isDisposed) throw new ObjectDisposedException("MapClient");

            using (ChannelFactory<IMapService> channelFactory = CreateChannelFactory())
            {
                IMapService channel = channelFactory.CreateChannel();
                return channel.RemoveItemsInMaintenance(mapItemIds.ToArray());
            }
        }
    }
}