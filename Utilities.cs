using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadabilityApi
{
    /// <summary>
    /// Contains all the utilty functions I don't know where else to put.
    /// </summary>
    class Utilities
    {
        /// <summary>
        /// Gets the query parameter from a url string.
        /// </summary>
        /// <param name="input">The url string to parsed.</param>
        /// <param name="parameterName">The parameter to be returned.</param>
        /// <returns>The value of the parameter.</returns>
        public static string GetQueryParameter(string input, string parameterName)
        {
            foreach (string item in input.Split('&'))
            {
                var parts = item.Split('=');
                if (parts[0] == parameterName)
                {
                    return parts[1];
                }
            }
            return String.Empty;
        }

    }
}
