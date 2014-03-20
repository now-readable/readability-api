using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ReadabilityApi.Models
{
    [JsonObject]
    public class Article
    {
        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("lead_image_url")]
        public string LeadImageUrl { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("excerpt")]
        public string Excerpt { get; set; }

        [JsonProperty("direction")]
        public string Direction { get; set; }

        [JsonProperty("word_count")]
        public int? WordCount { get; set; }

        [JsonProperty("date_published")]
        public DateTime? DatePublished { get; set; }

        [JsonProperty("dek")]
        public string Dek { get; set; }

        [JsonProperty("processed")]
        public bool? Processed { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("next_page_href")]
        public string NextPageHref { get; set; }

        [JsonProperty("content_size")]
        public string ContentSize { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("ShortUrl")]
        public string ShortUrl { get; set; }
    }
}

