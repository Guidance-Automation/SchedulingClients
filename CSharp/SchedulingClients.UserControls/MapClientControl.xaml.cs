using SchedulingClients.MapServiceReference;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SchedulingClients.UserControls
{
    public partial class MapClientControl : UserControl
    {
        public MapClientControl()
        {
            InitializeComponent();
        }

        private void getAllMoveDataButton_Click(object sender, RoutedEventArgs e)
        {
            IMapClient client = DataContext as IMapClient;
            IEnumerable<MoveData> nodeDatas;
            client.TryGetAllMoveData(out nodeDatas);

            moveDataDataGrid.ItemsSource = nodeDatas;
        }

        private void getAllNodeDataButton_Click(object sender, RoutedEventArgs e)
        {
            IMapClient client = DataContext as IMapClient;
            IEnumerable<NodeData> nodeDatas;
            client.TryGetAllNodeData(out nodeDatas);

            nodeDataDataGrid.ItemsSource = nodeDatas;
        }

        private void getTrajectoryButton_Click(object sender, RoutedEventArgs e)
        {
            int moveId = (int)trajectoryIdUpDown.Value;
            IMapClient client = DataContext as IMapClient;
            IEnumerable<WaypointData> waypoints;
            client.TryGetTrajectory(moveId, out waypoints);

            TrajectoryDialog dialog = new TrajectoryDialog();
            dialog.DataContext = waypoints;
            dialog.ShowDialog();
        }

        private void getAllParameterDataButton_Click(object sender, RoutedEventArgs e)
        {
            IMapClient client = DataContext as IMapClient;
            IEnumerable<ParameterData> parameterDatas;
            client.TryGetAllParameterData(out parameterDatas);

            parameterDataDataGrid.ItemsSource = parameterDatas;
        }

        private void getAllMaintenance_Click(object sender, RoutedEventArgs e)
        {
            IMapClient client = DataContext as IMapClient;
            IEnumerable<MapItemData> itemDatas;
            client.TryGetAllMaintenanceItems(out itemDatas);
            maintenanceItemsDataGrid.ItemsSource = itemDatas;
        }
    }
}