using System;
using System.Windows.Media;

namespace AppleMusicPlayer
{
    enum PlayingState { Playing = 1, Paused = 2, Stopped = 0 }

    interface IAudioPlayerController
    {
        void Play();
        void Pause();
        void Stop();
        PlayingState PlayingState { get; }
        bool CanPlay { get; }
        bool CanPause { get; }
        bool CanStop { get; }

        TimeSpan Position { get; set; }
        TimeSpan Duration { get; }
        event EventHandler MediaEnded;

        double Volume { get; set; }
        double Balance { get; set; }

        void Open(string fileName);
        event EventHandler MediaOpened;
        event EventHandler<ExceptionEventArgs> MediaFailed;

        TimeSpan Interval { get; set; }
    }
}
