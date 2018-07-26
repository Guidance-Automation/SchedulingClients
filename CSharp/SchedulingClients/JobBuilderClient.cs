using SchedulingClients.JobBuilderServiceReference;
using System;
using System.Net;
using GAClients;
using System.ServiceModel;

namespace SchedulingClients
{
    internal class JobBuilderClient : AbstractClient<IJobBuilderService>, IJobBuilderClient
    {
        private bool isDisposed = false;

        /// <summary>
        /// Creates a new JobBuilderClient
        /// </summary>
        /// <param name="netTcpUri">net.tcp address of the job builder service</param>
        public JobBuilderClient(Uri netTcpUri)
            : base(netTcpUri)
        {
        }

        /// <summary>
        /// Commits a job
        /// </summary>
        /// <param name="jobId">Id of the job to be committed</param>
        /// <param name="success">True if succesfull</param>
        /// <param name="agentId">
        /// Agent to assign the job to. If default scheduler will handle assignment
        /// </param>
        /// <returns>ServiceOperationResult</returns>
        public ServiceOperationResult TryCommit(int jobId, out bool success, int agentId = -1)
        {
            Logger.Info("TryCommit({0},{1})", jobId, agentId);

            try
            {
                var result = Commit(jobId, agentId);
                success = result.Item1;
                return ServiceOperationResultFactory.FromJobBuilderServiceCallData(result.Item2);
            }
            catch (Exception ex)
            {
                success = false;
                return HandleClientException(ex);
            }
        }

        /// <summary>
        /// Creates a new job
        /// </summary>
        /// <param name="jobData">Job data of new job instance</param>
        /// <returns>ServiceOperationResult</returns>
        public ServiceOperationResult TryCreateJob(out JobData jobData)
        {
            Logger.Info("TryCreateJob()");

            try
            {
                var result = CreateJob();
                jobData = result.Item1;
                return ServiceOperationResultFactory.FromJobBuilderServiceCallData(result.Item2);
            }
            catch (Exception ex)
            {
                jobData = new JobData() { JobId = -1 };
                return HandleClientException(ex);
            }
        }

        /// <summary>
        /// Creates a new ordered list task
        /// </summary>
        /// <param name="parentTaskId">Parent list task Id</param>
        /// <param name="isFinalised">Whether or not the task is finalised</param>
        /// <param name="listTaskId">Id of new list task</param>
        /// <returns>ServiceOperationResult</returns>
        public ServiceOperationResult TryCreateOrderedListTask(int parentTaskId, bool isFinalised, out int listTaskId)
        {
            Logger.Info("TryCreateOrderedListTask({0})", parentTaskId);

            try
            {
                var result = CreateOrderedListTask(parentTaskId, isFinalised);
                listTaskId = result.Item1;
                return ServiceOperationResultFactory.FromJobBuilderServiceCallData(result.Item2);
            }
            catch (Exception ex)
            {
                listTaskId = -1;
                return HandleClientException(ex);
            }
        }

		/// <summary>
		/// Creates a new unordered list task
		/// </summary>
		/// <param name="parentTaskId">Parent list task Id</param>
		/// <param name="listTaskId">Id of new list task</param>
		/// <returns>ServiceOperationResult</returns>
		public ServiceOperationResult TryCreateUnorderedListTask(int parentTaskId, out int listTaskId)
		{
			Logger.Info("TryCreateUnorderedListTask({0})", parentTaskId);

			try
			{
				var result = CreateUnorderedListTask(parentTaskId);
				listTaskId = result.Item1;
				return ServiceOperationResultFactory.FromJobBuilderServiceCallData(result.Item2);
			}
			catch (Exception ex)
			{
				listTaskId = -1;
				return HandleClientException(ex);
			}
		}

		/// <summary>
		/// Creates a new pipelinedtask
		/// </summary>
		/// <param name="parentTaskId">Parent list task Id</param>
		/// <param name="listTaskId">Id of new list task</param>
		/// <returns>ServiceOperationResult</returns>
		public ServiceOperationResult TryCreatePipelinedTask(int parentTaskId, bool isFinalised, out int listTaskId)
		{
			Logger.Info("TryCreatePipelinedTask({0})", parentTaskId);

			try
			{
				var result = CreatePipelinedTask(parentTaskId, isFinalised);
				listTaskId = result.Item1;
				return ServiceOperationResultFactory.FromJobBuilderServiceCallData(result.Item2);
			}
			catch (Exception ex)
			{
				listTaskId = -1;
				return HandleClientException(ex);
			}
		}

