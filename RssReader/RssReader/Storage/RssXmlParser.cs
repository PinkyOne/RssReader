// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RssXmlParser.cs" company="mercdev">
//   All rigths saved
// </copyright>
// <summary>
//   Defines the RssXmlParser type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace RssReader.Storage
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Xml.Linq;

    using SQLite;

    public class RssXmlParser : IParser
    {
       public RssFeed ParseXml(string url, string feed, int feedId)
        {
            var xmlDoc = XDocument.Parse(feed);

            var channelXmlNode = xmlDoc.Element("rss").Element("channel"); // Parsing xml

            if (channelXmlNode != null)
            {
                var title =
                    (from channelNode in channelXmlNode.Elements()
                     where channelNode.Name.LocalName == "title"
                     select channelNode).First().Value;
                var description =
                    (from channelNode in channelXmlNode.Elements()
                     where channelNode.Name.LocalName == "description"
                     select channelNode).First().Value;
                var link =
                    (from channelNode in channelXmlNode.Elements()
                     where channelNode.Name.LocalName == "link"
                     select channelNode).First().Value;
                var nodes = from channelNode in channelXmlNode.Elements()
                            where channelNode.Name.LocalName == "item"
                            select channelNode;
                var imageNode =
                    (from channelNode in channelXmlNode.Elements()
                     where channelNode.Name.LocalName == "image"
                     select channelNode).First();
                var imageUrl = imageNode.Element("url").Value;
                try
                {
                    var items =
                        new ObservableCollection<RssItem>(from node in nodes let item = new RssItem(node, feedId) select item);
                    return new RssFeed(url, title, link, description, imageUrl, items);
                }
                catch (Exception e)
                {
                    throw new Exception("Пустая последовательность");
                }
            }
            throw new Exception("Ошибка в XML. Описание канала не найдено!");
        }
    }
}