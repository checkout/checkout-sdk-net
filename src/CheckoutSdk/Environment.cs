using System;
using System.Text.RegularExpressions;

namespace Checkout
{
    public enum Environment
    {
        [Environment("https://api.sandbox.checkout.com/",
            "https://access.sandbox.checkout.com/connect/token",
            "https://files.sandbox.checkout.com/",
            "https://transfers.sandbox.checkout.com/",
            "https://balances.sandbox.checkout.com/")]
        Sandbox,

        [Environment("https://api.checkout.com/",
            "https://access.checkout.com/connect/token",
            "https://files.checkout.com/",
            "https://transfers.checkout.com/",
            "https://balances.checkout.com/")]
        Production
    }

    public class EnvironmentSubdomain
    {
        public Uri ApiUri { get; }
        public Uri AuthorizationUri { get; }

        public EnvironmentSubdomain(Environment environment, string subdomain)
        {
            ApiUri = CreateUrlWithSubdomain(environment.GetAttribute<EnvironmentAttribute>().ApiUri, subdomain);
            AuthorizationUri = CreateUrlWithSubdomain(environment.GetAttribute<EnvironmentAttribute>().AuthorizationUri, subdomain);
        }
        
        /// <summary>
        /// Applies subdomain transformation to any given URI.
        /// If the subdomain is valid (alphanumeric pattern), prepends it to the host.
        /// Otherwise, returns the original URI unchanged.
        /// </summary>
        /// <param name="originalUrl">The original URI to transform</param>
        /// <param name="subdomain">The subdomain to prepend</param>
        /// <returns>The transformed URI with subdomain, or original URI if subdomain is invalid</returns>
        private static Uri CreateUrlWithSubdomain(Uri originalUrl, string subdomain)
        {
            Uri newEnvironment = new Uri(originalUrl.ToString());
            
            Regex regex = new Regex(@"^[0-9a-z]+$");
            if (regex.IsMatch(subdomain))
            {
                UriBuilder merchantUrl = new UriBuilder(originalUrl);
                merchantUrl.Host = subdomain + "." + originalUrl.Host;
                merchantUrl.Scheme = originalUrl.Scheme;
                merchantUrl.Port = originalUrl.Port;

                newEnvironment = new Uri(merchantUrl.ToString());
            }

            return newEnvironment;
        }
    }
}