namespace RssReader.Storage
{
    using System.Threading.Tasks;
    using System.Xml.Linq;

    public interface IDownloader
    {
        Task<string> DownloadAsync(string url);

        string[] DownloadAsync(string[] urls);
    }
}