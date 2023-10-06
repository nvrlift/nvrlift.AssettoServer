using AssettoServer.Server.Configuration;

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
        var restartPath = Path.Join(_acServerConfiguration.BaseFolder, $"{Environment.ProcessId}.asrestart");
        var restartFile = File.Create(restartPath);
        restartFile.Close();
    }
}
