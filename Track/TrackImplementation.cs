using AssettoServer.Server.Configuration;
using IniParser;
using IniParser.Model;
using nvrlift.AssettoServer.ContentManager;
using nvrlift.AssettoServer.Restart;
using Serilog;

namespace nvrlift.AssettoServer.Track;

public class TrackImplementation
{
    private readonly ContentManagerImplementation _contentManagerImplementation;
    private readonly ACServerConfiguration _acServerConfiguration;
    private readonly IRestartImplementation _restartImplementation;

    public TrackImplementation(ContentManagerImplementation contentManagerImplementation,
        IRestartImplementation restartImplementation,
        ACServerConfiguration acServerConfiguration)
    {
        _contentManagerImplementation = contentManagerImplementation;
        _acServerConfiguration = acServerConfiguration;
        _restartImplementation = restartImplementation;
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

        // Notify about restart
        Log.Information($"Restarting server");
        
        // Restart Server
        _restartImplementation.InitiateRestart();
    }
}
