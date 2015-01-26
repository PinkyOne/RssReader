namespace RssReader.Storage
{
    using System.Threading.Tasks;

    using Windows.Foundation;
    using Windows.Web.Syndication;

    public interface IDownloader
    {
        IAsyncOperationWithProgress<SyndicationFeed, RetrievalProgress> DownloadAsync(string url);
    }
}