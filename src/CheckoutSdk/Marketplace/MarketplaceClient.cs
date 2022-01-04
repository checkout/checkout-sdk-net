using Checkout.Common;
using Checkout.Files;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Marketplace
{
    public class MarketplaceClient : AbstractClient, IMarketplaceClient
    {
        public const string MarketplacePath = "marketplace";
        public const string EntitiesPath = "entities";
        public const string InstrumentPath = "instruments";
        public const string FilesPath = "files";
        public const string SubdomainFilesPath = "files";

        private IFilesClient _fileClient;

        public MarketplaceClient(
            IApiClient apiClient,
            ICheckoutConfiguration configuration,
            IFilesClient filesClient)
            : base(apiClient, configuration, SdkAuthorizationType.OAuth)
        {
            this._fileClient = filesClient;
        }

        public async Task<OnboardEntityResponse> CreateEntity(OnboardEntityRequest entityRequest)
        {
            CheckoutUtils.ValidateParams("entityRequest", entityRequest);
            return await ApiClient.Post<OnboardEntityResponse>(BuildPath(MarketplacePath, EntitiesPath), SdkAuthorization(), entityRequest);
        }

        public async Task<OnboardEntityDetailsResponse> GetEntity(string entityId)
        {
            CheckoutUtils.ValidateParams("entityId", entityId);
            return await ApiClient.Get<OnboardEntityDetailsResponse>(BuildPath(MarketplacePath, EntitiesPath, entityId), SdkAuthorization());
        }

        public async Task<OnboardEntityResponse> UpdateEntity(OnboardEntityRequest entityRequest, string entityId)
        {
            CheckoutUtils.ValidateParams("entityRequest", entityRequest, "entityId", entityId);
            return await ApiClient.Put<OnboardEntityResponse>(BuildPath(MarketplacePath, EntitiesPath, entityId), SdkAuthorization(), entityRequest);
        }

        public async Task CreatePaymentInstrument(MarketplacePaymentInstrument marketplacePaymentInstrument, string entityId)
        {
            CheckoutUtils.ValidateParams("marketplacePaymentInstrument", marketplacePaymentInstrument, "entityId", entityId);
            await ApiClient.Post(BuildPath(MarketplacePath, EntitiesPath, entityId, InstrumentPath), SdkAuthorization(), marketplacePaymentInstrument);
        }

        public async Task<IdResponse> SubmitFile(MarketplaceFileRequest marketplaceFileRequest)
        {
            var filesApiConfiguration = _configuration.GetFilesApiConfiguration();
            if (filesApiConfiguration.GetEnvironment() == null)
            {
                throw new CheckoutFileException("Files API is not enabled in this client. It must be enabled in CheckoutFourSdk configuration.");
            }

            return await SubmitFile(marketplaceFileRequest.File, marketplaceFileRequest.Purpose.Value, multipartFileHeaderName: "path");
        }

        private Task<IdResponse> SubmitFile(string pathToFile, string purpose,
            CancellationToken cancellationToken = default, string multipartFileHeaderName = null)
        {
            CheckoutUtils.ValidateParams("pathToFile", pathToFile, "purpose", purpose);
            var dataContent = FilesClient.CreateMultipartRequest(pathToFile, purpose, multipartFileHeaderName);
            return ApiClient.PostFile<IdResponse>(FilesPath, SdkAuthorization(), dataContent, cancellationToken);
        }
    }
}