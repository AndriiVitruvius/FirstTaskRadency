using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTaskRadency.Service
{
    internal static class Logger
    {
       internal static FilesListener filesListener;
        /// <summary>
        /// Write Path file to Log and counting errors
        /// </summary>
        /// <param name="PathFile"></param>
        internal static void Error(string PathFile)
        {
            filesListener.WriteLine($"Error:{PathFile}");
            ViewModel.ViewModelBase.Error.Invoke(PathFile);
        }



        /// <summary>
        /// Counting Files
        /// </summary>
        internal static void ParsedFile() =>
             filesListener.WriteLine($"ParsedFile:");

        /// <summary>
        /// Counting Lines
        /// </summary>
        internal static void ParsedLine()=>
            filesListener.WriteLine($"ParsedLine:");


    }
}
