namespace Zammad.Connector.Core.IO
{
    public interface ISerializerResolver
    {
        ISerializer Resolve(string fileName);
    }
}
