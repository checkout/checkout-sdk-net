using System.Threading;
using System.Threading.Tasks;

namespace Checkout.MetaData.Card
{
    public class MetaDataCardClient : AbstractClient, IMetaDataCardClient
    {
        public MetaDataCardClient(IApiClient apiClient, CheckoutConfiguration configuration) :
            base(apiClient, configuration, SdkAuthorizationType.OAuth)
        {
        }

        public Task<MetaDataCardResponse> RequestCardMetaData(MetaDataCardRequest metaDataCardRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("metaDataCardRequest", metaDataCardRequest);
            return ApiClient.Post<MetaDataCardResponse>("metadata/card", SdkAuthorization(), metaDataCardRequest, cancellationToken);
        }
    }
}