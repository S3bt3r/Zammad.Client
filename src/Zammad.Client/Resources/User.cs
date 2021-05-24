using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Zammad.Client.Resources
{
    [JsonObject]
    public class User
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("organization_id")]
        public int? OrganizationId { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("firstname")]
        public string FirstName { get; set; }

        [JsonProperty("lastname")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("image_source")]
        public string ImageSource { get; set; }

        [JsonProperty("web")]
        public string Web { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("fax")]
        public string Fax { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("department")]
        public string Department { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("vip")]
        public bool Vip { get; set; }

        [JsonProperty("verified")]
        public bool Verified { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("last_login")]
        public DateTimeOffset? LastLogin { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("login_failed")]
        public int LoginFailed { get; set; }

        [JsonProperty("out_of_office")]
        public bool OutOfOffice { get; set; }

        [JsonProperty("out_of_office_start_at")]
        public DateTimeOffset? OutOfOfficeStartAt { get; set; }

        [JsonProperty("out_of_office_end_at")]
        public DateTimeOffset? OutOfOfficeEndAt { get; set; }

        [JsonProperty("out_of_office_replacement_id")]
        public int? OutOfOfficeReplacementId { get; set; }

        [JsonProperty("preferences")]
        public IDictionary<string, object> Preferences { get; set; }

        [JsonProperty("updated_by_id")]
        public int UpdatedById { get; set; }

        [JsonProperty("created_by_id")]
        public int CreatedById { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonExtensionData]
        public Dictionary<string, object> CustomAttributes { get; set; }
    }
}
