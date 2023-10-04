namespace nvrlift.AssettoServer.Track;

public class TrackData
{
    public TrackBaseType? Type { get; set; }
    public TrackBaseType? UpcomingType { get; set; }
    public double TransitionDuration { get; set; }
    public bool UpdateContentManager { get; set; }
    public bool IsInit { get; set; }
    public TrackData(TrackBaseType? type, TrackBaseType? upcomingType)
    {
        Type = type;
        UpcomingType = upcomingType;
    }
}
