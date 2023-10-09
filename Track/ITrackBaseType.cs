using System.ComponentModel;

namespace nvrlift.AssettoServer.Track;

public interface ITrackBaseType
{
    public string Name { get; set; }
    public string PresetFolder { get; set; }

    public bool Equals(ITrackBaseType compare)
    {
        if (PresetFolder == compare.PresetFolder)
            return true;

        return false;
    }
}
