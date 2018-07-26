using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SchedulingClients.FleetManagerServiceReference;
using System.IO;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Xml.Linq;
using GAClients;

namespace SchedulingClients.UserControls
{
    /// <summary>
    /// Interaction logic for FleetManagerClientControl.xaml
    /// </summary>
    public partial class FleetManagerClientControl : UserControl
    {
        public FleetManagerClientControl()
        {
            readonlyMailboxes = new ReadOnlyObservableCollection<KingpinStateMailbox>(mailboxes);
            InitializeComponent();

        }

        private void freezeButton_Click(object sender, RoutedEventArgs e)
        {
            IFleetManagerClient fleetManagerClient = DataContext as IFleetManagerClient;
            bool success;
            fleetManagerClient.TryRequestFreeze(out success);
        }

        private void unfreezeButton_Click(object sender, RoutedEventArgs e)
        {
            IFleetManagerClient fleetManagerClient = DataContext as IFleetManagerClient;
            bool success;
            fleetManagerClient.TryRequestUnfreeze(out success);
        }

        private void commitMenuItem_Click(object sender, RoutedEventArgs e)
        {

            IFleetManagerClient fleetManagerClient = DataContext as IFleetManagerClient;
            KingpinState state = fleetManagerClient.FleetState.KingpinStates.FirstOrDefault();

            // Crude bit of code to just add a straight line for a pose of zero and no waypoint
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();

            byte[] bytes;

            using (Stream resFileStream = assembly.GetManifestResourceStream(@"SchedulingClients.UserControls.Resources.extendedWaypointBytes.txt"))
            {
                bytes = new byte[resFileStream.Length];
                resFileStream.Read(bytes, 0, bytes.Length);
            }
            bool success;
            fleetManagerClient.TryCommitExtendedWaypoints(state.IPAddress, 1, BaseMovementType.Forwards, bytes, out success);
        }

        private ObservableCollection<KingpinStateMailbox> mailboxes = new ObservableCollection<KingpinStateMailbox>();

        private ReadOnlyObservableCollection<KingpinStateMailbox> readonlyMailboxes;

        internal ReadOnlyObservableCollection<KingpinStateMailbox> Mailboxes { get { return readonlyMailboxes; } }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is IFleetManagerClient)
            {
                IFleetManagerClient client = e.NewValue as IFleetManagerClient;
                client.PropertyChanged += Client_PropertyChanged;
                dataGrid.ItemsSource = mailboxes;
            }
        }

        private void HandleKingpinStates(IEnumerable<KingpinState> kingpinStates)
        {
            foreach(KingpinState kingpinState in kingpinStates.ToList())
            {
                KingpinStateMailbox mailbox = mailboxes.FirstOrDefault(e => e.KingpinState.IPAddress.Equals(kingpinState.IPAddress));

                if (mailbox != null)
                {
                    mailbox.Update(kingpinState);
                }
                else
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        mailboxes.Add(new KingpinStateMailbox(kingpinState));
                    });         
                }
            }
        }

        private void Client_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "LastFleetStateReceived":
                    {
                        IFleetManagerClient client = sender as IFleetManagerClient;
                        HandleKingpinStates(client.FleetState.KingpinStates);
                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }

        private void getKingpinDescription_Click(object sender, RoutedEventArgs e)
        {
            IFleetManagerClient client = DataContext as IFleetManagerClient;

            KingpinStateMailbox mailbox = dataGrid.SelectedItem as KingpinStateMailbox;
            XDocument xDocument;

            ServiceOperationResult result = client.TryGetKingpinDescription(mailbox.KingpinState.IPAddress, out xDocument);

            if (result.IsSuccessfull)
            {
                string tempFile = Path.GetTempPath() + System.Guid.NewGuid().ToString() + ".xml";
                xDocument.Save(tempFile);

                System.Diagnostics.Process.Start(tempFile);
            }         
        }

        private void createVirtualVehicleButton_Click(object sender, RoutedEventArgs e)
        {
            IFleetManagerClient client = DataContext as IFleetManagerClient;

            bool success;
#warning Angle conversion not performed correctly here
            client.TryCreateVirtualVehicle(ipAddressControl.IPAddress, poseControl.Pose, out success);

            if (success)
            {
                MessageBox.Show("Virtual vehicle creation was successful", "Virtual Vehicle Creation", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Virtual vehicle creation failed", "Virtual Vehicle Creation", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void removeVehicleButton_Click(object sender, RoutedEventArgs e)
        {
            IFleetManagerClient client = DataContext as IFleetManagerClient;

            bool success;
            client.TryRemoveVehicle(ipAddressControl.IPAddress, out success);

            if (success)
            {
                MessageBox.Show("Vehicle removal was successful", "Vehicle Removal", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Vehicle removal failed", "Vehicle Removal", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void setPoseButton_Click(object sender, RoutedEventArgs e)
        {
            IFleetManagerClient client = DataContext as IFleetManagerClient;

            bool success;
            client.TrySetPose(ipAddressControl.IPAddress, poseControl.Pose, out success);

            if (success)
            {
                MessageBox.Show("Pose setting was successful", "Set Pose", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Pose setting failed", "Set Pose", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

		private void resetKingpinButton_Click(object sender, RoutedEventArgs e)
		{
			IFleetManagerClient client = DataContext as IFleetManagerClient;

			bool success;
			client.TryResetKingpin(ipAddressControl.IPAddress, out success);

			if (success)
			{
				MessageBox.Show("Kingpin reset was successful", "Reset Kingpin", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			else
			{
				MessageBox.Show("Kingpin reset failed", "Reset Kingpin", MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}
	}
}