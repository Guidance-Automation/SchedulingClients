using System;
using System.Windows;
using System.Windows.Controls;
using SchedulingClients.JobStateServiceReference;
using System.Collections.ObjectModel;
using SchedulingClients.TaskStateServiceReference;

namespace SchedulingClients.UserControls
{
    /// <summary>
    /// Interaction logic for JobStateClientControl.xaml
    /// </summary>
    public partial class JobStateClientControl : UserControl
    {
        private ObservableCollection<JobProgressData> recentData = new ObservableCollection<JobProgressData>();

        public JobStateClientControl()
        {
            InitializeComponent();
            recentTaskProgressUpdatesDataGrid.DataContext = recentData;
        }

        private void Client_JobProgressUpdated(JobProgressData jobProgressData)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                recentData.Add(jobProgressData);
            }));
        }

        private void getJobStateButton_Click(object sender, RoutedEventArgs e)
        {
#warning NEEDS REFACTOR
            throw new NotImplementedException();
            /*
            JobStateData jobStateData;

            IJobStateClient jobStateClient = DataContext as IJobStateClient;
            if (jobStateClient.TryGetJobState((int)getJobStateId.Value, out jobStateData) == true)
            {
                jobStateDataControl.DataContext = jobStateData;
            }*/
        }

        private void getJobStateFromTaskIdButton_Click(object sender, RoutedEventArgs e)
        {
#warning NEEDS REFACTOR

            throw new NotImplementedException();
            /*
            JobStateData jobStateData;

            IJobStateClient jobStateClient = DataContext as IJobStateClient;
            if (jobStateClient.TryGetParentJobStateFromTaskId((int)getJobStateFromTaskId.Value, out jobStateData) == true)
            {
                jobStateDataControl.DataContext = jobStateData;
            }*/
        }

        private void UserControl_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is IJobStateClient)
            {
                IJobStateClient client = e.NewValue as IJobStateClient;
                client.JobProgressUpdated += Client_JobProgressUpdated;
            }
        }
    }
}