// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RssFeed.cs" company="mercdev">
//   All rigths saved
// </copyright>
// <summary>
//   Defines the RssFeed type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace RssReader.Storage
{
    using System;
    using System.Net;
    using System.Xml;
    using System.Xml.Linq;

    /// <summary>
    /// The feed.
    /// </summary>
    public class RssFeed
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RssFeed"/> class.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <exception cref="Exception">
        /// Throws io exception
        /// </exception>
        public RssFeed(string url)
        {
            this.Items = new RssItems();

            XmlReader xmlTextReader;

     // xmlTextReader = XmlReader.Create(Url);ow; }
            var xmlDoc = new XDocument(
                new XComment("This is a comment"), 
                new XElement(
                    "channel", 
                    new XElement("title", "data1"), 
                    new XElement("description", "data2"), 
                    new XElement("link", "data3"), 
                    new XElement(
                        "item",
                        new XElement("title", "Хромбуки cпасли мировой рынок ПК от сильного падения"),
                        new XElement(
                            "description",
                            "Здраствуй, Хабр! Хочу поделиться кодом простой программы, оторую я использую для уменьшения шума с цифровых фотограффий.Примерно восемь лет назад, рассматривая фотографии, снятые на свой первый цифровой фотоаппарат, я обнаружил, что некоторые снимки с тусклым освещением имеют какую-то странную мутность, цветные пятна, не резкость. В то время я еще не знал, что такое шум, как он зависит от параметра ISO и был очень разочарован, что фотоаппарат такой «некачественный». Однако, я обратил внимание, что на одинаковых снимках эти цветные пятна выглядят несколько по разному, меняются от кадра к кадру. Время шло, я научился снимать на ручных настройках, узнал, что такое шум, как правильно выставить светочуствительность и т.д. Спустя несколько лет, когда уже начал заниматься программированием, снова обратил внимание на то, что шум на изображениях не является статичным. В голове возникла идея: а что если взять, снять несколько абсолютно одинаковых изображений, а потом неким образом объединить их, устранив разность между снимками, т.е. шум? так, ниже представлены 4 изображения, демонстрирующие некие фотографии одного и того-же обьекта, со случайным шумом на каждом снимке. В качестве объекта представлены красные круги, в качестве шума — белые. Здраствуй, Хабр! Хочу поделиться кодом простой программы, которую я использую для уменьшения шума с цифровых фотограффий.Примерно восемь лет назад, рассматривая фотографии, снятые на свой первый цифровой фотоаппарат, я обнаружил, что некоторые снимки с тусклым освещением имеют какую-то странную мутность, цветные пятна, не резкость. В то время я еще не знал, что такое шум, как он зависит от параметра ISO и был очень разочарован, что фотоаппарат такой «некачественный». Однако, я обратил внимание, что на одинаковых снимках эти цветные пятна выглядят несколько по разному, меняются от кадра к кадру. Время шло, я научился снимать на ручных настройках, узнал, что такое шум, как правильно выставить светочуствительность и т.д. Спустя несколько лет, когда уже начал заниматься программированием, снова обратил внимание на то, что шум на изображениях не является статичным. В голове возникла идея: а что если взять, снять несколько абсолютно одинаковых изображений, а потом неким образом объединить их, устранив разность между снимками, т.е. шум? так, ниже представлены 4 изображения, демонстрирующие некие фотографии одного и того-же обьекта, со случайным шумом на каждом снимке. В качестве объекта представлены красные круги, в качестве шума — белые."),
                        new XElement("pubDate", "53.34.2222")), 
                    new XElement(
                        "item", 
                        new XElement("title", "dat3a45"), 
                        new XElement("description", "d4a52"), 
                        new XElement("pubDate", "12.43.1234")), 
                    new XElement(
                        "item", 
                        new XElement("title", "dat3a45"), 
                        new XElement("description", "d4a52"), 
                        new XElement("pubDate", "12.43.1234")), 
                    new XElement(
                        "item", 
                        new XElement("title", "dat3a45"), 
                        new XElement("description", "d4a52"), 
                        new XElement("pubDate", "12.43.1234")), 
                    new XElement(
                        "item", 
                        new XElement("title", "dat3a45"), 
                        new XElement("description", "d4a52"), 
                        new XElement("pubDate", "12.43.1234")), 
                    new XElement(
                        "item", 
                        new XElement(
                            "title", 
                            "datskdfghdksjfghlskdfglsdfhglskdhfglksdjhfglskdfhvsdkhfglsdfhglksdhfgksdhjf gdsl fhgd fgkdjs hfglsd hgsldj hgsld ghsdl hgsdl ghsdl gkhs3a45"), 
                        new XElement("description", "d4agffdffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddsszxcczczvczvfdzvfdzbfngfxmdrsntewdfguhjkhjgffdzsdfghjkmljhgfdresrdtfyguhikjlknbhvgfyghjbnhvgfygujbhvgcfdtfggcfxdsrdtfgfdsedrfgvhbjjkuhjk;.lkm,lkojikjhgyvhbgftdfcgvhbgyftdfghjgyftgvhbjgyftfgvhbjnkjhughyvhbjnjhgyvhbjhgvh52"), 
                        new XElement("pubDate", "12.43.1234")), 
                    new XElement(
                        "item", 
                        new XElement("title", "dat3a45"), 
                        new XElement("description", "d4a52"), 
                        new XElement("pubDate", "12.43.1234")), 
                    new XElement(
                        "item", 
                        new XElement("title", "dat3a45"), 
                        new XElement("description", "d4a52"), 
                        new XElement("pubDate", "12.43.1234")), 
                    new XElement(
                        "item", 
                        new XElement("title", "dat3a45"), 
                        new XElement("description", "d4a52"), 
                        new XElement("pubDate", "12.43.1234")), 
                    new XElement(
                        "item", 
                        new XElement("title", "dat3a45"), 
                        new XElement("description", "d4a52"), 
                        new XElement("pubDate", "12.43.1234")), 
                    new XElement(
                        "item", 
                        new XElement("title", "dat3a45"), 
                        new XElement("description", "d4a52"), 
                        new XElement("pubDate", "12.43.1234")), 
                    new XElement(
                        "item", 
                        new XElement("title", "dat3a45"), 
                        new XElement("description", "d4a52"), 
                        new XElement("pubDate", "12.43.1234")), 
                    new XElement(
                        "item", 
                        new XElement("title", "dat3a45"), 
                        new XElement("description", "d4a52"), 
                        new XElement("pubDate", "12.43.1234")), 
                    new XElement(
                        "item", 
                        new XElement("title", "dat3a45"), 
                        new XElement("description", "d4a52"), 
                        new XElement("pubDate", "12.43.1234")), 
                    new XElement(
                        "item", 
                        new XElement("title", "dat3a45"), 
                        new XElement("description", "d4a52"), 
                        new XElement("pubDate", "12.43.1234")), 
                    new XElement(
                        "item", 
                        new XElement("title", "dat3a45"), 
                        new XElement("description", "d4a52"), 
                        new XElement("pubDate", "12.43.1234")), 
                    new XElement(
                        "item", 
                        new XElement("title", "dat3a45"), 
                        new XElement("description", "d4a52"), 
                        new XElement("pubDate", "12.43.1234")), 
                    new XElement(
                        "item", 
                        new XElement("title", "dat3a45"), 
                        new XElement("description", "d4a52"), 
                        new XElement("pubDate", "12.43.1234")), 
                    new XElement(
                        "item", 
                        new XElement("title", "dat3a45"), 
                        new XElement("description", "d4a52"), 
                        new XElement("pubDate", "12.43.1234")), 
                    new XElement(
                        "item", 
                        new XElement("title", "dat3a45"), 
                        new XElement("description", "d4a52"), 
                        new XElement("pubDate", "12.43.1"))));

            try
            {
                var channelXmlNode = xmlDoc.Element("channel"); // парсим xml

                // P.S. надеюсь понятие не перепутал
                if (channelXmlNode != null)
                {
                    foreach (var channelNode in channelXmlNode.Elements())
                    {
                        switch (channelNode.Name.LocalName)
                        {
                            case "title":
                                {
                                    this.Title = channelNode.Value;
                                }

                                break;

                            case "description":
                                {
                                    this.Description = channelNode.Value;
                                }

                                break;

                            case "link":
                                {
                                    this.Link = channelNode.Value;
                                }

                                break;

                            case "item":
                                {
                                    // создаем новость
                                    var channelItem = new RssItem(channelNode);
                                    this.Items.Add(channelItem);
                                }

                                break;
                        }
                    }
                }
                else
                {
                    throw new Exception("Ошибка в XML. Описание канала не найдено!");
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ConnectFailure)
                {
                    throw new Exception("Невозможно соединиться с указаным источником.\r\n" + url);
                }

                throw;
            }
            catch (System.IO.FileNotFoundException)
            {
                throw new Exception("Файл " + url + "не найден!");
            }
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        public RssItems Items { get; private set; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the link.
        /// </summary>
        public string Link { get; private set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        public string Description { get; private set; } 
    }
}