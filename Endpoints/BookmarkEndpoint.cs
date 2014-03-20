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
    public class BookmarkEndpoint : IEndpoint
    {
        internal BookmarkEndpoint(ReadabilityClient readabilityClient) : base(readabilityClient) { }

        /// <summary>
        /// Favorites or unfavorites an article.
        /// </summary>
        /// <param name="bookmark">The bookmark to be favorited or unfavorited.</param>
        /// <returns>An awaitable task.</returns>
        public async Task ToggleFavorite(Bookmark bookmark)
        {
            var request = new RestRequest("bookmarks/" + bookmark.Id, Method.POST);
            if (bookmark.Archive)
            {
                request.AddParameter("favorite", 0);
            }
            else
            {
                request.AddParameter("favorite", 1);
            }
            await ReadabilityClient.MakeRequestAsync(request);
        }

        /// <summary>
        /// Archives or unarchives an article.
        /// </summary>
        /// <param name="bookmark">The bookmark to be archived or unarchived.</param>
        /// <returns>An awaitable task.</returns>
        public async Task ToggleArchive(Bookmark bookmark)
        {
            var request = new RestRequest("bookmarks/" + bookmark.Id, Method.POST);
            if (bookmark.Archive)
            {
                request.AddParameter("archive", 0);
            }
            else
            {
                request.AddParameter("archive", 1);
            }
            await ReadabilityClient.MakeRequestAsync(request);
        }

        /// <summary>
        /// Adds a given article to the reading list.
        /// </summary>
        /// <param name="url">The url to save.</param>
        /// <returns>An awaitable task.</returns>
        public async Task AddBookmark(string url)
        {
            var request = new RestRequest("bookmarks");
            request.AddParameter("url", url);
            request.Method = Method.POST;
            await ReadabilityClient.MakeRequestAsync(request);
        }

        /// <summary>
        /// Deletes an bookmark.
        /// </summary>
        /// <param name="bookmark">The bookmark to be deleted.</param>
        /// <returns>An awaitable task.</returns>
        public async Task Delete(Bookmark bookmark)
        {
            var request = new RestRequest("bookmarks/" + bookmark.Id, Method.DELETE);
            await ReadabilityClient.MakeRequestAsync(request);
        }
    }
}
