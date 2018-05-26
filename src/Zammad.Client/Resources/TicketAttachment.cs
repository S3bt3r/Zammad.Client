using System.Collections.Generic;
using Newtonsoft.Json;

namespace Zammad.Client.Resources
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
