using System;
using System.IO;
using System.Linq;
using Windows.Storage;

namespace AutomaticListigs.Utilities
{
    public class FileWriter
    {
       /// <summary>
       /// Searches for the targeted file in all subfolders and writes to it.
       /// </summary>
       /// <param name="dataFolder"></param>
       /// <param name="targetFileName"></param>
       /// <param name="data"></param>
        public void WriteDataToFile(StorageFolder dataFolder, string targetFileName, string data)
        {
            var directory = new DirectoryInfo(dataFolder.Path);
            var targetFiles = directory
                .GetFiles(targetFileName, SearchOption.AllDirectories);

            if (targetFiles.Length > 0)
            {
                File.WriteAllText(targetFiles.First().FullName, data);
                return;
            }

            throw new FileNotFoundException();
        }
    }
}
