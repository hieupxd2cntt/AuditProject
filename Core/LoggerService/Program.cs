using MassTransit;
using MessageQueue;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace LoggerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var bus = BusConfigurator.ConfigureBus((cfg, host) => {
                cfg.ReceiveEndpoint(host, RabbitMqConstants.LoggerServiceQueue, e => {
                    e.Consumer<LoggerRegisteredConsumer>();
                });
            });

            bus.Start();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
