using AssettoServer.Server.Configuration;
using Serilog;

namespace nvrlift.AssettoServer.Restart;

public class WindowsFileRestartImplementation : IRestartImplementation
{
    private readonly ACServerConfiguration _acServerConfiguration;

    public WindowsFileRestartImplementation(ACServerConfiguration acServerConfiguration)
    {
        _acServerConfiguration = acServerConfiguration;
    }

    public void InitiateRestart()
    {
        
        var restartPath = Path.Join(_acServerConfiguration.BaseFolder, "restart", $"{Environment.ProcessId}.asrestart");
        Log.Information($"Trying to create restart file: {restartPath}");
        var restartFile = File.Create(restartPath);
        restartFile.Close();
    }
}
