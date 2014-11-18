﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainPageView.xaml.cs" company="mercdev">
//   seavd
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RssReader.Views
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public sealed partial class ItemPageView : Page
    {
        private static double verticalOffset = 0;

        public ItemPageView()
        {
            this.InitializeComponent();
        }

        private void ItemClicked(object sender, ItemClickEventArgs e)
        {
            verticalOffset = this.ScrollViewer.VerticalOffset;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.ScrollViewer.ChangeView(this.ScrollViewer.HorizontalOffset, verticalOffset, this.ScrollViewer.ZoomFactor);
        }
    }
}