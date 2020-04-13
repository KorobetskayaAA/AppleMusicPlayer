using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AppleMusicPlayer
{
    public class SongList : INotifyPropertyChanged
    {
        public Song activeSong;

        public ObservableCollection<Song> Songs { get; }
        public Song ActiveSong 
        { 
            get => activeSong;
            set
            {
                if (value == activeSong) return;
                activeSong = value;
                OnPropertyChanged();
            }
        }

        public SongList()
        {
            Songs = new ObservableCollection<Song>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
