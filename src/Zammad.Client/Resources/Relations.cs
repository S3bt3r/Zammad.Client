using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Zammad.Client.Resources
{
    [JsonObject]
    public class Relations
    {
        [JsonProperty("links")]
        public List<Link> Links { get; set; }
        [JsonProperty("assets")]
        public Assets Assets { get; set; }
    }
}
