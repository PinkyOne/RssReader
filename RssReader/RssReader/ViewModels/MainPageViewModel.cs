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

        private readonly WinRTContainer container;

        public MainPageViewModel(WinRTContainer container, INavigationService navigationService)
        {
            this.navigationService = navigationService;
            this.container = container;
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

        public void GoToDetail(RssItem item)
        {
            this.navigationService.NavigateToViewModel<DetailPageViewModel>(item);
        }

        protected override async void OnInitialize()
        {
            var loader = container.GetInstance<IDownloader>();
            
            string s = await loader.AsyncDownload();
            if (this.Feed == null)
            {
                var items = RssXmlParser.ParseXml(loader.CreateDoc(s));
                this.Feed = items.Items;
            }
        }
    }
}