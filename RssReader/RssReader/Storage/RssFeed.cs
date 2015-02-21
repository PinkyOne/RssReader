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
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.Serialization;

    using Microsoft.Xaml.Interactivity;

    using Windows.UI.Xaml;

    public class RssFeed
    {
        private bool isInProgress = false;

        public RssFeed()
        {
            this.isInProgress = true;
        }

        public RssFeed(
            string url,
            string title,
            string link,
            string description,
            string imageUrl,
            ObservableCollection<RssItem> items)
        {
            this.Url = url ?? string.Empty;
            this.Title = title ?? string.Empty;
            this.Link = link ?? string.Empty;
            this.Description = description ?? string.Empty;
            this.Items = items;
            this.CountUnviewedItems = items.Count;
            this.ImageUrl = imageUrl
                            ?? @"C:\Users\Alex\Documents\GitHub\RssReader\RssReader\RssReader\Assets\placeholde.png";
        }

        public RssFeed(RssFeed feed, bool isInProgress)
        {
            this.Url = feed.Url;
            this.Title = feed.Title;
            this.Link = feed.Link;
            this.Description = feed.Description;
            this.Items = feed.Items;
            this.CountUnviewedItems = feed.CountUnviewedItems;
            this.ImageUrl = feed.ImageUrl
                            ?? @"C:\Users\Alex\Documents\GitHub\RssReader\RssReader\RssReader\Assets\placeholde.png";
            this.isInProgress = isInProgress;
        }

        public ObservableCollection<RssItem> Items { get; private set; }

        public string Title { get; private set; }

        public string Link { get; private set; }

        public string Description { get; private set; }

        public string Url { get; private set; }

        public int CountUnviewedItems { get; set; }

        public string ImageUrl { get; private set; }

        public bool OnDelete { get; set; }

        public bool IsShowing
        {
            get
            {
                return this.isInProgress;
            }
            set
            {
                this.isInProgress = value;
            }
        }

        public void AddRange(IEnumerable<RssItem> newItems)
        {
            var oldItems = this.Items.ToList();
            oldItems.AddRange(newItems);
            this.Items = new ObservableCollection<RssItem>(oldItems);
        }
    }
}
