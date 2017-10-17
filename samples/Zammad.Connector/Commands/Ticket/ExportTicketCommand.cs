using System.Threading.Tasks;
using Zammad.Connector.Core.Commands;
using Zammad.Connector.Core.Commands.Annotations;

namespace Zammad.Connector.Commands.Ticket
{
    [CliCommand("Export", "Ticket")]
    public class ExportTicketCommand : ICommand
    {
        [CliCommandArgument("-ExportFolder", true)]
        public string ExportFolder { get; set; }

        public async Task ExecuteAsync()
        {
        }
    }
}
