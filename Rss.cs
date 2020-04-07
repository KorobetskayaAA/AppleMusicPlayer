/* 
 * https://xmltocsharp.azurewebsites.net/
 * Licensed under the Apache License, Version 2.0
 * 
 * http://www.apache.org/licenses/LICENSE-2.0
 */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace AppleMusicPlayer.Rss
{
	[XmlRoot(ElementName = "link", Namespace = "http://www.w3.org/2005/Atom")]
	public class Link
	{
		[XmlAttribute(AttributeName = "rel")]
		public string Rel { get; set; }
		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }
		[XmlAttribute(AttributeName = "href")]
		public string Href { get; set; }
		[XmlElement(ElementName = "duration", Namespace = "http://itunes.apple.com/rss")]
		public string Duration { get; set; }
		[XmlAttribute(AttributeName = "title")]
		public string Title { get; set; }
		[XmlAttribute(AttributeName = "assetType", Namespace = "http://itunes.apple.com/rss")]
		public string AssetType { get; set; }
	}

	[XmlRoot(ElementName = "author", Namespace = "http://www.w3.org/2005/Atom")]
	public class Author
	{
		[XmlElement(ElementName = "name", Namespace = "http://www.w3.org/2005/Atom")]
		public string Name { get; set; }
		[XmlElement(ElementName = "uri", Namespace = "http://www.w3.org/2005/Atom")]
		public string Uri { get; set; }
	}

	[XmlRoot(ElementName = "id", Namespace = "http://www.w3.org/2005/Atom")]
	public class Id
	{
		[XmlAttribute(AttributeName = "id", Namespace = "http://itunes.apple.com/rss")]
		public string _id { get; set; }
		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "contentType", Namespace = "http://itunes.apple.com/rss")]
	public class ContentType
	{
		[XmlAttribute(AttributeName = "term")]
		public string Term { get; set; }
		[XmlAttribute(AttributeName = "label")]
		public string Label { get; set; }
		[XmlElement(ElementName = "contentType", Namespace = "http://itunes.apple.com/rss")]
		public ContentType Type { get; set; }
	}

	[XmlRoot(ElementName = "category", Namespace = "http://www.w3.org/2005/Atom")]
	public class Category
	{
		[XmlAttribute(AttributeName = "id", Namespace = "http://itunes.apple.com/rss")]
		public string Id { get; set; }
		[XmlAttribute(AttributeName = "term")]
		public string Term { get; set; }
		[XmlAttribute(AttributeName = "scheme")]
		public string Scheme { get; set; }
		[XmlAttribute(AttributeName = "label")]
		public string Label { get; set; }
	}

	[XmlRoot(ElementName = "artist", Namespace = "http://itunes.apple.com/rss")]
	public class Artist
	{
		[XmlAttribute(AttributeName = "href")]
		public string Href { get; set; }
		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "price", Namespace = "http://itunes.apple.com/rss")]
	public class Price
	{
		[XmlAttribute(AttributeName = "amount")]
		public string Amount { get; set; }
		[XmlAttribute(AttributeName = "currency")]
		public string Currency { get; set; }
		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "image", Namespace = "http://itunes.apple.com/rss")]
	public class Image
	{
		[XmlAttribute(AttributeName = "height")]
		public string Height { get; set; }
		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "releaseDate", Namespace = "http://itunes.apple.com/rss")]
	public class ReleaseDate
	{
		[XmlAttribute(AttributeName = "label")]
		public string Label { get; set; }
		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "collection", Namespace = "http://itunes.apple.com/rss")]
	public class Collection
	{
		[XmlElement(ElementName = "name", Namespace = "http://itunes.apple.com/rss")]
		public string Name { get; set; }
		[XmlElement(ElementName = "link", Namespace = "http://www.w3.org/2005/Atom")]
		public Link Link { get; set; }
		[XmlElement(ElementName = "contentType", Namespace = "http://itunes.apple.com/rss")]
		public ContentType ContentType { get; set; }
	}

	[XmlRoot(ElementName = "content", Namespace = "http://www.w3.org/2005/Atom")]
	public class Content
	{
		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }
		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "entry", Namespace = "http://www.w3.org/2005/Atom")]
	public class Entry
	{
		[XmlElement(ElementName = "updated", Namespace = "http://www.w3.org/2005/Atom")]
		public string Updated { get; set; }
		[XmlElement(ElementName = "id", Namespace = "http://www.w3.org/2005/Atom")]
		public Id Id { get; set; }
		[XmlElement(ElementName = "title", Namespace = "http://www.w3.org/2005/Atom")]
		public string Title { get; set; }
		[XmlElement(ElementName = "name", Namespace = "http://itunes.apple.com/rss")]
		public string Name { get; set; }
		[XmlElement(ElementName = "link", Namespace = "http://www.w3.org/2005/Atom")]
		public List<Link> Link { get; set; }
		[XmlElement(ElementName = "contentType", Namespace = "http://itunes.apple.com/rss")]
		public ContentType ContentType { get; set; }
		[XmlElement(ElementName = "category", Namespace = "http://www.w3.org/2005/Atom")]
		public Category Category { get; set; }
		[XmlElement(ElementName = "artist", Namespace = "http://itunes.apple.com/rss")]
		public Artist Artist { get; set; }
		[XmlElement(ElementName = "price", Namespace = "http://itunes.apple.com/rss")]
		public Price Price { get; set; }
		[XmlElement(ElementName = "image", Namespace = "http://itunes.apple.com/rss")]
		public List<Image> Image { get; set; }
		[XmlElement(ElementName = "rights", Namespace = "http://www.w3.org/2005/Atom")]
		public string Rights { get; set; }
		[XmlElement(ElementName = "releaseDate", Namespace = "http://itunes.apple.com/rss")]
		public ReleaseDate ReleaseDate { get; set; }
		[XmlElement(ElementName = "collection", Namespace = "http://itunes.apple.com/rss")]
		public Collection Collection { get; set; }
		[XmlElement(ElementName = "content", Namespace = "http://www.w3.org/2005/Atom")]
		public Content Content { get; set; }
	}

	[XmlRoot(ElementName = "feed", Namespace = "http://www.w3.org/2005/Atom")]
	public class Feed
	{
		[XmlElement(ElementName = "id", Namespace = "http://www.w3.org/2005/Atom")]
		public string Id { get; set; }
		[XmlElement(ElementName = "title", Namespace = "http://www.w3.org/2005/Atom")]
		public string Title { get; set; }
		[XmlElement(ElementName = "updated", Namespace = "http://www.w3.org/2005/Atom")]
		public string Updated { get; set; }
		[XmlElement(ElementName = "link", Namespace = "http://www.w3.org/2005/Atom")]
		public List<Link> Link { get; set; }
		[XmlElement(ElementName = "icon", Namespace = "http://www.w3.org/2005/Atom")]
		public string Icon { get; set; }
		[XmlElement(ElementName = "author", Namespace = "http://www.w3.org/2005/Atom")]
		public Author Author { get; set; }
		[XmlElement(ElementName = "rights", Namespace = "http://www.w3.org/2005/Atom")]
		public string Rights { get; set; }
		[XmlElement(ElementName = "entry", Namespace = "http://www.w3.org/2005/Atom")]
		public List<Entry> Entry { get; set; }
		[XmlAttribute(AttributeName = "im", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Im { get; set; }
		[XmlAttribute(AttributeName = "xmlns")]
		public string Xmlns { get; set; }
		[XmlAttribute(AttributeName = "lang", Namespace = "http://www.w3.org/XML/1998/namespace")]
		public string Lang { get; set; }
	}

}
