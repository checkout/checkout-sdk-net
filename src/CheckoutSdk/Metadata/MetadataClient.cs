using Checkout.Metadata.Card;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Metadata
{
    public class MetadataClient : AbstractClient, IMetadataClient
    {
        private const string MetadataPath = "metadata";
        private const string CardPath = "card";

        public MetadataClient(IApiClient apiClient, CheckoutConfiguration configuration) : base(apiClient,
            configuration, SdkAuthorizationType.SecretKeyOrOAuth)
        {
        }

        public Task<CardMetadataResponse> RequestCardMetadata(CardMetadataRequest cardMetadataRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("cardMetadataRequest", cardMetadataRequest);
            return ApiClient.Post<CardMetadataResponse>(BuildPath(MetadataPath, CardPath), SdkAuthorization(), cardMetadataRequest,
                cancellationToken);
        }
    }
}