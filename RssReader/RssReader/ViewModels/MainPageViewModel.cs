using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader.ViewModels
{
    using System.Collections.ObjectModel;

    using Caliburn.Micro;

    using RssReader.Storage;

    public class MainPageViewModel : Screen
    {
        private readonly INavigationService navigationService;

        private readonly IDownloader downloader;

        private readonly IParser parser;

        private readonly INewsHolder holder;

        private EventAggregator eventAggregator = new EventAggregator();

        public MainPageViewModel(
            INavigationService navigationService,
            IDownloader downloader,
            IParser parser,
            INewsHolder holder)
        {
            this.navigationService = navigationService;
            this.downloader = downloader;
            this.parser = parser;
            this.holder = holder;
        }

        public ObservableCollection<RssFeed> News { get; private set; }

        public void GoToDetail(RssFeed feed)
        {
            this.navigationService.NavigateToViewModel<ItemPageViewModel>(feed);
        }

        public void DeleteItem(RssFeed feed)
        {
            this.News.Remove(feed);
            this.holder.RemoveLine(feed);
        }

        public void AddNewsLine()
        {
            navigationService.NavigateToViewModel<AddPageViewModel>();
        }

        public new void Refresh()
        {
            holder.Refresh(downloader, parser);
        }

        protected override void OnInitialize()
        {
            if (this.News == null)
            {
                this.News = holder.GetNewsLines();
            }
        }
    }
}