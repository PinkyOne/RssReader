using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader.Storage
{
    using System.Xml.Linq;

    internal interface IDownloader
    {
        Task<string> AsyncDownload();

        XDocument CreateDoc(string feed);
    }
}
