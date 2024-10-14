using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SchedulingClients.MapServiceReference;
using BaseClients;

namespace SchedulingClients.Test
{
    /// <summary>
    /// Requires a server to be running on local host to be succesfull with a populated map
    /// </summary>
    [Category("MapClient")]
    public class TMapClient : AbstractClientTest
    {
        [OneTimeSetUp]
        public override void OneTimeSetup()
        {
            base.OneTimeSetup();
            IMapClient client = ClientFactory.CreateTcpMapClient(settings);
            clients.Add(client);
        }

        IMapClient MapClient => clients.First(e => e is IMapClient) as IMapClient;

        [Test]
        public void GetMapItems()
        {
            IEnumerable<NodeData> nodeDataset;
            MapClient.TryGetAllNodeData(out nodeDataset);

            Assert.IsNotNull(nodeDataset);

            IEnumerable<MoveData> moveDataset;
            MapClient.TryGetAllMoveData(out moveDataset);

            Assert.IsNotNull(moveDataset);
        }

        [Test]
        public void GetTrajectory()
        { 
            IEnumerable<MoveData> moveDataset;
            MapClient.TryGetAllMoveData(out moveDataset);

            Assert.IsNotNull(moveDataset);

            foreach(MoveData moveData in moveDataset)
            {
                IEnumerable<WaypointData> waypointData = null;
                MapClient.TryGetTrajectory(moveData.Id, out waypointData);

                Assert.IsNotNull(waypointData);
                CollectionAssert.IsNotEmpty(waypointData);
            }
        }

        [Test]
        public void SetMaintenanceNode()
        {
            MapClient.TryGetAllNodeData(out IEnumerable<NodeData> nodeDataset);
            Assert.IsNotNull(nodeDataset);

            NodeData nodeToSet = nodeDataset.FirstOrDefault();
            Assert.IsNotNull(nodeToSet);

            HashSet<int> nodes = new HashSet<int>()
            {
                nodeToSet.MapItemId
            };
            MapClient.TrySetInMaintenance(nodes);

            MapClient.TryGetAllMaintenanceItems(out var mapItemsInMaintenance);

            Assert.IsNotNull(mapItemsInMaintenance);
            Assert.That(mapItemsInMaintenance.Any(m => m.Id == nodeToSet.MapItemId));

            MapClient.TryRemoveFromMaintenance(nodes);
            MapClient.TryGetAllMaintenanceItems(out var mapItemsInMaintenanceAgain);
            Assert.IsNotNull(mapItemsInMaintenanceAgain);
            Assert.That(!mapItemsInMaintenanceAgain.Any(m => m.Id == nodeToSet.MapItemId));
        }
    }
}
