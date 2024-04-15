namespace TutorialProject.Application.Services.Interfaces;

public interface IRabbitMQServices : IDisposable
{
    void PublishMessage(string exchangeName);
}
