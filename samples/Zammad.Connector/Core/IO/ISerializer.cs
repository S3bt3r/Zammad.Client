using System.IO;
using System.Threading.Tasks;

namespace Zammad.Connector.Core.IO
{
    public interface ISerializer
    {
        Task SerializeAsync<T>(T value, Stream output);
        Task<T> DeserializerAsync<T>(Stream input);
    }
}
