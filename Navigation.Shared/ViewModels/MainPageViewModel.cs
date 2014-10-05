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
        /// The navigation service.
        /// </summary>
        private readonly INavigationService navigationService;

        /// <summary>
        /// The feed.
        /// </summary>
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
        
        /// <summary>
        /// Gets the feed.
        /// </summary>
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
        /// The go to detail.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        public void GoToDetail(RssItem item)
        {
            this.navigationService.NavigateToViewModel<DetailPageViewModel>(item);
        }

        /// <summary>
        /// The on activate.
        /// </summary>
        protected override void OnActivate()
        {
            if (this.Feed == null)
            {
                this.Feed = new RssFeed("asd").Items;
            }
        }
    }
}