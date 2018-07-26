using GAClients;
using SchedulingClients.AgentStateCastServiceReference;
using System;
using System.Net;
using System.ServiceModel;

namespace SchedulingClients
{
    internal class AgentStatecastClient : AbstractClient<IAgentStatecastService>, IAgentStatecastClient
	{
		private bool isDisposed = false;

		public AgentStatecastClient(Uri netTcpUri)
			:base(netTcpUri)
		{
		}

		public ServiceOperationResult TryGetEnumStatecastValue(int agentId, string parameterAlias, out byte parameterValue)
		{
			Logger.Info("TryGetEnumStatecastValue()");

			try
			{
				var result = GetEnumStatecastValue(agentId, parameterAlias);
				parameterValue = result.Item1;
				return ServiceOperationResultFactory.FromAgentStatecastServiceCallData(result.Item2);
			}
			catch(Exception ex)
			{
				parameterValue = 0;
				return HandleClientException(ex);
			}
		}

		public ServiceOperationResult TryGetFloatStatecastValue(int agentId, string parameterAlias, out float parameterValue)
		{
			Logger.Info("TryGetFloatStatecastValue()");

			try
			{
				var result = GetFloatStatecastValue(agentId, parameterAlias);
				parameterValue = result.Item1;
				return ServiceOperationResultFactory.FromAgentStatecastServiceCallData(result.Item2);
			}
			catch (Exception ex)
			{
				parameterValue = 0;
				return HandleClientException(ex);
			}
		}

		public ServiceOperationResult TryGetShortStatecastValue(int agentId, string parameterAlias, out short parameterValue)
		{
			Logger.Info("TryGetShortStatecastValue()");

			try
			{
				var result = GetShortStatecastValue(agentId, parameterAlias);
				parameterValue = result.Item1;
				return ServiceOperationResultFactory.FromAgentStatecastServiceCallData(result.Item2);
			}
			catch (Exception ex)
			{
				parameterValue = 0;
				return HandleClientException(ex);
			}
		}

		public ServiceOperationResult TryGetUShortStatecastValue(int agentId, string parameterAlias, out ushort parameterValue)
		{
			Logger.Info("TryGetUShortStatecastValue()");

			try
			{
				var result = GetUShortStatecastValue(agentId, parameterAlias);
				parameterValue = result.Item1;
				return ServiceOperationResultFactory.FromAgentStatecastServiceCallData(result.Item2);
			}
			catch (Exception ex)
			{
				parameterValue = 0;
				return HandleClientException(ex);
			}
		}

        public ServiceOperationResult TryGetUIntegerStatecastValue(int agentId, string parameterAlias, out uint parameterValue)
        {
            Logger.Info("TryGetUIntegerStatecastValue()");

            try
            {
                var result = GetUIntegerStatecastValue(agentId, parameterAlias);
                parameterValue = result.Item1;
                return ServiceOperationResultFactory.FromAgentStatecastServiceCallData(result.Item2);
            }
            catch (Exception ex)
            {
                parameterValue = 0;
                return HandleClientException(ex);
            }
        }

        public ServiceOperationResult TryGetIntegerStatecastValue(int agentId, string parameterAlias, out int parameterValue)
        {
            Logger.Info("TryGetIntegerStatecastValue()");

            try
            {
                var result = GetIntegerStatecastValue(agentId, parameterAlias);
                parameterValue = result.Item1;
                return ServiceOperationResultFactory.FromAgentStatecastServiceCallData(result.Item2);
            }
            catch (Exception ex)
            {
                parameterValue = 0;
                return HandleClientException(ex);
            }
        }

        public ServiceOperationResult TryGetIPAddressStatecastValue(int agentId, string parameterAlias, out IPAddress parameterValue)
		{
			Logger.Info("TryGetfloatStatecastValue()");

			try
			{
				var result = GetIPAddressStatecastValue(agentId, parameterAlias);
				parameterValue = result.Item1;
				return ServiceOperationResultFactory.FromAgentStatecastServiceCallData(result.Item2);
			}
			catch (Exception ex)
			{
				parameterValue = IPAddress.None;
				return HandleClientException(ex);
			}
		}

		public ServiceOperationResult TryGetStatecastDescription(int agentId, out CastType statecastDescription)
		{
			Logger.Info("TryGetStatecastDescription()");

			try
			{
				var result = GetStatecastDescription(agentId);
				statecastDescription = result.Item1;
				return ServiceOperationResultFactory.FromAgentStatecastServiceCallData(result.Item2);
			}
			catch (Exception ex)
			{
                statecastDescription = null;
				return HandleClientException(ex);
			}
		}

		private Tuple<byte, ServiceCallData> GetEnumStatecastValue(int agentId, string parameterAlias)
		{
			Logger.Debug(String.Format("GetEnumStatecastValue(agentId:{0}, parameterAlias:{1})", agentId, parameterAlias));

			if (isDisposed)
			{
				throw new ObjectDisposedException("AgentStatecastClient");
			}

			Tuple<byte, ServiceCallData> result;

			using (ChannelFactory<IAgentStatecastService> channelFactory = CreateChannelFactory())
			{
				IAgentStatecastService channel = channelFactory.CreateChannel();
				result = channel.GetEnumStatecastValue(agentId, parameterAlias);
				channelFactory.Close();
			}

			return result;
		}

