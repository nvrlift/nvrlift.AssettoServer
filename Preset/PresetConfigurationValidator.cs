using FluentValidation;
using JetBrains.Annotations;

namespace nvrlift.AssettoServer.Preset;

[UsedImplicitly]
public class PresetConfigurationValidator : AbstractValidator<PresetConfiguration>
{
    public PresetConfigurationValidator()
    {
    }
}
