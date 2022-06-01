using Checkout.Common;
using Checkout.Files;
using Checkout.Marketplace.Balances;
using Checkout.Marketplace.Payout.Request;
using Checkout.Marketplace.Payout.Response;
using Checkout.Marketplace.Transfer;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Marketplace
{
    public class MarketplaceClient : FilesClient, IMarketplaceClient
    {
        private const string MarketplacePath = "marketplace";
        private const string EntitiesPath = "entities";
        private const string InstrumentPath = "instruments";
        private const string TransfersPath = "transfers";
        private const string BalancesPath = "balances";
        private const string PayoutSchedulePath = "payout-schedules";

        private readonly IApiClient _transfersApiClient;
        private readonly IApiClient _balancesApiClient;

        public MarketplaceClient(
            IApiClient apiClient,
            IApiClient filesApiClient,
            IApiClient transfersApiClient,
            IApiClient balancesApiClient,
            CheckoutConfiguration configuration)
            : base(apiClient, filesApiClient, configuration)
        {
            _transfersApiClient = transfersApiClient;
            _balancesApiClient = balancesApiClient;
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

        public async Task<EmptyResponse> CreatePaymentInstrument(string entityId,
            MarketplacePaymentInstrument marketplacePaymentInstrument, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("marketplacePaymentInstrument", marketplacePaymentInstrument, "entityId",
                entityId);
            return await ApiClient.Post<EmptyResponse>(
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

        public async Task<CreateTransferResponse> InitiateTransferOfFunds(CreateTransferRequest createTransferRequest,
            string idempotencyKey = null,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("createTransferRequest", createTransferRequest);
            return await _transfersApiClient.Post<CreateTransferResponse>(TransfersPath,
                SdkAuthorization(SdkAuthorizationType.OAuth),
                createTransferRequest,
                cancellationToken,
                idempotencyKey);
        }
        
        public async Task<TransferDetailsResponse> RetrieveATransfer(string transferId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("transferId", transferId);
            return await _transfersApiClient.Get<TransferDetailsResponse>(BuildPath(TransfersPath, transferId),
                SdkAuthorization(SdkAuthorizationType.OAuth),
                cancellationToken);
        }

        public async Task<BalancesResponse> RetrieveEntityBalances(string entityId, BalancesQuery balancesQuery,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("filter", entityId, "balancesQuery", balancesQuery);
            return await _balancesApiClient.Query<BalancesResponse>(BuildPath(BalancesPath, entityId),
                SdkAuthorization(),
                balancesQuery,
                cancellationToken);
        }

        public async Task<EmptyResponse> UpdatePayoutSchedule(string entityId, Currency currency,
            UpdateScheduleRequest updateScheduleRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("entityId", entityId, "currency", currency, "updateScheduleRequest",
                updateScheduleRequest);
            return await ApiClient.Put<EmptyResponse>(
                BuildPath(MarketplacePath, EntitiesPath, entityId, PayoutSchedulePath),
                SdkAuthorization(SdkAuthorizationType.OAuth),
                new Dictionary<Currency, UpdateScheduleRequest>() {{currency, updateScheduleRequest}},
                cancellationToken);
        }

        public async Task<GetScheduleResponse> RetrievePayoutSchedule(string entityId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("entityId", entityId);
            return await ApiClient.Get<GetScheduleResponse>(
                BuildPath(MarketplacePath, EntitiesPath, entityId, PayoutSchedulePath),
                SdkAuthorization(SdkAuthorizationType.OAuth),
                cancellationToken);
        }
    }
}