		/// <summary>
		/// Creates a new servicing task
		/// </summary>
		/// <param name="parentListTaskId">Id of parent list task</param>
		/// <param name="nodeId">Id of node for service to be carried out</param>
		/// <param name="serviceType">Service type to be carried out</param>
		/// <param name="serviceTaskId">Id of newly created servicing task</param>
		/// <param name="expectedDuration">Expected duration of the task</param>
		/// <returns>ServiceOperationResult</returns>
		public ServiceOperationResult TryCreateServicingTask(int parentListTaskId, int nodeId, ServiceType serviceType, out int serviceTaskId, TimeSpan expectedDuration = default(TimeSpan))
        {
            Logger.Info("TryCreateNodeTask({0},{1})", parentListTaskId, nodeId);

            try
            {
                var result = CreateServicingTask(parentListTaskId, nodeId, serviceType, expectedDuration);
                serviceTaskId = result.Item1;
                return ServiceOperationResultFactory.FromJobBuilderServiceCallData(result.Item2);
            }
            catch (Exception ex)
            {
                serviceTaskId = -1;
                return HandleClientException(ex);
            }
        }

        /// <summary>
        /// Creates a new sleeping task
        /// </summary>
        /// <param name="parentListTaskId">Id of parent list task</param>
        /// <param name="nodeId">Id of node for service to be carried out</param>
        /// <param name="sleepingTaskId">Id of newly created sleeping task</param>
        /// <param name="expectedDuration">Expected duration of the task</param>
        /// <returns>ServiceOperationResult</returns>
        public ServiceOperationResult TryCreateSleepingTask(int parentListTaskId, int nodeId, out int sleepingTaskId, TimeSpan expectedDuration = default(TimeSpan))
        {
            Logger.Info("TryCreateNodeTask({0},{1})", parentListTaskId, nodeId);

            if (expectedDuration == TimeSpan.Zero)
            {
                expectedDuration = TimeSpan.FromSeconds(1);
            }

            try
            {
                var result = CreateSleepingTask(parentListTaskId, nodeId, expectedDuration);
                sleepingTaskId = result.Item1;
                return ServiceOperationResultFactory.FromJobBuilderServiceCallData(result.Item2);
            }
            catch (Exception ex)
            {
                sleepingTaskId = -1;
                return HandleClientException(ex);
            }
        }


        /// <summary>
        /// Creates a new await task
        /// </summary>
        /// <param name="parentListTaskId">Id of parent list task</param>
        /// <param name="nodeId">Id of node for await to be carried out</param>
        /// <param name="awaitTaskId">Id of newly created await task</param>
        /// <returns>ServiceOperationResult</returns>
        public ServiceOperationResult TryCreateAwaitingTask(int parentListTaskId, int nodeId, out int awaitTaskId)
        {
            Logger.Info("TryCreateAwaitTask({0},{1})", parentListTaskId, nodeId);

            try
            {
                var result = CreateAwaitingTask(parentListTaskId, nodeId);
                awaitTaskId = result.Item1;
                return ServiceOperationResultFactory.FromJobBuilderServiceCallData(result.Item2);
            }
            catch (Exception ex)
            {
                awaitTaskId = -1;
                return HandleClientException(ex);
            }
        }

        /// <summary>
        /// Creates a task to move an agent to a node
        /// </summary>
        /// <param name="parentListTaskId">Id of parent list task</param>
        /// <param name="nodeId">Id of node to move agent to</param>
        /// <param name="moveTaskId">Id of newly created move  task</param>
        /// <returns>ServiceOperationResult</returns>
        public ServiceOperationResult TryCreateMovingTask(int parentListTaskId, int nodeId, out int moveTaskId)
        {
            Logger.Info("TryCreateMoveTask({0},{1})", parentListTaskId, nodeId);

            try
            {
                var result = CreateMovingTask(parentListTaskId, nodeId);
                moveTaskId = result.Item1;
                return ServiceOperationResultFactory.FromJobBuilderServiceCallData(result.Item2);
            }
            catch (Exception ex)
            {
                moveTaskId = -1;
                return HandleClientException(ex);
            }
        }

