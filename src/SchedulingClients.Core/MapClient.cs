using BaseClients.Core;
using GAAPICommon.Architecture;
using GAAPICommon.Core.Dtos;
using MoreLinq;
using SchedulingClients.Core.MapServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace SchedulingClients.Core
{
    internal class MapClient : AbstractCallbackClient<IMapService>, IMapClient
    {
        private bool isDisposed = false;

        private readonly MapServiceCallback callback = new MapServiceCallback();

        public static TimeSpan MinimumHeartbeat => TimeSpan.FromMilliseconds(10000);

        public MapClient(Uri netTcpUri, TimeSpan heartbeat = default)
                    : base(netTcpUri, heartbeat)
        {
            callback.OccupyingMandateProgressChange += Callback_OccupyingMandateProgressChange;
        }

        private OccupyingMandateProgressDto occupyingMandateProgressDto = null;

        public OccupyingMandateProgressDto OccupyingMandateProgress
        {
            get { return occupyingMandateProgressDto; }

            private set
            {
                if (occupyingMandateProgressDto != value)
                {
                    occupyingMandateProgressDto = value;
                    OnProgressUpdated(occupyingMandateProgressDto);
                }
            }
        }

        public event Action<OccupyingMandateProgressDto> OccupyingMandateProgressUpdated;

        private void OnProgressUpdated(OccupyingMandateProgressDto occupyingMandateProgressDto)
        {
            Action<OccupyingMandateProgressDto> handlers = OccupyingMandateProgressUpdated;

            handlers?
               .GetInvocationList()
               .Cast<Action<OccupyingMandateProgressDto>>()
               .ForEach(e => e.BeginInvoke(occupyingMandateProgressDto, null, null));
        }

        private void Callback_OccupyingMandateProgressChange(OccupyingMandateProgressDto newProgressData)
        {
            OccupyingMandateProgress = newProgressData;
        }

        protected override void SetInstanceContext()
        {
            context = new InstanceContext(callback);
        }

        public IServiceCallResult<MoveDto[]> GetAllMoves()
        {
            Logger.Trace("GetAllMoveData()");
            return HandleAPICall<MoveDto[]>(e => e.GetAllMoveData());
        }

        public IServiceCallResult<NodeDto[]> GetAllNodes()
        {
            Logger.Trace("GetAllNodeData()");
            return HandleAPICall<NodeDto[]>(e => e.GetAllNodeData());
        }

        public IServiceCallResult<ParameterDto[]> GetAllParameters()
        {
            Logger.Trace("GetAllParameterData()");
            return HandleAPICall<ParameterDto[]>(e => e.GetAllParameterData());
        }

        public IServiceCallResult<WaypointDto[]> GetTrajectory(int moveId)
        {
            Logger.Trace($"GetTrajectory() moveId:{moveId}");
            return HandleAPICall<WaypointDto[]>(e => e.GetTrajectory(moveId));
        }

        public IServiceCallResult<OccupyingMandateProgressDto> GetOccupyingMandateProgress()
        {
            Logger.Trace("GetOccupyingMandateProgressData()");
            return HandleAPICall<OccupyingMandateProgressDto>(e => e.GetOccupyingMandateProgressData());
        }

        public IServiceCallResult SetOccupyingMandate(HashSet<int> mapItemIds, TimeSpan timeout)
        {
            if (mapItemIds == null)
                throw new ArgumentNullException("mapItemIds");

            Logger.Trace($"SetOccupyingMandate() {mapItemIds.Count} item(s)");
            return HandleAPICall(e => e.SetOccupyingMandate(mapItemIds.ToArray(), timeout));
        }

        public IServiceCallResult ClearOccupyingMandate()
        {
            Logger.Info("ClearOccupyingMandate()");
            return HandleAPICall(e => e.ClearOccupyingMandate());
        }

        protected override void Dispose(bool isDisposing)
        {
            Logger.Debug("Dispose({0})", isDisposing);

            if (isDisposed)
                return;

            callback.OccupyingMandateProgressChange -= Callback_OccupyingMandateProgressChange;

            isDisposed = true;

            base.Dispose(isDisposing);
        }

        protected override void HandleSubscriptionHeartbeat(IMapService channel, Guid key)
        {
            channel.SubscriptionHeartbeat(key);
        }

        protected override void HandleUnsubscribeHeartbeat(IMapService channel, Guid key)
        {
            channel.UnsubscribeHeartbeat(key);
        }
    }
}