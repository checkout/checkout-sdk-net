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
            "https://balances.sandbox.checkout.com/",
            "https://forward.sandbox.checkout.com/",
            "https://identity-verification.sandbox.checkout.com/")]
        Sandbox,

        [Environment("https://api.checkout.com/",
            "https://access.checkout.com/connect/token",
            "https://files.checkout.com/",
            "https://transfers.checkout.com/",
            "https://balances.checkout.com/",
            "https://forward.checkout.com/",
            "https://identity-verification.checkout.com/")]
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
        /// Applies subdomain transformation to any given URI, prepending the subdomain to the host.
        /// </summary>
        /// <param name="originalUrl">The original URI to transform</param>
        /// <param name="subdomain">The subdomain to prepend</param>
        /// <returns>The transformed URI with subdomain</returns>
        /// <exception cref="CheckoutArgumentException">Thrown when the subdomain is not a valid merchant-specific subdomain</exception>
        private static readonly Regex SubdomainRegex =
            new Regex(@"\A(?:pl-)?[a-z0-9]+\z", RegexOptions.None, TimeSpan.FromMilliseconds(100));

        private static Uri CreateUrlWithSubdomain(Uri originalUrl, string subdomain)
        {
            if (subdomain == null || !SubdomainRegex.IsMatch(subdomain))
            {
                throw new CheckoutArgumentException(
                    "invalid environment subdomain - provide your merchant-specific subdomain, typically " +
                    "your client ID excluding the cli_ prefix (see " +
                    "https://api-reference.checkout.com/#section/Base-URLs)");
            }

            UriBuilder merchantUrl = new UriBuilder(originalUrl);
            merchantUrl.Host = subdomain + "." + originalUrl.Host;
            merchantUrl.Scheme = originalUrl.Scheme;
            merchantUrl.Port = originalUrl.Port;

            return new Uri(merchantUrl.ToString());
        }
    }
}
