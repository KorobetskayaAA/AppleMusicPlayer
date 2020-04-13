using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace AppleMusicPlayer
{

    class DefaultAudioPlayerController : IAudioPlayerController, INotifyPropertyChanged
    {
        readonly MediaElement mediaElement;
        readonly DispatcherTimer timer;
        private PlayingState playingState = PlayingState.Stopped;

        public event EventHandler MediaEnded;
        public event EventHandler MediaOpened;
        public event EventHandler<ExceptionRoutedEventArgs> MediaFailed;
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

        public bool HasMediaSource => mediaElement.Source != null;
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
            get => mediaElement.Position;
            set
            {
                if (mediaElement.Position == value) return;
                mediaElement.Position = value;
                OnPropertyChanged();
            }
        }
        public TimeSpan Duration
        {
            get => mediaElement.NaturalDuration.HasTimeSpan
                ? mediaElement.NaturalDuration.TimeSpan
                : TimeSpan.FromMilliseconds(0);
        }

        public double Volume
        {
            get => mediaElement.Volume;
            set
            {
                if (mediaElement.Volume == value) return;
                mediaElement.Volume = value;
                OnPropertyChanged();
            }
        }
        public double Balance
        {
            get => mediaElement.Balance;
            set
            {
                if (mediaElement.Balance == value) return;
                mediaElement.Balance = value;
                OnPropertyChanged();
            }
        }

        public DefaultAudioPlayerController(MediaElement mediaElement)
        {
            this.mediaElement = mediaElement;
            this.mediaElement.MediaOpened += MediaPlayer_MediaOpened;
            this.mediaElement.MediaFailed += MediaPlayer_MediaFailed;
            this.mediaElement.MediaEnded += MediaPlayer_MediaEnded;

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

        private void MediaPlayer_MediaFailed(object sender, ExceptionRoutedEventArgs e)
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
            mediaElement.Play();
            timer.Start();
            OnPropertyChanged(nameof(Position));
            PlayingState = PlayingState.Playing;
        }

        public void Pause()
        {
            mediaElement.Pause();
            timer.Stop();
            OnPropertyChanged(nameof(Position));
            PlayingState = PlayingState.Paused;
        }

        public void Stop()
        {
            mediaElement.Stop();
            timer.Stop();
            OnPropertyChanged(nameof(Position));
            PlayingState = PlayingState.Stopped;
        }
    }
}
