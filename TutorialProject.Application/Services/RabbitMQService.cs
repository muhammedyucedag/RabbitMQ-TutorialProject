using RabbitMQ.Client;

namespace TutorialProject.Application.Services
{
    public class RabbitMQService
    {
        //ConnectionFactory hangi rabbitmq sunucusuna bağlanacağımızı belirleyen class
        private readonly ConnectionFactory _connectionFactory;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQService(IModel channel, IConnection connection, ConnectionFactory connectionFactory)
        {
            _channel = channel;
            _connection = connection;
            _connectionFactory = connectionFactory;
        }
    }
}
