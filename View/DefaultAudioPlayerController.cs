using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows.Threading;

namespace AppleMusicPlayer
{

    class DefaultAudioPlayerController : IAudioPlayerController
    {
        readonly MediaPlayer mediaPlayer;
        readonly DispatcherTimer timer;

        public event EventHandler MediaEnded;
        public event EventHandler MediaOpened;
        public event EventHandler<ExceptionEventArgs> MediaFailed;
        public event EventHandler Tick;

        public PlayingState PlayingState { get; private set; } = PlayingState.Stopped;

        public bool HasMediaSource => mediaPlayer.Source != null;
        public bool CanPlay => HasMediaSource && PlayingState != PlayingState.Playing;
        public bool CanPause => HasMediaSource && PlayingState == PlayingState.Playing;
        public bool CanStop => HasMediaSource && PlayingState != PlayingState.Stopped;

        public TimeSpan Interval { get => timer.Interval; set => timer.Interval = value; }
        public TimeSpan Position { get => mediaPlayer.Position; set => mediaPlayer.Position = value; }
        public TimeSpan Duration
        { 
            get => mediaPlayer.NaturalDuration.HasTimeSpan
                ? mediaPlayer.NaturalDuration.TimeSpan 
                : TimeSpan.FromMilliseconds(0); 
        }

        public double Volume { get => mediaPlayer.Volume; set => mediaPlayer.Volume = value; }
        public double Balance { get => mediaPlayer.Balance; set => mediaPlayer.Balance = value; }

        public DefaultAudioPlayerController()
        {
            mediaPlayer = new MediaPlayer();
            mediaPlayer.MediaOpened += MediaPlayer_MediaOpened;
            mediaPlayer.MediaFailed += MediaPlayer_MediaFailed;
            mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Tick(this, new EventArgs());
        }

        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            PlayingState = PlayingState.Stopped;
            timer.Stop();
            Tick(this, new EventArgs());
            MediaEnded(sender, e);
        }

        private void MediaPlayer_MediaFailed(object sender, ExceptionEventArgs e)
        {
            PlayingState = PlayingState.Stopped;
            timer.Stop();
            Tick(this, new EventArgs());
            MediaFailed(sender, e);
        }

        private void MediaPlayer_MediaOpened(object sender, EventArgs e)
        {
            Play();
            MediaOpened(sender, e);
        }

        public string TimingString
        {
            get => $"{Position.ToString(@"mm\:ss")} / {Duration.ToString(@"mm\:ss")}";
        }

        public void Play()
        {
            mediaPlayer.Play();
            timer.Start();
            PlayingState = PlayingState.Playing;
        }

        public void Pause()
        {
            mediaPlayer.Pause();
            timer.Stop();
            Tick(this, new EventArgs());
            PlayingState = PlayingState.Paused;
        }

        public void Stop()
        {
            mediaPlayer.Stop();
            timer.Stop();
            Tick(this, new EventArgs());
            PlayingState = PlayingState.Stopped;
        }

        public void Open(string uri)
        {
            mediaPlayer.Open(new Uri(uri));
        }
    }
}
