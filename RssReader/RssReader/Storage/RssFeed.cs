﻿// --------------------------------------------------------------------------------------------------------------------
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

    using Microsoft.Xaml.Interactivity;

    public class RssFeed : ObservableCollection<RssItem>
    {
        public RssFeed(string title, string link, string description, ObservableCollection<RssItem> items)
        {
            this.Title = title;
            this.Link = link;
            this.Description = description;
            this.Items = items;
        }
        
        public new ObservableCollection<RssItem> Items { get; private set; }

        public string Title { get; private set; }

        public string Link { get; private set; }

        public string Description { get; private set; }

        public string Url { get; private set; }

        public RssItem GetItem(string title)
        {
            return this.FirstOrDefault(itemForCheck => itemForCheck != null && itemForCheck.Title == title);
        }
    }
}
