using FirstTaskRadency.PaymentTransactions;
using FirstTaskRadency.PaymentTransactions.Information;
using FirstTaskRadency.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTaskRadency.Readers
{
    internal abstract class ReaderForFiles 
    {


        protected abstract string FormatFile { get; }

     
        internal bool IsFileFormatValid(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath);

            if (fileExtension.Equals(FormatFile, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }

        internal abstract IEnumerable<ITaransactionInformation> GetTaransatcionInformation(string path);

        protected ITaransactionInformation CreateTrInformationFromRowOrNull(IList<string> fields, string path)
        {
            try
            {
                string firstName = fields[0].Trim();

                string lastName = fields[1].Trim();

                string City = fields[2].Trim(new char[] { '“', '”', ' ' });

                string Street = fields[3].Trim(new char[] { '“', '”', ' ' });

                string Number = fields[4].Trim(new char[] { '“', '”', ' ' });


                decimal payment = decimal.Parse(fields[5], CultureInfo.InvariantCulture);

                DateTime date = DateTime.ParseExact(fields[6].Trim(), "yyyy-dd-MM", CultureInfo.InvariantCulture);

                long accountNumber = long.Parse(fields[7].Trim());
                string serviceName = fields[8].Trim();

                TransactionInformation transactionInformation = new TransactionInformation(firstName, lastName, new Address(City, Street, Number), payment, accountNumber, serviceName, date);

                return transactionInformation;
            }
            catch (Exception e)
            {
                ViewModel.ViewModelBase.Error.Invoke($"{e.Message}");
                Logger.Error(path);
                return null;
            }
            
        }
    }

}
