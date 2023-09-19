using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Zammad.Client.Resources
{
    public enum LinkType
    {
        child,
        parent
    }
    public enum LinkObject
    {
        Ticket
    }
    [JsonObject]
    public class Link
    {
        [JsonProperty("link_type")]
        public string LinkType { get; set; }

        [JsonProperty("link_object")]
        public string LinkObject { get; set; }

        [JsonProperty("link_object_value")]
        public int LinkObjectValue { get; set; }
    }
}
