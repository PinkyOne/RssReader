using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader.ViewModels
{
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    using Caliburn.Micro;

    using RssReader.Storage;

    using Windows.Foundation;
    using Windows.UI.Core;
    using Windows.Web.Syndication;

    using SQLite;

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

                var feedLoad = loader.DownloadAsync(url);

                holder.AddPlaceHolder();
                await feedLoad;
                var feedResult = feedLoad;
                if (feedResult == null)
                {
                    holder.RemovePlaceHolder();
                    eventAggregator.Publish("Can not download rss.\nTry another address.", Execute.OnUIThread);
                    return;
                }
                if (feedResult.Status == AsyncStatus.Completed)
                {
                    var feed = parser.ParseXml(
                        url,
                        feedResult.GetResults().GetXmlDocument(SyndicationFormat.Rss20).GetXml(),
                        holder.CountOfHeaders());
                    feedResult.Close();
                    if (feed != null)
                    {
                        holder.AddLine(feed);
                        
                        eventAggregator.Publish("All is ok", Execute.OnUIThread);
                    }
                    feedResult.Close();
                }
                else if (feedResult.Status == AsyncStatus.Error)
                {
                    var code = feedResult.ErrorCode;
                    feedResult.Close();
                    holder.RemovePlaceHolder();
                    eventAggregator.Publish("Can not download rss.\nTry another address.", Execute.OnUIThread);
                }

            }
            catch (Exception e)
            {
                eventAggregator.Publish(e.Message, Execute.OnUIThread);
            }
        }
    }
}