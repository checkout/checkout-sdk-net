using Checkout.Common;
using Checkout.Files;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Marketplace
{
    public class MarketplaceClient : FilesClient, IMarketplaceClient
    {
        private const string MarketplacePath = "marketplace";
        private const string EntitiesPath = "entities";
        private const string InstrumentPath = "instruments";

        public MarketplaceClient(
            IApiClient apiClient,
            IApiClient filesApiClient,
            CheckoutConfiguration configuration)
            : base(apiClient, filesApiClient, configuration)
        {
        }

        public async Task<OnboardEntityResponse> CreateEntity(OnboardEntityRequest entityRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("entityRequest", entityRequest);
            return await ApiClient.Post<OnboardEntityResponse>(
                BuildPath(MarketplacePath, EntitiesPath),
                SdkAuthorization(SdkAuthorizationType.OAuth),
                entityRequest,
                cancellationToken);
        }

        public async Task<OnboardEntityDetailsResponse> GetEntity(string entityId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("entityId", entityId);
            return await ApiClient.Get<OnboardEntityDetailsResponse>(
                BuildPath(MarketplacePath, EntitiesPath, entityId),
                SdkAuthorization(SdkAuthorizationType.OAuth),
                cancellationToken);
        }

        public async Task<OnboardEntityResponse> UpdateEntity(string entityId, OnboardEntityRequest entityRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("entityRequest", entityRequest, "entityId", entityId);
            return await ApiClient.Put<OnboardEntityResponse>(
                BuildPath(MarketplacePath, EntitiesPath, entityId),
                SdkAuthorization(SdkAuthorizationType.OAuth),
                entityRequest,
                cancellationToken);
        }

        public async Task<object> CreatePaymentInstrument(string entityId,
            MarketplacePaymentInstrument marketplacePaymentInstrument, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("marketplacePaymentInstrument", marketplacePaymentInstrument, "entityId",
                entityId);
            return await ApiClient.Post<object>(
                BuildPath(MarketplacePath, EntitiesPath, entityId, InstrumentPath),
                SdkAuthorization(SdkAuthorizationType.OAuth),
                marketplacePaymentInstrument,
                cancellationToken);
        }

        public async Task<IdResponse> SubmitFile(MarketplaceFileRequest marketplaceFileRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("marketplaceFileRequest", marketplaceFileRequest,
                "marketplaceFileRequest.purpose", marketplaceFileRequest.Purpose);
            return await SubmitFileToFilesApi(marketplaceFileRequest.File, marketplaceFileRequest.Purpose.Value,
                cancellationToken);
        }
    }
}