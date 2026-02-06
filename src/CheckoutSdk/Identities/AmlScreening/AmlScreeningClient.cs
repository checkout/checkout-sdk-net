using System.Threading;
using System.Threading.Tasks;
using Checkout.Identities.AmlScreening.Requests;
using Checkout.Identities.AmlScreening.Responses;

namespace Checkout.Identities.AmlScreening
{
    public class AmlScreeningClient : AbstractClient, IAmlScreeningClient
    {
        private const string AmlPath = "aml-verifications";

        public AmlScreeningClient(IApiClient apiClient, CheckoutConfiguration configuration) :
            base(apiClient, configuration, SdkAuthorizationType.SecretKeyOrOAuth)
        {
        }

        /// <summary>
        ///     Creates a new AML screening
        /// </summary>
        /// <param name="amlScreeningRequest">the AML screening request</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the AML screening response</returns>
        public Task<AmlScreeningResponse> CreateAmlScreening(AmlScreeningRequest amlScreeningRequest, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("amlScreeningRequest", amlScreeningRequest);
            return ApiClient.Post<AmlScreeningResponse>(AmlPath, 
                SdkAuthorization(), amlScreeningRequest, cancellationToken);
        }

        /// <summary>
        ///     Retrieves an existing AML screening by ID
        /// </summary>
        /// <param name="screeningId">the screening ID</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>the AML screening response</returns>
        public Task<AmlScreeningResponse> GetAmlScreening(string screeningId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("screeningId", screeningId);
            return ApiClient.Get<AmlScreeningResponse>(BuildPath(AmlPath, screeningId), 
                SdkAuthorization(), cancellationToken);
        }
    }
}