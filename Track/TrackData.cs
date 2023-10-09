namespace nvrlift.AssettoServer.Track;

public class TrackData
{
    public ITrackBaseType? Type { get; set; }
    public ITrackBaseType? UpcomingType { get; set; }
    public double TransitionDuration { get; set; }
    public bool IsInit { get; set; }

    public TrackData(ITrackBaseType? type, ITrackBaseType? upcomingType)
    {
        Type = type;
        UpcomingType = upcomingType;
    }
}
