using Checkout.Common;
using System.Threading.Tasks;

namespace Checkout.Marketplace
{
    public interface IMarketplaceClient
    {
        Task<OnboardEntityResponse> CreateEntity(OnboardEntityRequest entityRequest);

        Task<OnboardEntityDetailsResponse> GetEntity(string entityId);

        Task<OnboardEntityResponse> UpdateEntity(OnboardEntityRequest entityRequest, string entityId);

        Task CreatePaymentInstrument(MarketplacePaymentInstrument marketplacePaymentInstrument, string entityId);

        Task<IdResponse> SubmitFile(MarketplaceFileRequest marketplaceFileRequest);
    }
}