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
    
    public class RssXmlParser
    {
        public static RssItems ParseXml(XDocument xmlDoc)
        {
            try
            {
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
                    var nodes =
                        from channelNode in channelXmlNode.Elements()
                         where channelNode.Name.LocalName == "item"
                         select channelNode;

                    var items = new ObservableCollection<RssItem>(from node in nodes
                                                                  let item = new RssItem(node)
                                                                  select item);

                    return new RssItems(title, link, description,items);
                }
                else
                {
                    throw new Exception("Ошибка в XML. Описание канала не найдено!");
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                throw new Exception("Файл не найден!");
            }
        }
    }
}