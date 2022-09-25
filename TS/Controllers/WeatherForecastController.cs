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

        async Task<IEnumerable<WeatherForecast>> LongOperation()
        {
            // simulating long running operation
            await Task.Delay(1000);
            return Operation();
        }

        IEnumerable<WeatherForecast> Operation()
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

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            // quick synchronous operation
            return Operation();

            // long aynchronous operation
            // return await LongOperation();

            // block long asynchronous operation - DO NOT USE THIS PATTERN
            // return LongOperation().Result;

            // block long asynchronous operation in a non-standard way - DO NOT USE THIS PATTERN
            //var longOperation = LongOperation();
            //while (!longOperation.IsCompleted)
            //{
            //    Thread.Sleep(1000);
            //}
            //return longOperation.Result;
        }
    }
}
