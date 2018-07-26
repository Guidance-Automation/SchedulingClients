using NUnit.Framework;
using Moq;
using SchedulingClients.JobStateServiceReference;
using SchedulingClients.Client_Interfaces;
using SchedulingClients.JobBuilderServiceReference;
using System.Collections.Generic;

namespace SchedulingClients.Tutorials
{
    [TestFixture]
    public partial class Examples
    {
        /// <summary>
        /// This assumes the job in the AwaitJobWithDirectives example is already present
        /// 
        ///     Now we want to edit this further to make the agent drop the load at node 15
        ///    
        /// Assumes that kingpin can perform a directive where 
        ///     Parameter id 2 takes a byte value
        ///     A byte value of 11 corresponds to drop an item 
        /// </summary>
        [Test]
        public void EditAwaitJob()
        {
            Mock<IJobBuilderClient> moqJobBuilder = new Mock<IJobBuilderClient>();
            IJobBuilderClient jobBuilder = moqJobBuilder.Object;

            Mock<IJobStateClient> moqJobState = new Mock<IJobStateClient>();

            JobSummaryData jobSummaryMoq = new JobSummaryData()
            {
                JobId = 5,
                JobStatus = JobStatus.InProgress,
                RootOrderedListTaskId = 7,
                AssignedAgentId = 9,
                TaskSummaries = new List<TaskSummaryData>
                {
                    new TaskSummaryData()
                    {
                        TaskId = 7,
                        ParentTaskId = null,
                        TaskStatus = TaskStatus.InProgress,
                        TaskType = TaskType.OrderedList,
                        NodeTaskSummaryData = new NodeTaskSummaryData() {NodeId = -1 } // -1 to indicate invalid for a list task
                    },
                    new TaskSummaryData() // Pick task
                    {
                        TaskId = 8,
                        ParentTaskId = 7,
                        TaskStatus = TaskStatus.Completed,
                        TaskType = TaskType.ServiceAtNode,
                        NodeTaskSummaryData = new NodeTaskSummaryData() {NodeId = 6 }
                    },
                    new TaskSummaryData() // Drop task
                    {
                        TaskId = 9,
                        ParentTaskId = 7,
                        TaskStatus = TaskStatus.InProgress,
                        TaskType = TaskType.AwaitAtNode,
                        NodeTaskSummaryData = new NodeTaskSummaryData() {NodeId = 10}
                    }
                }.ToArray()
            };

            moqJobState.Setup(e => e.TryGetJobSummary(5, out jobSummaryMoq));
            IJobStateClient jobState = moqJobState.Object;

            //////////////////
            // Actual Calls //
            //////////////////

            JobSummaryData jobSummary;
            jobState.TryGetJobSummary(5, out jobSummary);

            // Determine from the job summary which part of the job you wish to edit.

            if (jobBuilder.TryBeginEditingJob(5).IsSuccessfull)
            {
                // We can edit the job -- add a service Task
                int serviceTaskId;
                jobBuilder.TryCreateServicingTask(jobSummary.RootOrderedListTaskId, 15, ServiceType.Execution, out serviceTaskId);

                // Add a directive
                jobBuilder.TryIssueDirective(serviceTaskId, '2', (byte)11); // Assume parameter id '2' vallue 11 is drop

                // Finish editing
                jobBuilder.TryFinishEditingJob(jobSummary.JobId);
            }
        }
    }
}
