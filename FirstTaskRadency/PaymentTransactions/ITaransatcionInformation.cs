using FirstTaskRadency.PaymentTransactions.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTaskRadency.PaymentTransactions
{
    public interface ITaransactionInformation
    {
        string FirstName { get; }   
        string LastName { get; }
        Address Address { get; }
        decimal  Payment { get; }

        long AccountNumber { get; } 
        string ServiceName { get; } 

        DateTime Date { get; }





    }
}
