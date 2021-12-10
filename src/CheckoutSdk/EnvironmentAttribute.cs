using System;

namespace Checkout
{
    public class EnvironmentAttribute : Attribute
    {
        public Uri ApiUri { get; }
        public Uri AuthorizationUri { get; }

        public EnvironmentAttribute(string apiUri, string authorizationUri)
        {
            ApiUri = new Uri(apiUri);
            AuthorizationUri = new Uri(authorizationUri);
        }
    }
}