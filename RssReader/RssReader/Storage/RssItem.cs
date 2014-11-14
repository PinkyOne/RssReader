// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RssItem.cs" company="mercdev">
//   All rights saved
// </copyright>
// <summary>
//   The RSS item.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace RssReader.Storage
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Xml.Linq;

    using Caliburn.Micro;

    [DataContract]
    public class RssItem : PropertyChangedBase
    {
        public RssItem(XElement rssItems)
        {
            this.Title =
                (from xmlElement in rssItems.Elements() where xmlElement.Name.LocalName == "title" select xmlElement)
                    .First().Value;
            this.Description =
                (from xmlElement in rssItems.Elements()
                 where xmlElement.Name.LocalName == "description"
                 select xmlElement).First().Value;

            this.Link =
                (from xmlElement in rssItems.Elements() where xmlElement.Name.LocalName == "link" select xmlElement)
                    .First().Value;

            this.PublicDate =
                (from xmlElement in rssItems.Elements() where xmlElement.Name.LocalName == "pubDate" select xmlElement)
                    .First().Value;

            this.Opacity = 1.0;
        }

        public string Item
        {
            get
            {
                return this.ToString();
            }
        }

        [DataMember]
        public string Title { get; private set; }

        [DataMember]
        public string Link { get; private set; }

        [DataMember]
        public string Description { get; private set; }

        [DataMember]
        public string PublicDate { get; private set; }

        [DataMember]
        public double Opacity { get; set; }

        public override string ToString()
        {
            return this.Title + Environment.NewLine + this.Description + Environment.NewLine + this.PublicDate;
        }
    }
}