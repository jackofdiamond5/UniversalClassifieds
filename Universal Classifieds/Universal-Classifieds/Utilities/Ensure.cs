using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace AutomaticListigs.Utilities
{
    public class Ensure
    {
        /// <summary>
        /// Ensures that a directory exists in the application's data folder.
        /// If such a folder does not exist, it is created.
        /// </summary>
        /// <param name="localFolder"></param>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public async Task DirectoryExistsAsync(StorageFolder localFolder, string folderName)
        {
            var localFolderContents = await localFolder.GetFoldersAsync();
            if (!localFolderContents.Any(f => f.Name.Equals(folderName)))
            {
                await localFolder.CreateFolderAsync(folderName);
            }
        }

        /// <summary>
        /// Ensures that the specified directory contains a file with the specified file name.
        /// If such a file does not exist, it is created.
        /// </summary>
        /// <param name="localFolder"></param>
        /// <param name="fileName"></param>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public async Task FileExistsInDirectoryAsync(StorageFolder localFolder, string fileName, string folderName)
        {
            var resourcesFolder = await localFolder.GetFolderAsync(folderName);
            var folderContents = await resourcesFolder.GetFilesAsync();
            if (!folderContents.Any(n => n.Name.Equals(fileName)))
            {
                await resourcesFolder.CreateFileAsync(fileName, CreationCollisionOption.FailIfExists);
            }
        }
    }
}
