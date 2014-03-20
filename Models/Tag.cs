using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadabilityApi.Models
{
    [JsonObject]
    public class Tag
    {
        [JsonProperty("id")]
        int Id { get; set; }

        [JsonProperty("text")]
        string Text { get; set; }

        [JsonProperty("applied_count")]
        int AppliedCount { get; set; }

        [JsonProperty("bookmark_ids")]
        List<int> BookmarkIds { get; set; }
    }
}
