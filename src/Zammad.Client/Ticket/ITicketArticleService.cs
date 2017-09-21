using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zammad.Client.Ticket
{
    public interface ITicketArticleService
    {
        Task<IList<TicketArticle>> GetTicketArticleListAsync();
        Task<TicketArticle> GetTicketArticleAsync(int id);
        Task<TicketArticle> CreateTicketArticleAsync(TicketArticle article);
    }
}
