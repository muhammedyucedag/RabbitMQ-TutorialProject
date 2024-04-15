using System.Text;

namespace TutorialProject.Application.Services
{
    // RabbitMQ sunucusuna mesaj yayınlamak için kullanılan bir yayıncı sınıfı
    public class RabbitMQPublisher
    {
        // RabbitMQService sınıfından bir örneği depolayan bir alan
        private readonly RabbitMQService _rabbitMQService;

        // Constructor, RabbitMQService örneğini alır
        public RabbitMQPublisher(RabbitMQService rabbitMQService)
        {
            _rabbitMQService = rabbitMQService;
        }

        // Mesaj yayınlama metodu, aldığı metni byte dizisine dönüştürüp RabbitMQService'e ileterek yayınlar
        public void Publish(string exchangeName, string routingKey, string message)
        {
            _rabbitMQService.PublishMessage(exchangeName, routingKey, Encoding.UTF8.GetBytes(message));
        }
    }
}
