using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Zammad.Client.Core;

namespace Zammad.Client.Ticket
{
    public class TicketClient : ZammadClient, ITicketService, ITicketArticleService, ITicketPriorityService, ITicketStateService
    {
        public TicketClient(ZammadAccount account)
            : base(account)
        {

        }

        #region ITicketService

        public Task<IList<Ticket>> GetTicketListAsync()
        {
            return ExecuteAsync<IList<Ticket>>(HttpMethod.Get, "/api/v1/tickets");
        }

        public Task<IList<Ticket>> GetTicketListAsync(int page, int count)
        {
            return ExecuteAsync<IList<Ticket>>(HttpMethod.Get, "/api/v1/tickets?page={page},per_page={count}");
        }

        public Task<IList<Ticket>> SearchTicketAsync(string query, int limit)
        {
            return ExecuteAsync<IList<Ticket>>(HttpMethod.Get, "/api/v1/tickets", $"?query={query}&limit={limit}");
        }

        public Task<Ticket> GetTicketAsync(int id)
        {
            return ExecuteAsync<Ticket>(HttpMethod.Get, "/api/v1/tickets/{id}");
        }

        public Task<Ticket> CreateTicketAsync(Ticket ticket)
        {
            return ExecuteAsync<Ticket>(HttpMethod.Post, "/api/v1/tickets", ticket);
        }

        public Task<Ticket> UpdateTicketAsync(int id, Ticket ticket)
        {
            return ExecuteAsync<Ticket>(HttpMethod.Put, "/api/v1/tickets/{id}", ticket);
        }

        public Task<bool> DeleteTicketAsync(int id)
        {
            return ExecuteAsync<bool>(HttpMethod.Delete, "/api/v1/tickets/{id}");
        }

        #endregion

        #region ITicketArticleService

        public Task<IList<TicketArticle>> GetTicketArticleListAsync()
        {
            return ExecuteAsync<IList<TicketArticle>>(HttpMethod.Get, "/api/v1/ticket_articles");
        }

        public Task<IList<TicketArticle>> GetTicketArticleListAsync(int page, int count)
        {
            return ExecuteAsync<IList<TicketArticle>>(HttpMethod.Get, "/api/v1/ticket_articles?page={page},per_page={count}");
        }

        public Task<IList<TicketArticle>> GetTicketArticleListForTicketAsync(int ticketId)
        {
            return ExecuteAsync<IList<TicketArticle>>(HttpMethod.Get, "/api/v1/ticket_articles/by_ticket/{ticketId}");
        }

        public Task<TicketArticle> GetTicketArticleAsync(int id)
        {
            return ExecuteAsync<TicketArticle>(HttpMethod.Get, "/api/v1/ticket_articles/{id}");
        }

        public Task<TicketArticle> CreateTicketArticleAsync(TicketArticle article)
        {
            return ExecuteAsync<TicketArticle>(HttpMethod.Post, "/api/v1/ticket_articles", article);
        }

        #endregion

        #region ITicketPriorityService

        public Task<IList<TicketPriority>> GetTicketPriorityListAsync()
        {
            return ExecuteAsync<IList<TicketPriority>>(HttpMethod.Get, "/api/v1/ticket_priorities");
        }

        public Task<IList<TicketPriority>> GetTicketPriorityListAsync(int page, int count)
        {
            return ExecuteAsync<IList<TicketPriority>>(HttpMethod.Get, "/api/v1/ticket_priorities?page={page},per_page={count}");
        }

        public Task<TicketPriority> GetTicketPriorityAsync(int id)
        {
            return ExecuteAsync<TicketPriority>(HttpMethod.Get, "/api/v1/ticket_priorities/{id}");
        }

        public Task<TicketPriority> CreateTicketPriorityAsync(TicketPriority priority)
        {
            return ExecuteAsync<TicketPriority>(HttpMethod.Post, "/api/v1/ticket_priorities", priority);
        }

        public Task<TicketPriority> UpdateTicketPriorityAsync(int id, TicketPriority priority)
        {
            return ExecuteAsync<TicketPriority>(HttpMethod.Put, "/api/v1/ticket_priorities/{id}", priority);
        }

        public Task<bool> DeleteTicketPriorityAsync(int id)
        {
            return ExecuteAsync<bool>(HttpMethod.Delete, "/api/v1/ticket_priorities/{id}");
        }

        #endregion

        #region ITicketStateService

        public Task<IList<TicketState>> GetTicketStateListAsync()
        {
            return ExecuteAsync<IList<TicketState>>(HttpMethod.Get, "/api/v1/ticket_states");
        }

        public Task<IList<TicketState>> GetTicketStateListAsync(int page, int count)
        {
            return ExecuteAsync<IList<TicketState>>(HttpMethod.Get, "/api/v1/ticket_states?page={page},per_page={count}");
        }

        public Task<TicketState> GetTicketStateAsync(int id)
        {
            return ExecuteAsync<TicketState>(HttpMethod.Get, "/api/v1/ticket_states/{id}");
        }

        public Task<TicketState> CreateTicketStateAsync(TicketState state)
        {
            return ExecuteAsync<TicketState>(HttpMethod.Post, "/api/v1/ticket_states", state);
        }

        public Task<TicketState> UpdateTicketStateAsync(int id, TicketState state)
        {
            return ExecuteAsync<TicketState>(HttpMethod.Put, "/api/v1/ticket_states/{id}", state);
        }

        public Task<bool> DeleteTicketStateAsync(int id)
        {
            return ExecuteAsync<bool>(HttpMethod.Delete, "/api/v1/ticket_states/{id}");
        }

        #endregion
    }
}
