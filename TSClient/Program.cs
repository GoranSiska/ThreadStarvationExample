using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TSClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            var client = new HttpClient();
            client.BaseAddress = new Uri(@"https://localhost:44343");

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
                _ = Task.Run(async () =>
                {
                    //while (!cancellationTokenSource.Token.IsCancellationRequested)
                    //{
                    Console.Write("s");
                    client.GetAsync("weatherforecast").ContinueWith(async (m) =>
                    {
                        if (m.Exception != null)
                        {
                            Console.WriteLine(m.Exception);
                        }
                        else
                        {
                            var r = await m;
                            var c = await r.Content.ReadAsStringAsync().ConfigureAwait(false);
                            //Console.WriteLine(c);
                            Console.Write("r");
                        }
                    });
                    //Console.WriteLine("calc");
                    //count++;
                    //var diff = DateTime.Now - start;
                    //Console.WriteLine(count / diff.TotalSeconds);
                    //Thread.Sleep(10000);
                    //}
                });

                Thread.Sleep(50);
            }

        }
    }
}
