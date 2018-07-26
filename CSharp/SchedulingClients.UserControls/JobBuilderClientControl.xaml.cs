using SchedulingClients.JobBuilderServiceReference;
using SchedulingClients.MapServiceReference;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SchedulingClients.UserControls
{
    public partial class JobBuilderClientControl : UserControl
    {
        private IMapClient mapClient = null;

        public JobBuilderClientControl()
        {
            InitializeComponent();
        }

        public void Configure(IMapClient client)
        {
            mapClient = client;
        }

        private void createJobButton_Click(object sender, RoutedEventArgs e)
        {
            IJobBuilderClient client = DataContext as IJobBuilderClient;
            JobData jobData;
            client.TryCreateJob(out jobData);
        }

        private void multiPickJobTest_Click(object sender, RoutedEventArgs e)
        {
            IJobBuilderClient client = DataContext as IJobBuilderClient;

            if (mapClient != null)
            {
                IEnumerable<NodeData> nodeData;
                if (mapClient.TryGetAllNodeData(out nodeData) == true)
                {
                    client.MultiPickJobTest(nodeData);
                }
                else
                {
                    MessageBox.Show("Failed to get node data from map client");
                }
            }
            else
            {
                MessageBox.Show("Map client is null");
            }
        }

        private void directiveTest_Click(object sender, RoutedEventArgs e)
        {
            IJobBuilderClient client = DataContext as IJobBuilderClient;

#warning Unsafe
            client.TryIssueDirective(taskIdUpDown.Value ?? 0, parameterIdUpDown.Value ?? 0, (byte)(valueUpDown.Value ?? 0));
        }

        private void moveJobButton_Click(object sender, RoutedEventArgs e)
        {
            IJobBuilderClient client = DataContext as IJobBuilderClient;

            if (mapClient != null)
            {
                IEnumerable<NodeData> nodeData;
                if (mapClient.TryGetAllNodeData(out nodeData) == true)
                {
                    client.MoveJobTest(nodeData, finalisedPipelineCheck.IsChecked ?? true);
                }
            }
        }

		private void beginEditingJobButton_Click(object sender, RoutedEventArgs e)
		{
            IJobBuilderClient client = DataContext as IJobBuilderClient;

			client.TryBeginEditingJob(editJobIdUpDown.Value ?? 0);
		}

		private void finishEditingJobButton_Click(object sender, RoutedEventArgs e)
		{
            IJobBuilderClient client = DataContext as IJobBuilderClient;

			client.TryFinishEditingJob(editJobIdUpDown.Value ?? 0);
		}

		private void finaliseButton_Click(object sender, RoutedEventArgs e)
		{
            IJobBuilderClient client = DataContext as IJobBuilderClient;

			client.TryFinaliseTask(taskToFinaliseUpDown.Value ?? 0);
		}

		private void beginEditingTaskButton_Click(object sender, RoutedEventArgs e)
		{
            IJobBuilderClient client = DataContext as IJobBuilderClient;

			client.TryBeginEditingTask(editTaskIdUpDown.Value ?? 0);
		}

		private void finishEditingTaskButton_Click(object sender, RoutedEventArgs e)
		{
            IJobBuilderClient client = DataContext as IJobBuilderClient;

			client.TryFinishEditingTask(editTaskIdUpDown.Value ?? 0);
		}

		private void nonFinalisedTaskButton_Click(object sender, RoutedEventArgs e)
		{
            IJobBuilderClient client = DataContext as IJobBuilderClient;

			int newTaskId;

			client.TryCreateOrderedListTask(parentTaskIdUpDown.Value ?? 0, false, out newTaskId);
		}

		private void addMoveTaskToParentButton_Click(object sender, RoutedEventArgs e)
		{
            IJobBuilderClient client = DataContext as IJobBuilderClient;

			int newMoveId;

			client.TryCreateMovingTask(addNodeTaskParentUpDown.Value ?? 0, addNodeTaskIdUpDown.Value ?? 0, out newMoveId);
		}

        private void addAwaitTaskToParentButton_Click(object sender, RoutedEventArgs e)
        {
            IJobBuilderClient client = DataContext as IJobBuilderClient;

            int newMoveId;

            client.TryCreateAwaitingTask(addNodeTaskParentUpDown.Value ?? 0, addNodeTaskIdUpDown.Value ?? 0, out newMoveId);
        }

        private void commitJobButton_Click(object sender, RoutedEventArgs e)
        {
            IJobBuilderClient client = DataContext as IJobBuilderClient;

            bool success;
            client.TryCommit(jobIdUpDown.Value ?? 1, out success);
        }
    }
}