		private Tuple<float, ServiceCallData> GetFloatStatecastValue(int agentId, string parameterAlias)
		{
			Logger.Debug(String.Format("GetFloatStatecastValue(agentId:{0}, parameterAlias:{1})", agentId, parameterAlias));

			if (isDisposed)
			{
				throw new ObjectDisposedException("AgentStatecastClient");
			}

			Tuple<float, ServiceCallData> result;

			using (ChannelFactory<IAgentStatecastService> channelFactory = CreateChannelFactory())
			{
				IAgentStatecastService channel = channelFactory.CreateChannel();
				result = channel.GetFloatStatecastValue(agentId, parameterAlias);
				channelFactory.Close();
			}

			return result;
		}

		private Tuple<short, ServiceCallData> GetShortStatecastValue(int agentId, string parameterAlias)
		{
			Logger.Debug(String.Format("GetShortStatecastValue(agentId:{0}, parameterAlias:{1})", agentId, parameterAlias));

			if (isDisposed)
			{
				throw new ObjectDisposedException("AgentStatecastClient");
			}

			Tuple<short, ServiceCallData> result;

			using (ChannelFactory<IAgentStatecastService> channelFactory = CreateChannelFactory())
			{
				IAgentStatecastService channel = channelFactory.CreateChannel();
				result = channel.GetShortStatecastValue(agentId, parameterAlias);
				channelFactory.Close();
			}

			return result;
		}

		private Tuple<ushort, ServiceCallData> GetUShortStatecastValue(int agentId, string parameterAlias)
		{
			Logger.Debug(String.Format("GetUShortStatecastValue(agentId:{0}, parameterAlias:{1})", agentId, parameterAlias));

			if (isDisposed)
			{
				throw new ObjectDisposedException("AgentStatecastClient");
			}

			Tuple<ushort, ServiceCallData> result;

			using (ChannelFactory<IAgentStatecastService> channelFactory = CreateChannelFactory())
			{
				IAgentStatecastService channel = channelFactory.CreateChannel();
				result = channel.GetUShortStatecastValue(agentId, parameterAlias);
				channelFactory.Close();
			}

			return result;
		}

        private Tuple<uint, ServiceCallData> GetUIntegerStatecastValue(int agentId, string parameterAlias)
        {
            Logger.Debug(String.Format("GetUIntegerStatecastValue(agentId:{0}, parameterAlias:{1})", agentId, parameterAlias));

            if (isDisposed)
            {
                throw new ObjectDisposedException("AgentStatecastClient");
            }

            Tuple<uint, ServiceCallData> result;

            using (ChannelFactory<IAgentStatecastService> channelFactory = CreateChannelFactory())
            {
                IAgentStatecastService channel = channelFactory.CreateChannel();
                result = channel.GetUIntegerStatecastValue(agentId, parameterAlias);
                channelFactory.Close();
            }

            return result;
        }

        private Tuple<int, ServiceCallData> GetIntegerStatecastValue(int agentId, string parameterAlias)
        {
            Logger.Debug(String.Format("GetIntegerStatecastValue(agentId:{0}, parameterAlias:{1})", agentId, parameterAlias));

            if (isDisposed)
            {
                throw new ObjectDisposedException("AgentStatecastClient");
            }

            Tuple<int, ServiceCallData> result;

            using (ChannelFactory<IAgentStatecastService> channelFactory = CreateChannelFactory())
            {
                IAgentStatecastService channel = channelFactory.CreateChannel();
                result = channel.GetIntegerStatecastValue(agentId, parameterAlias);
                channelFactory.Close();
            }

            return result;
        }

        private Tuple<IPAddress, ServiceCallData> GetIPAddressStatecastValue(int agentId, string parameterAlias)
		{
			Logger.Debug(String.Format("GetIPAddressStatecastValue(agentId:{0}, parameterAlias:{1})", agentId, parameterAlias));

			if (isDisposed)
			{
				throw new ObjectDisposedException("AgentStatecastClient");
			}

			Tuple<IPAddress, ServiceCallData> result;

			using (ChannelFactory<IAgentStatecastService> channelFactory = CreateChannelFactory())
			{
				IAgentStatecastService channel = channelFactory.CreateChannel();
				result = channel.GetIPAddressStatecastValue(agentId, parameterAlias);
				channelFactory.Close();
			}

			return result;
		}

		private Tuple<CastType, ServiceCallData> GetStatecastDescription(int agentId)
		{
			Logger.Debug(String.Format("GetStatecastDescription(agentId:{0})", agentId));

			if (isDisposed)
			{
				throw new ObjectDisposedException("AgentStatecastClient");
			}

			Tuple<CastType, ServiceCallData> result;

			using (ChannelFactory<IAgentStatecastService> channelFactory = CreateChannelFactory())
			{
				IAgentStatecastService channel = channelFactory.CreateChannel();
				result = channel.GetStatecastDescription(agentId);
				channelFactory.Close();
			}

			return result;
		}
	}
}
