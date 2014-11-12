using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace RssReader.Views
{
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

        private void OnLongTap(object sender, HoldingRoutedEventArgs e)
        {
            var item = sender as SelectorItem;
            if (item == null)
                return;

        }

        private void Refresh(object sender, RoutedEventArgs e)
        {

        }

        private void AddNewsline(object sender, RoutedEventArgs e)
        {

        }

        private void ListView_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void blocktapped(object sender, RightTappedRoutedEventArgs e)
        {

        }

        private void tapped(object sender, RightTappedRoutedEventArgs e)
        {

        }
    }
}
