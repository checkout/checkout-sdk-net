using System;

namespace Checkout
{
    public class EnvironmentAttribute : Attribute
    {
        public Uri ApiUri { get; }
        public Uri AuthorizationUri { get; }
        public Uri FilesApiUri { get; }
        public Uri TransfersApiUri { get; }
        public Uri BalancesApiUri { get; }
        public Uri ForwardApiUri { get; }
        public Uri IdentityApiUri { get; }

        public EnvironmentAttribute(
            string apiUri,
            string authorizationUri,
            string filesApiUri,
            string transfersApiUri,
            string balancesApiUri,
            string forwardApiUri,
            string identityApiUri)
        {
            ApiUri = new Uri(apiUri);
            FilesApiUri = new Uri(filesApiUri);
            AuthorizationUri = new Uri(authorizationUri);
            TransfersApiUri = new Uri(transfersApiUri);
            BalancesApiUri = new Uri(balancesApiUri);
            ForwardApiUri = new Uri(forwardApiUri);
            IdentityApiUri = new Uri(identityApiUri);
        }
    }
}