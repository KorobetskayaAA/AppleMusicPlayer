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

        const string AppleRssUri = "http://ax.itunes.apple.com/WebObjects/MZStoreServices.woa/ws/RSS/topsongs/limit=25/xml";

        public MainWindow()
        {
            DataContext = SongListFabric.LoadFromRss(AppleRssUri);

            dialogService = new DefaultDialogService();

            InitializeComponent();

            audioPlayerController = new DefaultAudioPlayerController(myMediaElement);

            audioPlayerController.MediaFailed += MediaPlayer_MediaFailed;
            audioPlayerController.MediaOpened += MediaPlayer_Updated;
            audioPlayerController.MediaEnded += MediaPlayer_Updated;

            PlayerControlPanel.DataContext = audioPlayerController;
        }

        private void MediaPlayer_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            dialogService.ShowMessage("Ошибка! Не удалось открыть файл!");
            CommandManager.InvalidateRequerySuggested();
        }

        private void MediaPlayer_Updated(object sender, EventArgs e)
        {
            CommandManager.InvalidateRequerySuggested();
        }

        void Play_Execute(object target, ExecutedRoutedEventArgs e)
        {
            audioPlayerController?.Play();
        }

        void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = audioPlayerController?.CanPlay ?? false;
        }

        void Pause_Execute(object target, ExecutedRoutedEventArgs e)
        {
            audioPlayerController?.Pause();
        }

        void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = audioPlayerController?.CanPause ?? false;
        }

        void Stop_Execute(object target, ExecutedRoutedEventArgs e)
        {
            audioPlayerController?.Stop();
        }

        void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = audioPlayerController?.CanStop ?? false;
        }

        void Close_Execute(object target, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (dialogService.OpenFileDialog("XML-файлы (*.xml)|*.xml|Все файлы (*.*)|*.*"))
            {
                DataContext = SongListFabric.LoadFromRss(dialogService.FileName);
            }
        }
    }
}
