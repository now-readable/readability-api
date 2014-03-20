using Newtonsoft.Json;
using ReadabilityApi.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadabilityApi.Endpoints
{
    public class BookmarkListEndpoint : IEndpoint
    {
        internal BookmarkListEndpoint(ReadabilityClient readabilityClient) : base(readabilityClient) { }

        /// <summary>
        /// Gets a single page of bookmarks.
        /// </summary>
        /// <param name="conditions">The conditions of the request. It gets and converted to a rest request.</param>
        public async Task<BookmarkList> GetBookmarks(Conditions conditions)
        {
            var request = BuildRequestFromConditions(conditions);
            request.Resource = "bookmarks";
            var content = await ReadabilityClient.MakeRequestAsync(request).ConfigureAwait(false);
            var js = new StringReader(content);
            var jr = new JsonTextReader(js);
            var serializer = new JsonSerializer();
            var bookmarkList = serializer.Deserialize<BookmarkList>(jr);

            return bookmarkList;
        }

        /// <summary>
        /// Gets all the bookmarks based on a set of conditions. Should be doing the recursion at this level though.
        /// </summary>
        /// <param name="conditions">The conditions of the request. It gets and converted to a rest request.</param>
        public async Task<BookmarkList> GetAllBookmarks(Conditions conditions)
        {
            conditions.PerPage = 50;
            var masterList = await GetBookmarks(conditions);

            var allTasks = new List<Task<BookmarkList>>();
            for (var i = 2; i <= masterList.Meta.NumPages; i++)
            {
                conditions.Page = i;
                allTasks.Add(GetBookmarks(conditions));
            }

            foreach (var bookmarkList in await Task.WhenAll(allTasks))
            {
                foreach (var bookmark in bookmarkList.Bookmarks)
                {
                    masterList.Bookmarks.Add(bookmark);
                };
            }

            return masterList;
        }
    }
}
