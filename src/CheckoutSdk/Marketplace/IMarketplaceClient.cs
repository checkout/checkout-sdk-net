using Checkout.Common;
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

        Task<object> CreatePaymentInstrument(string entityId, MarketplacePaymentInstrument marketplacePaymentInstrument,
            CancellationToken cancellationToken = default);

        Task<IdResponse> SubmitFile(MarketplaceFileRequest marketplaceFileRequest,
            CancellationToken cancellationToken = default);

        Task<CreateTransferResponse> InitiateTransferOfFunds(CreateTransferRequest createTransferRequest,
            CancellationToken cancellationToken = default);
    }
}