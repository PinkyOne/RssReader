using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader.Storage
{
    using System.Net.Http;
    
    public class RssDownloader : IDownloader
    {
        public async Task<string> DownloadAsync(string url)
        {
            var client = new HttpClient();
            return await client.GetStringAsync(url).ConfigureAwait(false);
        }

        public string[] DownloadAsync(string[] urls)
        {
            Task<string>[] tasks = (from url in urls let task = DownloadAsync(url) select task).ToArray();
            Task<string>.WaitAll(tasks);
            return (from task in tasks let feed = task.Result select feed).ToArray();
        }
    }
}
