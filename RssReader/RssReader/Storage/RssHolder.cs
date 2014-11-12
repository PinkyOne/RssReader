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
            NewsHeaders.Remove(url);
        }
    }
}
