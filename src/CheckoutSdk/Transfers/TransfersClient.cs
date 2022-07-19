using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Transfers
{
    public class TransfersClient : AbstractClient, ITransfersClient
    {
        private const string TransfersPath = "transfers";

        public TransfersClient(IApiClient apiClient,
            CheckoutConfiguration configuration)
            : base(apiClient, configuration, SdkAuthorizationType.OAuth)
        {
        }

        public async Task<CreateTransferResponse> InitiateTransferOfFunds(CreateTransferRequest createTransferRequest,
            string idempotencyKey = null,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("createTransferRequest", createTransferRequest);
            return await ApiClient.Post<CreateTransferResponse>(TransfersPath,
                SdkAuthorization(SdkAuthorizationType.OAuth),
                createTransferRequest,
                cancellationToken,
                idempotencyKey);
        }

        public async Task<TransferDetailsResponse> RetrieveATransfer(string transferId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("transferId", transferId);
            return await ApiClient.Get<TransferDetailsResponse>(BuildPath(TransfersPath, transferId),
                SdkAuthorization(SdkAuthorizationType.OAuth),
                cancellationToken);
        }
    }
}