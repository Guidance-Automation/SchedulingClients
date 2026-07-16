using GAAPICommon;
using GAAPICommon.Enums;
using GAAPICommon.Messages;
using GAAPICommon.Services.Maps;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Guidance.SchedulingClients.Maps;

/// <summary>
/// Client for interacting with the Map service.
/// </summary>
public class MapClient : IMapClient
{
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
                    OccupyingMandateProgressUpdated?.Invoke(occupyingMandateProgressDto);
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
