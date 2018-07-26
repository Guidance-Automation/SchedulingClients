using SchedulingClients.JobBuilderServiceReference;
using SchedulingClients.MapServiceReference;
using Sparrow.Chart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SchedulingClients.UserControls
{
    /// <summary>
    /// Interaction logic for Spammer.xaml
    /// </summary>
    public partial class Spammer : UserControl
    {
        private IMapClient mapClient = null;

        public Spammer()
        {
            InitializeComponent();
        }

        public void Configure(IMapClient mapClient)
        {
            DataContext = mapClient;
            this.mapClient = mapClient;
        }

        private void spamMapButton_Click(object sender, RoutedEventArgs e)
        {
            if (mapClient != null)
            {
                rttLineSeries.Points.Clear();

                Task.Factory.StartNew(() =>
                {
                    for (int i = 0; i < 100; i++)
                    {
                        IEnumerable<NodeData> mock;
                        DateTime before = DateTime.Now;
                        mapClient.TryGetAllNodeData(out mock);
                        TimeSpan rtt = DateTime.Now.Subtract(before);
                        Application.Current.Dispatcher.BeginInvoke((Action)(() =>
                   {
                            spamIndicator.IsChecked = !spamIndicator.IsChecked;
                            rttText.Text = rtt.Milliseconds.ToString();
                            rttLineSeries.Points.Add(new DoublePoint() { Data = i, Value = rtt.Milliseconds });
                        }));
                    }
                });
            }
            else
            {
                MessageBox.Show("Map client is null", "No Map Client", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void spamJobButton_Click(object sender, RoutedEventArgs e)
        {
            IJobBuilderClient jobBuilderClient = DataContext as IJobBuilderClient;

            if (mapClient != null)
            {
                rttLineSeries.Points.Clear();

                Task.Factory.StartNew(() =>
                {
                    Random random = new Random();
                    for (int i = 0; i < 100; i++)
                    {
                        DateTime before = DateTime.Now;
                        JobData jobData;
                        jobBuilderClient.TryCreateJob(out jobData);
                        IEnumerable<NodeData> nodeDataEnumerable;
                        mapClient.TryGetAllNodeData(out nodeDataEnumerable);
                        int sleepingTaskId1;
                        jobBuilderClient.TryCreateSleepingTask(jobData.RootOrderedListTaskId, nodeDataEnumerable.ElementAt(random.Next(nodeDataEnumerable.Count())).MapItemId, out sleepingTaskId1);
                        int sleepingTaskId2;
                        jobBuilderClient.TryCreateSleepingTask(jobData.RootOrderedListTaskId, nodeDataEnumerable.ElementAt(random.Next(nodeDataEnumerable.Count())).MapItemId, out sleepingTaskId2);
                        bool success;
                        jobBuilderClient.TryCommit(jobData.JobId, out success);
                        TimeSpan rtt = DateTime.Now.Subtract(before);
                        Application.Current.Dispatcher.BeginInvoke((Action)(() =>
                        {
                            if (!success || jobData.JobId < 0 || sleepingTaskId1 < 0 || sleepingTaskId2 < 0)
                            {
                                MessageBox.Show("Job creation failed");
                            }
                            else
                            {
                                spamIndicator.IsChecked = !spamIndicator.IsChecked;
                                rttText.Text = rtt.Milliseconds.ToString();
                                rttLineSeries.Points.Add(new DoublePoint() { Data = i, Value = rtt.Milliseconds });
                            }
                        }));
                    }
                });
            }
            else
            {
                MessageBox.Show("Map client is null", "No Map Client", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