        /// <summary>
        /// Issues a new directive
        /// </summary>
        /// <param name="taskId">Id of task directive is for</param>
        /// <param name="parameterId">Id of parameter directive is for</param>
        /// <param name="value">Value assigned to directive</param>
        /// <returns>ServiceOperationResult</returns>
        public ServiceOperationResult TryIssueDirective(int taskId, int parameterId, byte value)
        {
            Logger.Info("TryIssueDirective({0},{1},{2})", taskId, parameterId, value);

            try
            {
                var result = IssueDirective(taskId, parameterId, value);
                return ServiceOperationResultFactory.FromJobBuilderServiceCallData(result.Item2);
            }
            catch (Exception ex)
            {
                return HandleClientException(ex);
            }
        }

        /// <summary>
        /// Issues a new directive
        /// </summary>
        /// <param name="taskId">Id of task directive is for</param>
        /// <param name="parameterId">Id of parameter directive is for</param>
        /// <param name="value">Value assigned to directive</param>
        /// <returns>ServiceOperationResult</returns>
        public ServiceOperationResult TryIssueDirective(int taskId, int parameterId, short value)
        {
            Logger.Info("TryIssueDirective({0},{1},{2})", taskId, parameterId, value);

            try
            {
                var result = IssueDirective(taskId, parameterId, value);
                return ServiceOperationResultFactory.FromJobBuilderServiceCallData(result.Item2);
            }
            catch (Exception ex)
            {
                return HandleClientException(ex);
            }
        }

        /// <summary>
        /// Issues a new directive
        /// </summary>
        /// <param name="taskId">Id of task directive is for</param>
        /// <param name="parameterId">Id of parameter directive is for</param>
        /// <param name="value">Value assigned to directive</param>
        /// <returns>ServiceOperationResult</returns>
        public ServiceOperationResult TryIssueDirective(int taskId, int parameterId, ushort value)
        {
            Logger.Info("TryIssueDirective({0},{1},{2})", taskId, parameterId, value);

            try
            {
                var result = IssueDirective(taskId, parameterId, value);
                return ServiceOperationResultFactory.FromJobBuilderServiceCallData(result.Item2);
            }
            catch (Exception ex)
            {
                return HandleClientException(ex);
            }
        }

        /// <summary>
        /// Issues a new directive
        /// </summary>
        /// <param name="taskId">Id of task directive is for</param>
        /// <param name="parameterId">Id of parameter directive is for</param>
        /// <param name="value">Value assigned to directive</param>
        /// <returns>ServiceOperationResult</returns>
        public ServiceOperationResult TryIssueDirective(int taskId, int parameterId, float value)
        {
            Logger.Info("TryIssueDirective({0},{1},{2})", taskId, parameterId, value);

            try
            {
                var result = IssueDirective(taskId, parameterId, value);
                return ServiceOperationResultFactory.FromJobBuilderServiceCallData(result.Item2);
            }
            catch (Exception ex)
            {
                return HandleClientException(ex);
            }
        }

        /// <summary>
        /// Issues a new directive
        /// </summary>
        /// <param name="taskId">Id of task directive is for</param>
        /// <param name="parameterId">Id of parameter directive is for</param>
        /// <param name="value">Value assigned to directive</param>
        /// <returns>ServiceOperationResult</returns>
        public ServiceOperationResult TryIssueDirective(int taskId, int parameterId, IPAddress value)
        {
            Logger.Info("TryIssueDirective({0},{1},{2})", taskId, parameterId, value);

            try
            {
                var result = IssueDirective(taskId, parameterId, value);
                return ServiceOperationResultFactory.FromJobBuilderServiceCallData(result.Item2);
            }
            catch (Exception ex)
            {
                return HandleClientException(ex);
            }
        }

		/// <summary>
		/// Finalises an un-finalised ordered list task
		/// </summary>
		/// <param name="taskId">Ordered list task Id</param>
		/// <returns>ServiceOperationResult</returns>
		public ServiceOperationResult TryFinaliseTask(int taskId)
		{
			Logger.Info("TryFinaliseTask({0})", taskId);

			try
			{
				var result = FinaliseTask(taskId);
				return ServiceOperationResultFactory.FromJobBuilderServiceCallData(result);
			}
			catch (Exception ex)
			{
				return HandleClientException(ex);
			}
		}

		/// <summary>
		/// Puts a job into the editing state
		/// </summary>
		/// <param name="jobId">Id of the job to edit</param>
		/// <returns>ServiceOperationResult</returns>
		public ServiceOperationResult TryBeginEditingJob(int jobId)
		{
			Logger.Info("TryFinaliseTask({0})", jobId);

			try
			{
				var result = BeginEditingJob(jobId);
				return ServiceOperationResultFactory.FromJobBuilderServiceCallData(result);
			}
			catch (Exception ex)
			{
				return HandleClientException(ex);
			}
		}

