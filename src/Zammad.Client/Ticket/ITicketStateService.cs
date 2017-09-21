using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zammad.Client.Ticket
{
    public interface ITicketStateService
    {
        Task<IList<TicketState>> GetTicketStateListAsync();
        Task<TicketState> GetTicketStateAsync(int id);
        Task<TicketState> CreateTicketStateAsync(TicketState priority);
        Task<TicketState> UpdateTicketStateAsync(int id, TicketState priority);
        Task<bool> DeleteTicketStateAsync(int id);
    }
}
