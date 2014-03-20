using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadabilityApi.Models
{
    [JsonObject]
    public class User
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("date_joined")]
        public DateTime DateJoined { get; set; }

        [JsonProperty("has_active_subscription")]
        public bool HasActiveSubscription { get; set; }

        [JsonProperty("reading_limit")]
        public string ReadingLimit { get; set; }

        [JsonProperty("email_into_address")]
        public string EmailIntoAddress { get; set; }

        [JsonProperty("kindle_email_address")]
        public string KindleEmailAddress { get; set; }

        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; }
    }
}