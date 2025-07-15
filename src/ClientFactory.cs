using GAAPICommon.Services.Agents;
using GAAPICommon.Services.Jobs;
using GAAPICommon.Services.Maps;
using GAAPICommon.Services.Scheduling;
using GAAPICommon.Services.Servicing;
using Grpc.Net.Client;
using Guidance.SchedulingClients.Agents;
using Guidance.SchedulingClients.Jobs;
using Guidance.SchedulingClients.Maps;
using Guidance.SchedulingClients.Scheduling;
using Guidance.SchedulingClients.Servicing;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Guidance.SchedulingClients;

/// <summary>
/// Factory class for creating clients
/// </summary>
public static class ClientFactory
{
    /// <summary>
    /// Creates a new HTTP based IAgentClient instance.
    /// </summary>
    /// <param name="portSettings">Endpoint settings specifying port and IP address to use</param>
    /// <returns>HTTP based IAgentClient instance</returns>
    public static IAgentClient CreateAgentClient(IPAddress ipAddress, ushort httpPort = 41916, bool subscribe = true, ILogger<AgentClient>? logger = null)
    {
        Uri uri = new($"http://{ipAddress}:{httpPort}");
        GrpcChannel channel = GrpcChannel.ForAddress(uri);
        AgentServiceProto.AgentServiceProtoClient client = new(channel);
        ClientSettings settings = new(subscribe);
        return new AgentClient(client, settings, logger);
    }

    /// <summary>
    /// Creates a new HTTP based IJobBuilderClient instance.
    /// </summary>
    /// <param name="portSettings">Endpoint settings specifying port and IP address to use</param>
    /// <returns>HTTP based IJobBuilderClient instance</returns>
    public static IJobBuilderClient CreateJobBuilderClient(IPAddress ipAddress, ushort httpPort = 41916, ILogger<JobBuilderClient>? logger = null)
    {
        Uri uri = new($"http://{ipAddress}:{httpPort}");
        GrpcChannel channel = GrpcChannel.ForAddress(uri);
        JobBuilderServiceProto.JobBuilderServiceProtoClient client = new(channel);
        return new JobBuilderClient(client, logger);
    }

    /// <summary>
    /// Creates a new HTTP based IJobsStateClient instance.
    /// </summary>
    /// <param name="portSettings">Endpoint settings specifying port and IP address to use</param>
    /// <returns>HTTP based IJobsStateClient instance</returns>
    public static IJobsStateClient CreateJobsStateClient(IPAddress ipAddress, ushort httpPort = 41916, bool subscribe = true, ILogger<JobsStateClient>? logger = null)
    {
        Uri uri = new($"http://{ipAddress}:{httpPort}");
        GrpcChannel channel = GrpcChannel.ForAddress(uri);
        JobsStateServiceProto.JobsStateServiceProtoClient client = new(channel);
        ClientSettings settings = new(subscribe);
        return new JobsStateClient(client, settings, logger);
    }

    /// <summary>
    /// Creates a new HTTP based IJobStateClient instance.
    /// </summary>
    /// <param name="portSettings">Endpoint settings specifying port and IP address to use</param>
    /// <returns>HTTP based IJobStateClient instance</returns>
    public static IJobStateClient CreateJobStateClient(IPAddress ipAddress, ushort httpPort = 41916, bool subscribe = true, ILogger<JobStateClient>? logger = null)
    {
        Uri uri = new($"http://{ipAddress}:{httpPort}");
        GrpcChannel channel = GrpcChannel.ForAddress(uri);
        JobStateServiceProto.JobStateServiceProtoClient client = new(channel);
        ClientSettings settings = new(subscribe);
        return new JobStateClient(client, settings, logger);
    }

    /// <summary>
    /// Creates a new HTTP based ISchedulingClient instance.
    /// </summary>
    /// <param name="portSettings">Endpoint settings specifying port and IP address to use</param>
    /// <returns>HTTP based ISchedulingClient instance</returns>
    public static ISchedulingClient CreateSchedulingClient(IPAddress ipAddress, ushort httpPort = 41916, bool subscribe = true, ILogger<SchedulingClient>? logger = null)
    {
        Uri uri = new($"http://{ipAddress}:{httpPort}");
        GrpcChannel channel = GrpcChannel.ForAddress(uri);
        SchedulingServiceProto.SchedulingServiceProtoClient client = new(channel);
        ClientSettings settings = new(subscribe);
        return new SchedulingClient(client, settings, logger);
    }

    /// <summary>
    /// Creates a new HTTP based IMapClient instance.
    /// </summary>
    /// <param name="portSettings">Endpoint settings specifying port and IP address to use</param>
    /// <returns>HTTP based IMapClient instance</returns>
    public static IMapClient CreateMapClient(IPAddress ipAddress, ushort httpPort = 41916, bool subscribe = true, ILogger<MapClient>? logger = null)
    {
        Uri uri = new($"http://{ipAddress}:{httpPort}");
        GrpcChannel channel = GrpcChannel.ForAddress(uri);
        MapServiceProto.MapServiceProtoClient client = new(channel);
        ClientSettings settings = new(subscribe);
        return new MapClient(client, settings, logger);
    }


    /// <summary>
    /// Creates a new HTTP based IServicingClient instance.
    /// </summary>
    /// <param name="portSettings">Endpoint settings specifying port and IP address to use</param>
    /// <returns>HTTP based IServicingClient instance</returns>
    public static IServicingClient CreateServicingClient(IPAddress ipAddress, ushort httpPort = 41916, bool subscribe = true, ILogger<ServicingClient>? logger = null)
    {
        Uri uri = new($"http://{ipAddress}:{httpPort}");
        GrpcChannel channel = GrpcChannel.ForAddress(uri);
        ServicingServiceProto.ServicingServiceProtoClient client = new(channel);
        ClientSettings settings = new(subscribe);
        return new ServicingClient(client, settings, logger);
    }

    /// <summary>
    /// Creates a new HTTP based ITaskStateClient instance.
    /// </summary>
    /// <param name="portSettings">Endpoint settings specifying port and IP address to use</param>
    /// <returns>HTTP based ITaskStateClient instance</returns>
    public static ITaskStateClient CreateTaskStateClient(IPAddress ipAddress, ushort httpPort = 41916, bool subscribe = true, ILogger<TaskStateClient>? logger = null)
    {
        Uri uri = new($"http://{ipAddress}:{httpPort}");
        GrpcChannel channel = GrpcChannel.ForAddress(uri);
        TaskStateServiceProto.TaskStateServiceProtoClient client = new(channel);
        ClientSettings settings = new(subscribe);
        return new TaskStateClient(client, settings, logger);
    }
}