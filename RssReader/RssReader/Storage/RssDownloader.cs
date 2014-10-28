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

        public async Task<string> Download()
        {
            var client = new HttpClient();
            return await client.GetStringAsync(Url);
        }

        public XDocument CreateDoc(string feed)
        {
            return XDocument.Parse(feed);
        }
    }
}
