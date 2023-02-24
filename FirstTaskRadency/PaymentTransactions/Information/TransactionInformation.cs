using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FirstTaskRadency.PaymentTransactions.Information
{
    internal class TransactionInformation : ITaransactionInformation
    {
        public string FirstName { get; }

        public string LastName { get; }

        public Address Address { get; }

        public decimal Payment { get; }

        public long AccountNumber { get; }

        public string ServiceName { get; }

        public DateTime Date { get; }



        internal  TransactionInformation(string FirstName, string LastName, Address address, decimal Payment, long AccountNumber , string ServiceName,  DateTime dateTime)
        {
            CheackString(FirstName);
            CheackString(LastName);
            CheackString(ServiceName);

            this.FirstName = FirstName.Trim();
            this.ServiceName = ServiceName.Trim();
            this.Address = address;
            this.Payment= Payment;
            this.AccountNumber = AccountNumber;
            this.Date = dateTime;   

            //TODO : Error

            void CheackString(string str)
            {
                if (String.IsNullOrWhiteSpace(str) || !Regex.IsMatch(str, @"^\p{L}+$"))
                    throw new Exception("The string is empty or has invalid characters");
                if (str.Length > 512)
                    throw new Exception("String very long");
            }
        }



    }
}
