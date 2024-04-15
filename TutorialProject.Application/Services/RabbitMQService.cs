using RabbitMQ.Client;

namespace TutorialProject.Application.Services
{
    // RabbitMQ sunucusu ile iletişim için kullanılan servis sınıfı
    public class RabbitMQService : IDisposable
    {
        // RabbitMQ sunucusuna bağlanmak için bir ConnectionFactory örneği gereklidir.
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;

        // Constructor, ConnectionFactory örneğini alır ve bağlantıyı hemen oluşturur.
        public RabbitMQService(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            Connect(); // Constructor içinde bağlantıyı hemen oluşturuyoruz
        }

        // RabbitMQ sunucusuna bağlanan ve kanalı oluşturan metod
        public void Connect()
        {
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        // IDisposable arabiriminden gelen Dispose metodu, kaynakları serbest bırakır.
        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }

        // Mesaj yayınlama metodudur.
        public void PublishMessage(string exchangeName, string routingKey, byte[] body)
        {
            // Exchange tanımlama: Mesajların ne şekilde dağıtılacağını belirler.
            _channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, durable: true);

            // Mesaj yayınlama
            _channel.BasicPublish(exchangeName, string.IsNullOrEmpty(routingKey) ? "" : routingKey, null, body);
        }
    }
}
