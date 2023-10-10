using AssettoServer.Shared.Services;
using Microsoft.Extensions.Hosting;
using Serilog;
using VotingTrackPlugin;

namespace nvrlift.AssettoServer.Track;

public class TrackManager : CriticalBackgroundService
{
    private readonly TrackImplementation _trackImplementation;

    public TrackManager(TrackImplementation trackImplementation,
        IHostApplicationLifetime applicationLifetime) : base(applicationLifetime)
    {
        _trackImplementation = trackImplementation;
    }
    private RestartType _restartType = RestartType.Disabled;

    public void SetRestartType(RestartType restartType)
    {
        _restartType = restartType;
    }

    public TrackData CurrentTrack { get; private set; } = null!;

    public void SetTrack(TrackData track, RestartType restartType)
    {
        CurrentTrack = track;

        if (!CurrentTrack.IsInit)
            UpdateTrack(restartType);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                if(_restartType != RestartType.Disabled)
                    UpdateTrack(_restartType);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in track service update");
            }
            finally
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }

    private void UpdateTrack(RestartType restartType)
    {
        if (CurrentTrack.UpcomingType != null || !CurrentTrack.Type!.Equals(CurrentTrack.UpcomingType!))
        {
            Log.Information($"Track change to '{CurrentTrack.UpcomingType!.Name}' initiated");
            _trackImplementation.ChangeTrack(CurrentTrack, restartType);

            CurrentTrack.Type = CurrentTrack.UpcomingType;
            CurrentTrack.UpcomingType = null;
        }
    }
}
