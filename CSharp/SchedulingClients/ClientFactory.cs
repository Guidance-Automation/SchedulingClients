using GAClients;
using SchedulingClients.Client_Interfaces;

namespace SchedulingClients
{
    public static class ClientFactory
    {
        public static IAgentClient CreateTcpAgentClient(EndpointSettings portSettings)
        {
            return new AgentClient(portSettings.TcpAgentService());
        }

        public static IFleetManagerClient CreateTcpFleetManagerClient(EndpointSettings portSettings)
        {
            return new FleetManagerClient(portSettings.TcpFleetManagerService());
        }

        public static IJobBuilderClient CreateTcpJobBuilderClient(EndpointSettings portSettings)
        {
            return new JobBuilderClient(portSettings.TcpJobBuilderService());
        }

        public static IJobsStateClient CreateTcpJobsStateClient(EndpointSettings portSettings)
        {
            return new JobsStateClient(portSettings.TcpJobsStateService());
        }

        public static IJobStateClient CreateTcpJobStateClient(EndpointSettings portSettings)
        {
            return new JobStateClient(portSettings.TcpJobStateService());
        }

        public static IMapClient CreateTcpRoadmapClient(EndpointSettings portSettings)
        {
            return new MapClient(portSettings.TcpMapService());
        }

        public static IServicingClient CreateTcpServicingClient(EndpointSettings portSettings)
        {
            return new ServicingClient(portSettings.TcpServicingService());
        }

        public static IAgentBatteryStatusClient CreateTcpAgentBatteryStatusClient(EndpointSettings portSettings)
        {
            return new AgentBatteryStatusClient(portSettings.TcpAgentBatteryStatusService());
        }

        public static IAgentAttentionClient CreateTcpAgentAttentionClient(EndpointSettings portSettings)
        {
          return new AgentAttentionClient(portSettings.TcpAgentAttentionService());
        }

        public static ITaskStateClient CreateTcpTaskStatecastClient(EndpointSettings portSettings)
        {
            return new TaskStateClient(portSettings.TcpTaskStateService());
        }


        public static IAgentStatecastClient CreateTcpAgentStatecastClient(EndpointSettings portSettings)
        {
          return new AgentStatecastClient(portSettings.TcpAgentStatecastService());
        }

        public static IVersionClient CreateTcpVersionClient(EndpointSettings portSettings)
        {
            return new VersionClient(portSettings.TcpVersionService());
        }
    }
}