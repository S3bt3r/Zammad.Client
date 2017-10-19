using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Zammad.Client.Object
{
    [JsonObject]
    public class Object
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("object_lookup_id")]
        public int ObjectLookupId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("display")]
        public string Display { get; set; }

        [JsonProperty("data_type")]
        public string DataType { get; set; }

        [JsonProperty("data_option")]
        public ObjectDataOption DataOption { get; set; }

        [JsonProperty("data_option_new")]
        public dynamic DataOptionNew { get; set; }

        [JsonProperty("editable")]
        public bool Editable { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }
        
        [JsonProperty("screens")]
        public dynamic Screens { get; set; }
        
        [JsonProperty("to_create")]
        public bool ToCreate { get; set; }

        [JsonProperty("to_migrate")]
        public bool ToMigrate { get; set; }

        [JsonProperty("to_delete")]
        public bool ToDelete { get; set; }

        [JsonProperty("to_config")]
        public bool ToConfig { get; set; }

        [JsonProperty("position")]
        public int Position { get; set; }

        [JsonProperty("updated_by_id")]
        public int UpdatedById { get; set; }

        [JsonProperty("created_by_id")]
        public int CreatedById { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }
    }

    [JsonObject]
    public class ObjectDataOption
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("relation")]
        public string Relation { get; set; }

        [JsonProperty("autocapitalize")]
        public bool Autocapitalize { get; set; }

        [JsonProperty("multiple")]
        public bool Multiple { get; set; }

        [JsonProperty("guess")]
        public bool Guess { get; set; }

        [JsonProperty("null")]
        public bool Null { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("placeholder")]
        public string Placeholder { get; set; }

        [JsonProperty("minLengt")]
        public int MinLengt { get; set; }

        [JsonProperty("maxlength")]
        public int MaxLength { get; set; }

        [JsonProperty("translate")]
        public bool Translate { get; set; }

        [JsonProperty("item_class")]
        public string ItemClass { get; set; }

        [JsonProperty("permission")]
        public List<string> Permissions { get; set; } = new List<string>();
    }
}
