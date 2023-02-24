using FirstTaskRadency.PaymentTransactions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTaskRadency.Serialize
{
    public static class SerializeUserFile
    {
        public static string MakeJson(this IEnumerable<ITaransactionInformation> userFiles)
        {
            
            List<Output> outputs = new List<Output>();

            var Citys = userFiles.GroupBy(f => f.Address.City).ToList();


            foreach (var cityGroup in Citys)
            {
                string city = cityGroup.FirstOrDefault().Address.City;

                decimal totalCity = 0;

                List<Service> services = new List<Service>();

                IGrouping<string, ITaransactionInformation>[] ServiceGroup = cityGroup.GroupBy(s => s.ServiceName).ToArray();

                foreach (var itemService in ServiceGroup)
                {
                    decimal totalService = 0;

                    string NameService = itemService.FirstOrDefault().ServiceName;
                    var payers = new List<Payer>();


                    foreach (var itemIformation in itemService)
                    {

                        string firstName = itemIformation.FirstName;
                        string lastName = itemIformation.LastName;

                        decimal payment = itemIformation.Payment;

                        totalService += payment;
                        totalCity += payment;

                        DateTime date = itemIformation.Date;
                        long accountNumber = itemIformation.AccountNumber;


                        Payer payer = new Payer()
                        {
                            Name = $"{firstName} {lastName}",
                            Payment = payment,
                            Date = date,
                            AccountNumber = accountNumber
                        };

                        payers.Add(payer);

                    }

                    Service service = new Service()
                    {
                        Name = NameService,
                        Payers = payers,
                        Total = totalCity,
                    };

                    services.Add(service);



                }


                Output output = new Output()
                {
                    City = city,
                    Services = services,
                    Total = totalCity
                };
                outputs.Add(output);

            }

            // Serialize the output object to JSON
            string json = JsonConvert.SerializeObject(outputs, Newtonsoft.Json.Formatting.Indented);
            
            return json;


        }


        class Output
        {
            public string City { get; set; }
            public List<Service> Services { get; set; }
            public decimal Total { get; set; }
        }

        class Service
        {
            public string Name { get; set; }
            public List<Payer> Payers { get; set; }
            public decimal Total { get; set; }
        }

        class Payer
        {
            public string Name { get; set; }
            public decimal Payment { get; set; }
            public DateTime Date { get; set; }
            public long AccountNumber { get; set; }
        }
    }
}
