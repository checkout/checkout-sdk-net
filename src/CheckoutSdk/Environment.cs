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

        public EnvironmentSubdomain(Environment environment, string subdomain)
        {
            ApiUri = AddSubdomainToApiUrlEnvironment(environment, subdomain);
        }
        
        private static Uri AddSubdomainToApiUrlEnvironment(Environment environment, string subdomain)
        {
            Uri apiUrl = environment.GetAttribute<EnvironmentAttribute>().ApiUri;
            
            Uri newEnvironment = new Uri(apiUrl.ToString());
            
            Regex regex = new Regex(@"^[0-9a-z]{8}$");
            if (regex.IsMatch(subdomain))
            {
                UriBuilder merchantApiUrl = new UriBuilder(apiUrl.Host);
                merchantApiUrl.Host = subdomain + "." + merchantApiUrl.Host;
                merchantApiUrl.Scheme = apiUrl.Scheme;
                merchantApiUrl.Port = apiUrl.Port;

                newEnvironment = new Uri(merchantApiUrl.ToString());
            }

            return newEnvironment;
        }
    }
}