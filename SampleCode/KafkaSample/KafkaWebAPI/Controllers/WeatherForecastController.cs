using KafkaWebAPI.Publisher;
using Microsoft.AspNetCore.Mvc;

namespace KafkaWebAPI.Controllers
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
        private readonly IWeatherDataPublisher _weatherDataPublisher;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherDataPublisher weatherDataPublisher)
        {
            _logger = logger;
            _weatherDataPublisher = weatherDataPublisher;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var wether= Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
            foreach (var item in wether)
            {
                _weatherDataPublisher.ProduceWether(new Wether(item.Summary ?? "", item.TemperatureF));
            }
            return wether;
        }
    }
}