using System.Collections.Generic;
using System.Threading.Tasks;
using Zammad.Client.Resources;

namespace Zammad.Client.Services
{
    public interface ITicketStateService
    {
        Task<IList<TicketState>> GetTicketStateListAsync();
        Task<IList<TicketState>> GetTicketStateListAsync(int page, int count);
        Task<TicketState> GetTicketStateAsync(int id);
        Task<TicketState> CreateTicketStateAsync(TicketState state);
        Task<TicketState> UpdateTicketStateAsync(int id, TicketState state);
        Task<bool> DeleteTicketStateAsync(int id);
    }
}
