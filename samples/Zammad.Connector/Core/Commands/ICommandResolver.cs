namespace Zammad.Connector.Core.Commands
{
    public interface ICommandResolver
    {
        ICommand Resolve(string name, string[] args);
    }
}
