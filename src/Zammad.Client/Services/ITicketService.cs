using System.Collections.Generic;
using System.Threading.Tasks;
using Zammad.Client.Resources;

namespace Zammad.Client.Services
{
    public interface ITicketService
    {
        Task<IList<Ticket>> GetTicketListAsync();
        Task<IList<Ticket>> GetTicketListAsync(int page, int count);
        Task<IList<Ticket>> SearchTicketAsync(string query, int limit);
        Task<IList<Ticket>> SearchTicketAsync(string query, int limit, string sortBy, string orderBy);
        Task<Ticket> GetTicketAsync(int id);
        Task<Ticket> CreateTicketAsync(Ticket ticket, TicketArticle article);
        Task<Ticket> UpdateTicketAsync(int id, Ticket ticket);
        Task<bool> DeleteTicketAsync(int id);
    }
}
