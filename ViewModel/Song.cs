using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AppleMusicPlayer
{
    public class Song : INotifyPropertyChanged
    {
        public string Name { get; }
        public string Artist { get; }
        public string Category { get; }
        public string Image { get; }
        public string Link { get; }

        public Song(string name, string artist, 
            string category, string image, string link)
        {
            Name = name;
            Artist = artist;
            Category = category;
            Image = image;
            Link = link;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
