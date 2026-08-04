using GAAPICommon;
using GAAPICommon.Enums;
using GAAPICommon.Messages;
using GAAPICommon.Services.Maps;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Guidance.SchedulingClients.Maps;

using Google.Protobuf;

/// <summary>
/// Client for interacting with the Map service.
/// </summary>
public class MapClient : IMapClient
{
    private const int FileChunkSize = 64 * 1024;
    private bool _isDisposed;
    private CancellationTokenSource _cts = new();
    private readonly MapServiceProto.MapServiceProtoClient _client;
    private readonly ILogger? _logger;

    /// <summary>
    /// The current occupying mandate progress.
    /// </summary>
    public OccupyingMandateProgressDto? OccupyingMandateProgress { get; private set; }

    /// <summary>
    /// Event that is triggered when the occupying mandate progress is updated.
    /// </summary>
    public event Action<OccupyingMandateProgressDto>? OccupyingMandateProgressUpdated;

    /// <summary>
    /// Initializes a new instance of the MapClient class using an existing client instance.
    /// </summary>
    /// <param name="client">An existing instance of the MapServiceProtoClient.</param>
    /// <param name="settings">Client settings.</param>
    /// <param name="logger">Logger for logging messages.</param>
    public MapClient(MapServiceProto.MapServiceProtoClient client, ClientSettings settings, ILogger<MapClient>? logger)
    {
        _client = client;
        _logger = logger;
        _logger?.LogInformationIfEnabled("[MapClient] MapClient created");
        if (settings.Subscribe)
            Task.Run(Subscribe);
    }

