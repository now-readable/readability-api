using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RestSharp;

namespace ReadabilityApi
{
    /// <summary>
    /// Contains all the extensions I've made to standard classes.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Executes a request asynchronously by returning a task instead of taking a callback.
        /// </summary>
        /// <param name="this">The client to execute.</param>
        /// <param name="request">The request to execute with.</param>
        /// <returns>A task containing the response.</returns>
        public static Task<IRestResponse> ExecuteTaskAsync(this RestClient @this, RestRequest request)
        {
            if (@this == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<IRestResponse>();

            try
            {
                @this.ExecuteAsync(request, (response) =>
                {
                    if (response.ErrorException != null)
                        tcs.TrySetException(response.ErrorException);
                    else
                        tcs.TrySetResult(response);
                });

            }
            catch (Exception e)
            {
                var x= e;
            }
            return tcs.Task;
        }
    }   
}
