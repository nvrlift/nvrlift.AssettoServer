using System.ComponentModel;

namespace nvrlift.AssettoServer.Track;

public class TrackBaseType
{
    public required string Name { get; set; }
    public required string TrackFolder { get; set; }
    public required string TrackLayoutConfig { get; set; }
    public string CMLink { get; set; } = "";
    public string CMVersion { get; set; } = "";
    public float Weight { get; set; } = 1.0f;
}
