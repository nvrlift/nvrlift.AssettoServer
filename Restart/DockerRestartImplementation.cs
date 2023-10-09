using AssettoServer.Server.Configuration;

namespace nvrlift.AssettoServer.Restart;

public class DockerRestartImplementation : IRestartImplementation
{
    private readonly ACServerConfiguration _acServerConfiguration;

    public DockerRestartImplementation(ACServerConfiguration acServerConfiguration)
    {
        _acServerConfiguration = acServerConfiguration;
    }

    public void InitiateRestart(string preset)
    {
        throw new NotImplementedException();
    }
}
