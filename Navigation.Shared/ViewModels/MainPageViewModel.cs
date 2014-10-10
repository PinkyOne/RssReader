// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainPageViewModel.cs" company="mercdev">
//   Saved
// </copyright>
// <summary>
//   The main page view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Navigation.ViewModels
{
    using System.Collections.ObjectModel;

    using Caliburn.Micro;

    using RssReader.Storage;

    public class MainPageViewModel : Screen
    {
        private readonly INavigationService navigationService;

        private ObservableCollection<RssItem> feed;

        public MainPageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
        
        public ObservableCollection<RssItem> Feed
        {
            get
            {
                return this.feed;
            }

            private set
            {
                this.feed = value;
                this.NotifyOfPropertyChange(() => this.Feed);
            }
        }

        public void GoToDetail(RssItem item)
        {
            this.navigationService.NavigateToViewModel<DetailPageViewModel>(item);
        }

        protected override void OnActivate()
        {
            if (this.Feed == null)
            {
                this.Feed = new RssFeed("asd").Items;
            }
        }
    }
}