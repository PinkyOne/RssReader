using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader.ViewModels
{
    using Caliburn.Micro;

    using RssReader.Storage;

    public class AddPageViewModel : Screen
    {
        private readonly INavigationService navigationService;

        private readonly INewsHolder holder;

        private readonly IDownloader loader;

        public AddPageViewModel(INavigationService navigationService,IDownloader loader, INewsHolder holder)
        {
            this.loader = loader;
            this.holder = holder;
            this.navigationService = navigationService;
        }

        public void AddNewsLine(string url)
        {
            holder.AddLine(loader, url);
            navigationService.NavigateToViewModel<MainPageViewModel>();
        }
    }
}
