using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using Zammad.Client;
using Zammad.Connector.Commands.Ticket;
using Zammad.Connector.Core.Commands;

namespace Zammad.Connector
{
    class Program
    {
        private static IConfigurationRoot Configuration { get; set; }
        private static IServiceProvider ServiceProvider { get; set; }

        static async Task Main(string[] args)
        {
            Configuration = CreateConfiguration(Directory.GetCurrentDirectory(), "appsettings.json");
            ServiceProvider = CreateServiceProvider();

            await ServiceProvider.GetRequiredService<App>().RunAsync(args);
        }

        private static IConfigurationRoot CreateConfiguration(string basePath, string path)
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }

        private static IServiceProvider CreateServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            return serviceCollection.BuildServiceProvider();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Framework
            services.AddLogging(ConfigureLogging);
            services.AddOptions();
            
            // App
            services.AddScoped<App>();

            // Zammad Account
            services.AddSingleton(CreateZammadAccount);

            // Commands
            services.AddScoped<ICommandResolver, CliCommandResolver>();
            services.AddScoped<ExportTicketCommand>();
        }

        private static void ConfigureLogging(ILoggingBuilder logging)
        {
            logging.AddConsole();
        }

        private static ZammadAccount CreateZammadAccount(IServiceProvider serviceProvider)
        {
            return ZammadAccount.CreateBasicAccount(
                Configuration["Zammad:Endpoint"],
                Configuration["Zammad:User"],
                Configuration["Zammad:Password"]
            );
        }
    }
}
