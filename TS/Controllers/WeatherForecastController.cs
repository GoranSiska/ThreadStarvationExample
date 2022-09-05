using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        async Task<bool> PretendQueryCustomerFromDbAsync()
        {
            // To keep the demo app easy to set up and performing consistently we have replaced a real database query
            // with a fixed delay of 500ms. The impact on application performance should be similar to using a real
            // database that had similar latency.
            await Task.Delay(1000);
            return true;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            bool result = false;
            //Task dbTask = Task.Delay(1000);
            //while (!dbTask.IsCompleted)
            //{
            //    Thread.Sleep(10);
            //    result = true;
            //}
            //dbTask.Wait();

            result = PretendQueryCustomerFromDbAsync().Result;

            //result = Task.Run(() =>
            //{
            //    Thread.Sleep(1000);
            //    return true;
            //}).Result;

            //await Task.Yield();
            //var result = Task.Factory.StartNew(() => {
            //    Thread.Sleep(1000);
            //    return true;
            //}).Result;

            //await Task.Delay(1000);

            //result = true;

            if (result)
            {
                var rng = new Random();
                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
            } 
            else
            {
                return null;
            }
        }
    }
}
