﻿using Checkout.Accounts.Payout.Request;
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
        private const string InstrumentPath = "instruments";
        private const string PayoutSchedulePath = "payout-schedules";

        public AccountsClient(
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
                BuildPath(AccountsPath, EntitiesPath),
                SdkAuthorization(SdkAuthorizationType.OAuth),
                entityRequest,
                cancellationToken);
        }

        public async Task<OnboardEntityDetailsResponse> GetEntity(string entityId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("entityId", entityId);
            return await ApiClient.Get<OnboardEntityDetailsResponse>(
                BuildPath(AccountsPath, EntitiesPath, entityId),
                SdkAuthorization(SdkAuthorizationType.OAuth),
                cancellationToken);
        }

        public async Task<OnboardEntityResponse> UpdateEntity(string entityId, OnboardEntityRequest entityRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("entityRequest", entityRequest, "entityId", entityId);
            return await ApiClient.Put<OnboardEntityResponse>(
                BuildPath(AccountsPath, EntitiesPath, entityId),
                SdkAuthorization(SdkAuthorizationType.OAuth),
                entityRequest,
                cancellationToken);
        }

        public async Task<EmptyResponse> CreatePaymentInstrument(string entityId,
            AccountsPaymentInstrument accountsPaymentInstrument, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("accountsPaymentInstrument", accountsPaymentInstrument, "entityId",
                entityId);
            return await ApiClient.Post<EmptyResponse>(
                BuildPath(AccountsPath, EntitiesPath, entityId, InstrumentPath),
                SdkAuthorization(SdkAuthorizationType.OAuth),
                accountsPaymentInstrument,
                cancellationToken);
        }

        public async Task<IdResponse> SubmitFile(AccountsFileRequest accountsFileRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("accountsFileRequest", accountsFileRequest,
                "accountsFileRequest.purpose", accountsFileRequest.Purpose);
            return await SubmitFileToFilesApi(accountsFileRequest.File, accountsFileRequest.Purpose.Value,
                cancellationToken);
        }

        public async Task<EmptyResponse> UpdatePayoutSchedule(string entityId, Currency currency,
            UpdateScheduleRequest updateScheduleRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("entityId", entityId, "currency", currency, "updateScheduleRequest",
                updateScheduleRequest);
            return await ApiClient.Put<EmptyResponse>(
                BuildPath(AccountsPath, EntitiesPath, entityId, PayoutSchedulePath),
                SdkAuthorization(SdkAuthorizationType.OAuth),
                new Dictionary<Currency, UpdateScheduleRequest>() {{currency, updateScheduleRequest}},
                cancellationToken);
        }

        public async Task<GetScheduleResponse> RetrievePayoutSchedule(string entityId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("entityId", entityId);
            return await ApiClient.Get<GetScheduleResponse>(
                BuildPath(AccountsPath, EntitiesPath, entityId, PayoutSchedulePath),
                SdkAuthorization(SdkAuthorizationType.OAuth),
                cancellationToken);
        }
    }
}