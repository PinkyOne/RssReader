using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader.Storage
{
    using System.Collections.ObjectModel;

    public class RssHolder : INewsHolder
    {
        private static readonly ObservableCollection<string> NewsHeaders = new ObservableCollection<string>();

        public ObservableCollection<string> GetNewsLines()
        {
            return NewsHeaders;
        }

        public void AddLine(string url)
        {
            NewsHeaders.Add(url);
        }

        public void RemoveLine(string url)
        {
            string urlToDelete =null;
            foreach (var header in NewsHeaders)
            {
                if (header==url)
                {
                    urlToDelete = header;
                    break;
                }
            }
            /*var urlToDelete =
                (from header in NewsHeaders
                 where string.Compare(header, url, StringComparison.Ordinal) == 0
                 select header).First();*/
            NewsHeaders.Remove(urlToDelete);
        }
    }
}
