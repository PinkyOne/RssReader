namespace RssReader.Storage
{
    using System.Collections.ObjectModel;

    public interface INewsHolder
    {
        ObservableCollection<RssFeed> GetNewsLines();

        void AddLine(RssFeed feed);

        void RemoveLine(RssFeed feed);

        void Refresh(IDownloader loader, IParser parser);
    }
}
