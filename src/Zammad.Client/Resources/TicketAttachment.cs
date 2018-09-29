using System;
using System.Collections.Generic;
using System.IO;
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

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("mime-type")]
        public string MimeType { get; set; }

        [JsonProperty("preferences")]
        public IDictionary<string, object> Preferences { get; set; }

        public static TicketAttachment CreateFromFile(string fileName, string mimeType)
        {
            var buffer = File.ReadAllBytes(fileName);
            var base64 = Convert.ToBase64String(buffer);

            return new TicketAttachment
            {
                Filename = Path.GetFileName(fileName),
                Data = base64,
                MimeType = mimeType
            };
        }
    }
}
