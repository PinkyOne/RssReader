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

    /// <summary>
    /// The main page view model.
    /// </summary>
    public class MainPageViewModel : Screen
    {
        /// <summary>
        /// The navigation service of application.
        /// </summary>
        private readonly INavigationService navigationService;

        private ObservableCollection<RssItem> feed;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">
        /// The navigation service.
        /// </param>
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

        /// <summary>
        /// Goes to detail page on rss item.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
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