using Newtonsoft.Json;
using Zammad.Client.Core.Internal;

namespace Zammad.Client.Ticket.Internal
{
    [JsonObject]
    public class TicketWithArticle : Ticket
    {
        [JsonProperty("article")]
        public TicketArticle Article { get; set; }

        public static TicketWithArticle Combine(Ticket ticket, TicketArticle article)
        {
            var combined = new TicketWithArticle();
            TypeUtility.CopyProperties(ticket, combined);
            combined.Article = article;
            return combined;
        }
    }
}
