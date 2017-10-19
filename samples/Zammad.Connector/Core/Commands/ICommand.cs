using System.Threading.Tasks;

namespace Zammad.Connector.Core.Commands
{
    public interface ICommand
    {
        Task ExecuteAsync();
    }
}