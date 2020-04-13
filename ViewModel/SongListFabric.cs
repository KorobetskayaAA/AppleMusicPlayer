using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppleMusicPlayer.Rss;

namespace AppleMusicPlayer
{
    public static class SongListFabric
    {
        public static SongList LoadFromRss(string fileName)
        {
            Feed feed = RssLoader.Load(fileName);
            SongList songList = new SongList();
            foreach (Entry entry in feed.Entry)
            {
                songList.Songs.Add(new Song(
                    entry.Name,
                    entry.Artist.Text,
                    entry.Category.Label,
                    entry.Image.OrderBy(img => img.Height).First().Text,
                    entry.Link.Where(lnk => lnk.Type == "audio/x-m4a").First().Href
                ));
            }
            return songList;
        }
    }
}
