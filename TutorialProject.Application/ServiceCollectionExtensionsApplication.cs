using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using TutorialProject.Application.Services;

namespace TutorialProject.Application;

// Bu sınıf, IServiceCollection'a RabbitMQ ile ilgili servislerin eklenmesini sağlayan genişletme yöntemini içerir.
public static class ServiceCollectionExtensionsApplication
{
    public static IServiceCollection ServiceCollectionExtension(this IServiceCollection services, IConfiguration configuration, params Type[] types)
    {
        // RabbitMQ bağlantı bilgilerine erişmek için IConfiguration parametresi kullanılır.
        services.AddSingleton<ConnectionFactory>(sp =>
        {
            // IConfiguration'dan RabbitMQ bağlantı bilgilerini alır.
            var config = configuration.GetSection("RabbitMQ");

            // Bağlantı bilgileriyle bir ConnectionFactory örneği oluşturulur ve servis koleksiyonuna eklenir.
            return new ConnectionFactory()
            {
                HostName = config["HostName"],
                UserName = config["UserName"],
                Password = config["Password"]
            };
        });

        // RabbitMQService, RabbitMQ ile iletişim kurmak için kullanılacak bağlantıyı ve kanalı oluşturur.
        // Bağlantı bilgileri burada eklenmiş olan ConnectionFactory tarafından sağlanır.
        services.AddSingleton<RabbitMQService>();
        services.AddSingleton<RabbitMQPublisher>();

        // Servis koleksiyonu, IServiceCollection'ın döndürülmesiyle döndürülür.
        return services;
    }
}