// Copyright (c) Microsoft. All rights reserved.
namespace RssReader.Storage
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    using Caliburn.Micro;

    using Windows.Storage;
    using Windows.Storage.Streams;

    /// <summary>
    /// SuspensionManager captures global session state to simplify process lifetime management
    /// for an application.  Note that session state will be automatically cleared under a variety
    /// of conditions and should only be used to store information that would be convenient to
    /// carry across sessions, but that should be discarded when an application crashes or is
    /// upgraded.
    /// </summary>
    public class SuspensionManager
    {
        public static async Task SaveAsync(ObservableCollection<RssFeed> collection)
        {
            try
            {
                // Serialize the session state synchronously to avoid asynchronous access to shared
                // state
                var sessionData = new MemoryStream();
                var serializer = new DataContractSerializer(typeof(RssFeed[]));
                serializer.WriteObject(sessionData, collection.ToArray());

                // Get an output stream for the SessionState file and write the state asynchronously
                var file =
                    await
                    ApplicationData.Current.LocalFolder.CreateFileAsync(
                        "collection", 
                        CreationCollisionOption.ReplaceExisting);
                using (var fileStream = await file.OpenStreamForWriteAsync())
                {
                    sessionData.Seek(0, SeekOrigin.Begin);
                    await sessionData.CopyToAsync(fileStream);
                }
            }
            catch (Exception e)
            {
                throw new SuspensionManagerException(e);
            }
        }

        public static async Task<RssFeed[]> RestoreAsync()
        {
            try
            {
                // Get the input stream for the SessionState file
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync("collection");
                var inStream = file.OpenSequentialReadAsync().GetResults();

                // Deserialize the Session State
                var serializer = new DataContractSerializer(typeof(RssFeed[]));
                return (RssFeed[])serializer.ReadObject(inStream.AsStreamForRead());
            }
            catch (Exception e)
            {
                throw new SuspensionManagerException(e);
            }
        }
    }
}