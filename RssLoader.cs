using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace AppleMusicPlayer.Rss
{
    public static class RssLoader
    {
        public static Feed Load(Stream stream)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Feed));
            return (Feed)serializer.Deserialize(stream);
        }
        public static void Save(Stream stream, Feed feed)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Feed));
            serializer.Serialize(stream, feed);
        }
        public static Feed Load(string fileName)
        {
            return Load(File.OpenRead(fileName));
        }
        public static void Save(string fileName, Feed feed)
        {
            Save(File.OpenWrite(fileName), feed);
        }
    }
}
