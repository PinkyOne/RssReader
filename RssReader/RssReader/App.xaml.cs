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

    using Windows.ApplicationModel;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Input;

    using Caliburn.Micro;

    using RssReader.Storage;
    using RssReader.ViewModels;
    using RssReader.Views;

    using Windows.ApplicationModel.Activation;
    using Windows.UI.Xaml.Controls;

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

            MessageBinder.SpecialValues.Add("$clickeditem", c => ((ItemClickEventArgs)c.EventArgs).ClickedItem);
            MessageBinder.SpecialValues.Add(
                "$longtappeditem",
                c => ((TextBlock)((RightTappedRoutedEventArgs)c.EventArgs).OriginalSource).DataContext);

            this.container.PerRequest<ItemPageViewModel>();
            this.container.PerRequest<DetailPageViewModel>();
            this.container.PerRequest<MainPageViewModel>();
            this.container.PerRequest<AddPageViewModel>();


            var navigation = this.container.GetInstance(typeof(INavigationService), null) as INavigationService;
            if (navigation != null)
            {
                navigation.ResumeState();
            }
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

        protected async override void OnLaunched(LaunchActivatedEventArgs args)
        {
            var rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                // Associate the frame with a SuspensionManager key                                
                SuspensionManager.RegisterFrame(rootFrame, "AppFrame");

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // Restore the saved session state only when appropriate
                    try
                    {
                        await SuspensionManager.RestoreAsync();
                    }
                    catch (SuspensionManagerException)
                    {
                        // Something went wrong restoring state.
                        // Assume there is no state and continue
                    }
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }
            if (rootFrame.Content == null || !string.IsNullOrEmpty(args.Arguments))
            {
                // When the navigation stack isn't restored or there are launch arguments
                // indicating an alternate launch (e.g.: via toast or secondary tile), 
                // navigate to the appropriate page, configuring the new page by passing required 
                // information as a navigation parameter
                if (!rootFrame.Navigate(typeof(MainPageView), args.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }

            // Ensure the current window is active
            Window.Current.Activate();

            DisplayRootView<MainPageView>();
        }

        protected async override void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await SuspensionManager.SaveAsync();
            deferral.Complete();
        }
    }
}