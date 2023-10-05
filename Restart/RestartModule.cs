using Autofac;
using nvrlift.AssettoServer.ContentManager;
using nvrlift.AssettoServer.Track;
using VotingTrackPlugin;

namespace nvrlift.AssettoServer.Restart;

public class RestartModule
{
    private readonly NvrliftBaseConfiguration _configuration;

    public RestartModule(NvrliftBaseConfiguration configuration, ContainerBuilder builder)
    {
        _configuration = configuration;
        Load(builder);
    }
    
    protected void Load(ContainerBuilder builder)
    {
        if(_configuration.Restart == RestartType.Disabled) { }
        else if (_configuration.Restart == RestartType.WindowsFile)
            builder.RegisterType<WindowsFileRestartImplementation>().As<IRestartImplementation>().SingleInstance();
        else if (_configuration.Restart == RestartType.Docker)
            builder.RegisterType<DockerRestartImplementation>().As<IRestartImplementation>().SingleInstance();
    }
}
