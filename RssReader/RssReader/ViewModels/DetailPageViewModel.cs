// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DetailPageViewModel.cs" company="mercdev">
//   my saves all
// </copyright>
// <summary>
//   View model of page
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace RssReader.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    using Caliburn.Micro;

    using HtmlAgilityPack;

    using RssReader.Storage;

    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Documents;

    public class DetailPageViewModel : Screen
    {
        private readonly IHtmlToBlocksConverter converter;

        public DetailPageViewModel(IHtmlToBlocksConverter converter)
        {
            this.converter = converter;
        }

        public string Title
        {
            get
            {
                return this.Parameter.Title;
            }
        }

        public string PubDate
        {
            get
            {
                return this.Parameter.PublicDate;
            }
        }

        public RssItem Parameter { get; private set; }

        public void Feed(object sender)
        {
            var blocks = sender as BlockCollection;
            var newBlocks = this.converter.GenerateBlocksForHtml(this.Parameter.Description);

            // Add the blocks to the RichTextBlock
            if (blocks != null)
            {
                blocks.Clear();
                foreach (var b in newBlocks)
                {
                    blocks.Add(b);
                }
            }
        }
    }
}