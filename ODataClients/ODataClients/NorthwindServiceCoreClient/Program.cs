using System;
using System.Threading;

namespace NorthwindServiceCoreClient
{
    class Program
    {
        static void Main(string[] args)
        {
            const string serviceUri = "https://services.odata.org/V3/Northwind/Northwind.svc/";
            var entities = new NorthwindModel.NorthwindEntities(new Uri(serviceUri)); // breakpoint #1.1

            ManualResetEventSlim mre = new ManualResetEventSlim(); // (1) - Инициализация примитива синхронизации "событие".

            IAsyncResult asyncResult = entities.Employees.BeginExecute((ar) =>
            {
                Console.WriteLine("Employees in Northwind service:");
                var employees = entities.Employees.EndExecute(ar);

                foreach (var person in employees)
                {
                    Console.WriteLine("\t{0} {1}", person.FirstName, person.LastName);
                }

                mre.Set(); // (2) - Отправить сигнал методу WaitAll.

            }, null);

            WaitHandle.WaitAny(new[] { mre.WaitHandle }); // (3) - Блокировать поток выполнения, пока не будет получен сигнал.

            Console.WriteLine("Press any key to continue.");
            Console.ReadLine();
        }
    }
}
