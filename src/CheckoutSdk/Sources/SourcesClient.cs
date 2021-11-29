using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Sources
{
    public class SourcesClient : AbstractClient, ISourcesClient
    {
        private const string SourcesPath = "sources";

        public SourcesClient(IApiClient apiClient,
            CheckoutConfiguration configuration) : base(apiClient, configuration, SdkAuthorizationType.SecretKey)
        {
        }

        public Task<SepaSourceResponse> CreateSepaSource(SepaSourceRequest sepaSourceRequest,
            CancellationToken cancellationToken = default)
        {
            return ApiClient.Post<SepaSourceResponse>(SourcesPath, SdkAuthorization(), sepaSourceRequest,
                cancellationToken);
        }
    }
}