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
            // var client = new HttpClient();
            try
            {
                var client = new SyndicationClient();
                var feed = client.RetrieveFeedAsync(new Uri(url));
                return feed;

                // var httpResponse = client.GetStringAsync(url).ConfigureAwait(false);
                // Progress<Task<string>> progressCallback=new Progress<Task<string>>();
                // return await httpResponse;
            }
            catch (HttpRequestException e)
            {
                return null;
            }
        }
    }
}
