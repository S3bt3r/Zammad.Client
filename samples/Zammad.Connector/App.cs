using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Zammad.Connector.Core.Commands;

namespace Zammad.Connector
{
    public class App
    {
        private readonly ICommandResolver _commandResolver;
        private readonly AppOptions _options;
        private readonly ILogger _logger;

        public App(ICommandResolver commandResolver, IOptions<AppOptions> options, ILogger<App> logger)
        {
            _commandResolver = commandResolver;
            _options = options.Value;
            _logger = logger;
        }

        public async Task RunAsync(params string[] args)
        {
            if (args.Length > 0)
            {
                var name = args[0];
                args = args.Skip(1).ToArray();

                try
                {
                    var command = _commandResolver.Resolve(name, args);
                    await command.ExecuteAsync();

                    if (_options.AutoClose == false)
                    {
                        Console.ReadKey();
                    }
                }
                catch(Exception e)
                {
                    _logger.LogCritical(e.Message);
                }
            }
            else
            {
                _logger.LogError("No valid command was specified!");
            }
        }
    }
}
