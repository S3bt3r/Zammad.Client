using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zammad.Client.Ticket
{
    public interface ITicketService
    {
        Task<IList<Ticket>> GetTicketListAsync();
        Task<IList<Ticket>> GetTicketListAsync(int page, int count);
        Task<IList<Ticket>> SearchTicketAsync(string query, int limit);
        Task<Ticket> GetTicketAsync(int id);
        Task<Ticket> CreateTicketAsync(Ticket ticket, TicketArticle article);
        Task<Ticket> UpdateTicketAsync(int id, Ticket ticket);
        Task<bool> DeleteTicketAsync(int id);
    }
}
