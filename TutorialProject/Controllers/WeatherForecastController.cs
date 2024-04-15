using Microsoft.AspNetCore.Mvc;
using System.Text;
using TutorialProject.Application.Services;

namespace TutorialProject.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly RabbitMQPublisher _rabbitMQPublisher; // RabbitMQPublisher'� enjekte ediyoruz

    public WeatherForecastController(RabbitMQPublisher rabbitMQPublisher)
    {
        _rabbitMQPublisher = rabbitMQPublisher;
    }


    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        var message = "Bu bir test mesaj�d�r";
        _rabbitMQPublisher.Publish("HavadurumuExchange", "routingKey", message); // Publisher arac�l���yla yay�n yap�l�yor

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}