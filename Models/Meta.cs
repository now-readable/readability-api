using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadabilityApi.Models
{
    [JsonObject]
    public class Meta
    {
        [JsonProperty("num_pages")]
        public int NumPages { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("item_count_total")]
        public int ItemCountTotal { get; set; }

        [JsonProperty("item_count")]
        public int ItemCount { get; set; }
    }
}
