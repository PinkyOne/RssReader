namespace RssReader.Storage
{
    using System.Threading.Tasks;

    public interface IDownloader
    {
        Task<string> DownloadAsync(string url);
    }
}