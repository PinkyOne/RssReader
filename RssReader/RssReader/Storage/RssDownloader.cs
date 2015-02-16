using System.Threading.Tasks;

namespace RssReader.Storage
{
    using System;
    using System.Net;
    using System.Net.Http;

    using Windows.Foundation;
    using Windows.Web.Syndication;

    public class RssDownloader : IDownloader
    {
        public IAsyncOperationWithProgress<SyndicationFeed, RetrievalProgress> DownloadAsync(string url)
        {
            try
            {
                var testerClient = new HttpClient();
                var responseMessage = testerClient.GetAsync(url).Result;
                if (responseMessage.StatusCode == HttpStatusCode.OK)
                {
                    var client = new SyndicationClient();
                    var feed = client.RetrieveFeedAsync(new Uri(url));
                    return feed;
                }
                return null;
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }
    }
}
