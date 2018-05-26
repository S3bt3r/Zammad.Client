using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Zammad.Client.Resources
{
    [JsonObject]
    public class TicketArticle
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("ticket_id")]
        public int TicketId { get; set; }

        [JsonProperty("sender_id")]
        public int SenderId { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("cc")]
        public string CC { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("reply_to")]
        public string ReplyTo { get; set; }

        [JsonProperty("message_id")]
        public string MessageId { get; set; }

        [JsonProperty("message_id_md5")]
        public string MessageIdMD5 { get; set; }

        [JsonProperty("in_reply_to")]
        public string InReplyTo { get; set; }

        [JsonProperty("content_type")]
        public string ContentType { get; set; }

        [JsonProperty("references")]
        public string References { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("internal")]
        public bool Internal { get; set; }

        [JsonProperty("preferences")]
        public IDictionary<string, object> Preferences { get; set; }

        [JsonProperty("updated_by_id")]
        public int UpdatedById { get; set; }

        [JsonProperty("created_by_id")]
        public int CreatedById { get; set; }

        [JsonProperty("origin_by_id")]
        public int? OriginById { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonProperty("attachments")]
        public List<TicketAttachment> Attachments { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("sender")]
        public string Sender { get; set; }

        [JsonProperty("created_by")]
        public string CreatedBy { get; set; }

        [JsonProperty("updated_by")]
        public string UpdatedBy { get; set; }
    }
}
