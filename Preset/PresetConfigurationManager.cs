using AssettoServer.Server;
using AssettoServer.Server.Configuration;
using IniParser;
using IniParser.Model;
using nvrlift.AssettoServer.Restart;
using Polly;
using Serilog;
using VotingTrackPlugin;

namespace nvrlift.AssettoServer.Preset;

public class PresetConfigurationManager
{
    public PresetConfiguration CurrentConfiguration { get; }
    public List<PresetConfiguration> Configurations { get; }
    public List<PresetType> PresetTypes { get; }

    public PresetConfigurationManager(ACServerConfiguration acServerConfiguration)
    {
        CurrentConfiguration = PresetConfiguration.FromFile(acServerConfiguration.BaseFolder);

        var configs = new List<PresetConfiguration>();
        var directories = Directory.GetDirectories("presets");
        foreach (var dir in directories)
        {
            configs.Add(PresetConfiguration.FromFile(Path.Join(dir, "preset_cfg.yml")));
        }

        Configurations = configs;

        var types = new List<PresetType>();
        foreach (var conf in Configurations)
        {
            types.Add(conf.ToPresetType());
        }

        PresetTypes = types;
    }
}
