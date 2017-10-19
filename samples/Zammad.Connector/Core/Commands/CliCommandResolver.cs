using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Zammad.Connector.Core.Commands.Annotations;

namespace Zammad.Connector.Core.Commands
{
    public class CliCommandResolver : ICommandResolver
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CliCommandResolver> _logger;

        public CliCommandResolver(IServiceProvider serviceProvider, ILogger<CliCommandResolver> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }
        
        public ICommand Resolve(string name, params string[] args)
        {
            var command = CreateCommand(name);

            if (command == null)
            {
                throw new InvalidOperationException($"Cant resolve command \"{name}\".");
            }

            SetCommandArguments(command, args);

            return (ICommand)command;
        }

        private IEnumerable<Type> GetCommandTypes()
        {
            _logger.LogDebug("Query existing commands...");
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass)
                .Where(t => t.GetInterface(nameof(ICommand)) != null);
        }

        private object CreateCommand(string name)
        {
            var commandTypes = GetCommandTypes();
            foreach (var commandType in commandTypes)
            {
                _logger.LogDebug("Search command with name {0}...", name);
                var commandAttribute = CliCommandAttribute.Extract(commandType);
                if (commandAttribute != null)
                {
                    if (string.Equals(commandAttribute.Name, name, StringComparison.InvariantCultureIgnoreCase))
                    {
                        _logger.LogDebug("Create command with name {0}...", name);
                        return _serviceProvider.GetService(commandType);
                    }
                }
            }
            return null;
        }

        private IEnumerable<PropertyInfo> GetProperties(Type commandType)
        {
            _logger.LogDebug("Query command properties...");
            return commandType.GetProperties();
        }

        private void SetCommandArguments(object command, params string[] args)
        {
            var propertyInfos = GetProperties(command.GetType());
            foreach (var propertyInfo in propertyInfos)
            {
                var argument = CliCommandArgumentAttribute.Extract(propertyInfo);
                if (argument != null)
                {
                    var isSet = false;
                    for (var i = 0; i < args.Length; i++)
                    {
                        _logger.LogDebug("Set command property {0}...", argument.Name);
                        if (string.Equals(argument.Name, args[i], StringComparison.InvariantCultureIgnoreCase))
                        {
                            try
                            {
                                if (propertyInfo.GetType() == typeof(bool))
                                {
                                    propertyInfo.SetValue(command, true);
                                }
                                else
                                {
                                    propertyInfo.SetValue(command, args[i + 1]);
                                }
                                isSet = true;
                                break;
                            }
                            catch (Exception e)
                            {
                                throw new InvalidOperationException($"The parameter \"{argument.Name}\" could not be set.\r\n{e.Message}");

                            }
                        }
                    }
                    if (argument.Required && isSet == false)
                    {
                        throw new InvalidOperationException($"The parameter \"{argument.Name}\" is required.");
                    }
                }
            }
        }
    }
}