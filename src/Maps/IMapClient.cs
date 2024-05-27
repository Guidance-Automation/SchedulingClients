using GAAPICommon.Messages;

namespace GAClients.SchedulingClients.Maps;

/// <summary>
/// Client for interacting with the Map service.
/// </summary>
public interface IMapClient : IDisposable
{
    /// <summary>
    /// Gets all moves in the roadmap.
    /// </summary>
    /// <returns>Array of move dtos.</returns>
    public IEnumerable<MoveDto>? GetAllMoves();

    /// <summary>
    /// Gets all moves in the roadmap.
    /// </summary>
    /// <returns>Array of move dtos.</returns>
    public Task<IEnumerable<MoveDto>?> GetAllMovesAsync();

    /// <summary>
    /// Gets all nodes in the roadmap.
    /// </summary>
    /// <returns>Array of node dtos.</returns>
    public IEnumerable<NodeDto>? GetAllNodes();

    /// <summary>
    /// Gets all nodes in the roadmap.
    /// </summary>
    /// <returns>Array of node dtos.</returns>
    public Task<IEnumerable<NodeDto>?> GetAllNodesAsync();

    /// <summary>
    /// Gets all Kingpin parameters in the roadmap.
    /// </summary>
    /// <returns>An array of parameter dtos.</returns>
    public IEnumerable<ParameterDto>? GetAllParameters();

    /// <summary>
    /// Gets all Kingpin parameters in the roadmap.
    /// </summary>
    /// <returns>An array of parameter dtos.</returns>
    public Task<IEnumerable<ParameterDto>?> GetAllParametersAsync();

    /// <summary>
    /// Gets an individual moevs trajectory. 
    /// </summary>
    /// <param name="moveId">Target move Id.</param>
    /// <returns>Waypoint dto for given move.</returns>
    public IEnumerable<WaypointDto>? GetTrajectory(int moveId);

    /// <summary>
    /// Gets an individual moevs trajectory. 
    /// </summary>
    /// <param name="moveId">Target move Id.</param>
    /// <returns>Waypoint dto for given move.</returns>
    public Task<IEnumerable<WaypointDto>?> GetTrajectoryAsync(int moveId);

    /// <summary>
    /// Gets the state of the current occupying mandate.
    /// </summary>
    /// <returns>Occupying mandate progress dto of current mandate.</returns>
    public OccupyingMandateProgressDto? GetOccupyingMandateProgress();

    /// <summary>
    /// Gets the state of the current occupying mandate.
    /// </summary>
    /// <returns>Occupying mandate progress dto of current mandate.</returns>
    public Task<OccupyingMandateProgressDto?> GetOccupyingMandateProgressAsync();

    /// <summary>
    /// Sets a new occupying mandate.
    /// </summary>
    /// <param name="mapItemIds">Hash set of map items to occupy.</param>
    /// <param name="timeout">Allotted time to successfully apply the mandate.</param>
    /// <returns>Successful service call result on success.</returns>
    public bool SetOccupyingMandate(HashSet<int> mapItemIds, TimeSpan timeout);

    /// <summary>
    /// Sets a new occupying mandate.
    /// </summary>
    /// <param name="mapItemIds">Hash set of map items to occupy.</param>
    /// <param name="timeout">Allotted time to successfully apply the mandate.</param>
    /// <returns>Successful service call result on success.</returns>
    public Task<bool> SetOccupyingMandateAsync(HashSet<int> mapItemIds, TimeSpan timeout);

    /// <summary>
    /// Clears any occupying mandate.
    /// </summary>
    /// <returns>Successful service call result on success.</returns>
    public bool ClearOccupyingMandate();

    /// <summary>
    /// Clears any occupying mandate.
    /// </summary>
    /// <returns>Successful service call result on success.</returns>
    public Task<bool> ClearOccupyingMandateAsync();
    
    /// <summary>
    /// Unsubscribe from occupying mandate progress updates.
    /// </summary>
    public void Unsubscribe();

    /// <summary>
    /// Current progress of the current occupying mandate.
    /// </summary>
    public OccupyingMandateProgressDto? OccupyingMandateProgress { get; }

    /// <summary>
    /// Fired whenever occupying mandate progress is updated.
    /// </summary>
    public event Action<OccupyingMandateProgressDto>? OccupyingMandateProgressUpdated;
}