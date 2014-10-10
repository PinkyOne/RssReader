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
    using System.Xml.Linq;

    using Caliburn.Micro;

    /// <summary>
    /// The RSS item.
    /// </summary>
    public class RssItem : PropertyChangedBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RssItem"/> class.
        /// </summary>
        /// <param name="rssItems">
        /// The items.
        /// </param>
        public RssItem(XElement rssItems)
        {
            foreach (var xmlElement in rssItems.Elements())
            {
                // Parsing feed
                switch (xmlElement.Name.LocalName)
                {
                    case "title":
                    {
                        this.Title = xmlElement.Value;
                    }

                        break;
                    case "description":
                    {
                        this.Description = xmlElement.Value;
                    }

                        break;
                    case "link":
                    {
                        this.Link = xmlElement.Value;
                    }

                        break;
                    case "pubDate":
                    {
                        this.PublicDate = xmlElement.Value;
                    }

                        break;
                }
            }
        }

        public string Item
        {
            get { return this.ToString(); }
        }
        
        public string Title { get; private set; }

        public string Link { get; private set; }

        public string Description { get; private set; }

        public string PublicDate { get; private set; }

        public override string ToString()
        {
            return this.Title + Environment.NewLine + this.Description + Environment.NewLine + this.PublicDate;
        }
    }
}