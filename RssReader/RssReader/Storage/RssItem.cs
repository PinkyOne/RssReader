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
    using System.Net;
    using System.Runtime.Serialization;
    using System.Xml.Linq;

    using Caliburn.Micro;

    public class RssItem : PropertyChangedBase
    {
        public RssItem(XElement rssItem)
        {
            this.Title =
                (from xmlElement in rssItem.Elements() where xmlElement.Name.LocalName == "title" select xmlElement)
                    .FirstOrDefault().Value ?? string.Empty;
            this.Description =
                (from xmlElement in rssItem.Elements()
                 where xmlElement.Name.LocalName == "description"
                 select xmlElement).FirstOrDefault().Value ?? string.Empty;

            var s = WebUtility.HtmlDecode(Description);

            this.Link =
                (from xmlElement in rssItem.Elements() where xmlElement.Name.LocalName == "link" select xmlElement)
                    .FirstOrDefault().Value ?? string.Empty;

            this.PublicDate =
                (from xmlElement in rssItem.Elements() where xmlElement.Name.LocalName == "pubDate" select xmlElement)
                    .FirstOrDefault().Value ?? string.Empty;
            string url = null;
            var sequence = from xmlElement in rssItem.Elements()
                           where xmlElement.Name.LocalName == "image"
                           select xmlElement;
            var seqHaveElements = sequence.Any();
            if (seqHaveElements)
                url =
                    (from xmlElement in rssItem.Elements() where xmlElement.Name.LocalName == "image" select xmlElement)
                        .FirstOrDefault().Value;
            this.ImageUrl = url ?? @"C:\Users\Alex\Documents\GitHub\RssReader\RssReader\RssReader\Assets\placeholde.png";
            this.Opacity = 1.0;
        }

        public string Item
        {
            get
            {
                return this.ToString();
            }
        }

        public string Title { get; private set; }

        public string Link { get; private set; }

        public string Description { get; private set; }

        public string PublicDate { get; private set; }

        public string ImageUrl { get; private set; }

        public double Opacity { get; set; }

        public override string ToString()
        {
            return this.Title + Environment.NewLine + this.Description + Environment.NewLine + this.PublicDate;
        }

        public override bool Equals(object obj)
        {
            var anotherItem = obj as RssItem;
            return anotherItem != null && (this.Title.Equals(anotherItem.Title) && this.PublicDate.Equals(anotherItem.PublicDate));
        }
    }
}