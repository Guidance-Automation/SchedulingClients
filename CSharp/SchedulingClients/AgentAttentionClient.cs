using GAClients;
using SchedulingClients.AgentAttentionServiceReference;
using System;
using System.ServiceModel;

namespace SchedulingClients
{
    internal class AgentAttentionClient : AbstractCallbackClient<IAgentAttentionService>, IAgentAttentionClient
    {
		private AgentAttentionServiceCallback callback = new AgentAttentionServiceCallback();

		private TimeSpan heartbeat;

		private bool isDisposed = false;

		/// <summary>
		/// Creates an AgentAttentionClient
		/// </summary>
		/// <param name="netTcpUri">net.tcp address of the job state service</param>
		/// <param name="heartbeat">Heartbeat</param>
		public AgentAttentionClient(Uri netTcpUri, TimeSpan heartbeat = default(TimeSpan))
			: base(netTcpUri)
		{
			this.heartbeat = heartbeat < TimeSpan.FromMilliseconds(1000) ? TimeSpan.FromMilliseconds(1000) : heartbeat;
		}

        /// <summary>
        /// Change to agent attention status
        /// </summary>
        public event Action<AgentAttentionData[]> AgentAttentionChange
        {
            add { callback.AgentAttentionChange += value; }
            remove { callback.AgentAttentionChange -= value; }
        }

		/// <summary>
		/// Hearbeat time
		/// </summary>
		public TimeSpan Heartbeat { get { return heartbeat; } }

		protected override void Dispose(bool isDisposing)
		{
			Logger.Debug("Dispose({0})", isDisposing);

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

			ChannelFactory<IAgentAttentionService> channelFactory = CreateChannelFactory();
			IAgentAttentionService agentAttentionService = channelFactory.CreateChannel();

			bool? exceptionCaught;

			while (!Terminate)
			{
				exceptionCaught = null;

				try
				{
					Logger.Trace("SubscriptionHeartbeat({0})", Key);
					agentAttentionService.SubscriptionHeartbeat(Key);
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
					agentAttentionService = channelFactory.CreateChannel();
				}

				heartbeatReset.WaitOne(Heartbeat);
			}

			Logger.Debug("HeartbeatThread exit");
		}

		protected override void SetInstanceContext()
		{
			this.context = new InstanceContext(this.callback);
		}
	}
}
