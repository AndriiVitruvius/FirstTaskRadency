using FirstTaskRadency.PaymentTransactions;
using FirstTaskRadency.Serialize;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO.Packaging;
using System.Diagnostics;
using FirstTaskRadency.Service;
using FirstTaskRadency.Readers;

namespace FirstTaskRadency.Model
{
    internal static class MainModel
    {
        internal static void SaveFileInNewFormat(FileSystemEventArgs FileSystem)
        {
           

            string fullPath = FileSystem.FullPath;

            try
            {

                IEnumerable<ReaderForFiles> ReadersFiles = new List<ReaderForFiles>(2)
                {
                    new CsvReader(),
                    new TxtReader(),

                    //can make more readers 
                };


                foreach (var handler in ReadersFiles)
                    if (handler.IsFileFormatValid(fullPath))
                    {
                        Logger.ParsedFile();

                        IEnumerable<ITaransactionInformation> Information = handler.GetTaransatcionInformation(fullPath);

                        if (Information.Count() == 0)
                            break;

                        string outputJson = Information.MakeJson();
                        SaveJsonFile(outputJson);

                        break;
                    }

            }
            catch (Exception)
            {
                Logger.Error(fullPath);
            }
        }

        private static void SaveJsonFile(string outputJson)
        {
            string pathMainFolder = ConfigurationManager.AppSettings["FolderB"];


            // Make Folder 
            string outputFolder = Path.Combine(pathMainFolder, DateTime.Now.ToString("MM-dd-yyyy"));
            Directory.CreateDirectory(outputFolder);

            //Make File 
            string outputPathname = MakeName(outputFolder);
            File.WriteAllText(outputPathname, outputJson);

            ///Add +1 to name
            string MakeName(string pathFolder)
            {
                string baseFileName = "output";
                string fileExtension = ".json";

                StringBuilder sb = new StringBuilder();

               
                sb.Append(baseFileName);
                sb.Append(fileExtension);

                string fileName = Path.Combine(pathFolder, sb.ToString());
                int fileCount = 0;

                while (File.Exists(fileName))
                {
                    fileCount++;
                    fileName = Path.Combine(pathFolder, $"{baseFileName}{fileCount}{fileExtension}");
                }


                return fileName;

            }
        }
    }
}
