// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainPageView.xaml.cs" company="mercdev">
//   seavd
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace RssReader.Views
{
    using System;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Media.Imaging;

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

        private void ImageImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            var image = sender as Image;
            if (image != null)
            {
                image.Source = new BitmapImage(new Uri("ms-appx:///Assets/placeholde.png"));
            }
        }
    }
}
