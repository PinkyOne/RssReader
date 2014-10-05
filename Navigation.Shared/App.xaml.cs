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

        /// <summary>
        /// The configure.
        /// </summary>
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

        /// <summary>
        /// The on launched.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            DisplayRootView<MainPageView>();
        }

        /// <summary>
        /// The get instance.
        /// </summary>
        /// <param name="service">
        /// The service.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        protected override object GetInstance(Type service, string key)
        {
            return this.container.GetInstance(service, key);
        }

        /// <summary>
        /// The get all instances.
        /// </summary>
        /// <param name="service">
        /// The service.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return this.container.GetAllInstances(service);
        }

        /// <summary>
        /// The build up.
        /// </summary>
        /// <param name="instance">
        /// The instance.
        /// </param>
        protected override void BuildUp(object instance)
        {
            this.container.BuildUp(instance);
        }
    }
}