using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SchedulingClients.JobBuilderServiceReference;
using Moq;

namespace SchedulingClients.Tutorials
{
    [TestFixture]
    [Category("Tutorials")]
    public partial class Examples
    {
        /// <summary>
        /// This describes how to create a job to simply move any agent to a node
        ///     Move to node 6
        /// </summary>
        [Test]
        public void BasicMove()
        {
            // Setting up moq
            Mock<IJobBuilderClient> mockJobBuilder = new Mock<IJobBuilderClient>();

            JobData jobDataMoq = new JobData(){JobId = 3, RootOrderedListTaskId = 7 };
            mockJobBuilder.Setup(e => e.TryCreateJob(out jobDataMoq));

            IJobBuilderClient jobBuilderClient = mockJobBuilder.Object;
            
            //////////////////
            // Actual calls //
            //////////////////

            // First we create a job:
            JobData jobData;
            jobBuilderClient.TryCreateJob(out jobData);

            // JobData will contain
            // jobData.JobId -> Job identifier of the job we just created;
            // jobData.RootOrderedListTaskId -> This is the task Id of the root ordered list task.

            // Now we create the GoTo task 
            // Here we are adding it the the orderedListTask
            // Sending to node 6
            // With the ServiceType GoTo:

            int moveTaskId;
            jobBuilderClient.TryCreateMovingTask(jobData.RootOrderedListTaskId, 6, out moveTaskId);

            // Finally we commit the job
            bool success;
            jobBuilderClient.TryCommit(jobData.JobId, out success);
        }
    }
}
