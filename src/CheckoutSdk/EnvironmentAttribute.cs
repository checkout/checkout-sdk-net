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

        public EnvironmentAttribute(
            string apiUri,
            string authorizationUri,
            string filesApiUri,
            string transfersApiUri,
            string balancesApiUri)
        {
            ApiUri = new Uri(apiUri);
            FilesApiUri = new Uri(filesApiUri);
            AuthorizationUri = new Uri(authorizationUri);
            TransfersApiUri = new Uri(transfersApiUri);
            BalancesApiUri = new Uri(balancesApiUri);
        }
    }
}