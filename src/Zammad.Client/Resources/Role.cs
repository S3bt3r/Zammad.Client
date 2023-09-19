using System;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;

namespace Zammad.Client.Resources
{
    [JsonObject]
    public class Role
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("signature_id")]
        public int? SignatureId { get; set; }

        [JsonProperty("email_address_id")]
        public int EmailAddressId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("assignment_timeout")]
        public int? AssignmentTimeout { get; set; }

        [JsonProperty("follow_up_possible")]
        public string FollowUpPossible { get; set; }

        [JsonProperty("reopen_time_in_days")]
        public int? ReopenTimeInDays { get; set; }

        [JsonProperty("follow_up_assignment")]
        public bool FollowUpAssignment { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("shared_drafts")]
        public bool SharedDrafts { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("updated_by_id")]
        public int UpdatedById { get; set; }

        [JsonProperty("create_by_id")]
        public int CreatedById { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("user_ids")]
        public List<int> UserIds { get; set; }
    }
}
