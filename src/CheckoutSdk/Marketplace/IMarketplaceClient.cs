using Checkout.Common;
using Checkout.Marketplace.Balances;
using Checkout.Marketplace.Payout.Request;
using Checkout.Marketplace.Payout.Response;
using Checkout.Marketplace.Transfer;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Marketplace
{
    public interface IMarketplaceClient
    {
        Task<OnboardEntityResponse> CreateEntity(OnboardEntityRequest entityRequest,
            CancellationToken cancellationToken = default);

        Task<OnboardEntityDetailsResponse> GetEntity(string entityId, CancellationToken cancellationToken = default);

        Task<OnboardEntityResponse> UpdateEntity(string entityId, OnboardEntityRequest entityRequest,
            CancellationToken cancellationToken = default);

        Task<EmptyResponse> CreatePaymentInstrument(string entityId, MarketplacePaymentInstrument marketplacePaymentInstrument,
            CancellationToken cancellationToken = default);

        Task<IdResponse> SubmitFile(MarketplaceFileRequest marketplaceFileRequest,
            CancellationToken cancellationToken = default);

        Task<CreateTransferResponse> InitiateTransferOfFunds(CreateTransferRequest createTransferRequest,
            string idempotencyKey = null,
            CancellationToken cancellationToken = default);
        
        Task<TransferDetailsResponse> RetrieveATransfer(string transferId,
            CancellationToken cancellationToken = default);

        Task<BalancesResponse> RetrieveEntityBalances(string entityId, BalancesQuery balancesQuery,
            CancellationToken cancellationToken = default);

        Task<EmptyResponse> UpdatePayoutSchedule(string entityId, Currency currency,
            UpdateScheduleRequest updateScheduleRequest, CancellationToken cancellationToken = default);

        Task<GetScheduleResponse>
            RetrievePayoutSchedule(string entityId, CancellationToken cancellationToken = default);
    }
}