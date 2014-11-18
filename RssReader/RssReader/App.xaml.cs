// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="mrecdev">
//   saved
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RssReader
{
    using System;
    using System.Collections.Generic;

    using Caliburn.Micro;

    using RssReader.Storage;
    using RssReader.ViewModels;
    using RssReader.Views;

    using Windows.ApplicationModel;
    using Windows.ApplicationModel.Activation;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;

    public sealed partial class App
    {
        private WinRTContainer container;

        private SimpleContainer childContainer;

        public App()
        {
            InitializeComponent();
        }

        protected override void Configure()
        {
            this.container = new WinRTContainer();

            this.container.RegisterWinRTServices();

            childContainer = this.container.CreateChildContainer();

            this.container.Singleton<IDownloader, RssDownloader>();
            this.container.Singleton<IParser, RssXmlParser>();
            this.container.Singleton<INewsHolder, RssHolder>();

            MessageBinder.SpecialValues.Add("$clickeditem", c => ((ItemClickEventArgs)c.EventArgs).ClickedItem);
            MessageBinder.SpecialValues.Add(
                "$longtappeditem",
                c => ((TextBlock)((RightTappedRoutedEventArgs)c.EventArgs).OriginalSource).DataContext);

            this.container.PerRequest<ItemPageViewModel>();
            this.container.PerRequest<DetailPageViewModel>();
            this.container.PerRequest<MainPageViewModel>();
            this.container.PerRequest<AddPageViewModel>();
        }

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            this.container.RegisterNavigationService(rootFrame);
        }

        protected override object GetInstance(Type service, string key)
        {
            return this.container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return this.container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            this.container.BuildUp(instance);
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            DisplayRootView<MainPageView>();
        }

        protected override async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await SuspensionManager.SaveAsync(container.GetInstance<INewsHolder>().GetNewsLines());
            deferral.Complete();
        }
    }
}