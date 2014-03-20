using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Shell;
using RestSharp;
using RestSharp.Authenticators;
using System.IO.IsolatedStorage;

using ReadabilityApi.Models;
using System.Threading.Tasks;
using ReadabilityApi.Endpoints;
using Windows.System;

namespace ReadabilityApi
{
    public class ReadabilityClient
    {
        private UserEndpoint _userEndpoint;
        public UserEndpoint UserEndpoint
        {
            get
            {
                if (_userEndpoint == null)
                {
                    _userEndpoint = new UserEndpoint(this);
                }
                return _userEndpoint;
            }
        }

        private BookmarkEndpoint _bookmarkEndpoint;
        public BookmarkEndpoint BookmarkEndpoint
        {
            get
            {
                if (_bookmarkEndpoint == null)
                {
                    _bookmarkEndpoint = new BookmarkEndpoint(this);
                }
                return _bookmarkEndpoint;
            }
        }

        private BookmarkListEndpoint _bookmarkListEndpoint;
        public BookmarkListEndpoint BookmarkListEndpoint
        {
            get
            {
                if (_bookmarkListEndpoint == null)
                {
                    _bookmarkListEndpoint = new BookmarkListEndpoint(this);
                }
                return _bookmarkListEndpoint;
            }
        }
        
        private ArticleEndpoint _articleEndpoint;
        public ArticleEndpoint ArticleEndpoint
        {
            get
            {
                if (_articleEndpoint == null)
                {
                    _articleEndpoint = new ArticleEndpoint(this);
                }
                return _articleEndpoint;
            }
        }

        /// <summary>
        /// Modify this file (preferably in a .gitignore'd partial class) to initialize ApiBaseUri, ConsumerKey, ConsumerSecret.
        /// </summary>
        public ReadabilityClient(string apiBaseUrl, string consumerKey, string consumerSecret)
        {
            ApiBaseUrl = apiBaseUrl;
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;

            IsolatedStorageSettings isss = IsolatedStorageSettings.ApplicationSettings;
            isss.TryGetValue<string>("oauth_token", out OAuthToken);
            isss.TryGetValue<string>("oauth_token_secret", out OAuthTokenSecret);
            isss.TryGetValue<string>("access_token", out AccessToken);
            isss.TryGetValue<string>("access_token_secret", out AccessTokenSecret);
        }

        /// <summary>
        /// Start authenticating the user by getting the auth token and then sending them off to the browser to authenticate with Readability.
        /// </summary>
        internal async void BeginAuth()
        {
            IsolatedStorageSettings isss = IsolatedStorageSettings.ApplicationSettings;

            if (AccessToken == null || AccessToken == "")
            {
                var client = new RestClient(ApiBaseUrl);
                client.Authenticator = OAuth1Authenticator.ForRequestToken(ConsumerKey, ConsumerSecret);
                var request = new RestRequest("oauth/request_token");
                var response = await client.ExecuteTaskAsync(request);
                OAuthToken = Utilities.GetQueryParameter(response.Content, "oauth_token");
                OAuthTokenSecret = Utilities.GetQueryParameter(response.Content, "oauth_token_secret");

                if (isss.Contains("oath_token"))
                {
                    isss.Remove("oauth_token");
                }
                if (isss.Contains("oauth_token_secret"))
                {
                    isss.Remove("oauth_token_secret");
                }
                isss.Add("oauth_token", OAuthToken);
                isss.Add("oauth_token_secret", OAuthTokenSecret);
                isss.Save();

                string authorizeUrl = ApiBaseUrl + "oauth/authorize?oauth_token=" + OAuthToken + "&oauth_callback=nowreadable:Home";
                await Launcher.LaunchUriAsync(new Uri(authorizeUrl));
            }
        }

        /// <summary>
        /// Makes a request to get the access token for us.
        /// </summary>
        /// <param name="verifierKey">The key to use to verify the user.</param>
        /// <param name="callback">A function to call when we're done.</param>
        internal async Task<bool> GetAccessToken(string verifierKey)
        {
            var request = new RestRequest("oauth/access_token");
            request.AddParameter("oauth_verifier", verifierKey, ParameterType.GetOrPost);

            var client = new RestClient(ApiBaseUrl);
            client.Authenticator = OAuth1Authenticator.ForAccessToken(ConsumerKey, ConsumerSecret, OAuthToken, OAuthTokenSecret);
            var response = await client.ExecuteTaskAsync(request);
            if (response.Content == "")
                return false;
            AccessToken = Utilities.GetQueryParameter(response.Content, "oauth_token");
            AccessTokenSecret = Utilities.GetQueryParameter(response.Content, "oauth_token_secret");

            IsolatedStorageSettings isss = IsolatedStorageSettings.ApplicationSettings;
            isss.Add("access_token", AccessToken);
            isss.Add("access_token_secret", AccessTokenSecret);
            isss.Save();
            return true;
        }

        /// <summary>
        /// Makes an async request.
        /// </summary>
        /// <param name="request">The request to be made.</param>
        /// <returns>The string that was retrieved.</returns>
        internal async Task<string> MakeRequestAsync(RestRequest request)
        {
            var client = new RestClient(ApiBaseUrl);
            client.Authenticator = OAuth1Authenticator.ForProtectedResource(ConsumerKey, ConsumerSecret, AccessToken, AccessTokenSecret);
            var response = await client.ExecuteTaskAsync(request);
            return response.Content;
        }

        private string ApiBaseUrl;
        private string ConsumerKey;
        private string ConsumerSecret;
        private string OAuthToken;
        private string OAuthTokenSecret;
        private string AccessToken;
        private string AccessTokenSecret;
    }
}