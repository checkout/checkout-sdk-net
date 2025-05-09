﻿using Checkout.Accounts.Entities.Request;
using Checkout.Accounts.Entities.Response;
using Checkout.Accounts.Payout.Request;
using Checkout.Accounts.Payout.Response;
using Checkout.Common;
using Checkout.Files;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Accounts
{
    public interface IAccountsClient
    { 
        Task<OnboardEntityResponse> CreateEntity(
            OnboardEntityRequest entityRequest,
            CancellationToken cancellationToken = default);
        
        Task<OnboardSubEntityDetailsResponse> GetSubEntityMembers(
            string entityId,
            CancellationToken cancellationToken = default);
        
        Task<OnboardSubEntityResponse> ReinviteSubEntityMember(
            string entityId,
            string userId,
            OnboardSubEntityRequest subEntityRequest,
            CancellationToken cancellationToken = default);

        Task<OnboardEntityDetailsResponse> GetEntity(
            string entityId,
            CancellationToken cancellationToken = default);

        Task<OnboardEntityResponse> UpdateEntity(
            string entityId,
            OnboardEntityRequest entityRequest,
            CancellationToken cancellationToken = default);

        [Obsolete("Use CreatePaymentInstrument for PaymentInstrumentRequest instead", false)]
        Task<EmptyResponse> CreatePaymentInstrument(
            string entityId,
            AccountsPaymentInstrument accountsPaymentInstrument,
            CancellationToken cancellationToken = default);

        Task<IdResponse> CreatePaymentInstrument(
            string entityId,
            PaymentInstrumentRequest paymentInstrumentRequest,
            CancellationToken cancellationToken = default);
        
        Task<PaymentInstrumentDetailsResponse> RetrievePaymentInstrumentDetails(
            string entityId,
            string paymentInstrumentId,
            CancellationToken cancellationToken = default);

        Task<IdResponse> UpdatePaymentInstrument(
            string entityId,
            string instrumentId,
            UpdatePaymentInstrumentRequest updatePaymentInstrumentRequest,
            CancellationToken cancellationToken = default);

        Task<PaymentInstrumentQueryResponse> QueryPaymentInstruments(
            string entityId,
            PaymentInstrumentsQuery query = null,
            CancellationToken cancellationToken = default);

        Task<GetScheduleResponse> RetrievePayoutSchedule(
            string entityId,
            CancellationToken cancellationToken = default);

        Task<EmptyResponse> UpdatePayoutSchedule(
            string entityId,
            Currency currency,
            UpdateScheduleRequest updateScheduleRequest,
            CancellationToken cancellationToken = default);
        
        Task<IdResponse> SubmitFile(
            AccountsFileRequest accountsFileRequest,
            CancellationToken cancellationToken = default);
        
        Task<UploadFileResponse> UploadFile(
            string entityId,
            AccountsFileRequest accountsFileRequest,
            CancellationToken cancellationToken = default);
        
        Task<FileDetailsResponse> RetrieveFile(
            string entityId,
            string fileId,
            CancellationToken cancellationToken = default);
    }
}