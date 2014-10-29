namespace RssReader.Storage
{
    using System.Xml.Linq;

    public interface IParser
    {
        RssItems ParseXml(XDocument xmlDoc);

        XDocument CreateDoc(string feed);
    }
}