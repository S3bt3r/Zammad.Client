using Newtonsoft.Json;
using System;

namespace Zammad.Client.Ticket
{
    [JsonObject]
    public class TicketState
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("state_type_id")]
        public int StateTypeId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("next_state_id")]
        public int? NextStateId { get; set; }

        [JsonProperty("ignore_escalation")]
        public bool IgnoreEscalation { get; set; }

        [JsonProperty("default_create")]
        public bool DefaultCreate { get; set; }

        [JsonProperty("default_follow_up")]
        public bool DefaultFollowUp { get; set; }

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
