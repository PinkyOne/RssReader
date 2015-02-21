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

    public class ItemPageViewModel : Screen
    {
        private readonly INavigationService navigationService;

        public ItemPageViewModel(
            INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public ObservableCollection<RssItem> Feed
        {
            get
            {
                return Parameter.GetItems();
            }
        }

        public RssFeed Parameter { get; private set; }

        public void GoToDetail(RssItem item)
        {
            this.navigationService.NavigateToViewModel<DetailPageViewModel>(item);
            if (item.Opacity == 1)
            {
                item.Opacity = 0.5;
                Parameter.CountUnviewedItems--;
            }
        }
    }
}