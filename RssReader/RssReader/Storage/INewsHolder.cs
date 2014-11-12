using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader.Storage
{
    using System.Collections.ObjectModel;

    public interface INewsHolder
    {
        ObservableCollection<string> GetNewsLines();

        void AddLine(string url);

        void RemoveLine(string url);
    }
}
