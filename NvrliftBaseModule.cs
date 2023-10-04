using AssettoServer.Server.Plugin;
using Autofac;
using nvrlift.AssettoServer.ContentManager;
using nvrlift.AssettoServer.Track;
using VotingTrackPlugin;

namespace nvrlift.AssettoServer;

public class NvrliftBaseModule : AssettoServerModule<NvrliftBaseConfiguration> // Module
{
    /*private readonly NvrliftBaseConfiguration _configuration;

    public NvrliftBaseModule(NvrliftBaseConfiguration configuration)
    {
        _configuration = configuration;
    } */
    
    protected override void Load(ContainerBuilder builder)
    {
        //if (_configuration.Track) {
            builder.RegisterType<TrackImplementation>().AsSelf().SingleInstance();
            builder.RegisterType<TrackManager>().AsSelf().SingleInstance();
        //} if (_configuration.ContentManager) {
            builder.RegisterType<ContentManagerImplementation>().AsSelf().SingleInstance();
        //}
    }
}
