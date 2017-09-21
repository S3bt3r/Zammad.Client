using System;
using Newtonsoft.Json;

namespace Zammad.Client.OnlineNotification
{
    public class OnlineNotification
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("o_id")]
        public int ObjectId { get; set; }

        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("seen")]
        public bool Seen { get; set; }

        [JsonProperty("updated_by_id")]
        public int UpdatedById { get; set; }

        [JsonProperty("created_by_id")]
        public int CreatedById { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
