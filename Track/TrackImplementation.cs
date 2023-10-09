using AssettoServer.Server;
using AssettoServer.Server.Configuration;
using IniParser;
using IniParser.Model;
using nvrlift.AssettoServer.ContentManager;
using nvrlift.AssettoServer.Restart;
using Polly;
using Serilog;

namespace nvrlift.AssettoServer.Track;

public class TrackImplementation
{
    private readonly IRestartImplementation _restartImplementation;
    private readonly ContentManagerImplementation _contentManagerImplementation;
    private readonly ACServerConfiguration _acServerConfiguration;
    private readonly SessionManager _sessionManager;
    private readonly EntryCarManager _entryCarManager;
    private readonly ChecksumManager _checksumManager;

    public TrackImplementation(ContentManagerImplementation contentManagerImplementation,
        SessionManager sessionManager,
        ACServerConfiguration acServerConfiguration, EntryCarManager entryCarManager, ChecksumManager checksumManager, IRestartImplementation restartImplementation)
    {
        _contentManagerImplementation = contentManagerImplementation;
        _acServerConfiguration = acServerConfiguration;
        _entryCarManager = entryCarManager;
        _checksumManager = checksumManager;
        _restartImplementation = restartImplementation;
        _sessionManager = sessionManager;
    }

    public void ChangeTrack(TrackData track)
    {
        var iniPath = Path.Join(_acServerConfiguration.BaseFolder, "server_cfg.ini");
        if (File.Exists(iniPath))
        {
            Log.Information("'server_cfg.ini' found, track change starting...");

            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(iniPath);

            // I am replicating ACServerConfiguration.Server
            // [IniField("SERVER", "TRACK")] public string Track { get; init; } = "";
            // [IniField("SERVER", "CONFIG_TRACK")] public string TrackConfig { get; init; } = "";
            data["SERVER"]["TRACK"] = track.UpcomingType!.TrackFolder;
            data["SERVER"]["CONFIG_TRACK"] = track.UpcomingType!.TrackLayoutConfig;

            Log.Information($"Writing track change to server_cfg.ini");
            parser.WriteFile(iniPath, data);
        }
        else
        {
            Log.Error("Couldn't change track, 'server_cfg.ini' not found.");
            return;
        }

        // Content Manager Changes
        if (track.ContentManager)
            if (_contentManagerImplementation.UpdateTrackConfig(track.UpcomingType))
                Log.Information("ContentManager configuration updated.");
            else
                Log.Error("Failed to update ContentManager configuration.");

        
        // Next Session
        Log.Information("Reconnecting all clients track change.");
        foreach (var client in _entryCarManager.EntryCars.Select(c => c.Client))
        {
            if (client == null) continue;
            _entryCarManager.KickAsync(client, "SERVER RESTART").GetAwaiter().GetResult();
            Log.Information($"Kicking {client.Name} for Server reset.");
            //client?.SendPacket(new LuaReconnectClients());
        }
        // Notify about restart
        Log.Information($"Restarting server");
        _restartImplementation.InitiateRestart();
        
        // _checksumManager.Initialize();
        // _sessionManager.NextSession();
    }
}
