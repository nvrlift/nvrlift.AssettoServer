using AssettoServer.Server.Plugin;
using Autofac;
using nvrlift.AssettoServer.ContentManager;
using nvrlift.AssettoServer.Restart;
using nvrlift.AssettoServer.Track;
using VotingTrackPlugin;

namespace nvrlift.AssettoServer;

public class NvrliftBaseModule : AssettoServerModule<NvrliftBaseConfiguration> //Module
{
    /*
    private readonly NvrliftBaseConfiguration _configuration;

    public NvrliftBaseModule(NvrliftBaseConfiguration configuration)
    {
        _configuration = configuration;
    } */

    protected override void Load(ContainerBuilder builder)
    {
        /*
        if(_configuration.Restart == RestartType.Disabled) { }
        else if (_configuration.Restart == RestartType.WindowsFile)
            builder.RegisterType<WindowsFileRestartImplementation>().As<IRestartImplementation>().SingleInstance();
        else if (_configuration.Restart == RestartType.Docker)
            builder.RegisterType<DockerRestartImplementation>().As<IRestartImplementation>().SingleInstance();
            */

        builder.RegisterType<WindowsFileRestartImplementation>().As<IRestartImplementation>().SingleInstance();

        builder.RegisterType<TrackImplementation>().AsSelf().SingleInstance();
        builder.RegisterType<TrackManager>().AsSelf().SingleInstance();
        builder.RegisterType<ContentManagerImplementation>().AsSelf().SingleInstance();
        // if (_configuration.Track)  { } 
        // if (_configuration.ContentManager) { }
    }
}
