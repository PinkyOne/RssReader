// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DetailPageViewModel.cs" company="mercdev">
//   my saves all
// </copyright>
// <summary>
//   View model of page
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Navigation.ViewModels
{
    using Caliburn.Micro;

    using RssReader.Storage;

    /// <summary>
    /// View model of page
    /// </summary>
    public class DetailPageViewModel : Screen
    {
        /// <summary>
        /// The navigation service of application.
        /// </summary>
        private INavigationService navigationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DetailPageViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">
        /// The navigation service of application.
        /// </param>
        /// <param name="item">
        /// The viewed item.
        /// </param>
        public DetailPageViewModel(INavigationService navigationService, RssItem item)
        {
            this.navigationService = navigationService;
            this.Parameter = item;
        }

        public string Title { get; private set; }

        public string PubDate { get; private set; }

        public string Feed { get; private set; }

        public RssItem Parameter { get; private set; }

        protected override void OnActivate()
        {
            this.Title = this.Parameter.Title;
            this.PubDate = this.Parameter.PublicDate;
            this.Feed = this.Parameter.Description;
            this.NotifyOfPropertyChange(() => this.Title);
            this.NotifyOfPropertyChange(() => this.Feed);
            this.NotifyOfPropertyChange(() => this.PubDate);
        }
    }
}
