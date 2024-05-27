using GAAPICommon.Messages;

namespace GAClients.SchedulingClients.Servicing;

public interface IServicingClient : IDisposable
{
    /// <summary>
    /// Gets all outstanding service requests.
    /// </summary>
    /// <returns>Array of service state dtos.</returns>
    public IEnumerable<ServiceStateDto>? GetOutstandingServiceRequests();

    /// <summary>
    /// Gets all outstanding service requests.
    /// </summary>
    /// <returns>Array of service state dtos.</returns>
    public Task<IEnumerable<ServiceStateDto>?> GetOutstandingServiceRequestsAsync();

    /// <summary>
    /// Marks a service task as complete.
    /// </summary>
    /// <param name="taskId">target task id</param>
    /// <returns>Successful service call result on success</returns>
    public bool SetServiceComplete(int taskId);

    /// <summary>
    /// Marks a service task as complete.
    /// </summary>
    /// <param name="taskId">target task id</param>
    /// <returns>Successful service call result on success</returns>
    public Task<bool> SetServiceCompleteAsync(int taskId);

    /// <summary>
    /// Unsubscribes from the service updates.
    /// </summary>
    public void Unsubscribe();

    /// <summary>
    /// Fired whenever a new service request is received. 
    /// </summary>
    public event Action<ServiceStateDto>? ServiceRequest;
}