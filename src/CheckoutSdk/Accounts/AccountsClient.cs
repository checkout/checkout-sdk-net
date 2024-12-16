using Checkout.Accounts.Entities.Request;
using Checkout.Accounts.Entities.Response;
using Checkout.Accounts.Payout.Request;
using Checkout.Accounts.Payout.Response;
using Checkout.Common;
using Checkout.Files;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Accounts
{
    public class AccountsClient : FilesClient, IAccountsClient
    {
        private const string AccountsPath = "accounts";
        private const string EntitiesPath = "entities";
        private const string MembersPath = "members";
        private const string FilesPath = "files";
        private const string InstrumentPath = "instruments";
        private const string PayoutSchedulePath = "payout-schedules";
        private const string PaymentInstrumentsPath = "payment-instruments";

        public AccountsClient(
            IApiClient apiClient,
            IApiClient filesApiClient,
            CheckoutConfiguration configuration)
            : base(apiClient, filesApiClient, configuration)
        {
        }

        public async Task<OnboardEntityResponse> CreateEntity(
            OnboardEntityRequest entityRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("entityRequest", entityRequest);
            return await ApiClient.Post<OnboardEntityResponse>(
                BuildPath(AccountsPath, EntitiesPath),
                SdkAuthorization(),
                entityRequest,
                cancellationToken);
        }

        public async Task<OnboardSubEntityDetailsResponse> GetSubEntityMembers(
            string entityId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("entityId", entityId);
            return await ApiClient.Get<OnboardSubEntityDetailsResponse>(
                BuildPath(AccountsPath, EntitiesPath, entityId, MembersPath),
                SdkAuthorization(),
                cancellationToken);
        }

        public async Task<OnboardSubEntityResponse> ReinviteSubEntityMember(
            string entityId,
            string userId,
            OnboardSubEntityRequest subEntityRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("entityId", entityId, "userId", userId, 
                "subEntityRequest", subEntityRequest);
            return await ApiClient.Put<OnboardSubEntityResponse>(
                BuildPath(AccountsPath, EntitiesPath, entityId, MembersPath, userId),
                SdkAuthorization(),
                subEntityRequest,
                cancellationToken);
        }

        public async Task<OnboardEntityDetailsResponse> GetEntity(
            string entityId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("entityId", entityId);
            return await ApiClient.Get<OnboardEntityDetailsResponse>(
                BuildPath(AccountsPath, EntitiesPath, entityId),
                SdkAuthorization(),
                cancellationToken);
        }

        public async Task<OnboardEntityResponse> UpdateEntity(
            string entityId,
            OnboardEntityRequest entityRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("entityId", entityId, "entityRequest", entityRequest);
            return await ApiClient.Put<OnboardEntityResponse>(
                BuildPath(AccountsPath, EntitiesPath, entityId),
                SdkAuthorization(),
                entityRequest,
                cancellationToken);
        }

        public async Task<EmptyResponse> CreatePaymentInstrument(
            string entityId,
            AccountsPaymentInstrument accountsPaymentInstrument,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("entityId", entityId, "accountsPaymentInstrument", accountsPaymentInstrument);
            return await ApiClient.Post<EmptyResponse>(
                BuildPath(AccountsPath, EntitiesPath, entityId, InstrumentPath),
                SdkAuthorization(),
                accountsPaymentInstrument,
                cancellationToken);
        }

        public async Task<IdResponse> CreatePaymentInstrument(
            string entityId,
            PaymentInstrumentRequest paymentInstrumentRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("entityId", entityId, "paymentInstrumentRequest", paymentInstrumentRequest);
            return await ApiClient.Post<IdResponse>(
                BuildPath(AccountsPath, EntitiesPath, entityId, PaymentInstrumentsPath),
                SdkAuthorization(),
                paymentInstrumentRequest,
                cancellationToken);
        }

        public async Task<PaymentInstrumentDetailsResponse> RetrievePaymentInstrumentDetails(
            string entityId,
            string paymentInstrumentId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("entityId", entityId, "paymentInstrumentId", paymentInstrumentId);
            return await ApiClient.Get<PaymentInstrumentDetailsResponse>(
                BuildPath(AccountsPath, EntitiesPath, entityId, PaymentInstrumentsPath, paymentInstrumentId),
                SdkAuthorization(),
                cancellationToken);
        }

        public async Task<IdResponse> UpdatePaymentInstrument(
            string entityId,
            string instrumentId,
            UpdatePaymentInstrumentRequest updatePaymentInstrumentRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("entityId", entityId, "instrumentId", instrumentId,
                "updatePaymentInstrumentRequest", updatePaymentInstrumentRequest);
            return await ApiClient.Patch<IdResponse>(
                BuildPath(AccountsPath, EntitiesPath, entityId, PaymentInstrumentsPath, instrumentId),
                SdkAuthorization(),
                updatePaymentInstrumentRequest,
                cancellationToken);
        }

        public async Task<PaymentInstrumentQueryResponse> QueryPaymentInstruments(
            string entityId,
            PaymentInstrumentsQuery query = null,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("entityId", entityId);
            return await ApiClient.Query<PaymentInstrumentQueryResponse>(
                BuildPath(AccountsPath, EntitiesPath, entityId, PaymentInstrumentsPath),
                SdkAuthorization(),
                query,
                cancellationToken);
        }

        public async Task<GetScheduleResponse> RetrievePayoutSchedule(
            string entityId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("entityId", entityId);
            return await ApiClient.Get<GetScheduleResponse>(
                BuildPath(AccountsPath, EntitiesPath, entityId, PayoutSchedulePath),
                SdkAuthorization(),
                cancellationToken);
        }

        public async Task<EmptyResponse> UpdatePayoutSchedule(
            string entityId,
            Currency currency,
            UpdateScheduleRequest updateScheduleRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("entityId", entityId, "currency", currency, "updateScheduleRequest",
                updateScheduleRequest);
            return await ApiClient.Put<EmptyResponse>(
                BuildPath(AccountsPath, EntitiesPath, entityId, PayoutSchedulePath),
                SdkAuthorization(),
                new Dictionary<Currency, UpdateScheduleRequest>() { { currency, updateScheduleRequest } },
                cancellationToken);
        }

        public async Task<IdResponse> SubmitFile(
            AccountsFileRequest accountsFileRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("accountsFileRequest", accountsFileRequest,
                "accountsFileRequest.purpose", accountsFileRequest.Purpose);
            return await SubmitFileToFilesApi(accountsFileRequest.File, accountsFileRequest.Purpose.Value,
                cancellationToken);
        }

        public async Task<UploadFileResponse> UploadFile(
            string entityId,
            AccountsFileRequest accountsFileRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("accountsFileRequest", accountsFileRequest);
            return await ApiClient.Post<UploadFileResponse>(
                BuildPath(AccountsPath, entityId, FilesPath),
                SdkAuthorization(),
                accountsFileRequest,
                cancellationToken);
        }

        public async Task<FileDetailsResponse> RetrieveFile(
            string entityId,
            string fileId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("entityId", entityId, "fileId", fileId);
            return await ApiClient.Get<FileDetailsResponse>(
                BuildPath(AccountsPath, entityId, FilesPath, fileId),
                SdkAuthorization(),
                cancellationToken);
        }
    }
}