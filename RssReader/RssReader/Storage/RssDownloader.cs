using System.Threading.Tasks;

namespace RssReader.Storage
{
    using System;
    using System.Net.Http;

    using Windows.Foundation;
    using Windows.Web.Syndication;

    public class RssDownloader : IDownloader
    {
        public IAsyncOperationWithProgress<SyndicationFeed, RetrievalProgress> DownloadAsync(string url)
        {
            try
            {
                var client = new SyndicationClient();
                var feed = client.RetrieveFeedAsync(new Uri(url));
                return feed;
            }
            catch (HttpRequestException e)
            {
                return null;
            }
        }
    }
}
