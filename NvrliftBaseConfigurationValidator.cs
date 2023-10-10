using FluentValidation;
using JetBrains.Annotations;

namespace nvrlift.AssettoServer;

[UsedImplicitly]
public class NvrliftBaseConfigurationValidator : AbstractValidator<NvrliftBaseConfiguration>
{
    public NvrliftBaseConfigurationValidator()
    {
    }
}
