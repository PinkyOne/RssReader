namespace RssReader.Storage
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Windows.UI.Xaml.Controls;

    using Caliburn.Micro;

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
            foreach (RssFeed newsFeed in newsHeaders)
            {
                var url = newsFeed.Url;
                var feed = parser.ParseXml(url, await loader.DownloadAsync(url));
                var newItems = from item in feed.Items
                               join oldItem in newsFeed.Items on item.Title equals oldItem.Title into
                                   coincidingItems
                               from consItem in coincidingItems.DefaultIfEmpty()
                               select consItem;
                Execute.OnUIThread(
                    () =>
                        {
                            foreach (var item in newItems)
                            {
                                newsFeed.Items.Add(item);
                            }
                        });
            }
        }
    }
}
