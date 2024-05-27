using GAAPICommon.Services.Agents;
using GAAPICommon.Services.Jobs;
using GAAPICommon.Services.Maps;
using GAAPICommon.Services.Scheduling;
using GAAPICommon.Services.Servicing;
using GAClients.SchedulingClients.Agents;
using GAClients.SchedulingClients.Jobs;
using GAClients.SchedulingClients.Maps;
using GAClients.SchedulingClients.Scheduling;
using GAClients.SchedulingClients.Servicing;
using Microsoft.Extensions.DependencyInjection;

namespace GAClients.SchedulingClients;

public static class DIExtensions
{
    /// <summary>
    /// Registers gRPC clients and their associated wrappers into the provided IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to which the gRPC clients and their associated wrappers will be added.</param>
    /// <param name="endpoint">The base URI for the gRPC services. All registered gRPC clients will use this URI to connect to the server.</param>
    /// <param name="autoSubscribe">If true, the clients will automatically subscribe when connected.</param>
    /// <returns>The IServiceCollection with clients added.</returns>
    public static IServiceCollection RegisterSchedulingClients(this IServiceCollection services, Uri endpoint, bool autoSubscribe)
    {
        services.AddSingleton(f => new ClientSettings(autoSubscribe));

        services.AddGrpcClientExt<AgentServiceProto.AgentServiceProtoClient>(endpoint);
        services.AddGrpcClientExt<JobBuilderServiceProto.JobBuilderServiceProtoClient>(endpoint);
        services.AddGrpcClientExt<JobsStateServiceProto.JobsStateServiceProtoClient>(endpoint);
        services.AddGrpcClientExt<JobStateServiceProto.JobStateServiceProtoClient>(endpoint);
        services.AddGrpcClientExt<TaskStateServiceProto.TaskStateServiceProtoClient>(endpoint);
        services.AddGrpcClientExt<MapServiceProto.MapServiceProtoClient>(endpoint);
        services.AddGrpcClientExt<SchedulingServiceProto.SchedulingServiceProtoClient>(endpoint);
        services.AddGrpcClientExt<ServicingServiceProto.ServicingServiceProtoClient>(endpoint);

        services.AddTransient<IAgentClient, AgentClient>();
        services.AddTransient<IJobBuilderClient, JobBuilderClient>();
        services.AddTransient<IJobsStateClient, JobsStateClient>();
        services.AddTransient<IJobStateClient, JobStateClient>();
        services.AddTransient<ITaskStateClient, TaskStateClient>();
        services.AddTransient<IMapClient, MapClient>();
        services.AddTransient<ISchedulingClient, SchedulingClient>();
        services.AddTransient<IServicingClient, ServicingClient>();
        return services;
    }

    private static IServiceCollection AddGrpcClientExt<T>(this IServiceCollection services, Uri uri) where T : class
    {
        services.AddGrpcClient<T>(o =>
        {
            o.Address = uri;
        });
        return services;
    }
}