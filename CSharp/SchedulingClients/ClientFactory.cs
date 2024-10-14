using BaseClients;

namespace SchedulingClients
{
    public static class ClientFactory
    {
        public static IAgentClient CreateTcpAgentClient(EndpointSettings portSettings)
        {
            return new AgentClient(portSettings.TcpAgentService());
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

        public static IMapClient CreateTcpMapClient(EndpointSettings portSettings)
        {
            return new MapClient(portSettings.TcpMapService());
        }

        public static IServicingClient CreateTcpServicingClient(EndpointSettings portSettings)
        {
            return new ServicingClient(portSettings.TcpServicingService());
        }

        public static ITaskStateClient CreateTcpTaskStatecastClient(EndpointSettings portSettings)
        {
            return new TaskStateClient(portSettings.TcpTaskStateService());
        }
    }
}