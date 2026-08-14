using GAAPICommon.Messages;

namespace Guidance.SchedulingClients.Maps;

using GAAPICommon.Services.Maps;

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
    /// Gets the current occupiers committed to roadmap nodes and moves.
    /// </summary>
    /// <returns>An array of roadmap occupier dtos.</returns>
    public IEnumerable<RoadmapOccupierDto>? GetRoadmapOccupiers();

    /// <summary>
    /// Gets the current occupiers committed to roadmap nodes and moves.
    /// </summary>
    /// <returns>An array of roadmap occupier dtos.</returns>
    public Task<IEnumerable<RoadmapOccupierDto>?> GetRoadmapOccupiersAsync(
        CancellationToken cancellationToken = default);

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

    /// <summary>Gets point-cloud points from the active roadmap.</summary>
    public GetPointCloudResult? GetPointCloud(int maxPoints = 0);

    /// <summary>Gets point-cloud points from the active roadmap asynchronously.</summary>
    public Task<GetPointCloudResult?> GetPointCloudAsync(int maxPoints = 0);

    /// <summary>Gets the occupiers committed to one roadmap item.</summary>
    public IEnumerable<RoadmapOccupierDto>? GetMapItemOccupiers(int mapItemId);

    /// <summary>Gets the occupiers committed to one roadmap item asynchronously.</summary>
    public Task<IEnumerable<RoadmapOccupierDto>?> GetMapItemOccupiersAsync(int mapItemId);

    /// <summary>Gets persisted trajectory-generation settings.</summary>
    public TrajectorySettingsResult? GetTrajectorySettings();

    /// <summary>Gets persisted trajectory-generation settings asynchronously.</summary>
    public Task<TrajectorySettingsResult?> GetTrajectorySettingsAsync();

    /// <summary>Updates persisted trajectory-generation settings.</summary>
    public TrajectorySettingsResult? SetTrajectorySettings(double clothoidWaypointSpacingMeters);

    /// <summary>Updates persisted trajectory-generation settings asynchronously.</summary>
    public Task<TrajectorySettingsResult?> SetTrajectorySettingsAsync(double clothoidWaypointSpacingMeters);

    /// <summary>Gets the active roadmap.</summary>
    public MapCardDto? GetActiveRoadmap();

    /// <summary>Gets the active roadmap asynchronously.</summary>
    public Task<MapCardDto?> GetActiveRoadmapAsync();

    /// <summary>Gets the roadmap selected for the next restart.</summary>
    public MapCardDto? GetNextRoadmap();

    /// <summary>Gets the roadmap selected for the next restart asynchronously.</summary>
    public Task<MapCardDto?> GetNextRoadmapAsync();

    /// <summary>Gets the current versions of all roadmaps.</summary>
    public IEnumerable<MapCardDto>? GetRoadmaps();

    /// <summary>Gets the current versions of all roadmaps asynchronously.</summary>
    public Task<IEnumerable<MapCardDto>?> GetRoadmapsAsync();

    /// <summary>Gets every roadmap and its version history.</summary>
    public IEnumerable<RoadmapCatalogItemDto>? GetRoadmapCatalog();

    /// <summary>Gets every roadmap and its version history asynchronously.</summary>
    public Task<IEnumerable<RoadmapCatalogItemDto>?> GetRoadmapCatalogAsync();

    /// <summary>Gets active, next, and catalog roadmap data in one snapshot.</summary>
    public GetRoadmapOverviewResult? GetRoadmapOverview();

    /// <summary>Gets active, next, and catalog roadmap data in one snapshot asynchronously.</summary>
    public Task<GetRoadmapOverviewResult?> GetRoadmapOverviewAsync();

    /// <summary>Selects a roadmap for activation.</summary>
    public bool ActivateRoadmap(int id);

    /// <summary>Selects a roadmap for activation asynchronously.</summary>
    public Task<bool> ActivateRoadmapAsync(int id);

    /// <summary>Deletes a roadmap.</summary>
    public bool DeleteRoadmap(int id);

    /// <summary>Deletes a roadmap asynchronously.</summary>
    public Task<bool> DeleteRoadmapAsync(int id);

    /// <summary>Uploads a roadmap using bounded gRPC chunks.</summary>
    public Task<RoadmapUploadResultDto?> UploadRoadmapAsync(
        Stream contents,
        string fileName,
        string changeMessage,
        CancellationToken cancellationToken = default);

    /// <summary>Exports a roadmap to traNsitrak using bounded gRPC chunks.</summary>
    public Task<byte[]?> ExportTransitrakAsync(
        Stream contents,
        string fileName,
        string exportName,
        CancellationToken cancellationToken = default);

    /// <summary>Downloads a roadmap using bounded gRPC chunks.</summary>
    public Task<byte[]?> DownloadRoadmapAsync(
        int id,
        CancellationToken cancellationToken = default);

    /// <summary>Downloads the active roadmap using bounded gRPC chunks.</summary>
    public Task<byte[]?> DownloadActiveRoadmapAsync(CancellationToken cancellationToken = default);

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
