using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Zammad.Client.Resources
{
    [JsonObject]
    public class Group
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("signature_id")]
        public int? SignatureId { get; set; }

        [JsonProperty("email_address_id")]
        public int? EmailAddressId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("assignment_timeout")]
        public TimeSpan? AssignmentTimeout { get; set; }

        [JsonProperty("follow_up_possible")]
        public string FollowUpPossible { get; set; }

        [JsonProperty("follow_up_assignment")]
        public bool FollowUpAssignment { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("user_ids")]
        public IList<int> UserIds { get; set; }

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
