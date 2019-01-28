using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace AutomaticListigs.Utilities
{
    public class FileReader
    {
        /// <summary>
        /// Searches for the targeted file in all subfolders and reads from it.
        /// </summary>
        /// <param name="dataFolder"></param>
        /// <param name="targetFileName"></param>
        /// <returns></returns>
        public string ReadDataFromFile(StorageFolder dataFolder, string targetFileName)
        { 
            var directory = new DirectoryInfo(dataFolder.Path);
            var targetFiles = directory
                .GetFiles(targetFileName, SearchOption.AllDirectories);

            if (targetFiles.Length > 0)
                return File.ReadAllText(targetFiles.First().FullName);

            throw new FileNotFoundException();
        }
    }
}
