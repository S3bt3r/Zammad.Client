using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Zammad.Client.Core;
using Zammad.Client.Ticket.Internal;

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
            return GetAsync<IList<Ticket>>("/api/v1/tickets");
        }

        public Task<IList<Ticket>> GetTicketListAsync(int page, int count)
        {
            return GetAsync<IList<Ticket>>("/api/v1/tickets", $"?page={page},per_page={count}");
        }

        public Task<IList<Ticket>> SearchTicketAsync(string query, int limit)
        {
            return GetAsync<IList<Ticket>>("/api/v1/tickets", $"?query={query}&limit={limit}");
        }

        public Task<Ticket> GetTicketAsync(int id)
        {
            return GetAsync<Ticket>($"/api/v1/tickets/{id}");
        }

        public Task<Ticket> CreateTicketAsync(Ticket ticket, TicketArticle article)
        {
            return PostAsync<Ticket>("/api/v1/tickets", TicketWithArticle.Combine(ticket, article));
        }

        public Task<Ticket> UpdateTicketAsync(int id, Ticket ticket)
        {
            return PutAsync<Ticket>($"/api/v1/tickets/{id}", ticket);
        }

        public Task<bool> DeleteTicketAsync(int id)
        {
            return DeleteAsync($"/api/v1/tickets/{id}");
        }

        #endregion

        #region ITicketArticleService

        public Task<IList<TicketArticle>> GetTicketArticleListAsync()
        {
            return GetAsync<IList<TicketArticle>>("/api/v1/ticket_articles");
        }

        public Task<IList<TicketArticle>> GetTicketArticleListAsync(int page, int count)
        {
            return GetAsync<IList<TicketArticle>>("/api/v1/ticket_articles", $"?page={page},per_page={count}");
        }

        public Task<IList<TicketArticle>> GetTicketArticleListForTicketAsync(int ticketId)
        {
            return GetAsync<IList<TicketArticle>>($"/api/v1/ticket_articles/by_ticket/{ticketId}");
        }

        public Task<TicketArticle> GetTicketArticleAsync(int id)
        {
            return GetAsync<TicketArticle>($"/api/v1/ticket_articles/{id}");
        }

        public Task<TicketArticle> CreateTicketArticleAsync(TicketArticle article)
        {
            return PostAsync<TicketArticle>("/api/v1/ticket_articles", article);
        }

        public Task<Stream> GetTicketArticleAttachmentAsync(int ticketId, int articleId, int id)
        {
            return GetAsync($"/api/v1/ticket_attachment/{ticketId}/{articleId}/{id}");
        }

        #endregion

        #region ITicketPriorityService

        public Task<IList<TicketPriority>> GetTicketPriorityListAsync()
        {
            return GetAsync<IList<TicketPriority>>("/api/v1/ticket_priorities");
        }

        public Task<IList<TicketPriority>> GetTicketPriorityListAsync(int page, int count)
        {
            return GetAsync<IList<TicketPriority>>("/api/v1/ticket_priorities", $"?page={page},per_page={count}");
        }

        public Task<TicketPriority> GetTicketPriorityAsync(int id)
        {
            return GetAsync<TicketPriority>($"/api/v1/ticket_priorities/{id}");
        }

        public Task<TicketPriority> CreateTicketPriorityAsync(TicketPriority priority)
        {
            return PostAsync<TicketPriority>("/api/v1/ticket_priorities", priority);
        }

        public Task<TicketPriority> UpdateTicketPriorityAsync(int id, TicketPriority priority)
        {
            return PutAsync<TicketPriority>($"/api/v1/ticket_priorities/{id}", priority);
        }

        public Task<bool> DeleteTicketPriorityAsync(int id)
        {
            return DeleteAsync($"/api/v1/ticket_priorities/{id}");
        }

        #endregion

        #region ITicketStateService

        public Task<IList<TicketState>> GetTicketStateListAsync()
        {
            return GetAsync<IList<TicketState>>("/api/v1/ticket_states");
        }

        public Task<IList<TicketState>> GetTicketStateListAsync(int page, int count)
        {
            return GetAsync<IList<TicketState>>("/api/v1/ticket_states", $"?page={page},per_page={count}");
        }

        public Task<TicketState> GetTicketStateAsync(int id)
        {
            return GetAsync<TicketState>($"/api/v1/ticket_states/{id}");
        }

        public Task<TicketState> CreateTicketStateAsync(TicketState state)
        {
            return PostAsync<TicketState>("/api/v1/ticket_states", state);
        }

        public Task<TicketState> UpdateTicketStateAsync(int id, TicketState state)
        {
            return PutAsync<TicketState>($"/api/v1/ticket_states/{id}", state);
        }

        public Task<bool> DeleteTicketStateAsync(int id)
        {
            return DeleteAsync($"/api/v1/ticket_states/{id}");
        }

        #endregion
    }
}
