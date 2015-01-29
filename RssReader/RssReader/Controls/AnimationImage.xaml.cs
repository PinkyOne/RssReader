using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml.Media.Animation;

namespace RssReader.Controls
{
    using Windows.UI.Xaml.Media.Animation;

    public sealed partial class AnimationImage : UserControl
    {
        #region Public Events

        /// <summary>
        /// Fired whenever the image has loaded
        /// </summary>
        public EventHandler ImageLoaded;

        #endregion

        #region Private Fields

        private static readonly DependencyProperty imageUrlProperty = DependencyProperty.Register(
            "ImageUrl",
            typeof(string),
            typeof(AnimationImage),
            new PropertyMetadata(string.Empty, ImageUrlPropertyChanged));

        private readonly List<WriteableBitmap> bitmapFrames = new List<WriteableBitmap>();

        private bool playOnLoad = true;

        #endregion

        #region Constructors

        public AnimationImage()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the url of the image e.g. "/Assets/MyAnimation.gif"
        /// </summary>
        public string ImageUrl
        {
            get
            {
                return (string)GetValue(imageUrlProperty);
            }
            set
            {
                SetValue(imageUrlProperty, value);
            }
        }

        /// <summary>
        /// Gets the number of frames in the gif animation
        /// </summary>
        public uint FrameCount { get; private set; }

        public bool PlayOnLoad
        {
            get
            {
                return this.playOnLoad;
            }
            set
            {
                this.playOnLoad = value;
            }
        }

        #endregion

        #region Private Methods

        private static void ImageUrlPropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            var control = (AnimationImage)sender;
            control.LoadImage();
        }

        private async void LoadImage()
        {
            if (string.IsNullOrEmpty(ImageUrl))
            {
                return;
            }

            try
            {
                // Get the file e.g. "/Assets/MyAnimation.gif"
                var storageFile =
                    await StorageFile.GetFileFromApplicationUriAsync(new Uri(string.Format("ms-appx://{0}", ImageUrl)));

                using (var res = await storageFile.OpenAsync(FileAccessMode.Read))
                {
                    // Get the GIF decoder, to perform the magic
                    var decoder = await BitmapDecoder.CreateAsync(BitmapDecoder.GifDecoderId, res);

                    // Now we know the number of frames
                    FrameCount = decoder.FrameCount;

                    // Extract each frame and create a WriteableBitmap for each of these (store them in an internal list)
                    for (uint frameIndex = 0; frameIndex < FrameCount; frameIndex++)
                    {
                        var frame = await decoder.GetFrameAsync(frameIndex);

                        var writeableBitmap = new WriteableBitmap(
                            (int)decoder.OrientedPixelWidth,
                            (int)decoder.OrientedPixelHeight);

                        // Extract the pixel data and fill the WriteableBitmap with them
                        var bitmapTransform = new BitmapTransform();
                        var pixelDataProvider =
                            await
                            frame.GetPixelDataAsync(
                                BitmapPixelFormat.Bgra8,
                                decoder.BitmapAlphaMode,
                                bitmapTransform,
                                ExifOrientationMode.IgnoreExifOrientation,
                                ColorManagementMode.DoNotColorManage);
                        var pixels = pixelDataProvider.DetachPixelData();

                        using (var stream = writeableBitmap.PixelBuffer.AsStream())
                        {
                            stream.Write(pixels, 0, pixels.Length);
                        }

                        // Finally we have a frame (WriteableBitmap) that can internally be stored.
                        this.bitmapFrames.Add(writeableBitmap);
                    }
                }

                // Fill out the story board for the animation magic
                BuildStoryBoard();

                // Start the animation if needed and fire the event
                if (PlayOnLoad)
                {
                    storyboard.Begin();

                    if (ImageLoaded != null)
                    {
                        ImageLoaded(this, null);
                    }
                }
            }
            catch
            {
                if (!Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                {
                    throw;
                }
            }
        }

        private void BuildStoryBoard()
        {
            // Clear the story board, if it has previously been filled
            if (storyboard.Children.Count > 0)
            {
                storyboard.Children.Clear();
            }

            // Now create the animation as a set of ObjectAnimationUsingKeyFrames (I love this name!)
            var anim = new ObjectAnimationUsingKeyFrames();
            anim.BeginTime = TimeSpan.FromSeconds(0);

            var ts = new TimeSpan();
            var speed = TimeSpan.FromMilliseconds(100); // Standard GIF framerate 10 fps?

            // Create each DiscreteObjectKeyFrame and advance the KeyTime by 100 ms (=10 fps) and add it to 
            // the storyboard.
            for (int frameIndex = 0; frameIndex < this.bitmapFrames.Count; frameIndex++)
            {
                var keyFrame = new DiscreteObjectKeyFrame();

                keyFrame.KeyTime = KeyTime.FromTimeSpan(ts);
                keyFrame.Value = this.bitmapFrames[frameIndex];

                ts = ts.Add(speed);
                anim.KeyFrames.Add(keyFrame);
            }

            // Connect the image control with the story board
            Storyboard.SetTarget(anim, image);
            Storyboard.SetTargetProperty(anim, "Source");

            // And finally add the animation-set to the storyboard
            storyboard.Children.Add(anim);
        }

        #endregion
    }
}
