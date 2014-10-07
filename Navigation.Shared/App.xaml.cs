// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="mrecdev">
//   saved
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Navigation
{
    using System;
    using System.Collections.Generic;

    using Caliburn.Micro;

    using Navigation.ViewModels;
    using Navigation.Views;

    using Windows.ApplicationModel.Activation;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App
    {
        /// <summary>
        /// The container.
        /// </summary>
        private WinRTContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            InitializeComponent();
        }

        protected override void Configure()
        {
            this.container = new WinRTContainer();

            this.container.RegisterWinRTServices();

            MessageBinder.SpecialValues.Add("$clickeditem", c => ((ItemClickEventArgs)c.EventArgs).ClickedItem);

            this.container.PerRequest<MainPageViewModel>();
            this.container.PerRequest<DetailPageViewModel>();
        }

        /// <summary>
        /// The prepare view first.
        /// </summary>
        /// <param name="rootFrame">
        /// The root frame.
        /// </param>
        protected override void PrepareViewFirst(Frame rootFrame)
        {
            this.container.RegisterNavigationService(rootFrame);
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            DisplayRootView<MainPageView>();
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
    }
}