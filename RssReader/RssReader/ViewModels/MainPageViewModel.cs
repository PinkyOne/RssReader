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

    public class MainPageViewModel : Screen
    {
        private static ObservableCollection<RssItem> feed;

        private readonly INavigationService navigationService;

        public MainPageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public ObservableCollection<RssItem> Feed
        {
            get
            {
                return MainPageViewModel.feed;
            }

            private set
            {
                MainPageViewModel.feed = value;
                this.NotifyOfPropertyChange(() => this.Feed);
            }
        }

        public void GoToDetail(RssItem item)
        {
            this.navigationService.NavigateToViewModel<DetailPageViewModel>(item);
        }

        protected override async void OnInitialize()
        {
            var client = new HttpClient();
            var s = await RssDownloader.Download("http://news.yandex.ua/movies.rss");
            if (this.Feed == null)
            {
                var asd = new RssXmlParser(RssDownloader.CreateDoc(s)).Items;
                this.Feed = new RssXmlParser(RssDownloader.CreateDoc(s)).Items;
            }
        }
    }
}