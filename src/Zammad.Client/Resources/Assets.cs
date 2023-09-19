using System;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;

namespace Zammad.Client.Resources
{
    [JsonObject]
    public class Assets
    {
        [JsonProperty("Ticket")]
        public Dictionary<string, Ticket> Ticket { get; set; }
        
        [JsonProperty("Group")]
        public Dictionary<string, Group> Group { get; set; }
        
        [JsonProperty("User")]
        public Dictionary<string, User> User { get; set; }

        [JsonProperty("Role")]
        public Dictionary<string, Role> Role { get; set; }

        [JsonProperty("Organization")]
        public Dictionary<string, Organization> Organization { get; set; }
    }
}
