using AssettoServer.Server;
using AssettoServer.Server.Configuration;
using nvrlift.AssettoServer.Restart;
using Serilog;

namespace nvrlift.AssettoServer.Track;

public class TrackImplementation
{
    private readonly RestartImplementation _restartImplementation;
    private readonly ACServerConfiguration _acServerConfiguration;
    private readonly SessionManager _sessionManager;
    private readonly EntryCarManager _entryCarManager;
    private readonly ChecksumManager _checksumManager;

    public TrackImplementation(SessionManager sessionManager,
        ACServerConfiguration acServerConfiguration, EntryCarManager entryCarManager, ChecksumManager checksumManager, RestartImplementation restartImplementation)
    {
        _acServerConfiguration = acServerConfiguration;
        _entryCarManager = entryCarManager;
        _checksumManager = checksumManager;
        _restartImplementation = restartImplementation;
        _sessionManager = sessionManager;
    }

    public void ChangeTrack(TrackData track, RestartType restartType)
    {
        // Next Session
        Log.Information("Kicking all clients for track change.");
        foreach (var client in _entryCarManager.EntryCars.Select(c => c.Client))
        {
            if (client == null) continue;
            
            /*
            // Kick all clients for restart 
            _entryCarManager.KickAsync(client, "SERVER RESTART").GetAwaiter().GetResult();
            Log.Information($"Kicking {client.Name} for Server reset.");
            */
            
            //client?.SendPacket(new LuaReconnectClients());
        }
        // Notify about restart
        Log.Information($"Restarting server");
        _restartImplementation.InitiateRestart(track.UpcomingType!.PresetFolder, restartType);
        
        // _checksumManager.Initialize();
        // _sessionManager.NextSession();
    }
}
