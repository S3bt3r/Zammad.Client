using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zammad.Client.Ticket
{
    public interface ITicketArticleService
    {
        Task<IList<TicketArticle>> GetTicketArticleListAsync();
        Task<IList<TicketArticle>> GetTicketArticleListAsync(int page, int count);
        Task<IList<TicketArticle>> GetTicketArticleListForTicketAsync(int ticketId);
        Task<TicketArticle> GetTicketArticleAsync(int id);
        Task<TicketArticle> CreateTicketArticleAsync(TicketArticle article);
    }
}
