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

    public class DetailPageViewModel : Screen
    {

        public string Title
=======
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
>>>>>>> origin/master
        {
            get
            {
                return this.Parameter.Title;
            }
        }

<<<<<<< HEAD
        public string PubDate
        {
            get
            {
                return this.Parameter.PublicDate;
            }
        }

        public string Feed
=======
        public string Title { get; private set; }

        public string PubDate { get; private set; }

        public string Feed { get; private set; }

        public RssItem Parameter { get; private set; }

        protected override void OnActivate()
        {
            get
            {
                return this.Parameter.Description;
            }
        }

        public RssItem Parameter { get; private set; }
    }
}
