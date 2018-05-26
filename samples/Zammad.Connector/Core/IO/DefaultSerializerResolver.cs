using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;
using Zammad.Connector.Core.IO.Annotations;

namespace Zammad.Connector.Core.IO
{
    public class DefaultSerializerResolver : ISerializerResolver
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DefaultSerializerResolver> _logger;

        public DefaultSerializerResolver(IServiceProvider serviceProvider, ILogger<DefaultSerializerResolver> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public ISerializer Resolve(string fileName)
        {
            var fileExtension = Path.GetExtension(fileName);
            var serializer = CreateSerilaizer(fileExtension);

            if (serializer == null)
            {
                throw new InvalidOperationException($"Cant resolve serializer for file extension \"{fileExtension}\".");
            }

            return (ISerializer)serializer;
        }

        private IEnumerable<Type> GerSerializerTypes()
        {
            _logger.LogDebug("Query existing serializer...");
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass)
                .Where(t => t.GetInterface(nameof(ISerializer)) != null);
        }

        private object CreateSerilaizer(string fileExtension)
        {
            var serializerTypes = GerSerializerTypes();
            foreach (var serializerType in serializerTypes)
            {
                _logger.LogDebug("Search serializer for file extension {0}...", fileExtension);
                var serializerAttribute = SerializerAttribute.Extract(serializerType);
                if (serializerAttribute != null)
                {
                    if (string.Equals(serializerAttribute.FileExtension, fileExtension, StringComparison.InvariantCultureIgnoreCase))
                    {
                        _logger.LogDebug("Create serializer for file extension {0}...", fileExtension);
                        return _serviceProvider.GetService(serializerType);
                    }
                }
            }
            return null;
        }
    }
}