		/// <summary>
		/// Puts a job in the editing state back to in progress
		/// </summary>
		/// <param name="jobId">Id of the job to finish editing</param>
		/// <returns>ServiceOperationResult</returns>
		public ServiceOperationResult TryFinishEditingJob(int jobId)
		{
			Logger.Info("TryFinaliseTask({0})", jobId);

			try
			{
				var result = FinishEditingJob(jobId);
				return ServiceOperationResultFactory.FromJobBuilderServiceCallData(result);
			}
			catch (Exception ex)
			{
				return HandleClientException(ex);
			}
		}

		/// <summary>
		/// Puts a job into the editing state
		/// </summary>
		/// <param name="taskId">Id of the task to edit</param>
		/// <returns>ServiceOperationResult</returns>
		public ServiceOperationResult TryBeginEditingTask(int taskId)
		{
			Logger.Info("TryFinaliseTask({0})", taskId);

			try
			{
				var result = BeginEditingTask(taskId);
				return ServiceOperationResultFactory.FromJobBuilderServiceCallData(result);
			}
			catch (Exception ex)
			{
				return HandleClientException(ex);
			}
		}

		/// <summary>
		/// Puts a job in the editing state back to its previous state
		/// </summary>
		/// <param name="taskId">Id of the task to finish editing</param>
		/// <returns>ServiceOperationResult</returns>
		public ServiceOperationResult TryFinishEditingTask(int taskId)
		{
			Logger.Info("TryFinaliseTask({0})", taskId);

			try
			{
				var result = FinishEditingTask(taskId);
				return ServiceOperationResultFactory.FromJobBuilderServiceCallData(result);
			}
			catch (Exception ex)
			{
				return HandleClientException(ex);
			}
		}

        private Tuple<bool, ServiceCallData> Commit(int jobId, int agentId = -1)
        {
            Logger.Debug("Commit({0},{1})", jobId, agentId);

            if (isDisposed)
            {
                throw new ObjectDisposedException("JobBuilderClient");
            }

            Tuple<bool, ServiceCallData> result;

            using (ChannelFactory<IJobBuilderService> channelFactory = CreateChannelFactory())
            {
                IJobBuilderService channel = channelFactory.CreateChannel();
                result = channel.CommitJob(jobId, agentId);
                channelFactory.Close();
            }

            return result;
        }

        private Tuple<JobData, ServiceCallData> CreateJob()
        {
            Logger.Debug("CreateJob()");

            if (isDisposed)
            {
                throw new ObjectDisposedException("JobBuilderClient");
            }

            Tuple<JobData, ServiceCallData> result;

            using (ChannelFactory<IJobBuilderService> channelFactory = CreateChannelFactory())
            {
                IJobBuilderService channel = channelFactory.CreateChannel();
                result = channel.CreateJob();
                channelFactory.Close();
            }

            return result;
        }

        private Tuple<int, ServiceCallData> CreateUnorderedListTask(int parentTaskId)
        {
            Logger.Debug("CreateListTask({0})", parentTaskId);

            if (isDisposed)
            {
                throw new ObjectDisposedException("JobBuilderClient");
            }

            Tuple<int, ServiceCallData> result;

            using (ChannelFactory<IJobBuilderService> channelFactory = CreateChannelFactory())
            {
                IJobBuilderService channel = channelFactory.CreateChannel();
                result = channel.CreateUnorderedListTask(parentTaskId);
                channelFactory.Close();
            }

            return result;
        }

		private Tuple<int, ServiceCallData> CreateOrderedListTask(int parentTaskId, bool isFinalised)
		{
			Logger.Debug("CreateOrderedListTask({0})", parentTaskId);

			if (isDisposed)
			{
				throw new ObjectDisposedException("JobBuilderClient");
			}

			Tuple<int, ServiceCallData> result;

			using (ChannelFactory<IJobBuilderService> channelFactory = CreateChannelFactory())
			{
				IJobBuilderService channel = channelFactory.CreateChannel();
				result = channel.CreateOrderedListTask(parentTaskId, isFinalised);
				channelFactory.Close();
			}

			return result;
		}

		private Tuple<int, ServiceCallData> CreatePipelinedTask(int parentTaskId, bool isFinalised)
		{
			Logger.Debug("CreateListTask({0})", parentTaskId);

			if (isDisposed)
			{
				throw new ObjectDisposedException("JobBuilderClient");
			}

			Tuple<int, ServiceCallData> result;

			using (ChannelFactory<IJobBuilderService> channelFactory = CreateChannelFactory())
			{
				IJobBuilderService channel = channelFactory.CreateChannel();
				result = channel.CreatePipelinedTask(parentTaskId, isFinalised);
				channelFactory.Close();
			}

			return result;
		}

