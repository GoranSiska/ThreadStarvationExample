using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TSClient
{
    internal class Program
    {
#pragma warning disable S1075 // URIs should not be hardcoded
        private const string UriString = @"https://localhost:44343";
#pragma warning restore S1075 // URIs should not be hardcoded

        protected Program()
        {}

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var client = new HttpClient();
            client.BaseAddress = new Uri(UriString);

            Console.WriteLine("Press S to start");
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey().Key;
                    if (key == ConsoleKey.S)
                    {
                        break;
                    }
                }
            }

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey().Key;
                    if (key == ConsoleKey.B)
                    {
                        break;
                    }
                }

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
                _ = Task.Run(async () =>
                {
                    Console.Write("s"); //sent
                    client.GetAsync("weatherforecast").ContinueWith(async (responseTask) =>
                    {
                        if (responseTask.Exception != null)
                        {
                            Console.WriteLine(responseTask.Exception);
                            return;
                        }

                        var response = await responseTask;
                        await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        Console.Write("r"); //received
                    });
                });
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                
                // throttle client
                Thread.Sleep(50);
            }

        }
    }
}
