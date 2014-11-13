using System.Linq;
using System.Threading.Tasks;

namespace RssReader.Storage
{
    using System;
    using System.Collections.ObjectModel;
    using System.Net.Http;
    
    public class RssDownloader : IDownloader
    {
        public async Task<string> DownloadAsync(string url)
        {
            var client = new HttpClient();
            try
            {
                return await client.GetStringAsync(url).ConfigureAwait(false);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public string[] DownloadAsync(string[] urls)
        {
            Task<string>[] tasks = (from url in urls let task = DownloadAsync(url) select task).ToArray();
            Task.WaitAll(tasks);
            return (from task in tasks let feed = task.Result select feed).ToArray();
        }
    }
}
