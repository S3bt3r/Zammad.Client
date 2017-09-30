using Newtonsoft.Json;
using System.Collections.Generic;

namespace Zammad.Client.Tag.Internal
{
    [JsonObject]
    public class TagList
    {
        [JsonProperty("tags")]
        public IList<Tag> Tags { get; set; } = new List<Tag>();
    }
}
