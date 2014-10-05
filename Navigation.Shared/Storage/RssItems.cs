// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RssItems.cs" company="mercdev">
//   mercdev
// </copyright>
// <summary>
//   Defines the RssItems type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace RssReader.Storage
{
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// The items.
    /// </summary>
    public class RssItems : ObservableCollection<RssItem>
    {
        /// <summary>
        /// The get item.
        /// </summary>
        /// <param name="title">
        /// The title.
        /// </param>
        /// <returns>
        /// The <see cref="RssItem"/>.
        /// </returns>
        public RssItem GetItem(string title)
        {
            return this.FirstOrDefault(itemForCheck => itemForCheck != null && itemForCheck.Title == title);
        }
    }
}
