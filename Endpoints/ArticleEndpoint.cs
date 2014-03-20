using Newtonsoft.Json;
using ReadabilityApi.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ReadabilityApi.Endpoints
{
    public class ArticleEndpoint : IEndpoint
    {
        internal ArticleEndpoint(ReadabilityClient readabilityClient) : base(readabilityClient) { }
        
        /// <summary>
        /// Gets a specific article from a bookmark based on its href.
        /// </summary>
        /// <param name="bookmark">The bookmark that has the article to be returned.</param>
        /// <returns>The full article as a string. This is useful for immediately saving it.</returns>
        public async Task<Article> GetArticle(Bookmark bookmark)
        {
            var request = new RestRequest(bookmark.ArticleHref.Replace("/api/rest/v1", ""));
            var content = await ReadabilityClient.MakeRequestAsync(request).ConfigureAwait(false);
            var js = new StringReader(content);
            var jr = new JsonTextReader(js);
            var serializer = new JsonSerializer();
            return serializer.Deserialize<Article>(jr);
        }
    }
}
