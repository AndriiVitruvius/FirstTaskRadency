using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FirstTaskRadency.PaymentTransactions.Information
{
    public class Address
    {
        public string City { get; }
        public string Street { get; }
        public string Number { get; }
        public Address(string lineAddress)
        {
            string[] strings = lineAddress.Split(',');

            if (strings.Length != 3)
                throw new ArgumentException("Wrong Address");

            foreach (var item in strings)
                CheackString(item);
            

            City = strings[0];
            Street = strings[1];
            Number = strings[2];


        }

        public Address(string City, string Street, string Number)
        {
            CheackString(City);
            CheackStringWithNumber(Street);
            CheackStringNumber(Number);
 
            this.City = City;
            this.Street = Street;
            this.Number = Number;

        }

        private void CheackString(string str)
        {
            if (String.IsNullOrWhiteSpace(str) || !Regex.IsMatch(str, "^[A-Z][a-zA-Z]*$"))
                 throw new Exception("The string is empty or has invalid characters");
            if (str.Length > 512)
                throw new Exception("String very long");
        }

        private void CheackStringWithNumber(string str)
        {
            if (String.IsNullOrWhiteSpace(str) || !Regex.IsMatch(str, @"\b\w+\d+\w*\b"))
                throw new Exception("The string is empty or has invalid characters");
            if (str.Length > 512)
                throw new Exception("String very long");
        }

        private void CheackStringNumber(string str)
        {
            if (String.IsNullOrWhiteSpace(str) || !Regex.IsMatch(str, "^[0-9]*$"))
                throw new Exception("The string is empty or has invalid characters");
            if (str.Length > 256)
                throw new Exception("String very long");
        }
    }
}
