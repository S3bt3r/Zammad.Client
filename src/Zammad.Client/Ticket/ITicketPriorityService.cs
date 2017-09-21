using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zammad.Client.Ticket
{
    public interface ITicketPriorityService
    {
        Task<IList<TicketPriority>> GetTicketPriorityListAsync();
        Task<TicketPriority> GetTicketPriorityAsync(int id);
        Task<TicketPriority> CreateTicketPriorityAsync(TicketPriority priority);
        Task<TicketPriority> UpdateTicketPriorityAsync(int id, TicketPriority priority);
        Task<bool> DeleteTicketPriorityAsync(int id);
    }
}
