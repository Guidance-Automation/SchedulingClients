using NUnit.Framework;
using Moq;
using SchedulingClients.JobStateServiceReference;
using System.Threading;
using System.Net;

namespace SchedulingClients.Tutorials
{
    [TestFixture]
    public partial class Examples
    {
        /// <summary>
        /// This describes how the JobProgress event is fired
        /// 
        ///     Everytime the job changes state a progress event will be fired.
        ///     
        /// Includes additional demonstration:
        ///     
        ///     Usage of the progress data to receive a statecast parameter from the agent assigned the job
        ///     
        /// </summary>
        [Test]
        public void JobProgressData()
        {
            Mock<IJobStateClient> moqJobState = new Mock<IJobStateClient>();
            IJobStateClient jobStateClient = moqJobState.Object;

            Mock<IAgentStatecastClient> moqAgentStatecast = new Mock<IAgentStatecastClient>();
            IAgentStatecastClient agentStateCastClient = moqAgentStatecast.Object;

            //////////////////
            // Actual Calls //
            //////////////////

            JobProgressData receivedData = null;
            ManualResetEvent eventHandled = new ManualResetEvent(false);

            jobStateClient.JobProgressUpdated += delegate(JobProgressData jobProgressData)
            {
                receivedData = jobProgressData;
                eventHandled.Set();
            };

            TriggerJobProgress(moqJobState, JobStatus.Assigning); // To mimic a job being in the assigned state

            eventHandled.WaitOne(5000);
            Assert.IsNotNull(receivedData);
            Assert.AreEqual(JobStatus.Assigning, receivedData.JobStatus);


            TriggerJobProgress(moqJobState, JobStatus.Waiting, 9); // To mimic a job being in the waiting state

            eventHandled.WaitOne(5000);
            Assert.IsNotNull(receivedData);
            Assert.AreEqual(JobStatus.Waiting, receivedData.JobStatus);
            Assert.Greater(receivedData.AssignedAgentId, -1); // To be waiting we must have an assigned agent


            TriggerJobProgress(moqJobState, JobStatus.InProgress, 9); // To mimic a job being in the inProgress state

            eventHandled.WaitOne(5000);
            Assert.IsNotNull(receivedData);
            Assert.AreEqual(JobStatus.InProgress, receivedData.JobStatus);
            Assert.Greater(receivedData.AssignedAgentId, -1); // To be waiting we must have an assigned agent

            // We could now use this information to find out about a particular state cast item, for example:
            //
            //  Parameter "routerIPAddress"
            //  An IP address corresponding to that of the router on the AGV.            

            IPAddress routerIPAddress;
            agentStateCastClient.TryGetIPAddressStatecastValue(receivedData.AssignedAgentId, "routerIPAddress", out routerIPAddress);
        }

        private void TriggerJobProgress(Mock<IJobStateClient> moqJobState, JobStatus jobStatus, int assignedAgentId = -1)
        {
            moqJobState.Raise(e => e.JobProgressUpdated += null, new JobProgressData()
            {
                JobId = 6,
                JobStatus = jobStatus,
                AssignedAgentId = assignedAgentId
            });
        }
    }
}
