﻿namespace RssReader.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Caliburn.Micro;

    using Windows.Foundation;
    using Windows.Storage;
    using Windows.UI.Xaml;
    using Windows.Web.Syndication;

    public class RssHolder : INewsHolder
    {
        private static ObservableCollection<RssFeed> newsHeaders = new ObservableCollection<RssFeed>();
        
        private bool isBusy = false;

        private Timer timer;

        private IParser parser;

        private IDownloader loader;

        private IEventAggregator aggregator;

        public RssHolder(IDownloader loader, IParser parser, IEventAggregator aggregator)
        {
            try
            {
                this.aggregator = aggregator;
                this.aggregator.Subscribe(this);
                this.parser = parser;
                this.loader = loader;
                TimerCallback callback = this.RefreshOnTime;
                this.timer = new Timer(callback, null, 60000, 3000);
                newsHeaders = new ObservableCollection<RssFeed>(SuspensionManager.RestoreAsync().Result);
            }
            catch (Exception)
            {
                newsHeaders = new ObservableCollection<RssFeed>();
            }
        }

        public bool IsBusy()
        {
            return isBusy;
        }

        public ObservableCollection<RssFeed> GetNewsLines()
        {
            return newsHeaders;
        }

        public void AddLine(RssFeed feed)
        {
            isBusy = true;
            Execute.OnUIThread(
                () =>
                    {
                        newsHeaders[newsHeaders.Count - 1] = feed;
                        isBusy = false;
                    });
        }

        public void RemovePlaceHolder()
        {
            newsHeaders.Remove(newsHeaders[newsHeaders.Count - 1]);
        }

        public void AddPlaceHolder()
        {
            newsHeaders.Add(new RssFeed());
        }

        public void RemoveLine(RssFeed feed)
        {
            newsHeaders.Remove(feed);
        }

        public async void Refresh(IDownloader loader, IParser parser)
        {
            if (!isBusy)
            {
                isBusy = true;
                foreach (var newsFeed in newsHeaders)
                {
                    var url = newsFeed.Url;
                    var feedLoad = loader.DownloadAsync(url);
                    newsFeed.IsShowing = Visibility.Visible;
                    aggregator.Publish("HolderBusy", Execute.OnUIThread);
                    feedLoad.GetAwaiter().UnsafeOnCompleted(
                        () =>
                            {
                                try
                                {
                                    var feed = parser.ParseXml(
                                        url,
                                        feedLoad.GetResults().GetXmlDocument(SyndicationFormat.Rss20).GetXml());
                                    var newItems = from oldItem in newsFeed.Items
                                                   join item in feed.Items on oldItem equals item
                                                   where !item.Equals(oldItem)
                                                   select item;
                                    var rssItems = newItems as IList<RssItem> ?? newItems.ToList();
                                    newsFeed.AddRange(rssItems);
                                    isBusy = false;
                                    newsFeed.IsShowing = Visibility.Collapsed;
                                    aggregator.Publish("HolderBusy", Execute.OnUIThread);
                                }
                                catch (Exception)
                                {
                                }
                            });
                }
            }
        }

        private void RefreshOnTime(object state)
        {
            this.Refresh(this.loader, this.parser);
            this.timer.Dispose();
            TimerCallback callback = this.RefreshOnTime;
            this.timer = new Timer(callback, null, 60000, 3000);
        }
    }
}
