using System.IO;

namespace Zammad.Connector.Core.IO
{
    public interface IFileService
    {
        Stream CreateFile(string fileName);
        Stream OpenReadFile(string fileName);
    }
}
