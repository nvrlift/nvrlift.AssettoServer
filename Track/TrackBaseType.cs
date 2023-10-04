using System.ComponentModel;

namespace nvrlift.AssettoServer.Track;

public class TrackBaseType
{
    public string Name { get; set; }
    public string TrackFolder { get; set; }
    public string TrackLayoutConfig { get; set; }
    public string CMLink { get; set; } = "";
    public string CMVersion { get; set; } = "";
}
