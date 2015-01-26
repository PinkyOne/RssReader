using System;
using System.Linq;

namespace RssReader.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Net.Http;
    using System.Threading;

    using Caliburn.Micro;

    using RssReader.Storage;

    public class MainPageViewModel : Screen, IHandle<string>
    {
        private readonly INavigationService navigationService;

        private readonly IDownloader downloader;

        private readonly IParser parser;

        private readonly INewsHolder holder;

        private IEventAggregator eventAggregator;

        private Timer timer;

        private bool onMainThread;

        public MainPageViewModel(
            INavigationService navigationService,
            IDownloader downloader,
            IParser parser,
            INewsHolder holder,
            IEventAggregator eventAggregator)
        {
            this.navigationService = navigationService;
            this.downloader = downloader;
            this.parser = parser;
            this.holder = holder;
            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);
            TimerCallback timerCallback = this.Refresh;
            timer = new Timer(timerCallback, null, 100000, 3000);
        }

        public ObservableCollection<RssFeed> News
        {
            get
            {
                return holder.GetNewsLines();
            }
        }

        public void GoToDetail(RssFeed feed)
        {
            this.navigationService.NavigateToViewModel<ItemPageViewModel>(feed);
        }

        public void UnmarkItem()
        {
            var line = from feed in News where feed.OnDelete select feed;
            if (line.Any()) line.First().OnDelete = false;
        }

        public void PrepareToDelete(RssFeed feed)
        {
            this.UnmarkItem();
            feed.OnDelete = true;
        }

        public void DeleteItem()
        {
            this.holder.RemoveLine((from line in News where line.OnDelete select line).First());
        }

        public void AddNewsLine()
        {
            navigationService.NavigateToViewModel<AddPageViewModel>();
        }

        public new void Refresh()
        {
            holder.Refresh(downloader, parser);
        }

        public void Handle(string message)
        {
            if (message != "All is ok") navigationService.NavigateToViewModel<ExceptionPageViewModel>(message);
        }

        protected override void OnActivate()
        {
            onMainThread = true;
        }

        protected override void OnDeactivate(bool close)
        {
            onMainThread = false;
        }

        private void Refresh(object state)
        {
            this.Refresh();
        }
    }
}