using Newtonsoft.Json;

namespace Zammad.Client.Tag
{
    [JsonObject]
    public class Tag
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
