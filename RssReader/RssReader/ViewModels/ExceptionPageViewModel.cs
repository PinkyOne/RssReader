using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader.ViewModels
{
    using System.Net.Http;

    using Caliburn.Micro;

    public class ExceptionPageViewModel : Screen
    {
        private readonly INavigationService navigationService;

        public ExceptionPageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public string Parameter { get; private set; }

        public void ReturnToMainPage()
        {
            Execute.OnUIThread(() => navigationService.GoBack());
        }
    }
}
