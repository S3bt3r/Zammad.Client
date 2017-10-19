using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Zammad.Connector.Core.IO.Annotations;

namespace Zammad.Connector.Core.IO
{
    [Serializer(".xml")]
    public class XmlSerializer : ISerializer
    {
        private readonly XmlSerializerOptions _options;
        private readonly ILogger<XmlSerializer> _logger;

        public XmlSerializer(IOptions<XmlSerializerOptions> options, ILogger<XmlSerializer> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        public async Task SerializeAsync<T>(T value, Stream output)
        {
            _logger.LogDebug("Create xml writer...");
            using (var stream = new MemoryStream())
            using (var writer = XmlWriter.Create(stream, GetWriterSettings()))
            {
                _logger.LogDebug("Serialize...");
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                serializer.Serialize(stream, value);
                
                _logger.LogDebug("Copy to output...");
                stream.Seek(0, SeekOrigin.Begin);
                await stream.CopyToAsync(output);
            }
        }

        private XmlWriterSettings GetWriterSettings()
        {
            return new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding(_options.Encoding),
                OmitXmlDeclaration = true,
                CloseOutput = false
            };
        }

        public async Task<T> DeserializerAsync<T>(Stream input)
        {
            _logger.LogDebug("Create xml reader...");
            using (var stream = new MemoryStream())
            using (var reader = XmlReader.Create(stream, GetReaderSettings()))
            {
                _logger.LogDebug("Copy from input..."); 
                await input.CopyToAsync(stream);

                _logger.LogDebug("Deserialize...");
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                var value = serializer.Deserialize(input);

                return (T)value;
            }
        }

        private XmlReaderSettings GetReaderSettings()
        {
            return new XmlReaderSettings
            {
                IgnoreComments = true,
                CloseInput = false
            };
        }
    }
}
