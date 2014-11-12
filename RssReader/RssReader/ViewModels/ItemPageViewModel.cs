// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainPageViewModel.cs" company="mercdev">
//   Saved
// </copyright>
// <summary>
//   The main page view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace RssReader.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Net;
    using System.Net.Http;
    using System.Runtime.InteropServices;

    using Caliburn.Micro;

    using RssReader.Storage;

    public class ItemPageViewModel : Screen
    {
        private static ObservableCollection<RssItem> feed;

        private readonly INavigationService navigationService;

        private readonly WinRTContainer container;

        private readonly IDownloader downloader;

        private readonly IParser parser;

        public ItemPageViewModel(
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

        public ObservableCollection<RssItem> Feed
        {
            get
            {
                return feed;
            }

            private set
            {
                feed = value;
                this.NotifyOfPropertyChange(() => this.Feed);
            }
        }

        public RssFeed Parameter { get; private set; }

        public void GoToDetail(RssItem item)
        {
            this.navigationService.NavigateToViewModel<DetailPageViewModel>(item);
        }

        protected override async void OnInitialize()
        {
            string[] s =
                downloader.DownloadAsync(
                    new[] { "http://news.yandex.ru/computers.rss", "http://news.yandex.ru/auto.rss" });
            if (this.Feed == null)
            {
                this.Feed = parser.ParseXml(s[0]).Items;
            }
        }
    }
}