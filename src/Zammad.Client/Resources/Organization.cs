using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Zammad.Client.Resources
{
    [JsonObject]
    public class Organization
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("shared")]
        public bool Shared { get; set; }

        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("domain_assignment")]
        public bool DomainAssignment { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("member_ids")]
        public IList<int> MemberIds { get; set; }

        [JsonProperty("updated_by_id")]
        public int UpdatedById { get; set; }

        [JsonProperty("created_by_id")]
        public int CreatedById { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonExtensionData]
        public Dictionary<string, object> CustomAttributes { get; set; }
    }
}
