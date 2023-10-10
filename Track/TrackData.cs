using nvrlift.AssettoServer.Preset;

namespace nvrlift.AssettoServer.Track;

public class TrackData
{
    public PresetType? Type { get; set; }
    public PresetType? UpcomingType { get; set; }
    public double TransitionDuration { get; set; }
    public bool IsInit { get; set; }

    public TrackData(PresetType? type, PresetType? upcomingType)
    {
        Type = type;
        UpcomingType = upcomingType;
    }
}
