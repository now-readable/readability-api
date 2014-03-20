using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadabilityApi.Models
{
    [JsonObject]
    public class Conditions
    {
        [JsonProperty("opened_since")]
        public DateTime? OpenedSince { get; set; }

        [JsonProperty("added_until")]
        public DateTime? AddedUntil { get; set; }

        [JsonProperty("opened_until")]
        public DateTime? OpenedUntil { get; set; }

        [JsonProperty("archived_until")]
        public DateTime? ArchivedUntil { get; set; }

        [JsonProperty("favorite")]
        public int? Favorite { get; set; }

        [JsonProperty("archived_since")]
        public DateTime? ArchivedSince { get; set; }

        [JsonProperty("favorited_since")]
        public DateTime? FavoritedSince { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("per_page")]
        public int? PerPage { get; set; }

        [JsonProperty("favorited_until")]
        public DateTime? FavoritedUntil { get; set; }

        [JsonProperty("archive")]
        public int? Archive { get; set; }

        [JsonProperty("added_since")]
        public DateTime? AddedSince { get; set; }

        [JsonProperty("order")]
        public string Order { get; set; }

        [JsonProperty("only_deleted")]
        public int? OnlyDeleted { get; set; }

        [JsonProperty("page")]
        public int? Page { get; set; }

        [JsonProperty("updated_since")]
        public DateTime? UpdatedSince { get; set; }

        [JsonProperty("updated_until")]
        public DateTime? UpdatedUntil { get; set; }
    }
}