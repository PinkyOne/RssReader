namespace RssReader.Storage
{
    using System.Collections.ObjectModel;
    using System.Linq;

    using Windows.UI.Core;
    using Windows.UI.Xaml;

    using Caliburn.Micro;

    public class RssHolder : INewsHolder
    {
        private static ObservableCollection<RssFeed> newsHeaders = new ObservableCollection<RssFeed>();

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
                newsHeaders[i] = feed;
            }
        }
    }
}