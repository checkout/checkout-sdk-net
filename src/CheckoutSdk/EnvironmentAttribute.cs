using System;

namespace Checkout
{
    public class EnvironmentAttribute : Attribute
    {
        public Uri ApiUri { get; }
        public Uri AuthorizationUri { get; }
        public Uri FileUri { get; }

        public EnvironmentAttribute(string apiUri, string authorizationUri, string fileUri = null)
        {
            ApiUri = new Uri(apiUri);
            if (!string.IsNullOrEmpty(fileUri))
            {
                FileUri = new Uri(fileUri);
            }
            AuthorizationUri = new Uri(authorizationUri);
        }
    }
}