// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DetailPageViewModel.cs" company="mercdev">
//   my saves all
// </copyright>
// <summary>
//   View model of page
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RssReader.ViewModels
{
    using Caliburn.Micro;

    using RssReader.Storage;

    public class DetailPageViewModel : Screen
    {
        public string Title
        {
            get
            {
                return this.Parameter.Title;
            }
        }

        public string PubDate
        {
            get
            {
                return this.Parameter.PublicDate;
            }
        }

        public string Feed
        {
            get
            {
                return this.Parameter.Description;
            }
        }

        public RssItem Parameter { get; private set; }
    }
}