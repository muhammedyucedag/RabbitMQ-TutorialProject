using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "admin",
            Password = "password"
        };

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            var exchangeName = "HavadurumuExchange";
            var queueName = "HavadurumuQueue";
            var routingKey = "routingKey";

            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, durable: true);
            channel.QueueDeclare(queueName, true, false, false, null);
            channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routingKey);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received message: {message}");
            };

            channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

            Console.WriteLine("Consumer started. Press any key to exit.");
            Console.ReadLine();
        }
    }
}
