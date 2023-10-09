using AssettoServer.Server;
using AssettoServer.Server.Configuration;
using AssettoServer.Shared.Services;
using Microsoft.Extensions.Hosting;
using Polly;
using Serilog;

namespace nvrlift.AssettoServer.Track;

public class TrackManager : CriticalBackgroundService
{
    private readonly ACServerConfiguration _configuration;
    private readonly SessionManager _timeSource;
    private readonly TrackImplementation _trackImplementation;

    public TrackManager(TrackImplementation trackImplementation,
        ACServerConfiguration configuration,
        SessionManager timeSource,
        IHostApplicationLifetime applicationLifetime) : base(applicationLifetime)
    {
        
        _trackImplementation = trackImplementation;
        _configuration = configuration;
        _timeSource = timeSource;
    }

    public TrackData CurrentTrack { get; private set; } = null!;

    public void SetTrack(TrackData track)
    {
        CurrentTrack = track;

        if (!CurrentTrack.IsInit)
            UpdateTrack();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                UpdateTrack();
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

    private void UpdateTrack()
    {
        if (CurrentTrack.UpcomingType != null || !CurrentTrack.Type!.Equals(CurrentTrack.UpcomingType!))
        {
            Log.Information($"Track change to '{CurrentTrack.UpcomingType!.Name}' initiated");
            _trackImplementation.ChangeTrack(CurrentTrack);

            CurrentTrack.Type = CurrentTrack.UpcomingType;
            CurrentTrack.UpcomingType = null;
        }
    }
}
