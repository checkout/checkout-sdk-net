using Checkout.Metadata.Card;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Metadata
{
    public class MetadataClient : AbstractClient, IMetadataClient
    {
        public MetadataClient(IApiClient apiClient, CheckoutConfiguration configuration) : base(apiClient,
            configuration, SdkAuthorizationType.SecretKeyOrOAuth)
        {
        }

        public Task<CardMetadataResponse> RequestCardMetadata(CardMetadataRequest cardMetadataRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("cardMetadataRequest", cardMetadataRequest);
            return ApiClient.Post<CardMetadataResponse>("metadata/card", SdkAuthorization(), cardMetadataRequest,
                cancellationToken);
        }
    }
}