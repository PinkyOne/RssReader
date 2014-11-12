using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader.ViewModels
{
    using System.Collections.ObjectModel;

    using Caliburn.Micro;

    using RssReader.Storage;

    public class MainPageViewModel : Screen
    {
        private readonly INavigationService navigationService;

        private readonly WinRTContainer container;

        private readonly IDownloader downloader;

        private readonly IParser parser;

        public MainPageViewModel(
            WinRTContainer container,
            INavigationService navigationService,
            IDownloader downloader,
            IParser parser)
        {
            this.navigationService = navigationService;
            this.container = container;
            this.downloader = downloader;
            this.parser = parser;
        }
        
        public ObservableCollection<RssFeed> News { get; private set; }

        public void GoToDetail(RssFeed feed)
        {
            this.navigationService.NavigateToViewModel<ItemPageViewModel>(feed);
        }

        public void DeleteItem(RssFeed feed)
        {
            this.News.Remove(feed);
        }

        public void AddNewsLine()
        {
            navigationService.NavigateToViewModel<AddPageViewModel>();
        }

        public async void Refresh()
        {
        }

        protected override void OnInitialize()
        {
            if (this.News == null)
            {
                string[] news =
                    downloader.DownloadAsync(
                        new[] { "http://news.yandex.ru/computers.rss", "http://news.yandex.ru/auto.rss" });

                this.News =
                    new ObservableCollection<RssFeed>(
                        (from stringFeed in news let feed = parser.ParseXml(stringFeed) select feed).ToArray());
            }
        }
    }
}