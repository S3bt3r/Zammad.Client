using Newtonsoft.Json;
using System.Collections.Generic;

namespace Zammad.Client.Ticket
{
    [JsonObject]
    public class TicketAttachment
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("preferences")]
        public IDictionary<string, object> Preferences { get; set; }
    }
}
