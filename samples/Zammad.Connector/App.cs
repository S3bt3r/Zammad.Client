using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Zammad.Connector.Core.Commands;

namespace Zammad.Connector
{
    public class App
    {
        private readonly ICommandResolver _commandResolver;
        private readonly ILogger _logger;

        public App(ICommandResolver commandResolver, ILogger<App> logger)
        {
            _commandResolver = commandResolver;
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