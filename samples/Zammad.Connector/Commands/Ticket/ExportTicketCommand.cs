using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Zammad.Client;
using Zammad.Connector.Core.Commands;
using Zammad.Connector.Core.Commands.Annotations;

namespace Zammad.Connector.Commands.Ticket
{
    [CliCommand("Export", "Ticket")]
    public class ExportTicketCommand : ICommand
    {
        private readonly ZammadAccount _account;
        private readonly ILogger<ExportTicketCommand> _logger;

        public ExportTicketCommand(ZammadAccount account, ILogger<ExportTicketCommand> logger)
        {
            _account = account;
            _logger = logger;
        }

        [CliCommandArgument("-ExportFolder", true)]
        public string ExportFolder { get; set; }

        public async Task ExecuteAsync()
        {
            _logger.LogInformation("Create ticket client for endpoint {0}...", _account.Endpoint);
            var ticketClient = _account.CreateTicketClient();

            _logger.LogInformation("Retrieve all tickets...");
            var ticketList = await ticketClient.GetTicketListAsync();
            _logger.LogInformation("{0} tickets were obtained.", ticketList?.Count);

            if (ticketList?.Count > 0)
            {
                foreach(var ticket in ticketList)
                {
                    _logger.LogInformation("Processing ticket {0}...", ticket.Id);
                    
                    _logger.LogInformation("Retrieve all ticket articles for ticket {0}...", ticket.Id);
                    var articleList = await ticketClient.GetTicketArticleListForTicketAsync(ticket.Id);
                    _logger.LogInformation("{0} ticket articles were obtained.", articleList?.Count);

                    if (articleList?.Count > 0)
                    {
                        foreach(var article in articleList)
                        {
                            _logger.LogInformation("Processing ticket article {0} from ticket {1}...", article.Id, ticket.Id);
                        }
                    }
                }
            }
        }
    }
}
