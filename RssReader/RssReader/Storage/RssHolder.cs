namespace RssReader.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;

    using Windows.Foundation;
    using Windows.Storage;

    using Caliburn.Micro;

    using Windows.Web.Syndication;

    public class RssHolder : INewsHolder, IResult
    {
        private static ObservableCollection<RssFeed> newsHeaders = new ObservableCollection<RssFeed>();

        public RssHolder()
        {
            try
            {
                newsHeaders = new ObservableCollection<RssFeed>(SuspensionManager.RestoreAsync().Result);
            }
            catch (Exception)
            {
                newsHeaders = new ObservableCollection<RssFeed>();
            }
        }

        public ObservableCollection<RssFeed> GetNewsLines()
        {
            return newsHeaders;
        }

        public void AddLine(RssFeed feed)
        {
            Caliburn.Micro.Execute.OnUIThread(() => newsHeaders.Add(feed));
        }

        public void RemoveLine(RssFeed feed)
        {
            newsHeaders.Remove(feed);
        }

        public async void Refresh(IDownloader loader, IParser parser)
        {
            foreach (var newsFeed in newsHeaders)
            {
                var url = newsFeed.Url;
                var feedLoad = loader.DownloadAsync(url);
                while (feedLoad.Status != AsyncStatus.Completed)
                {
                    var pr = feedLoad.Progress;
                }
                var feed = parser.ParseXml(url, (await feedLoad).GetXmlDocument(SyndicationFormat.Rss20).GetXml());
                var newItems = from oldItem in newsFeed.Items
                               join item in feed.Items on oldItem equals item
                               where !item.Equals(oldItem)
                               select item;
                var rssItems = newItems as IList<RssItem> ?? newItems.ToList();
                newsFeed.AddRange(rssItems);
            }
        }

        public void Execute(ActionExecutionContext context)
        {
            var worker = new System.ComponentModel.BackgroundWorker();
            using (var backgroundWorker = new System.ComponentModel BackgroundWorker())
            {
                backgroundWorker.DoWork += (e, sender) => action();
                backgroundWorker.RunWorkerCompleted += (e, sender) => Completed(this, new ResultCompletionEventArgs());
                backgroundWorker.RunWorkerAsync();
            }
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };
    }
}
