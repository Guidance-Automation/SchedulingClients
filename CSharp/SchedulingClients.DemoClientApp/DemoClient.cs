using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using NLog;
using System.IO;
using NLog.Targets;
using NLog.Config;
using BaseClients;

namespace SchedulingClients.DemoClientApp
{
    public class DemoClient : IDisposable
    {
        private readonly string logFolderPath;

        private ObservableCollection<IClient> clients = new ObservableCollection<IClient>();

        private EndpointSettings endpointSettings;

        private bool isDisposed = false;

        private ReadOnlyObservableCollection<IClient> readOnlyClients;

        public DemoClient(IPAddress ipAddress, int httpPort, int tcpPort )
        {
            this.endpointSettings = new EndpointSettings(ipAddress, httpPort, tcpPort);
            readOnlyClients = new ReadOnlyObservableCollection<IClient>(clients);

            logFolderPath = Path.Combine(Path.GetTempPath(), "Scheduling Clients Demo Client App");
            Directory.CreateDirectory(logFolderPath);

            Configure();
        }

        ~DemoClient()
        {
            Dispose(false);
        }

		public IAgentClient AgentClient { get { return (IAgentClient)clients.FirstOrDefault(e => e is IAgentClient); } }

		public ReadOnlyObservableCollection<IClient> Clients { get { return readOnlyClients; } }

        public bool IsDisposed { get { return isDisposed; } }

        public IJobBuilderClient JobBuilderClient { get { return (IJobBuilderClient)clients.FirstOrDefault(e => e is IJobBuilderClient); } }

        public IJobsStateClient JobsStateClient { get { return (IJobsStateClient)clients.FirstOrDefault(e => e is IJobsStateClient); } }

        public IJobStateClient JobStateClient { get { return (IJobStateClient)clients.FirstOrDefault(e => e is IJobStateClient); } }

        public string LogFolderPath { get { return logFolderPath; } }

        public IMapClient RoadmapClient { get { return (IMapClient)clients.FirstOrDefault(e => e is IMapClient); } }

        public IServicingClient ServicingClient { get { return (IServicingClient)clients.FirstOrDefault(e => e is IServicingClient); } }

        public void Configure()
        {
            clients.Add(ClientFactory.CreateTcpAgentClient(endpointSettings));
            clients.Add(ClientFactory.CreateTcpJobBuilderClient(endpointSettings));

            clients.Add(ClientFactory.CreateTcpJobStateClient(endpointSettings));
            clients.Add(ClientFactory.CreateTcpJobsStateClient(endpointSettings));

            clients.Add(ClientFactory.CreateTcpMapClient(endpointSettings));
            clients.Add(ClientFactory.CreateTcpServicingClient(endpointSettings));

            LogManager.Configuration = new LoggingConfiguration();

            foreach (IClient client in clients)
            {
                client.Logger = GetLogger(client);
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool isDisposing)
        {
            if (isDisposed)
            {
                return;
            }

            foreach (IClient client in clients.ToList())
            {
                client.Dispose();         
                clients.Remove(client);
            }

            isDisposed = true;
        }

        private Logger GetLogger(IClient client)
        {
            string name = client.GetType().Name;

            FileTarget target = new FileTarget(name);
            target.Layout = @"${processtime} ${level: padding = -8:padCharacter = } ${message}";
            target.FileName = Path.Combine(new[] { logFolderPath, name + ".log" });

            LoggingRule rule = new LoggingRule(name, LogLevel.Trace, target);
            LogManager.Configuration.LoggingRules.Add(rule);
            LogManager.Configuration.AddTarget(target);

            LogManager.ReconfigExistingLoggers();

            return LogManager.GetLogger(name);
        }
    }
}