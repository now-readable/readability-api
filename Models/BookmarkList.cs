using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadabilityApi.Models
{
    [JsonObject]
    public class BookmarkList
    {
        public BookmarkList()
        {
            Conditions = new Conditions();
            Meta = new Meta();
            Bookmarks = new ObservableCollection<Bookmark>();
        }

        [JsonProperty("conditions")]
        public Conditions Conditions { get; set; }

        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("bookmarks")]
        public ObservableCollection<Bookmark> Bookmarks { get; set; }
    }
}
