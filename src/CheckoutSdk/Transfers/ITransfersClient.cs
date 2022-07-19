using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Transfers
{
    public interface ITransfersClient
    {
        Task<CreateTransferResponse> InitiateTransferOfFunds(CreateTransferRequest createTransferRequest,
            string idempotencyKey = null,
            CancellationToken cancellationToken = default);

        Task<TransferDetailsResponse> RetrieveATransfer(string transferId,
            CancellationToken cancellationToken = default);
    }
}