using System;
using System.Threading;

namespace TripPinUnchaseCoreClient
{
    class Program
    {
        static void Main(string[] args)
        {
            const string serviceUri = "https://services.odata.org/TripPinRESTierService";
            var container = new Microsoft.OData.Service.Sample.TrippinInMemory.Models.Container(new Uri(serviceUri));

            ManualResetEventSlim mre = new ManualResetEventSlim(); // (1) - Инициализация примитива синхронизации "событие".

            IAsyncResult asyncResult = container.People.BeginExecute((ar) =>
            {
                Console.WriteLine("People in TripPin service:");
                var people = container.People.EndExecute(ar);

                foreach (var person in people)
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
