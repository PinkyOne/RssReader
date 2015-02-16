using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader.Storage
{
    using Windows.UI.Xaml.Documents;

    public interface IHtmlToBlocksConverter
    {
        List<Block> GenerateBlocksForHtml(string xhtml);
    }
}
