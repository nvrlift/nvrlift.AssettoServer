using AssettoServer.Server.Plugin;
using Autofac;

namespace nvrlift.AssettoServer;

public class NvrliftBaseModule : AssettoServerModule<VotingTrackConfiguration>
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<VotingTrack>().AsSelf().As<IAssettoServerAutostart>().SingleInstance();
    }
}
