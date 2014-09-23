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
        /// The navigation service.
        /// </summary>
        private INavigationService navigationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DetailPageViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">
        /// The navigation Service.
        /// </param>
        /// <param name="item">
        /// The item.
        /// </param>
        public DetailPageViewModel(INavigationService navigationService, RssItem item)
        {
            this.navigationService = navigationService;
            this.Parameter = item;
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets public date
        /// </summary>
        public string PubDate { get; set; }

        /// <summary>
        /// Gets or sets the feed.
        /// </summary>
        public string Feed { get; set; }

        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        public RssItem Parameter { get; set; }

        /// <summary>
        /// The on activate.
        /// </summary>
        protected override void OnActivate()
        {
            this.Title = this.Parameter.Title;
            this.PubDate = this.Parameter.PublicDate;
            this.Feed = this.Parameter.Description;
            this.NotifyOfPropertyChange(() => this.Title);
            this.NotifyOfPropertyChange(() => this.Feed);
            this.NotifyOfPropertyChange(() => this.PubDate);
        }

        /// <summary>
        /// The on deactivate.
        /// </summary>
        /// <param name="close">
        /// The close.
        /// </param>
        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
        }
    }
}
