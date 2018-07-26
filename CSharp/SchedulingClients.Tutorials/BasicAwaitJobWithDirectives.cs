using Moq;
using NUnit.Framework;
using SchedulingClients.JobBuilderServiceReference;
using SchedulingClients.Client_Interfaces;

namespace SchedulingClients.Tutorials
{
    [TestFixture]
    public partial class Examples
    {
        /// <summary>
        /// This describes how to send any agent:
        /// 
        ///     Receive an item at node 6 and then await for tasks at node 10
        /// 
        ///     This breaks down into three main calls:
        /// 
        ///     Service at node 6 with service type execute
        ///     Use a directive at node 6
        ///     Then drive towards node 10 and await for tasks
        ///     
        /// Assumes that kingpin can perform a directive where 
        ///     Parameter id 2 takes a byte value
        ///     A byte value of 10 corresponds to receive an item 
        /// </summary>
        [Test]
        public void AwaitJobWithDirectives()
        {
            Mock<IJobBuilderClient> moqJobBuilder = new Mock<IJobBuilderClient>();

            JobData jobDataMoq = new JobData() { JobId = 2, RootOrderedListTaskId = 3 };
            moqJobBuilder.Setup(e => e.TryCreateJob(out jobDataMoq));

            IJobBuilderClient jobBuilder = moqJobBuilder.Object;


            //////////////////
            // Actual Calls //
            //////////////////

            // First we create a job
            JobData jobData;
            jobBuilder.TryCreateJob(out jobData);

            // Create a node task to go to the target node
            int node6TaskId;
            jobBuilder.TryCreateServicingTask(jobData.RootOrderedListTaskId, 6, ServiceType.Execution, out node6TaskId);
         
            // Issue a directive to this task, parameter 2, with value 10 'receive item'
            jobBuilder.TryIssueDirective(node6TaskId, '2', (byte)10);

            // Create an await task
            int node10TaskId;
            jobBuilder.TryCreateAwaitingTask(jobData.RootOrderedListTaskId, 10, out node10TaskId);

            // Finally commit the job
            bool success;
            jobBuilder.TryCommit(jobData.JobId, out success);

        }
    }
}
