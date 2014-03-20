using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadabilityApi.Models
{
    [JsonObject]
    public class Bookmark : IEquatable<Bookmark>
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("read_percent")]
        public string ReadPercent { get; set; }

        [JsonProperty("date_updated")]
        public DateTime? DateUpdated { get; set; }

        [JsonProperty("favorite")]
        public bool Favorite { get; set; }

        [JsonProperty("article")]
        public Article Article { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("date_archived")]
        public DateTime? DateArchived { get; set; }

        [JsonProperty("date_opened")]
        public DateTime? DateOpened { get; set; }

        [JsonProperty("date_added")]
        public DateTime? DateAdded { get; set; }

        [JsonProperty("article_href")]
        public string ArticleHref { get; set; }

        [JsonProperty("date_favorited")]
        public DateTime? DateFavorited { get; set; }

        [JsonProperty("archive")]
        public bool Archive { get; set; }

        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; }
        
        public  bool Equals(Bookmark bookmark)
        {
            return this.Id == bookmark.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}