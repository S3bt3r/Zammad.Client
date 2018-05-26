using System;
using Newtonsoft.Json;

namespace Zammad.Client.Resources
{
    [JsonObject]
    public class TicketPriority
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("default_create")]
        public bool DefaultCreate { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("created_by_id")]
        public int CreatedById { get; set; }

        [JsonProperty("updated_by_id")]
        public int UpdatedById { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
