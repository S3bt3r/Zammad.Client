using System.Collections.Generic;
using Newtonsoft.Json;

namespace Zammad.Client.Resources.Internal
{
    [JsonObject]
    public class TagList
    {
        [JsonProperty("tags")]
        public IList<Tag> Tags { get; set; } = new List<Tag>();
    }
}
