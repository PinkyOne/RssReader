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

        public AddPageViewModel(INavigationService navigationService, INewsHolder holder)
        {
            this.holder = holder;
            this.navigationService = navigationService;
        }

        public void AddNewsLine(string url)
        {
            holder.AddLine(url);
            navigationService.NavigateToViewModel<MainPageViewModel>();
        }
    }
}
