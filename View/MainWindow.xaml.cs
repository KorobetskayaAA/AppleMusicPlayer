using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AppleMusicPlayer
{
    enum PlayingState { Playing = 1, Paused = 2, Stopped = 0 }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MediaPlayer mediaPlayer;
        IDialogService dialogService;
        DispatcherTimer timer;
        PlayingState playingState = PlayingState.Stopped;

        public MainWindow()
        {
            InitializeComponent();
            dialogService = new DefaultDialogService();

            mediaPlayer = new MediaPlayer();
            mediaPlayer.MediaOpened += MediaPlayer_MediaOpened;
            mediaPlayer.MediaFailed += MediaPlayer_MediaFailed; ;
            mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            timer.Stop();
            Timer_Tick(null, null);
            playingState = PlayingState.Stopped;
        }

        private void MediaPlayer_MediaFailed(object sender, ExceptionEventArgs e)
        {
            dialogService.ShowMessage("Ошибка! Не удалось открыть файл!");
            playingState = PlayingState.Stopped;
            timer.Stop();
            Timer_Tick(null, null);
        }

        private void MediaPlayer_MediaOpened(object sender, EventArgs e)
        {
             Play_Execute(null, null);
             TimingProgress.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
        }

        private void OpenAudioFile_Execute(object target, ExecutedRoutedEventArgs e)
        {
            if (dialogService.OpenFileDialog())
            {
                mediaPlayer.Open(new Uri(dialogService.FileName));
            }
        }

        void Play_Execute(object target, ExecutedRoutedEventArgs e)
        {
            mediaPlayer.Play();
            timer.Start();
            Timer_Tick(null, null);
            playingState = PlayingState.Playing;
        }

        void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (mediaPlayer != null) && (mediaPlayer.Source != null)
                && playingState != PlayingState.Playing;
        }

        void Pause_Execute(object target, ExecutedRoutedEventArgs e)
        {
            mediaPlayer.Pause();
            timer.Stop();
            playingState = PlayingState.Paused;
        }

        void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (mediaPlayer != null) && (mediaPlayer.Source != null)
                && playingState == PlayingState.Playing;
        }

        void Stop_Execute(object target, ExecutedRoutedEventArgs e)
        {
            mediaPlayer.Stop();
            timer.Stop();
            Timer_Tick(null, null);
            playingState = PlayingState.Stopped;
        }

        void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (mediaPlayer != null) && (mediaPlayer.Source != null)
                && playingState != PlayingState.Stopped;
        }

        void Close_Execute(object target, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            TimingText.Text = (mediaPlayer.Source != null) ? string.Format("{0}/{1}",
                        mediaPlayer.Position.ToString(@"mm\:ss"),
                        mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss"))
                : TimingText.Text = "00:00";
            TimingProgress.Value = mediaPlayer.Position.TotalSeconds;
        }

        private void TimingProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TimeSpan ts = TimeSpan.FromSeconds(e.NewValue);
            if (mediaPlayer.Position != ts) {
                mediaPlayer.Position = ts;
                TimingText.Text = (mediaPlayer.Source != null) ? string.Format("{0}/{1}",
                        mediaPlayer.Position.ToString(@"mm\:ss"),
                        mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss"))
                    : TimingText.Text = "00:00";
            }
        }
    }
}
