using System;
using SchedulingClients.JobStateServiceReference;
using System.ServiceModel;
using GAClients;

namespace SchedulingClients
{
    internal class JobStateClient : AbstractCallbackClient<IJobStateService>, IJobStateClient
    {
        private JobStateServiceCallback callback = new JobStateServiceCallback();

        private TimeSpan heartbeat;

        private bool isDisposed = false;

        /// <summary>
        /// Creates a JobStateClient
        /// </summary>
        /// <param name="netTcpUri">net.tcp address of the job state service</param>
        public JobStateClient(Uri netTcpUri, TimeSpan hearbeat = default(TimeSpan))
            : base(netTcpUri)
        {
            this.heartbeat = heartbeat < TimeSpan.FromMilliseconds(1000) ? TimeSpan.FromMilliseconds(1000) : heartbeat;
        }

        public event Action<JobProgressData> JobProgressUpdated
        {
            add { callback.JobProgressUpdated += value; }
            remove { callback.JobProgressUpdated -= value; }
        }

        /// <summary>
        /// Heartbeat time
        /// </summary>
        public TimeSpan Heartbeat { get { return heartbeat; } }

        /// <summary>
        /// Gets the state of a specific job
        /// </summary>
        /// <param name="jobId">Job id</param>
        /// <param name="jobState">State job is in</param>
        /// <returns>ServiceOperationResult</returns>
        public ServiceOperationResult TryGetJobSummary(int jobId, out JobSummaryData jobSummaryData)
        {
            Logger.Info("TryGetJobState()");

            try
            {
                var result = GetJobSummary(jobId);
                jobSummaryData = result.Item1;
                return ServiceOperationResultFactory.FromJobStateServiceCallData(result.Item2);
            }
            catch (Exception ex)
            {
                jobSummaryData = null;
                return HandleClientException(ex);
            }
        }

        /// <summary>
        /// Gets the summary of a specific job form the id one of its child tasks
        /// </summary>
        /// <param name="taskId">Task id</param>
        /// <param name="jobSummaryData">Job summary</param>
        /// <returns>ServiceOperationResult</returns>
        public ServiceOperationResult TryGetParentJobSummaryFromTaskId(int taskId, out JobSummaryData jobSummaryData)
        {
            Logger.Info("TryGetJobState()");

            try
            {
                var result = GetParentJobSummaryFromTaskId(taskId);
                jobSummaryData = result.Item1;
                return ServiceOperationResultFactory.FromJobStateServiceCallData(result.Item2);
            }
            catch (Exception ex)
            {
                jobSummaryData = null;
                return HandleClientException(ex);
            }
        }

        /// <summary>
        /// Gets the summary of the curent job for an agent
        /// </summary>
        /// <param name="agentId">Agent id</param>
        /// <param name="jobSummaryData">Job summary</param>
        /// <returns>ServiceOperationResult</return
        public ServiceOperationResult TryGetCurrentJobSummaryForAgentId(int agentId, out JobSummaryData jobSummaryData)
        {
            Logger.Info("TryGetCurrentJobSummaryForAgentId()");

            try
            {
                var result = GetCurrentJobSummaryForAgentId(agentId);
                jobSummaryData = result.Item1;
                return ServiceOperationResultFactory.FromJobStateServiceCallData(result.Item2);
            }
            catch (Exception ex)
            {
                jobSummaryData = null;
                return HandleClientException(ex);
            }
        }


        private Tuple<JobSummaryData, ServiceCallData> GetCurrentJobSummaryForAgentId(int agentId)
        {
            Logger.Debug("GetCurrentJobSummaryForAgentId()");

            if (isDisposed)
            {
                throw new ObjectDisposedException("JobStateClient");
            }
            Tuple<JobSummaryData, ServiceCallData> result;

            using (ChannelFactory<IJobStateService> channelFactory = CreateChannelFactory())
            {
                IJobStateService channel = channelFactory.CreateChannel();
                result = channel.GetCurrentJobSummaryForAgentId(agentId);
                channelFactory.Close();
            }

            return result;
        }


        protected override void Dispose(bool isDisposing)
        {
            if (isDisposed)
            {
                return;
            }

            isDisposed = true;

            base.Dispose(isDisposing);
        }

        protected override void HeartbeatThread()
        {
            Logger.Debug("HeartbeatThread()");

            ChannelFactory<IJobStateService> channelFactory = CreateChannelFactory();
            IJobStateService servicingService = channelFactory.CreateChannel();

            bool? exceptionCaught;

            while (!Terminate)
            {
                exceptionCaught = null;

                try
                {
                    Logger.Trace("SubscriptionHeartbeat({0})", Key);
                    servicingService.SubscriptionHeartbeat(Key);
                    IsConnected = true;
                    exceptionCaught = false;
                }
                catch (EndpointNotFoundException)
                {
                    Logger.Warn("HeartbeatThread - EndpointNotFoundException. Is the server running?");
                    exceptionCaught = true;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    exceptionCaught = true;
                }

                if (exceptionCaught == true)
                {
                    channelFactory.Abort();
                    IsConnected = false;

                    channelFactory = CreateChannelFactory(); // Create a new channel as this one is dead
                    servicingService = channelFactory.CreateChannel();
                }

                heartbeatReset.WaitOne(Heartbeat);
            }

            Logger.Debug("HeartbeatThread exit");
        }

        protected override void SetInstanceContext()
        {
            this.context = new InstanceContext(this.callback);
        }

        private Tuple<JobSummaryData, ServiceCallData> GetJobSummary(int jobId)
        {
            Logger.Debug("GetJobSummary()");

            if (isDisposed)
            {
                throw new ObjectDisposedException("JobStateClient");
            }
            Tuple<JobSummaryData, ServiceCallData> result;

            using (ChannelFactory<IJobStateService> channelFactory = CreateChannelFactory())
            {
                IJobStateService channel = channelFactory.CreateChannel();
                result = channel.GetJobSummary(jobId);
                channelFactory.Close();
            }

            return result;
        }

        private Tuple<JobSummaryData, ServiceCallData> GetParentJobSummaryFromTaskId(int taskId)
        {
            Logger.Debug("GetParentJobSummaryFromTaskId()");

            if (isDisposed)
            {
                throw new ObjectDisposedException("JobStateClient");
            }

            Tuple<JobSummaryData, ServiceCallData> result;

            using (ChannelFactory<IJobStateService> channelFactory = CreateChannelFactory())
            {
                IJobStateService channel = channelFactory.CreateChannel();
                result = channel.GetParentJobSummaryFromTaskId(taskId);
                channelFactory.Close();
            }

            return result;
        }
    }
}