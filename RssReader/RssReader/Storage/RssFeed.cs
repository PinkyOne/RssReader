// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RssItems.cs" company="mercdev">
//   mercdev
// </copyright>
// <summary>
//   Defines the RssItems type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace RssReader.Storage
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Xaml.Interactivity;

    [DataContract]
    public class RssFeed
    {
        public RssFeed(string url, string title, string link, string description, ObservableCollection<RssItem> items)
        {
            this.Url = url;
            this.Title = title;
            this.Link = link;
            this.Description = description;
            this.Items = items;
            this.CountUnviewedItems = items.Count;
        }

        [DataMember]
        public ObservableCollection<RssItem> Items { get; private set; }

        [DataMember]
        public string Title { get; private set; }

        [DataMember]
        public string Link { get; private set; }

        [DataMember]
        public string Description { get; private set; }

        [DataMember]
        public string Url { get; private set; }

        [DataMember]
        public int CountUnviewedItems { get; set; }
    }
}
