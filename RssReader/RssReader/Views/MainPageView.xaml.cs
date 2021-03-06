﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace RssReader.Views
{
    using Windows.UI.Xaml.Media.Imaging;

    using Caliburn.Micro;

    using RssReader.Storage;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPageView : Page
    {
        private static double verticalOffset = 0;

        public MainPageView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void ItemClicked(object sender, ItemClickEventArgs e)
        {
            verticalOffset = this.ScrollViewer.VerticalOffset;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.ScrollViewer.ChangeView(
                this.ScrollViewer.HorizontalOffset,
                verticalOffset,
                this.ScrollViewer.ZoomFactor);
        }

        private void ListViewRightTapped1(object sender, RightTappedRoutedEventArgs e)
        {
            var listView = sender as ListView;
            if (listView != null)
            {
                Flyout.ShowAttachedFlyout(listView);
            }
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            var flyout = this.ListView.FindName("Flyout") as Flyout;
            if (flyout != null)
            {
                flyout.Hide();
            }
        }

        private void ExceptionFlyoutButtonClick(object sender, RoutedEventArgs e)
        {
            var flyout = this.ListView.FindName("ExceptionFlyout") as Flyout;
            if (flyout != null)
            {
                flyout.Hide();
            }
        }

        private void ImageImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            var image = sender as Image;
            if (image != null)
            {
                image.Source = new BitmapImage(new Uri("ms-appx:///Assets/placeholde.png"));
            }

            this.InvalidateArrange();
        }
    }
}
