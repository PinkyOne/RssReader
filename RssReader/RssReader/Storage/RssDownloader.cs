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

    public class RssDownloader
    {
        const string url = "http://news.yandex.ua/movies.rss";

        public static async Task<string> Download()
        {
            var client = new HttpClient();
            return await client.GetStringAsync(url);
        }

        public static XDocument CreateDoc(string feed)
        {
            return XDocument.Parse(feed);
        }
    }
}
