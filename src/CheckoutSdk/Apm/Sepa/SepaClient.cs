using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Apm.Sepa
{
    public class SepaClient : AbstractClient, ISepaClient
    {
        private const string SepaMandatesPath = "sepa/mandates";
        private const string PproPath = "ppro";
        private const string CancelPath = "cancel";

        public SepaClient(
            IApiClient apiClient,
            CheckoutConfiguration configuration) : base(apiClient, configuration, SdkAuthorizationType.SecretKey)
        {
        }

        public Task<MandateResponse> GetMandate(string mandateId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("mandateId", mandateId);
            return ApiClient.Get<MandateResponse>(BuildPath(SepaMandatesPath, mandateId), SdkAuthorization(),
                cancellationToken);
        }

        public Task<SepaResource> CancelMandate(string mandateId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("mandateId", mandateId);
            return ApiClient.Post<SepaResource>(
                BuildPath(SepaMandatesPath, mandateId, CancelPath),
                SdkAuthorization(),
                null,
                cancellationToken,
                null);
        }

        public Task<MandateResponse> GetMandateViaPpro(string mandateId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("mandateId", mandateId);
            return ApiClient.Get<MandateResponse>(BuildPath(PproPath, SepaMandatesPath, mandateId), SdkAuthorization(),
                cancellationToken);
        }

        public Task<SepaResource> CancelMandateViaPpro(string mandateId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("mandateId", mandateId);
            return ApiClient.Post<SepaResource>(
                BuildPath(PproPath, SepaMandatesPath, mandateId, CancelPath),
                SdkAuthorization(),
                null,
                cancellationToken,
                null);
        }
    }
}