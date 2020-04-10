using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Media;
using System.Windows.Threading;

namespace AppleMusicPlayer
{

    class DefaultAudioPlayerController : IAudioPlayerController, INotifyPropertyChanged
    {
        readonly MediaPlayer mediaPlayer;
        readonly DispatcherTimer timer;
        private PlayingState playingState = PlayingState.Stopped;

        public event EventHandler MediaEnded;
        public event EventHandler MediaOpened;
        public event EventHandler<ExceptionEventArgs> MediaFailed;
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public PlayingState PlayingState 
        { 
            get => playingState;
            private set
            {
                if (value == playingState) return;
                playingState = value;
                OnPropertyChanged(nameof(PlayingState));
                OnPropertyChanged(nameof(CanPlay));
                OnPropertyChanged(nameof(CanPause));
                OnPropertyChanged(nameof(CanStop));
            }
        }

        public bool HasMediaSource => mediaPlayer.Source != null;
        public bool CanPlay => HasMediaSource && PlayingState != PlayingState.Playing;
        public bool CanPause => HasMediaSource && PlayingState == PlayingState.Playing;
        public bool CanStop => HasMediaSource && PlayingState != PlayingState.Stopped;

        public TimeSpan Interval 
        { 
            get => timer.Interval;
            set
            {
                if (timer.Interval == value) return;
                timer.Interval = value;
                OnPropertyChanged();
            }
        }
        public TimeSpan Position 
        { 
            get => mediaPlayer.Position;
            set
            {
                if (mediaPlayer.Position == value) return;
                mediaPlayer.Position = value;
                OnPropertyChanged();
            }
        }
        public TimeSpan Duration
        {
            get => mediaPlayer.NaturalDuration.HasTimeSpan
                ? mediaPlayer.NaturalDuration.TimeSpan
                : TimeSpan.FromMilliseconds(0);
        }

        public double Volume 
        { 
            get => mediaPlayer.Volume;
            set
            {
                if (mediaPlayer.Volume == value) return;
                mediaPlayer.Volume = value;
                OnPropertyChanged();
            }
        }
        public double Balance 
        { 
            get => mediaPlayer.Balance;
            set
            {
                if (mediaPlayer.Balance == value) return;
                mediaPlayer.Balance = value;
                OnPropertyChanged();
            }
        }

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
            OnPropertyChanged(nameof(Position));
        }

        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            PlayingState = PlayingState.Stopped;
            timer.Stop();
            OnPropertyChanged(nameof(Position));
            MediaEnded?.Invoke(sender, e);
        }

        private void MediaPlayer_MediaFailed(object sender, ExceptionEventArgs e)
        {
            PlayingState = PlayingState.Stopped;
            timer.Stop();
            OnPropertyChanged(nameof(Position));
            OnPropertyChanged(nameof(Duration));
            MediaFailed?.Invoke(sender, e);
        }

        private void MediaPlayer_MediaOpened(object sender, EventArgs e)
        {
            Play();
            MediaOpened?.Invoke(sender, e);
            OnPropertyChanged(nameof(Duration));
        }

        public void Play()
        {
            mediaPlayer.Play();
            timer.Start();
            OnPropertyChanged(nameof(Position));
            PlayingState = PlayingState.Playing;
        }

        public void Pause()
        {
            mediaPlayer.Pause();
            timer.Stop();
            OnPropertyChanged(nameof(Position));
            PlayingState = PlayingState.Paused;
        }

        public void Stop()
        {
            mediaPlayer.Stop();
            timer.Stop();
            OnPropertyChanged(nameof(Position));
            PlayingState = PlayingState.Stopped;
        }

        public void Open(string uri)
        {
            mediaPlayer.Open(new Uri(uri));
        }
    }
}
