namespace RssReader.Storage
{
    public interface IParser
    {
        RssFeed ParseXml(string url, string feed, int feedId);
    }
}