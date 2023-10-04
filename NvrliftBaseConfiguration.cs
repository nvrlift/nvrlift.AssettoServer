using AssettoServer.Server.Configuration;
using JetBrains.Annotations;
using YamlDotNet.Serialization;
using nvrlift.AssettoServer.Track;

namespace VotingTrackPlugin;

[UsedImplicitly(ImplicitUseKindFlags.Assign, ImplicitUseTargetFlags.WithMembers)]
public class NvrliftBaseConfiguration : IValidateConfiguration<NvrliftBaseConfigurationValidator>
{
    
    public bool ContentManager { get; init; } = false;
    public bool Track { get; init; } = false;

}
