using System;
using System.Windows.Navigation;

namespace ReadabilityApi
{
    /// <summary>
    /// Trims the URI so that we can navigate to it.
    /// </summary>
    public class AssociationUriMapper : UriMapperBase
    {
        public override Uri MapUri(Uri uri)
        {
            var tempUri = System.Net.HttpUtility.UrlDecode(uri.ToString());

            // URI association launch for contoso.
            if (tempUri.Contains("nowreadable:Home"))
            {
                var newUri = "/Views/Home.xaml" + tempUri.Replace("/Protocol?encodedLaunchUri=nowreadable:Home", "");
                return new Uri(newUri, UriKind.Relative);
            }

            // Otherwise perform normal launch.
            return uri;
        }
    }
}