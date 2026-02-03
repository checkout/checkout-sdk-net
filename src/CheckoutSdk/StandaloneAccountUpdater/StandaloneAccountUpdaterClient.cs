using Checkout.StandaloneAccountUpdater.Requests;
using Checkout.StandaloneAccountUpdater.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.StandaloneAccountUpdater
{
    public class StandaloneAccountUpdaterClient : AbstractClient, IStandaloneAccountUpdaterClient
    {
        private const string AccountUpdaterPath = "account-updater/cards";

        public StandaloneAccountUpdaterClient(IApiClient apiClient, CheckoutConfiguration configuration)
            : base(apiClient, configuration, SdkAuthorizationType.OAuth)
        {
        }

        public Task<GetUpdatedCardCredentialsResponse> GetUpdatedCardCredentials(GetUpdatedCardCredentialsRequest request,
                                                                                CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("request", request);
            return ApiClient.Post<GetUpdatedCardCredentialsResponse>(
                AccountUpdaterPath,
                SdkAuthorization(),
                request,
                cancellationToken);
        }
    }
}