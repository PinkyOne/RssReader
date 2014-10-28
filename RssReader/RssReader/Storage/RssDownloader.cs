using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader.Storage
{
    using System.Net.Http;
    using System.Xml.Linq;

    using Windows.Networking.BackgroundTransfer;

    public class RssDownloader : IDownloader
    {
        private const string Url = "http://news.yandex.ua/movies.rss";

        public async Task<string> AsyncDownload()
        {
            var client = new HttpClient();
            Task<string> stringAsync = client.GetStringAsync(Url);
            stringAsync.ConfigureAwait(false);
            return await stringAsync;
        }

        public XDocument CreateDoc(string feed)
        {
            return XDocument.Parse(feed);
        }
    }
}
