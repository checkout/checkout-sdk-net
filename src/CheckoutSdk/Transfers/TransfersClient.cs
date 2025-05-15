using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Transfers
{
    public class TransfersClient : AbstractClient, ITransfersClient
    {
        private const string TransfersPath = "transfers";

        public TransfersClient(IApiClient apiClient,
            CheckoutConfiguration configuration)
            : base(apiClient, configuration, SdkAuthorizationType.SecretKeyOrOAuth)
        {
        }

        public async Task<CreateTransferResponse> InitiateTransferOfFunds(CreateTransferRequest createTransferRequest,
            string idempotencyKey,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("createTransferRequest", createTransferRequest, "idempotencyKey", idempotencyKey);
            return await ApiClient.Post<CreateTransferResponse>(TransfersPath,
                SdkAuthorization(),
                createTransferRequest,
                cancellationToken,
                idempotencyKey);
        }

        public async Task<TransferDetailsResponse> RetrieveATransfer(string transferId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("transferId", transferId);
            return await ApiClient.Get<TransferDetailsResponse>(BuildPath(TransfersPath, transferId),
                SdkAuthorization(),
                cancellationToken);
        }
    }
}