using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Zammad.Client.Resources
{
    [JsonObject]
    public class Ticket
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("group_id")]
        public int GroupId { get; set; }

        [JsonProperty("priority_id")]
        public int? PriorityId { get; set; }

        [JsonProperty("state_id")]
        public int? StateId { get; set; }

        [JsonProperty("organization_id")]
        public int? OrganizationId { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("owner_id")]
        public int OwnerId { get; set; }

        [JsonProperty("customer_id")]
        public int CustomerId { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("first_response_at")]
        public DateTimeOffset? FirstResponseAt { get; set; }

        [JsonProperty("first_response_escalation_at")]
        public DateTimeOffset? FirstResponseEscalationAt { get; set; }

        [JsonProperty("first_response_in_min")]
        public int? FirstResponseInMin { get; set; }

        [JsonProperty("first_response_diff_in_min")]
        public int? FirstResponseDiffInMin { get; set; }

        [JsonProperty("close_at")]
        public DateTimeOffset? CloseAt { get; set; }

        [JsonProperty("close_escalation_at")]
        public DateTimeOffset? CloseEscalationAt { get; set; }

        [JsonProperty("close_in_min")]
        public int? CloseInMin { get; set; }

        [JsonProperty("close_diff_in_min")]
        public int? CloseDiffInMin { get; set; }

        [JsonProperty("update_escalation_at")]
        public DateTimeOffset? UpdateEscalationAt { get; set; }

        [JsonProperty("update_in_min")]
        public int? UpdateInMin { get; set; }

        [JsonProperty("update_diff_in_min")]
        public int? UpdateDiffInMin { get; set; }

        [JsonProperty("last_contact_at")]
        public DateTimeOffset? LastContactAt { get; set; }

        [JsonProperty("last_contact_agent_at")]
        public DateTimeOffset? LastContactAgentAt { get; set; }

        [JsonProperty("last_contact_customer_at")]
        public DateTimeOffset? LastContactCustomerAt { get; set; }

        [JsonProperty("last_owner_update_at")]
        public DateTimeOffset? LastOwnerUpdateAt { get; set; }

        [JsonProperty("create_article_type_id")]
        public int? CreateArticleTypeId { get; set; }

        [JsonProperty("create_article_sender_id")]
        public int? CreateArticleSenderId { get; set; }

        [JsonProperty("article_count")]
        public int? ArticleCount { get; set; }

        [JsonProperty("escalation_at")]
        public DateTimeOffset? EscalationAt { get; set; }

        [JsonProperty("pending_time")]
        public DateTimeOffset? PendingTime { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("time_unit")]
        public double? TimeUnit { get; set; }

        [JsonProperty("preferences")]
        public IDictionary<string, object> Preferences { get; set; }

        [JsonProperty("updated_by_id")]
        public int? UpdatedById { get; set; }

        [JsonProperty("created_by_id")]
        public int? CreatedById { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonExtensionData]
        public Dictionary<string, object> CustomAttributes { get; set; }
    }
}
