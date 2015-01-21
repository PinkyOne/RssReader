using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader.ViewModels
{
    using System.Runtime.InteropServices;

    using Caliburn.Micro;

    using RssReader.Storage;

    using Windows.UI.Core;

    public class AddPageViewModel : Screen
    {
        private readonly INavigationService navigationService;

        private readonly INewsHolder holder;

        private readonly IDownloader loader;

        private readonly IParser parser;

        private readonly IEventAggregator eventAggregator;

        public AddPageViewModel(
            INavigationService navigationService,
            INewsHolder holder,
            IDownloader loader,
            IParser parser,
            IEventAggregator eventAggregator)
        {
            this.parser = parser;
            this.holder = holder;
            this.loader = loader;
            this.navigationService = navigationService;
            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);
        }

        public async void AddNewsLine(string url)
        {
            try
            {
                navigationService.NavigateToViewModel<MainPageViewModel>();
                string feed = await loader.DownloadAsync(url).ConfigureAwait(false);
                if (feed != null)
                {
                    var rssFeed = parser.ParseXml(url, feed);
                    holder.AddLine(rssFeed);
                    eventAggregator.Publish("All is ok", Execute.OnUIThread);
                    return;
                }
                eventAggregator.Publish("Can not download rss.\nTry another address.", Execute.OnUIThread);
            }
            catch (Exception e)
            {
                eventAggregator.Publish(e.Message, Execute.OnUIThread);
            }
        }
    }
}