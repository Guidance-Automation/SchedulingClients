using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SchedulingClients.JobStateServiceReference;
using SchedulingClients.Client_Interfaces;
using System;
using SchedulingClients.JobBuilderServiceReference;

namespace SchedulingClients.Tutorials
{
    [TestFixture]
    public partial class Examples
    {
        /// <summary>
        /// This assumes that a job is in progress to pick from one node and drop at another. 
        ///     Pick Node is node 56
        ///     Drop Node is node 78
        ///     And that the assigned agent is 9.
        /// 
        ///     Now we want to edit this job to change the drop destination
        ///     New destination is node 15
        /// 
        /// </summary>
        [Test]
        public void ChangeDestination()
        {
            Mock<IJobBuilderClient> moqJobBuilder = new Mock<IJobBuilderClient>();
            IJobBuilderClient jobBuilder = moqJobBuilder.Object;

            Mock<IJobsStateClient> moqJobsStateClient = new Mock<IJobsStateClient>();
            bool abortSuccessMoq = true;
            moqJobsStateClient.Setup(e => e.TryAbortTask(9, out abortSuccessMoq));

            IJobsStateClient jobsState = moqJobsStateClient.Object;

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
                        NodeTaskSummaryData = new NodeTaskSummaryData() {NodeId = -1 } // -1 to indciate invalid for a list task
                    },
                    new TaskSummaryData() // Pick task
                    {
                        TaskId = 8,
                        ParentTaskId = 7,
                        TaskStatus = TaskStatus.Completed,
                        TaskType = TaskType.ServiceAtNode,
                        NodeTaskSummaryData = new NodeTaskSummaryData() {NodeId = 56 }
                    },
                    new TaskSummaryData() // Drop task
                    {
                        TaskId = 9,
                        ParentTaskId = 7,
                        TaskStatus = TaskStatus.InProgress,
                        TaskType = TaskType.ServiceAtNode,
                        NodeTaskSummaryData = new NodeTaskSummaryData() {NodeId = 78}
                    }
                }.ToArray()               
            };

            moqJobState.Setup(e => e.TryGetJobSummary(5, out jobSummaryMoq));
            IJobStateClient jobState = moqJobState.Object;
            
            //////////////////
            // Actual Calls //
            //////////////////

            if (jobBuilder.TryBeginEditingJob(5).IsSuccessfull)
            {

                JobSummaryData jobSummary;
                jobState.TryGetJobSummary(5, out jobSummary);

                // From the job summary we can deduce that we want to abort task 9 and add another drop task. 

                bool abortSuccess;
                jobsState.TryAbortTask(9, out abortSuccess);

                if (abortSuccess)
                {
                    // The drop had not been performed, so we succesfully abort

                    // We can edit the job -- add a Execution Task
                    int serviceTaskId;
                    jobBuilder.TryCreateServicingTask(jobSummary.RootOrderedListTaskId, 15, ServiceType.Execution, out serviceTaskId);

                    // Add a directive
                    jobBuilder.TryIssueDirective(serviceTaskId, '2', (byte)11); // Assume parameter id '2' value 11 is drop

                    // Finish editing
                    jobBuilder.TryFinishEditingJob(jobSummary.JobId);
                }
                else
                {
                    // Too late! Passed the point of no return. 
                    throw new InvalidOperationException("Job passed point of no return - change destination not possible!");
                }
            }
        }
    }
}
