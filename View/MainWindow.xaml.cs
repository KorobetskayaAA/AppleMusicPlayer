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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media;

namespace AppleMusicPlayer
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IDialogService dialogService;
        IAudioPlayerController audioPlayerController;

        public MainWindow()
        {
            audioPlayerController = new DefaultAudioPlayerController();
            audioPlayerController.MediaFailed += MediaPlayer_MediaFailed;
            audioPlayerController.MediaOpened += MediaPlayer_MediaOpened;
            audioPlayerController.Tick += TimingProgress_Tick;

            dialogService = new DefaultDialogService();

            InitializeComponent();
        }

        private void MediaPlayer_MediaFailed(object sender, ExceptionEventArgs e)
        {
            dialogService.ShowMessage("Ошибка! Не удалось открыть файл!");
        }

        private void MediaPlayer_MediaOpened(object sender, EventArgs e)
        {
            TimingProgress.Maximum = audioPlayerController.Duration.TotalSeconds;
        }

        private void OpenAudioFile_Execute(object target, ExecutedRoutedEventArgs e)
        {
            if (dialogService.OpenFileDialog())
            {
                audioPlayerController.Open(dialogService.FileName);
            }
        }

        void Play_Execute(object target, ExecutedRoutedEventArgs e)
        {
            audioPlayerController.Play();
        }

        void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = audioPlayerController.CanPlay;
        }

        void Pause_Execute(object target, ExecutedRoutedEventArgs e)
        {
            audioPlayerController.Pause();
        }

        void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = audioPlayerController.CanPause;
        }

        void Stop_Execute(object target, ExecutedRoutedEventArgs e)
        {
            audioPlayerController.Stop();
        }

        void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = audioPlayerController.CanStop;
        }

        void Close_Execute(object target, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        void TimingProgress_Tick(object sender, EventArgs e)
        {
            TimingText.Text = audioPlayerController.TimingString;
            TimingProgress.Value = audioPlayerController.Position.TotalSeconds;
        }

        private void TimingProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TimeSpan ts = TimeSpan.FromSeconds(e.NewValue);
            if (audioPlayerController.Position != ts) {
                audioPlayerController.Position = ts;
                TimingText.Text = audioPlayerController.TimingString;
            }
        }
    }
}
