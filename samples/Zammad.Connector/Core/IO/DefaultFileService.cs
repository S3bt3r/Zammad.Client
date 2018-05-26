using System.IO;
using Microsoft.Extensions.Logging;

namespace Zammad.Connector.Core.IO
{
    public class DefaultFileService : IFileService
    {
        private readonly ILogger<DefaultFileService> _logger;

        public DefaultFileService(ILogger<DefaultFileService> logger)
        {
            _logger = logger;
        }

        public Stream CreateFile(string fileName)
        {
            CreateDirectoryForFileIfNotExists(fileName);
            _logger.LogDebug("Create file {0}...", fileName);
            return File.Create(fileName);
        }

        private void CreateDirectoryForFileIfNotExists(string fileName)
        {
            var directoryName = Path.GetDirectoryName(fileName);
            if (Directory.Exists(directoryName) == false)
            {
                _logger.LogDebug("Create folder {0}...", directoryName);
                Directory.CreateDirectory(directoryName);
            }
        }

        public Stream OpenReadFile(string fileName)
        {
            _logger.LogDebug("Open file for read...", fileName);
            return File.OpenRead(fileName);
        }
    }
}
