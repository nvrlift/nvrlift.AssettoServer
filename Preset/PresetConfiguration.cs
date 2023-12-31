using JetBrains.Annotations;
using Serilog;
using YamlDotNet.Serialization;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace nvrlift.AssettoServer.Preset;

[UsedImplicitly(ImplicitUseKindFlags.Assign, ImplicitUseTargetFlags.WithMembers)]
public class PresetConfiguration
{
    public required string Name { get; set; }
    public RandomTrackPresetEntry? RandomTrack { get; set; }
    public VotingTrackPresetEntry? VotingTrack { get; set; }
    [YamlIgnore] public string PresetFolder { get; set; }
    [YamlIgnore] public string Path { get; set; }
    

    public bool Equals(PresetConfiguration compare)
    {
        if (PresetFolder == compare.PresetFolder)
            return true;

        return false;
    }

    public PresetType ToPresetType()
    {
        return new PresetType()
        {
            Name = Name,
            PresetFolder = PresetFolder,
            Weight = RandomTrack?.Weight ?? 1.0f,
        };
    }
    
    public static PresetConfiguration FromFile(string path)
    {
        Log.Information($"Loading Config for preset '{path}'");
        using var stream = File.OpenText(path);

        var deserializer = new DeserializerBuilder()
            .IgnoreUnmatchedProperties()
            .Build();

        var yamlParser = new Parser(stream);
        yamlParser.Consume<StreamStart>();
        yamlParser.Accept<DocumentStart>(out _);

        var cfg = deserializer.Deserialize<PresetConfiguration>(yamlParser);

        cfg.Path = path;
        cfg.PresetFolder = System.IO.Path.GetDirectoryName(path)!;
        Log.Debug($"-- {path}.PresetFolder '{cfg.PresetFolder}'");
        
        return cfg;
    }
}

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class RandomTrackPresetEntry
{
    public bool Enabled { get; set; } = false;
    public float Weight { get; set; } = 1.0f;
}

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class VotingTrackPresetEntry
{
    public bool Enabled { get; set; } = false;
}

