using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Apm.Ideal
{
    public class IdealClient : AbstractClient, IIdealClient
    {
        private const string IdealExternalPath = "ideal-external";
        private const string IssuersPath = "issuers";

        public IdealClient(
            IApiClient apiClient,
            CheckoutConfiguration configuration) : base(apiClient, configuration, SdkAuthorizationType.SecretKey)
        {
        }

        public Task<IdealInfo> GetInfo(CancellationToken cancellationToken = default)
        {
            return ApiClient.Get<IdealInfo>(IdealExternalPath, SdkAuthorization(), cancellationToken);
        }

        public Task<IssuerResponse> GetIssuers(CancellationToken cancellationToken = default)
        {
            return ApiClient.Get<IssuerResponse>(BuildPath(IdealExternalPath, IssuersPath), SdkAuthorization(),
                cancellationToken);
        }
    }
}