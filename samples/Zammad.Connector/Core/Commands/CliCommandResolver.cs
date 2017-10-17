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

        public CliCommandResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
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
                var commandAttribute = CliCommandAttribute.Extract(commandType);
                if (commandAttribute != null)
                {
                    if (string.Equals(commandAttribute.Name, name, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return _serviceProvider.GetService(commandType);
                    }
                }
            }
            return null;
        }

        private IEnumerable<PropertyInfo> GetProperties(Type commandType)
        {
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