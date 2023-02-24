using FirstTaskRadency.PaymentTransactions;
using FirstTaskRadency.PaymentTransactions.Information;
using FirstTaskRadency.Service;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTaskRadency.Readers
{
    internal class CsvReader : ReaderForFiles
    {
        protected override string FormatFile => ".csv";

        internal override IEnumerable<ITaransactionInformation> GetTaransatcionInformation(string path)
        {
            IEnumerable<string[]> rows = CsvRead(path);

            foreach (var item in rows)
            {
                Logger.ParsedLine();

                if (item.Length != 9)
                {
                    Logger.Error(path);
                    continue;
                }
                var TrInformation = CreateTrInformationFromRowOrNull(item);
                
                if(TrInformation is null)
                {
                    Logger.Error(path);
                    continue;
                }

                yield return TrInformation;
            }
              
        }

        private static IEnumerable<string[]> CsvRead(string path)
        {
            Encoding encoding = Encoding.UTF8;

            // Use TextFieldParser to read the CSV file
            using (TextFieldParser parser = new TextFieldParser(path, encoding))
            {
                // Set the delimiter
                parser.Delimiters = new string[] { "," };

                // Skip the first line
                parser.ReadLine();

                // Read the remaining lines
                while (!parser.EndOfData)
                {
                    // Parse the line into an array of strings
                    string[] fields = parser.ReadFields();

                   
                     yield return fields ;
                }
            }
        }
    }
}
