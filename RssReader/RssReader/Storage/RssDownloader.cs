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
        private const string Url = "http://news.yandex.ua/movies.rss";

        public async Task<string> DownloadAsync()
        {
            var client = new HttpClient();
            return await client.GetStringAsync(Url).ConfigureAwait(false);
        }
    }
}
