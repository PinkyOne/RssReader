namespace RssReader.Storage
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    public interface INewsHolder
    {
        ObservableCollection<RssFeed> GetNewsLines();

        void AddLine(RssFeed feed);

        void AddPlaceHolder();

        void RemoveLine(RssFeed feed);

        void Refresh(IDownloader loader, IParser parser);

        bool IsBusy();
    }
}
