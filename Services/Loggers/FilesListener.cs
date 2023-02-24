using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTaskRadency.Service
{
    internal class FilesListener : TraceListener
    {

        private int parsedFiles ;

        private int parsedLines ;

        private int foundErrors ;

        private List<string> invalidFiles = new List<string>();


        public FilesListener()
        {
            parsedFiles = 0;
            parsedLines = 0;
            foundErrors = 0;
        }

        public override void Write(string message)
        {

        }

        public override void WriteLine(string message)
        {
            if (message.StartsWith("Error:"))
            {
                foundErrors++;
                invalidFiles.Add(message.Substring(6));
            }
            else if (message.StartsWith("ParsedFile:"))
            {
                parsedFiles++;
            }
            else if(message.StartsWith("ParsedLine:"))
            {
                 parsedLines++;
            }
        }


        public void WriteMetaLog()
        {
            string pathMainFolder = ConfigurationManager.AppSettings["PathMainFolder"];


            //Make Folder if dont have
            string outputFolder = Path.Combine(pathMainFolder, DateTime.Now.ToString("MM-dd-yyyy"));
            Directory.CreateDirectory(outputFolder);

  
            string metaLogFilePath = MakeName(outputFolder);


            //Delete repets
            HashSet<string> filesPathFilter = new HashSet<string>();
            foreach (var item in invalidFiles)
                filesPathFilter.Add(item);    




            // Build the meta.log message
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"parsed_files: {parsedFiles}");
            sb.AppendLine($"parsed_lines: {parsedLines}");
            sb.AppendLine($"found_errors: {foundErrors}");
            sb.AppendLine($"invalid_files: [{string.Join(", ", filesPathFilter)}]");


            // Write the meta.log message to the file
            File.WriteAllText(metaLogFilePath, sb.ToString());


            string MakeName(string pathFolder)
            {
                string baseFileName = "meta";
                string fileExtension = ".log";

                StringBuilder sb1 = new StringBuilder();


                sb1.Append(baseFileName);
                sb1.Append(fileExtension);

                string fileName = Path.Combine(pathFolder, sb1.ToString());
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
