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
<<<<<<< HEAD
=======
        /// <summary>
        /// The navigation service of application.
        /// </summary>
>>>>>>> origin/master
        private readonly INavigationService navigationService;

        private ObservableCollection<RssItem> feed;

        public MainPageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
<<<<<<< HEAD
        
=======

>>>>>>> origin/master
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

<<<<<<< HEAD
=======
        /// <summary>
        /// Goes to detail page on rss item.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
>>>>>>> origin/master
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