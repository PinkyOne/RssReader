// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="mrecdev">
//   saved
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace RssReader
{
    using System;
    using System.Collections.Generic;

    using Caliburn.Micro;

    using HockeyApp;

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

        public App()
        {
            InitializeComponent();
        }

        protected override void Configure()
        {
            this.container = new WinRTContainer();

            this.container.RegisterWinRTServices();

            this.container.Singleton<IDownloader, RssDownloader>();
            this.container.Singleton<IParser, RssXmlParser>();
            this.container.Singleton<INewsHolder, RssHolder>();
            this.container.Singleton<IHtmlToBlocksConverter, HtmlToBlocksConverter>();

            MessageBinder.SpecialValues.Add("$clickeditem", c => ((ItemClickEventArgs)c.EventArgs).ClickedItem);
            MessageBinder.SpecialValues.Add(
                "$longtappeditem",
                c => ((FrameworkElement)((RightTappedRoutedEventArgs)c.EventArgs).OriginalSource).DataContext);

            this.container.PerRequest<ItemPageViewModel>();
            this.container.PerRequest<DetailPageViewModel>();
            this.container.PerRequest<MainPageViewModel>();
            this.container.PerRequest<AddPageViewModel>();
            this.container.PerRequest<ExceptionPageViewModel>();
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

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            DisplayRootView<MainPageView>();
            HockeyClient.Current.Configure("41c0efbe685a736005dca035c8636a54");
            await HockeyClient.Current.SendCrashesAsync();
        }

        protected override async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await SuspensionManager.SaveAsync(container.GetInstance<INewsHolder>().GetNewsLines());
            deferral.Complete();
        }
    }
}