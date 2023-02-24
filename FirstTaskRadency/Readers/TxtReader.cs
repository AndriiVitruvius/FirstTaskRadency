using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using FirstTaskRadency.PaymentTransactions;
using FirstTaskRadency.PaymentTransactions.Information;
using FirstTaskRadency.Service;
using Newtonsoft.Json;


namespace FirstTaskRadency.Readers
{
    internal class TxtReader : ReaderForFiles
    {
        protected override string FormatFile => ".txt";

        internal override IEnumerable<ITaransactionInformation> GetTaransatcionInformation(string filePath)
        {
            var rows = ReadTxt(filePath);

            foreach (string row in rows)
            {
                Logger.ParsedLine();

                string pattern = ",";

                string[] fields = Regex.Split(row, pattern); 

                if (fields.Length != 9)
                {
                    Logger.Error(filePath);
                    continue;
                }

                var TrInformation = CreateTrInformationFromRowOrNull(fields);

                if(TrInformation == null)
                    continue;
                

                yield return TrInformation;

            }


        }

        private static IEnumerable<string> ReadTxt(string filePath)
        {

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                    yield return line;
            }

        }


    }
}
