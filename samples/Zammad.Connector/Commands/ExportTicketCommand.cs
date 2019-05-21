using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Zammad.Client;
using Zammad.Client.Resources;
using Zammad.Connector.Core.Commands;
using Zammad.Connector.Core.Commands.Annotations;
using Zammad.Connector.Core.IO;

namespace Zammad.Connector.Commands
{
    [CliCommand("Export", "Ticket")]
    public class ExportTicketCommand : ICommand
    {
        private readonly ZammadAccount _account;
        private readonly IFileService _fileService;
        private readonly ISerializerResolver _serializerResolver;
        private readonly ILogger<ExportTicketCommand> _logger;

        public ExportTicketCommand(ZammadAccount account, IFileService fileService, ISerializerResolver serializerResolver, ILogger<ExportTicketCommand> logger)
        {
            _account = account;
            _fileService = fileService;
            _serializerResolver = serializerResolver;
            _logger = logger;
        }

        [CliCommandArgument("-ExportFile", true)]
        public string ExportFile { get; set; }

        public async Task ExecuteAsync()
        {
            _logger.LogInformation("Create ticket client for endpoint {0}...", _account.Endpoint);
            var ticketClient = _account.CreateTicketClient();

            _logger.LogInformation("Retrieve all tickets...");
            var ticketList = await ticketClient.GetTicketListAsync().ConfigureAwait(false);
            _logger.LogInformation("{0} tickets were obtained.", ticketList?.Count);

            if (ticketList?.Count > 0)
            {
                _logger.LogInformation("Create ticket export...");
                var export = new ExportTicket();

                foreach (var ticket in ticketList)
                {
                    try
                    {
                        _logger.LogInformation("Processing ticket {0}...", ticket.Id);
                        var ticketItem = CreateTicketItem(ticket);
                        export.Tickets.Add(ticketItem);

                        _logger.LogInformation("Retrieve all ticket articles for ticket {0}...", ticket.Id);
                        var articleList = await ticketClient.GetTicketArticleListForTicketAsync(ticket.Id).ConfigureAwait(false);
                        _logger.LogInformation("{0} ticket articles were obtained.", articleList?.Count);

                        if (articleList?.Count > 0)
                        {
                            foreach (var article in articleList)
                            {
                                _logger.LogInformation("Processing ticket article {0} from ticket {1}...", article.Id, ticket.Id);
                                var articleItem = CreateArticleItem(article);
                                ticketItem.Articles.Add(articleItem);
                            }
                        }
                    }
                    catch(Exception e)
                    {
                        _logger.LogError("Error by ticket {0}\r\n{1}", ticket.Id, e.Message);
                    }
                }

                await WriteExportAsync(export).ConfigureAwait(false);
            }
        }

        private ExportTicketItem CreateTicketItem(Ticket ticket)
        {
            return new ExportTicketItem
            {
                Id = ticket.Id,
                GroupId = ticket.GroupId,
                PriorityId = ticket.PriorityId ?? 0,
                StateId = ticket.StateId ?? 0,
                OrganizationId = ticket.OrganizationId,
                Number = ticket.Number,
                Title = ticket.Title,
                OwnerId = ticket.OwnerId,
                CustomerId = ticket.CustomerId,
                Note = ticket.Note,
                Type = ticket.Type,
                TimeUnit = ticket.TimeUnit,
            };
        }
       
        private ExportTicketArticleItem CreateArticleItem(TicketArticle article)
        {
            return new ExportTicketArticleItem
            {
                Id = article.Id,
                Subject = article.Subject,
                ContentType = article.ContentType,
                Body = article.Body,
                Internal = article.Internal,
                Type = article.Type,
            };
        }

        private async Task WriteExportAsync(ExportTicket export)
        {
            using (var fileStream = _fileService.CreateFile(ExportFile))
            {
                var serializer = _serializerResolver.Resolve(ExportFile);
                await serializer.SerializeAsync(export, fileStream).ConfigureAwait(false);
            }
        }
    }
}
