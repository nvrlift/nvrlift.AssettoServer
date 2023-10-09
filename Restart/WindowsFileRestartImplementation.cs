using AssettoServer.Server;
using AssettoServer.Server.Configuration;
using AssettoServer.Shared.Network.Packets.Outgoing;
using Serilog;

namespace nvrlift.AssettoServer.Restart;

public class WindowsFileRestartImplementation : IRestartImplementation
{
    private readonly ACServerConfiguration _acServerConfiguration;
    private readonly EntryCarManager _entryCarManager;

    public WindowsFileRestartImplementation(ACServerConfiguration acServerConfiguration, EntryCarManager entryCarManager)
    {
        _acServerConfiguration = acServerConfiguration;
        _entryCarManager = entryCarManager;
    }

    public void InitiateRestart()
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
            
        var restartPath = Path.Join(_acServerConfiguration.BaseFolder, "restart", $"{Environment.ProcessId}.asrestart");
        Log.Information($"Trying to create restart file: {restartPath}");
        var restartFile = File.Create(restartPath);
        restartFile.Close();
    }
}