		private Tuple<int, ServiceCallData> CreateServicingTask(int parentTaskId, int nodeId, ServiceType serviceType, TimeSpan expectedDuration)
        {
            Logger.Debug("CreateServicingTask({0},{1},{2},{3})", parentTaskId, nodeId, serviceType, expectedDuration);

            if (isDisposed)
            {
                throw new ObjectDisposedException("JobBuilderClient");
            }

            Tuple<int, ServiceCallData> result;

            using (ChannelFactory<IJobBuilderService> channelFactory = CreateChannelFactory())
            {
                IJobBuilderService channel = channelFactory.CreateChannel();
                result = channel.CreateServicingTask(parentTaskId, nodeId, serviceType, expectedDuration);
                channelFactory.Close();
            }

            return result;
        }

        private Tuple<int, ServiceCallData> CreateSleepingTask(int parentTaskId, int nodeId, TimeSpan expectedDuration)
        {
            Logger.Debug("CreateSleepingTask({0},{1},{2})", parentTaskId, nodeId, expectedDuration);

            if (isDisposed)
            {
                throw new ObjectDisposedException("JobBuilderClient");
            }

            Tuple<int, ServiceCallData> result;

            using (ChannelFactory<IJobBuilderService> channelFactory = CreateChannelFactory())
            {
                IJobBuilderService channel = channelFactory.CreateChannel();
                result = channel.CreateSleepingTask(parentTaskId, nodeId, expectedDuration);
                channelFactory.Close();
            }

            return result;
        }

        private Tuple<int, ServiceCallData> CreateAwaitingTask(int parentTaskId, int nodeId)
        {
            Logger.Debug("CreateAwaitingTask({0},{1})", parentTaskId, nodeId);

            if (isDisposed)
            {
                throw new ObjectDisposedException("JobBuilderClient");
            }

            Tuple<int, ServiceCallData> result;

            using (ChannelFactory<IJobBuilderService> channelFactory = CreateChannelFactory())
            {
                IJobBuilderService channel = channelFactory.CreateChannel();
                result = channel.CreateAwaitingTask(parentTaskId, nodeId);
                channelFactory.Close();
            }

            return result;
        }

        private Tuple<int, ServiceCallData> CreateMovingTask(int parentTaskId, int nodeId)
        {
            Logger.Debug("CreateMovingTask({0},{1})", parentTaskId, nodeId);

            if (isDisposed)
            {
                throw new ObjectDisposedException("JobBuilderClient");
            }

            Tuple<int, ServiceCallData> result;

            using (ChannelFactory<IJobBuilderService> channelFactory = CreateChannelFactory())
            {
                IJobBuilderService channel = channelFactory.CreateChannel();
                result = channel.CreateMovingTask(parentTaskId, nodeId);
                channelFactory.Close();
            }

            return result;
        }

        private Tuple<int, ServiceCallData> IssueDirective(int taskId, int parameterId, byte value)
        {
            Logger.Debug("IssueDirective({0},{1},{2})", taskId, parameterId, value);

            if (isDisposed)
            {
                throw new ObjectDisposedException("JobBuilderClient");
            }

            Tuple<int, ServiceCallData> result;

            using (ChannelFactory<IJobBuilderService> channelFactory = CreateChannelFactory())
            {
                IJobBuilderService channel = channelFactory.CreateChannel();
                result = channel.IssueEnumDirective(taskId, parameterId, value);
                channelFactory.Close();
            }

            return result;
        }

        private Tuple<int, ServiceCallData> IssueDirective(int taskId, int parameterId, short value)
        {
            Logger.Debug("IssueDirective({0},{1},{2})", taskId, parameterId, value);

            if (isDisposed)
            {
                throw new ObjectDisposedException("JobBuilderClient");
            }

            Tuple<int, ServiceCallData> result;

            using (ChannelFactory<IJobBuilderService> channelFactory = CreateChannelFactory())
            {
                IJobBuilderService channel = channelFactory.CreateChannel();
                result = channel.IssueShortDirective(taskId, parameterId, value);
                channelFactory.Close();
            }

            return result;
        }

