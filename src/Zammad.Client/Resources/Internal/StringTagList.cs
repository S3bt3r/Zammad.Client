using System.Collections.Generic;
using Newtonsoft.Json;

namespace Zammad.Client.Resources.Internal
{
    [JsonObject]
    public class StringTagList
    {
        [JsonProperty("tags")]
        public IList<string> Tags { get; set; } = new List<string>();
    }
}
