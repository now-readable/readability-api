using Newtonsoft.Json;
using RestSharp;
using System;
using System.IO;

using ReadabilityApi.Models;
using System.Threading.Tasks;

namespace ReadabilityApi.Endpoints
{
    public class UserEndpoint : IEndpoint
    {
        internal UserEndpoint(ReadabilityClient readabilityClient) : base(readabilityClient) { }

        /// <summary>
        /// Gets the current user from Readability.
        /// </summary>
        public async Task<User> GetCurrentUser()
        {
            var request = new RestRequest("/users/_current");
            var content = await ReadabilityClient.MakeRequestAsync(request);
            var js = new StringReader(content);
            var jr = new JsonTextReader(js);
            var serializer = new JsonSerializer();
            return serializer.Deserialize<User>(jr);
        }

        /// <summary>
        /// Begin authentication for the user.
        /// </summary>
        public void BeginAuth()
        {
            ReadabilityClient.BeginAuth();
        }

        /// <summary>
        /// Complete authentication with a manually entered string.
        /// </summary>
        /// <param name="verifierKey">The string that user entered manually.</param>
        /// <param name="callback">A function to call when the user has been added.</param>
        public async Task<User> CompleteAuth(string verifierKey)
        {
            var result = await ReadabilityClient.GetAccessToken(verifierKey);
            if (result == true)
            {
                return await GetCurrentUser();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Complete the authentication with a URI.
        /// </summary>
        /// <param name="uri">The URI that finished the auth process. We will parse this for the verification key.</param>
        /// <param name="callback">A function to call when the user has been added.</param>
        public async Task<User> CompleteAuth(Uri uri)
        {
            if (uri.OriginalString.Contains("oauth_callback"))
            {
                var decodedUri = System.Net.HttpUtility.UrlDecode(uri.OriginalString);
                var arguments = decodedUri.Split('?');
                if (arguments.Length < 1)
                    return null;

                // 2 is the second part of the url (after the second question mark)
                var verifierKey = Utilities.GetQueryParameter(arguments[2], "oauth_verifier");
                await ReadabilityClient.GetAccessToken(verifierKey);
                return await GetCurrentUser();
            }
            return null;
        }
    }
}