    /// <summary>
    /// Clears the occupying mandate.
    /// </summary>
    /// <returns>True if the operation succeeded, otherwise false.</returns>
    public bool ClearOccupyingMandate()
    {
        _logger?.LogTraceIfEnabled("[MapClient] ClearOccupyingMandate() called");
        try
        {
            ClearOccupyingMandateRequest request = new();
            _logger?.LogDebugIfEnabled("[MapClient] Sending ClearOccupyingMandateRequest");
            GenericResult response = _client.ClearOccupyingMandate(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformationIfEnabled("[MapClient] ClearOccupyingMandate() succeeded");
                return true;
            }
            else
            {
                _logger?.LogErrorIfEnabled("[MapClient] ClearOccupyingMandate() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogErrorIfEnabled(ex, "[MapClient] Error clearing occupying mandate");
            return false;
        }
    }

    /// <summary>
    /// Clears the occupying mandate asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains true if the operation succeeded, otherwise false.</returns>
    public async Task<bool> ClearOccupyingMandateAsync()
    {
        _logger?.LogTraceIfEnabled("[MapClient] ClearOccupyingMandateAsync() called");
        try
        {
            ClearOccupyingMandateRequest request = new();
            _logger?.LogDebugIfEnabled("[MapClient] Sending ClearOccupyingMandateRequest");
            GenericResult response = await _client.ClearOccupyingMandateAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformationIfEnabled("[MapClient] ClearOccupyingMandateAsync() succeeded");
                return true;
            }
            else
            {
                _logger?.LogErrorIfEnabled("[MapClient] ClearOccupyingMandateAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogErrorIfEnabled(ex, "[MapClient] Error clearing occupying mandate");
            return false;
        }
    }

    /// <summary>
    /// Gets all moves.
    /// </summary>
    /// <returns>The result containing all moves, or null if an error occurred.</returns>
    public IEnumerable<MoveDto>? GetAllMoves()
    {
        _logger?.LogTraceIfEnabled("[MapClient] GetAllMoves() called");
        try
        {
            GetAllMoveDataRequest request = new();
            _logger?.LogDebugIfEnabled("[MapClient] Sending GetAllMoveDataRequest");
            GetAllMoveDataResult response = _client.GetAllMoveData(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformationIfEnabled("[MapClient] GetAllMoves() succeeded");
                return response.Moves;
            }
            else
            {
                _logger?.LogErrorIfEnabled("[MapClient] GetAllMoves() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogErrorIfEnabled(ex, "[MapClient] Error getting all moves");
            return null;
        }
    }

    /// <summary>
    /// Gets all moves asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains all moves, or null if an error occurred.</returns>
    public async Task<IEnumerable<MoveDto>?> GetAllMovesAsync()
    {
        _logger?.LogTraceIfEnabled("[MapClient] GetAllMovesAsync() called");
        try
        {
            GetAllMoveDataRequest request = new();
            _logger?.LogDebugIfEnabled("[MapClient] Sending GetAllMoveDataRequest");
            GetAllMoveDataResult response = await _client.GetAllMoveDataAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformationIfEnabled("[MapClient] GetAllMovesAsync() succeeded");
                return response.Moves;
            }
            else
            {
                _logger?.LogErrorIfEnabled("[MapClient] GetAllMovesAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogErrorIfEnabled(ex, "[MapClient] Error getting all moves");
            return null;
        }
    }

    /// <summary>
    /// Gets all nodes.
    /// </summary>
    /// <returns>The result containing all nodes, or null if an error occurred.</returns>
    public IEnumerable<NodeDto>? GetAllNodes()
    {
        _logger?.LogTraceIfEnabled("[MapClient] GetAllNodes() called");
        try
        {
            GetAllNodeDataRequest request = new();
            _logger?.LogDebugIfEnabled("[MapClient] Sending GetAllNodeDataRequest");
            GetAllNodeDataResult response = _client.GetAllNodeData(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformationIfEnabled("[MapClient] GetAllNodes() succeeded");
                return response.Nodes;
            }
            else
            {
                _logger?.LogErrorIfEnabled("[MapClient] GetAllNodes() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogErrorIfEnabled(ex, "[MapClient] Error getting all nodes");
            return null;
        }
    }

    /// <summary>
    /// Gets all nodes asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains all nodes, or null if an error occurred.</returns>
    public async Task<IEnumerable<NodeDto>?> GetAllNodesAsync()
    {
        _logger?.LogTraceIfEnabled("[MapClient] GetAllNodesAsync() called");
        try
        {
            GetAllNodeDataRequest request = new();
            _logger?.LogDebugIfEnabled("[MapClient] Sending GetAllNodeDataRequest");
            GetAllNodeDataResult response = await _client.GetAllNodeDataAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformationIfEnabled("[MapClient] GetAllNodesAsync() succeeded");
                return response.Nodes;
            }
            else
            {
                _logger?.LogErrorIfEnabled("[MapClient] GetAllNodesAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogErrorIfEnabled(ex, "[MapClient] Error getting all nodes");
            return null;
        }
    }

    /// <summary>
    /// Gets all parameters.
    /// </summary>
    /// <returns>The result containing all parameters, or null if an error occurred.</returns>
    public IEnumerable<ParameterDto>? GetAllParameters()
    {
        _logger?.LogTraceIfEnabled("[MapClient] GetAllParameters() called");
        try
        {
            GetAllParameterDataRequest request = new();
            _logger?.LogDebugIfEnabled("[MapClient] Sending GetAllParameterDataRequest");
            GetAllParameterDataResult response = _client.GetAllParameterData(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformationIfEnabled("[MapClient] GetAllParameters() succeeded");
                return response.Parameters;
            }
            else
            {
                _logger?.LogErrorIfEnabled("[MapClient] GetAllParameters() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogErrorIfEnabled(ex, "[MapClient] Error getting all parameters");
            return null;
        }
    }

    /// <summary>
    /// Gets all parameters asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains all parameters, or null if an error occurred.</returns>
    public async Task<IEnumerable<ParameterDto>?> GetAllParametersAsync()
    {
        _logger?.LogTraceIfEnabled("[MapClient] GetAllParametersAsync() called");
        try
        {
            GetAllParameterDataRequest request = new();
            _logger?.LogDebugIfEnabled("[MapClient] Sending GetAllParameterDataRequest");
            GetAllParameterDataResult response = await _client.GetAllParameterDataAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformationIfEnabled("[MapClient] GetAllParametersAsync() succeeded");
                return response.Parameters;
            }
            else
            {
                _logger?.LogErrorIfEnabled("[MapClient] GetAllParametersAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogErrorIfEnabled(ex, "[MapClient] Error getting all parameters");
            return null;
        }
    }

    /// <summary>
    /// Gets the current occupiers committed to roadmap nodes and moves.
    /// </summary>
    /// <returns>The roadmap occupiers, or null if an error occurred.</returns>
    public IEnumerable<RoadmapOccupierDto>? GetRoadmapOccupiers()
    {
        _logger?.LogTraceIfEnabled("[MapClient] GetRoadmapOccupiers() called");
        try
        {
            GetRoadmapOccupiersRequest request = new();
            _logger?.LogDebugIfEnabled("[MapClient] Sending GetRoadmapOccupiersRequest");
            GetRoadmapOccupiersResult response = _client.GetRoadmapOccupiers(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformationIfEnabled("[MapClient] GetRoadmapOccupiers() succeeded");
                return response.Occupiers;
            }

            _logger?.LogErrorIfEnabled(
                "[MapClient] GetRoadmapOccupiers() failed with {ServiceCode} and message {ExceptionMessage}",
                response.ServiceCode,
                response.ExceptionMessage);
            return null;
        }
        catch (Exception ex)
        {
            _logger?.LogErrorIfEnabled(ex, "[MapClient] Error getting roadmap occupiers");
            return null;
        }
    }

    /// <summary>
    /// Gets the current occupiers committed to roadmap nodes and moves asynchronously.
    /// </summary>
    /// <returns>The roadmap occupiers, or null if an error occurred.</returns>
    public async Task<IEnumerable<RoadmapOccupierDto>?> GetRoadmapOccupiersAsync(
        CancellationToken cancellationToken = default)
    {
        _logger?.LogTraceIfEnabled("[MapClient] GetRoadmapOccupiersAsync() called");
        try
        {
            GetRoadmapOccupiersRequest request = new();
            _logger?.LogDebugIfEnabled("[MapClient] Sending GetRoadmapOccupiersRequest");
            GetRoadmapOccupiersResult response = await _client.GetRoadmapOccupiersAsync(
                request,
                cancellationToken: cancellationToken);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformationIfEnabled("[MapClient] GetRoadmapOccupiersAsync() succeeded");
                return response.Occupiers;
            }

            _logger?.LogErrorIfEnabled(
                "[MapClient] GetRoadmapOccupiersAsync() failed with {ServiceCode} and message {ExceptionMessage}",
                response.ServiceCode,
                response.ExceptionMessage);
            return null;
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger?.LogErrorIfEnabled(ex, "[MapClient] Error getting roadmap occupiers");
            return null;
        }
    }

    /// <summary>
    /// Gets the occupying mandate progress.
    /// </summary>
    /// <returns>The result containing the occupying mandate progress, or null if an error occurred.</returns>
    public OccupyingMandateProgressDto? GetOccupyingMandateProgress()
    {
        _logger?.LogTraceIfEnabled("[MapClient] GetOccupyingMandateProgress() called");
        try
        {
            GetOccupyingMandateRequest request = new();
            _logger?.LogDebugIfEnabled("[MapClient] Sending GetOccupyingMandateRequest");
            GetOccupyingMandateProgressDataResult response = _client.GetOccupyingMandateProgressData(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformationIfEnabled("[MapClient] GetOccupyingMandateProgress() succeeded");
                return response.OccupyingMandateProgress;
            }
            else
            {
                _logger?.LogErrorIfEnabled("[MapClient] GetOccupyingMandateProgress() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogErrorIfEnabled(ex, "[MapClient] Error getting occupying mandate progress");
            return null;
        }
    }

    /// <summary>
    /// Gets the occupying mandate progress asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the occupying mandate progress, or null if an error occurred.</returns>
    public async Task<OccupyingMandateProgressDto?> GetOccupyingMandateProgressAsync()
    {
        _logger?.LogTraceIfEnabled("[MapClient] GetOccupyingMandateProgressAsync() called");
        try
        {
            GetOccupyingMandateRequest request = new();
            _logger?.LogDebugIfEnabled("[MapClient] Sending GetOccupyingMandateRequest");
            GetOccupyingMandateProgressDataResult response = await _client.GetOccupyingMandateProgressDataAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformationIfEnabled("[MapClient] GetOccupyingMandateProgressAsync() succeeded");
                return response.OccupyingMandateProgress;
            }
            else
            {
                _logger?.LogErrorIfEnabled("[MapClient] GetOccupyingMandateProgressAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogErrorIfEnabled(ex, "[MapClient] Error getting occupying mandate progress");
            return null;
        }
    }

    /// <summary>
    /// Gets the trajectory for a specific move.
    /// </summary>
    /// <param name="moveId">The ID of the move.</param>
    /// <returns>The result containing the trajectory, or null if an error occurred.</returns>
    public IEnumerable<WaypointDto>? GetTrajectory(int moveId)
    {
        _logger?.LogTraceIfEnabled("[MapClient] GetTrajectory() called with {MoveId}", moveId);
        try
        {
            GetTrajectoryRequest request = new() { MoveId = moveId };
            _logger?.LogDebugIfEnabled("[MapClient] Sending GetTrajectoryRequest");
            GetTrajectoryResult response = _client.GetTrajectory(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformationIfEnabled("[MapClient] GetTrajectory() succeeded");
                return response.Waypoints;
            }
            else
            {
                _logger?.LogErrorIfEnabled("[MapClient] GetTrajectory() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogErrorIfEnabled(ex, "[MapClient] Error getting trajectory");
            return null;
        }
    }

    /// <summary>
    /// Gets the trajectory for a specific move asynchronously.
    /// </summary>
    /// <param name="moveId">The ID of the move.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the trajectory, or null if an error occurred.</returns>
    public async Task<IEnumerable<WaypointDto>?> GetTrajectoryAsync(int moveId)
    {
        _logger?.LogTraceIfEnabled("[MapClient] GetTrajectoryAsync() called with {MoveId}", moveId);
        try
        {
            GetTrajectoryRequest request = new() { MoveId = moveId };
            _logger?.LogDebugIfEnabled("[MapClient] Sending GetTrajectoryRequest");
            GetTrajectoryResult response = await _client.GetTrajectoryAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformationIfEnabled("[MapClient] GetTrajectoryAsync() succeeded");
                return response.Waypoints;
            }
            else
            {
                _logger?.LogErrorIfEnabled("[MapClient] GetTrajectoryAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogErrorIfEnabled(ex, "[MapClient] Error getting trajectory");
            return null;
        }
    }

    public GetPointCloudResult? GetPointCloud(int maxPoints = 0)
        => Call(
            () => _client.GetPointCloud(new GetPointCloudRequest { MaxPoints = maxPoints }),
            response => response.ServiceCode,
            nameof(GetPointCloud));

    public Task<GetPointCloudResult?> GetPointCloudAsync(int maxPoints = 0)
        => CallAsync(
            () => _client.GetPointCloudAsync(new GetPointCloudRequest { MaxPoints = maxPoints }),
            response => response.ServiceCode,
            nameof(GetPointCloudAsync));

    public IEnumerable<RoadmapOccupierDto>? GetMapItemOccupiers(int mapItemId)
        => Call(
            () => _client.GetMapItemOccupiers(new GetMapItemOccupiersRequest { MapItemId = mapItemId }),
            response => response.ServiceCode,
            nameof(GetMapItemOccupiers))?.Occupiers;

    public async Task<IEnumerable<RoadmapOccupierDto>?> GetMapItemOccupiersAsync(int mapItemId)
        => (await CallAsync(
            () => _client.GetMapItemOccupiersAsync(
                new GetMapItemOccupiersRequest { MapItemId = mapItemId }),
            response => response.ServiceCode,
            nameof(GetMapItemOccupiersAsync)))?.Occupiers;

    public TrajectorySettingsResult? GetTrajectorySettings()
        => Call(
            () => _client.GetTrajectorySettings(new GetTrajectorySettingsRequest()),
            response => response.ServiceCode,
            nameof(GetTrajectorySettings));

    public Task<TrajectorySettingsResult?> GetTrajectorySettingsAsync()
        => CallAsync(
            () => _client.GetTrajectorySettingsAsync(new GetTrajectorySettingsRequest()),
            response => response.ServiceCode,
            nameof(GetTrajectorySettingsAsync));

    public TrajectorySettingsResult? SetTrajectorySettings(double clothoidWaypointSpacingMeters)
        => Call(
            () => _client.SetTrajectorySettings(new SetTrajectorySettingsRequest
            {
                ClothoidWaypointSpacingMeters = clothoidWaypointSpacingMeters
            }),
            response => response.ServiceCode,
            nameof(SetTrajectorySettings));

    public Task<TrajectorySettingsResult?> SetTrajectorySettingsAsync(double clothoidWaypointSpacingMeters)
        => CallAsync(
            () => _client.SetTrajectorySettingsAsync(new SetTrajectorySettingsRequest
            {
                ClothoidWaypointSpacingMeters = clothoidWaypointSpacingMeters
            }),
            response => response.ServiceCode,
            nameof(SetTrajectorySettingsAsync));

    public MapCardDto? GetActiveRoadmap()
        => Call(
            () => _client.GetActiveRoadmap(new GetRoadmapRequest()),
            response => response.ServiceCode,
            nameof(GetActiveRoadmap))?.Roadmap;

    public async Task<MapCardDto?> GetActiveRoadmapAsync()
        => (await CallAsync(
            () => _client.GetActiveRoadmapAsync(new GetRoadmapRequest()),
            response => response.ServiceCode,
            nameof(GetActiveRoadmapAsync)))?.Roadmap;

    public MapCardDto? GetNextRoadmap()
        => Call(
            () => _client.GetNextRoadmap(new GetRoadmapRequest()),
            response => response.ServiceCode,
            nameof(GetNextRoadmap))?.Roadmap;

    public async Task<MapCardDto?> GetNextRoadmapAsync()
        => (await CallAsync(
            () => _client.GetNextRoadmapAsync(new GetRoadmapRequest()),
            response => response.ServiceCode,
            nameof(GetNextRoadmapAsync)))?.Roadmap;

    public IEnumerable<MapCardDto>? GetRoadmaps()
        => Call(
            () => _client.GetRoadmaps(new GetRoadmapRequest()),
            response => response.ServiceCode,
            nameof(GetRoadmaps))?.Roadmaps;

    public async Task<IEnumerable<MapCardDto>?> GetRoadmapsAsync()
        => (await CallAsync(
            () => _client.GetRoadmapsAsync(new GetRoadmapRequest()),
            response => response.ServiceCode,
            nameof(GetRoadmapsAsync)))?.Roadmaps;

    public IEnumerable<RoadmapCatalogItemDto>? GetRoadmapCatalog()
        => Call(
            () => _client.GetRoadmapCatalog(new GetRoadmapRequest()),
            response => response.ServiceCode,
            nameof(GetRoadmapCatalog))?.Catalog;

    public async Task<IEnumerable<RoadmapCatalogItemDto>?> GetRoadmapCatalogAsync()
        => (await CallAsync(
            () => _client.GetRoadmapCatalogAsync(new GetRoadmapRequest()),
            response => response.ServiceCode,
            nameof(GetRoadmapCatalogAsync)))?.Catalog;

    public GetRoadmapOverviewResult? GetRoadmapOverview()
        => Call(
            () => _client.GetRoadmapOverview(new GetRoadmapRequest()),
            response => response.ServiceCode,
            nameof(GetRoadmapOverview));

    public Task<GetRoadmapOverviewResult?> GetRoadmapOverviewAsync()
        => CallAsync(
            () => _client.GetRoadmapOverviewAsync(new GetRoadmapRequest()),
            response => response.ServiceCode,
            nameof(GetRoadmapOverviewAsync));

    public bool ActivateRoadmap(int id)
        => Call(
            () => _client.ActivateRoadmap(new RoadmapIdRequest { Id = id }),
            response => response.ServiceCode,
            nameof(ActivateRoadmap)) != null;

    public async Task<bool> ActivateRoadmapAsync(int id)
        => await CallAsync(
            () => _client.ActivateRoadmapAsync(new RoadmapIdRequest { Id = id }),
            response => response.ServiceCode,
            nameof(ActivateRoadmapAsync)) != null;

    public bool DeleteRoadmap(int id)
        => Call(
            () => _client.DeleteRoadmap(new RoadmapIdRequest { Id = id }),
            response => response.ServiceCode,
            nameof(DeleteRoadmap)) != null;

    public async Task<bool> DeleteRoadmapAsync(int id)
        => await CallAsync(
            () => _client.DeleteRoadmapAsync(new RoadmapIdRequest { Id = id }),
            response => response.ServiceCode,
            nameof(DeleteRoadmapAsync)) != null;

    public async Task<RoadmapUploadResultDto?> UploadRoadmapAsync(
        Stream contents,
        string fileName,
        string changeMessage,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(contents);
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName);
        ArgumentException.ThrowIfNullOrWhiteSpace(changeMessage);

        try
        {
            using AsyncClientStreamingCall<RoadmapUploadChunk, RoadmapUploadResultDto> call =
                _client.UploadRoadmap(cancellationToken: cancellationToken);
            await WriteChunksAsync(
                contents,
                data => call.RequestStream.WriteAsync(new RoadmapUploadChunk
                {
                    Data = data,
                    FileName = Path.GetFileName(fileName),
                    ChangeMessage = changeMessage
                }),
                cancellationToken);
            await call.RequestStream.CompleteAsync();
            return await call.ResponseAsync;
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger?.LogErrorIfEnabled(ex, "[MapClient] Error uploading roadmap");
            return null;
        }
    }

    public async Task<byte[]?> ExportTransitrakAsync(
        Stream contents,
        string fileName,
        string exportName,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(contents);
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName);
        ArgumentException.ThrowIfNullOrWhiteSpace(exportName);

        try
        {
            using AsyncDuplexStreamingCall<TransitrakExportChunk, FileChunk> call =
                _client.ExportTransitrak(cancellationToken: cancellationToken);
            await WriteChunksAsync(
                contents,
                data => call.RequestStream.WriteAsync(new TransitrakExportChunk
                {
                    Data = data,
                    FileName = Path.GetFileName(fileName),
                    ExportName = exportName
                }),
                cancellationToken);
            await call.RequestStream.CompleteAsync();
            return await ReadChunksAsync(call.ResponseStream, cancellationToken);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger?.LogErrorIfEnabled(ex, "[MapClient] Error exporting roadmap to traNsitrak");
            return null;
        }
    }

    public async Task<byte[]?> DownloadRoadmapAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            using AsyncServerStreamingCall<FileChunk> call = _client.DownloadRoadmap(
                new RoadmapIdRequest { Id = id },
                cancellationToken: cancellationToken);
            return await ReadChunksAsync(call.ResponseStream, cancellationToken);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger?.LogErrorIfEnabled(ex, "[MapClient] Error downloading roadmap");
            return null;
        }
    }

    public async Task<byte[]?> DownloadActiveRoadmapAsync(
        CancellationToken cancellationToken = default)
    {
        try
        {
            using AsyncServerStreamingCall<FileChunk> call = _client.DownloadActiveRoadmap(
                new GetRoadmapRequest(),
                cancellationToken: cancellationToken);
            return await ReadChunksAsync(call.ResponseStream, cancellationToken);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger?.LogErrorIfEnabled(ex, "[MapClient] Error downloading active roadmap");
            return null;
        }
    }

    private TResponse? Call<TResponse>(
        Func<TResponse> call,
        Func<TResponse, int> serviceCode,
        string operation)
        where TResponse : class
    {
        try
        {
            TResponse response = call();
            int code = serviceCode(response);
            if (code == (int)ServiceCode.NoError)
                return response;

            _logger?.LogErrorIfEnabled(
                "[MapClient] {Operation} failed with {ServiceCode}",
                operation,
                code);
            return null;
        }
        catch (Exception ex)
        {
            _logger?.LogErrorIfEnabled(ex, "[MapClient] {Operation} failed", operation);
            return null;
        }
    }

    private async Task<TResponse?> CallAsync<TResponse>(
        Func<AsyncUnaryCall<TResponse>> call,
        Func<TResponse, int> serviceCode,
        string operation)
        where TResponse : class
    {
        try
        {
            TResponse response = await call();
            int code = serviceCode(response);
            if (code == (int)ServiceCode.NoError)
                return response;

            _logger?.LogErrorIfEnabled(
                "[MapClient] {Operation} failed with {ServiceCode}",
                operation,
                code);
            return null;
        }
        catch (Exception ex)
        {
            _logger?.LogErrorIfEnabled(ex, "[MapClient] {Operation} failed", operation);
            return null;
        }
    }

    private static async Task WriteChunksAsync(
        Stream contents,
        Func<ByteString, Task> write,
        CancellationToken cancellationToken)
    {
        byte[] buffer = new byte[FileChunkSize];
        int bytesRead;
        while ((bytesRead = await contents.ReadAsync(buffer, cancellationToken)) > 0)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await write(ByteString.CopyFrom(buffer, 0, bytesRead));
        }
    }

    private static async Task<byte[]> ReadChunksAsync(
        IAsyncStreamReader<FileChunk> responseStream,
        CancellationToken cancellationToken)
    {
        await using MemoryStream output = new();
        while (await responseStream.MoveNext(cancellationToken))
            responseStream.Current.Data.WriteTo(output);
        return output.ToArray();
    }

    /// <summary>
    /// Sets the occupying mandate.
    /// </summary>
    /// <param name="mapItemIds">The IDs of the map items.</param>
    /// <param name="timeout">The timeout duration.</param>
    /// <returns>True if the operation succeeded, otherwise false.</returns>
    public bool SetOccupyingMandate(HashSet<int> mapItemIds, TimeSpan timeout)
    {
        _logger?.LogTraceIfEnabled("[MapClient] SetOccupyingMandate() called with {MapItemIds} and {Timeout}", mapItemIds, timeout);
        try
        {
            SetOccupyingMandateRequest request = new()
            {
                MapItemIds = { mapItemIds },
                Timeout = Google.Protobuf.WellKnownTypes.Duration.FromTimeSpan(timeout)
            };
            _logger?.LogDebugIfEnabled("[MapClient] Sending SetOccupyingMandateRequest");
            GenericResult response = _client.SetOccupyingMandate(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformationIfEnabled("[MapClient] SetOccupyingMandate() succeeded");
                return true;
            }
            else
            {
                _logger?.LogErrorIfEnabled("[MapClient] SetOccupyingMandate() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogErrorIfEnabled(ex, "[MapClient] Error setting occupying mandate");
            return false;
        }
    }

    /// <summary>
    /// Sets the occupying mandate asynchronously.
    /// </summary>
    /// <param name="mapItemIds">The IDs of the map items.</param>
    /// <param name="timeout">The timeout duration.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains true if the operation succeeded, otherwise false.</returns>
    public async Task<bool> SetOccupyingMandateAsync(HashSet<int> mapItemIds, TimeSpan timeout)
    {
        _logger?.LogTraceIfEnabled("[MapClient] SetOccupyingMandateAsync() called with {MapItemIds} and {Timeout}", mapItemIds, timeout);
        try
        {
            SetOccupyingMandateRequest request = new()
            {
                MapItemIds = { mapItemIds },
                Timeout = Google.Protobuf.WellKnownTypes.Duration.FromTimeSpan(timeout)
            };
            _logger?.LogDebugIfEnabled("[MapClient] Sending SetOccupyingMandateRequest");
            GenericResult response = await _client.SetOccupyingMandateAsync(request);
            if (response.ServiceCode == (int)ServiceCode.NoError)
            {
                _logger?.LogInformationIfEnabled("[MapClient] SetOccupyingMandateAsync() succeeded");
                return true;
            }
            else
            {
                _logger?.LogErrorIfEnabled("[MapClient] SetOccupyingMandateAsync() failed with {ServiceCode} and message {ExceptionMessage}", response.ServiceCode, response.ExceptionMessage);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogErrorIfEnabled(ex, "[MapClient] Error setting occupying mandate");
            return false;
        }
    }

    /// <summary>
    /// Unsubscribe from occupying mandate progress updates.
    /// </summary>
    public void Unsubscribe()
    {
        _cts?.Cancel();
    }

    /// <summary>
    /// Subscribes to occupying mandate progress updates and raises the OccupyingMandateProgressUpdated event when an update is received.
    /// </summary>
    private async Task Subscribe()
    {
        _logger?.LogTraceIfEnabled("[MapClient] Subscribe() started");
        _cts = new();
        while (!_cts.IsCancellationRequested)
        {
            try
            {
                MapSubscribeRequest request = new();
                _logger?.LogDebugIfEnabled("[MapClient] Sending MapSubscribeRequest");
                using AsyncServerStreamingCall<OccupyingMandateProgressDto> streamingCall = _client.Subscribe(request);

                await foreach (OccupyingMandateProgressDto? occupyingMandateProgressDto in streamingCall.ResponseStream.ReadAllAsync(_cts.Token))
                {
                    _logger?.LogTraceIfEnabled("[MapClient] Received OccupyingMandateProgressDto: {OccupyingMandateProgressDto}", occupyingMandateProgressDto);
                    SubscriptionCallbackDispatcher.Invoke(
                        OccupyingMandateProgressUpdated,
                        occupyingMandateProgressDto,
                        _logger,
                        nameof(MapClient));
                }
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
            {
                _logger?.LogInformationIfEnabled("[MapClient] Subscription cancelled");
                break;
            }
            catch (Exception ex)
            {
                _logger?.LogWarningIfEnabled(ex, "[MapClient] Exception during subscription. Retrying...");
                await Task.Delay(1000);
            }
        }
        _logger?.LogTraceIfEnabled("[MapClient] Subscribe() ended");
    }

    /// <summary>
    /// Disposes of the client resources.
    /// </summary>
    /// <param name="disposing">Indicates whether the method is called from the Dispose method or from a finalizer.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed)
            return;

        if (disposing)
        {
            _logger?.LogTraceIfEnabled("[MapClient] Disposing resources");
            Unsubscribe();
            _cts.Dispose();
        }

        _isDisposed = true;
        _logger?.LogInformationIfEnabled("[MapClient] MapClient disposed");
    }

    /// <summary>
    /// Disposes of the client, releasing all managed resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
