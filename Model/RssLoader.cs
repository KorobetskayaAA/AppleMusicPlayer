using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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
            if (File.Exists(fileName))
            {
                return Load(File.OpenRead(fileName));
            }
            else
            {
                WebRequest request = WebRequest.Create(fileName);
                WebResponse response = request.GetResponse();
                return Load(response.GetResponseStream());
            }
        }
        public static void Save(string fileName, Feed feed)
        {
            Save(File.OpenWrite(fileName), feed);
        }
    }
}
