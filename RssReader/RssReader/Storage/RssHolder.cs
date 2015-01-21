namespace RssReader.Storage
{
    using Caliburn.Micro;

    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Windows.UI.Core;
    using Windows.UI.Xaml;

    public class RssHolder : INewsHolder
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
            Execute.OnUIThread(() => newsHeaders.Add(feed));
        }

        public void RemoveLine(RssFeed feed)
        {
            newsHeaders.Remove(feed);
        }

        public async void Refresh(IDownloader loader, IParser parser)
        {
            for (int i = 0; i < newsHeaders.Count; i++)
            {
                var feed = newsHeaders[i];
                feed = parser.ParseXml(feed.Url, await loader.DownloadAsync(feed.Url));
                Execute.OnUIThread(() => newsHeaders[i] = feed);
            }
        }
    }
}