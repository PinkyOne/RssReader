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
    using System.Linq;
    using System.Xml.Linq;
    
    public class RssXmlParser
    {
        public RssXmlParser(XDocument xmlDoc)
        {
            this.Items = new RssItems();
            try
            {
                var channelXmlNode = xmlDoc.Element("rss").Element("channel"); // Parsing xml

                if (channelXmlNode != null)
                {
                    this.Title =
                        (from channelNode in channelXmlNode.Elements()
                         where channelNode.Name.LocalName == "title"
                         select channelNode).First().Value;
                    this.Description =
                        (from channelNode in channelXmlNode.Elements()
                         where channelNode.Name.LocalName == "description"
                         select channelNode).First().Value;
                    this.Link =
                        (from channelNode in channelXmlNode.Elements()
                         where channelNode.Name.LocalName == "link"
                         select channelNode).First().Value;
                    var items =
                        from channelNode in channelXmlNode.Elements()
                         where channelNode.Name.LocalName == "item"
                         select channelNode;
                    foreach (var channelNode in items)
                    {
                        this.Items.Add(new RssItem(channelNode));
                    }
                    /*foreach (var channelNode in channelXmlNode.Elements())
                    {
                        switch (channelNode.Name.LocalName)
                        {
                            case "title":
                                {
                                    this.Title = channelNode.Value;
                                }

                                break;

                            case "description":
                                {
                                    this.Description = channelNode.Value;
                                }

                                break;

                            case "link":
                                {
                                    this.Link = channelNode.Value;
                                }

                                break;

                            case "item":
                                {
                                    var channelItem = new RssItem(channelNode);
                                    this.Items.Add(channelItem);
                                }

                                break;
                        }
                    }*/
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

        public RssItems Items { get; private set; }

        public string Title { get; private set; }

        public string Link { get; private set; }

        public string Description { get; private set; }
    }
}