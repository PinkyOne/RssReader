namespace RssReader.Storage
{
    using System.Xml.Linq;

    public interface IParser
    {
        RssFeed ParseXml(string feed);
    }
}