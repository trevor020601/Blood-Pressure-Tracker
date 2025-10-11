using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
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

        /// <summary>
        /// Gets weather forecast summaries
        /// </summary>
        /// <returns>All weather forecast summaries</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /GetWeatherForecast
        ///     {
        ///         "Date": "2025-10-10",
        ///         "TemperatureC": -20,
        ///         "Summary": "Freezing"
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Returns array of weather forecast summaries</response>
        [HttpGet(Name = "GetWeatherForecast")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
