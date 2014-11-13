using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader.ViewModels
{
    using System.Runtime.InteropServices;

    using Windows.UI.Core;

    using Caliburn.Micro;

    using RssReader.Storage;

    public class AddPageViewModel : Screen
    {
        private readonly INavigationService navigationService;

        private readonly INewsHolder holder;

        private readonly IDownloader loader;

        private readonly IParser parser;

        public AddPageViewModel(INavigationService navigationService, INewsHolder holder,IDownloader loader,IParser parser)
        {
            this.parser = parser;
            this.holder = holder;
            this.loader = loader;
            this.navigationService = navigationService;
        }

        public async void AddNewsLine(string url)
        {
            navigationService.NavigateToViewModel<MainPageViewModel>();
            string feed = await loader.DownloadAsync(url).ConfigureAwait(false);
            var rssFeed = parser.ParseXml(url, feed);
            holder.AddLine(rssFeed);
        }
    }
}
