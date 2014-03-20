using Newtonsoft.Json;
using ReadabilityApi.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReadabilityApi.Endpoints
{
    public abstract class IEndpoint
    {
        internal IEndpoint(ReadabilityClient readabilityClient)
        {
            ReadabilityClient = readabilityClient;
        }
        
        /// <summary>
        /// Generic function to make requests for protected data.
        /// </summary>
        /// <param name="request">The request to be made.</param>
        /// <param name="callback">A function to call when done with the request.</param>
        protected async void MakeRequest(RestRequest request)
        {
            await ReadabilityClient.MakeRequestAsync(request);
        }

        /// <summary>
        /// Builds a request object from a Condition object.
        /// </summary>
        /// <param name="conditions">The conditions to be converted into a request.</param>
        /// <returns></returns>
        protected RestRequest BuildRequestFromConditions(Conditions conditions)
        {
            RestRequest request = new RestRequest();
            var properties = conditions.GetType().GetProperties().Where(a => a.MemberType.Equals(MemberTypes.Property) && a.GetValue(conditions) != null);
            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute(typeof(JsonPropertyAttribute));
                var castAttribute = (JsonPropertyAttribute)attribute;
                request.AddParameter(castAttribute.PropertyName, property.GetValue(conditions));
            }
            return request;
        }

        protected ReadabilityClient ReadabilityClient;
    }
}
