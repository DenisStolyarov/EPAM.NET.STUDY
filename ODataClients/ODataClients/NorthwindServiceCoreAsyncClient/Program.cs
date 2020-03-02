using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NorthwindServiceCoreAsyncClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string serviceUri = "https://services.odata.org/V3/Northwind/Northwind.svc/";
            var entities = new NorthwindModel.NorthwindEntities(new Uri(serviceUri)); // breakpoint #2.1

            var employees = await Task<IEnumerable<NorthwindModel.Employee>>.Factory.FromAsync(
                entities.Employees.BeginExecute(null, null),
                (iar) =>
                {
                    return entities.Employees.EndExecute(iar); // breakpoint #2.2
                });

            Console.WriteLine("Employees in Northwind service:"); // breakpoint #2.3
            foreach (var person in employees)
            {
                Console.WriteLine("\t{0} {1}", person.FirstName, person.LastName);
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadLine();
        }
    }
}
