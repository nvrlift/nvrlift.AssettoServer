using AssettoServer.Server.Configuration;
using JetBrains.Annotations;
using YamlDotNet.Serialization;
using nvrlift.AssettoServer.Track;

namespace VotingTrackPlugin;

[UsedImplicitly(ImplicitUseKindFlags.Assign, ImplicitUseTargetFlags.WithMembers)]
public class NvrliftBaseConfiguration : IValidateConfiguration<NvrliftBaseConfigurationValidator>
{
    public RestartType Restart { get; init; } = RestartType.Disabled;
}

public enum RestartType
{
    Disabled,
    WindowsFile,
    Docker
}
