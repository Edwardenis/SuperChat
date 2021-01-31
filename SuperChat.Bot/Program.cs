using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SuperChat.BL.QueueModels;
using SuperChat.Bot.Consumers;
using SuperChat.Core.ConfigModels;
using SuperChat.Services.IoC;
using System;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SuperChat.Bot
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            //setup DI
            var serviceCollection = new ServiceCollection()
                        .AddLogging();
            serviceCollection.AddServicesRegistry();
            //
            var configuration = createConfiguration();

            #region MassTransint Config
            //Set up maassTransit
            ConfigureMassTransit(configuration, serviceCollection);
            #endregion
            //
            var serviceProvider= serviceCollection.BuildServiceProvider();


            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");
            //
            var busControl = serviceProvider.GetRequiredService<IBusControl>();

            await busControl.StartAsync(new CancellationTokenSource(TimeSpan.FromSeconds(10)).Token);
            try
            {
                Console.WriteLine("Press enter to exit");

                await Task.Run(() => Console.ReadLine());
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            finally
            {
                await busControl.StopAsync();
            }
        }

        private static void ConfigureMassTransit(IConfiguration configuration, IServiceCollection serviceCollection)
        {
            var rabbitMqSettings = configuration.GetSection("RabbitMqSettings").Get<RabbitMqSettings>();
            serviceCollection.AddMassTransit(config =>
            {
                config.AddConsumer<StockRequestConsumer>();
                config.SetKebabCaseEndpointNameFormatter();
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(rabbitMqSettings.Host);
                    cfg.ReceiveEndpoint(rabbitMqSettings.StockRequestQueueName, x =>
                    {
                        x.ConfigureConsumer<StockRequestConsumer>(ctx);
                    });
                });
            });
        }

        private static IConfiguration createConfiguration()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true);

            return builder.Build();
        }
    }
}
