using AssettoServer.Server;
using AssettoServer.Server.Configuration;
using IniParser;
using IniParser.Model;
using nvrlift.AssettoServer.Restart;
using Polly;
using Serilog;

namespace nvrlift.AssettoServer.Track;

public class TrackImplementation
{
    private readonly IRestartImplementation _restartImplementation;
    private readonly ACServerConfiguration _acServerConfiguration;
    private readonly SessionManager _sessionManager;
    private readonly EntryCarManager _entryCarManager;
    private readonly ChecksumManager _checksumManager;

    public TrackImplementation(SessionManager sessionManager,
        ACServerConfiguration acServerConfiguration, EntryCarManager entryCarManager, ChecksumManager checksumManager, IRestartImplementation restartImplementation)
    {
        _acServerConfiguration = acServerConfiguration;
        _entryCarManager = entryCarManager;
        _checksumManager = checksumManager;
        _restartImplementation = restartImplementation;
        _sessionManager = sessionManager;
    }

    public void ChangeTrack(TrackData track)
    {
        // Next Session
        Log.Information("Kicking all clients for track change.");
        foreach (var client in _entryCarManager.EntryCars.Select(c => c.Client))
        {
            if (client == null) continue;
            _entryCarManager.KickAsync(client, "SERVER RESTART").GetAwaiter().GetResult();
            Log.Information($"Kicking {client.Name} for Server reset.");
            //client?.SendPacket(new LuaReconnectClients());
        }
        // Notify about restart
        Log.Information($"Restarting server");
        _restartImplementation.InitiateRestart(track.UpcomingType!.PresetFolder);
        
        // _checksumManager.Initialize();
        // _sessionManager.NextSession();
    }
}
