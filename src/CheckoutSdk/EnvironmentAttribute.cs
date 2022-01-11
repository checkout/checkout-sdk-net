using System;

namespace Checkout
{
    public class EnvironmentAttribute : Attribute
    {
        public Uri ApiUri { get; }
        public Uri AuthorizationUri { get; }
        public Uri FilesApiUri { get; }

        public EnvironmentAttribute(string apiUri, string authorizationUri, string filesApiUri)
        {
            ApiUri = new Uri(apiUri);
            FilesApiUri = new Uri(filesApiUri);
            AuthorizationUri = new Uri(authorizationUri);
        }
    }
}