        private Tuple<int, ServiceCallData> IssueDirective(int taskId, int parameterId, ushort value)
        {
            Logger.Debug("IssueDirective({0},{1},{2})", taskId, parameterId, value);

            if (isDisposed)
            {
                throw new ObjectDisposedException("JobBuilderClient");
            }

            Tuple<int, ServiceCallData> result;

            using (ChannelFactory<IJobBuilderService> channelFactory = CreateChannelFactory())
            {
                IJobBuilderService channel = channelFactory.CreateChannel();
                result = channel.IssueUShortDirective(taskId, parameterId, value);
                channelFactory.Close();
            }

            return result;
        }

        private Tuple<int, ServiceCallData> IssueDirective(int taskId, int parameterId, float value)
        {
            Logger.Debug("IssueDirective({0},{1},{2})", taskId, parameterId, value);

            if (isDisposed)
            {
                throw new ObjectDisposedException("JobBuilderClient");
            }

            Tuple<int, ServiceCallData> result;

            using (ChannelFactory<IJobBuilderService> channelFactory = CreateChannelFactory())
            {
                IJobBuilderService channel = channelFactory.CreateChannel();
                result = channel.IssueFloatDirective(taskId, parameterId, value);
                channelFactory.Close();
            }

            return result;
        }

        private Tuple<int, ServiceCallData> IssueDirective(int taskId, int parameterId, IPAddress value)
        {
            Logger.Debug("IssueDirective({0},{1},{2})", taskId, parameterId, value);

            if (isDisposed)
            {
                throw new ObjectDisposedException("JobBuilderClient");
            }

            Tuple<int, ServiceCallData> result;

            using (ChannelFactory<IJobBuilderService> channelFactory = CreateChannelFactory())
            {
                IJobBuilderService channel = channelFactory.CreateChannel();
                result = channel.IssueIPAddressDirective(taskId, parameterId, value);
                channelFactory.Close();
            }

            return result;
        }

		private ServiceCallData FinaliseTask(int taskId)
		{
			Logger.Debug("FinaliseTask({0})", taskId);

			if (isDisposed)
			{
				throw new ObjectDisposedException("JobBuilderClient");
			}

			ServiceCallData result;

			using (ChannelFactory<IJobBuilderService> channelFactory = CreateChannelFactory())
			{
				IJobBuilderService channel = channelFactory.CreateChannel();
				result = channel.FinaliseTask(taskId);
				channelFactory.Close();
			}

			return result;
		}

		private ServiceCallData BeginEditingJob(int jobId)
		{
			Logger.Debug("BeginEditingJob({0})", jobId);

			if (isDisposed)
			{
				throw new ObjectDisposedException("JobBuilderClient");
			}

			ServiceCallData result;

			using (ChannelFactory<IJobBuilderService> channelFactory = CreateChannelFactory())
			{
				IJobBuilderService channel = channelFactory.CreateChannel();
				result = channel.BeginEditingJob(jobId);
				channelFactory.Close();
			}

			return result;
		}

		private ServiceCallData FinishEditingJob(int jobId)
		{
			Logger.Debug("FinishEditingJob({0})", jobId);

			if (isDisposed)
			{
				throw new ObjectDisposedException("JobBuilderClient");
			}

			ServiceCallData result;

			using (ChannelFactory<IJobBuilderService> channelFactory = CreateChannelFactory())
			{
				IJobBuilderService channel = channelFactory.CreateChannel();
				result = channel.FinishEditingJob(jobId);
				channelFactory.Close();
			}

			return result;
		}

		private ServiceCallData BeginEditingTask(int taskId)
		{
			Logger.Debug("BeginEditingTask({0})", taskId);

			if (isDisposed)
			{
				throw new ObjectDisposedException("JobBuilderClient");
			}

			ServiceCallData result;

			using (ChannelFactory<IJobBuilderService> channelFactory = CreateChannelFactory())
			{
				IJobBuilderService channel = channelFactory.CreateChannel();
				result = channel.BeginEditingTask(taskId);
				channelFactory.Close();
			}

			return result;
		}

		private ServiceCallData FinishEditingTask(int taskId)
		{
			Logger.Debug("FinishEditingTask({0})", taskId);

			if (isDisposed)
			{
				throw new ObjectDisposedException("JobBuilderClient");
			}

			ServiceCallData result;

			using (ChannelFactory<IJobBuilderService> channelFactory = CreateChannelFactory())
			{
				IJobBuilderService channel = channelFactory.CreateChannel();
				result = channel.FinishEditingTask(taskId);
				channelFactory.Close();
			}

			return result;
		}
    }
}