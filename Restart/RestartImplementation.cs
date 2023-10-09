using System.Text;
using AssettoServer.Server;
using AssettoServer.Server.Configuration;
using AssettoServer.Shared.Network.Packets.Outgoing;
using Serilog;
using VotingTrackPlugin;

namespace nvrlift.AssettoServer.Restart;

public class RestartImplementation
{
    private readonly ACServerConfiguration _acServerConfiguration;
    private readonly EntryCarManager _entryCarManager;

    public RestartImplementation(ACServerConfiguration acServerConfiguration, EntryCarManager entryCarManager)
    {
        _acServerConfiguration = acServerConfiguration;
        _entryCarManager = entryCarManager;
    }

    public void InitiateRestart(string preset, RestartType type)
    {
        //Kick clients
        Log.Information("Kicking all clients for restart.");
        foreach (var entryCar in _entryCarManager.EntryCars)
        {
            var client = entryCar.Client;
            if (client != null)
            {
                _entryCarManager.KickAsync(client, "Server restarting").GetAwaiter().GetResult();
            }
        }

        switch (type)
        {
            case RestartType.WindowsFile:
            {
                var restartPath = Path.Join(_acServerConfiguration.BaseFolder, "restart", $"{Environment.ProcessId}.asrestart");
                Log.Information($"Trying to create restart file: {restartPath}");
                var restartFile = File.Create(restartPath);
                byte[] content = new UTF8Encoding(true).GetBytes(preset);
                restartFile.Write(content, 0, content.Length);
                restartFile.Close();
                break;
            }
            case RestartType.Docker:
            {
                throw new NotImplementedException();
            }
            default:
            {
                throw new NotImplementedException();
            }
        }
            
    }
}
