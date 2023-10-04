using FluentValidation;
using JetBrains.Annotations;

namespace VotingTrackPlugin;

[UsedImplicitly]
public class NvrliftBaseConfigurationValidator : AbstractValidator<NvrliftBaseConfiguration>
{
    public NvrliftBaseConfigurationValidator()
    {
    }
}
