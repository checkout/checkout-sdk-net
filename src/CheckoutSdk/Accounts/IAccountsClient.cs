using Checkout.Accounts.Entities.Request;
using Checkout.Accounts.Entities.Response;
using Checkout.Accounts.Payout.Request;
using Checkout.Accounts.Payout.Response;
using Checkout.Accounts.ReserveRules;
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
            CancellationToken cancellationToken = default,
            string schemaVersion = "3.0");
        
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
            CancellationToken cancellationToken = default,
            string schemaVersion = "3.0");

        Task<OnboardEntityResponse> UpdateEntity(
            string entityId,
            OnboardEntityRequest entityRequest,
            CancellationToken cancellationToken = default,
            string schemaVersion = "3.0");

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
        
        /// <summary>
        /// Uploads a file to the Files API (POST /files on the Files host), as a multipart request.
        /// The returned ID is what document <c>front</c> and <c>back</c> properties take.
        /// </summary>
        /// <param name="accountsFileRequest">The path to the file, its content type, and its purpose.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The ID of the uploaded file.</returns>
        Task<IdResponse> SubmitFile(
            AccountsFileRequest accountsFileRequest,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a file upload for a sub-entity (POST /entities/{entityId}/files on the Files host).
        /// The response carries the file ID and an upload link; the file content itself is sent to
        /// that link, not in this request.
        /// The endpoint takes <see cref="AccountsFileRequest.Purpose"/> only; leave
        /// <see cref="Common.AbstractFileRequest.File"/> and
        /// <see cref="Common.AbstractFileRequest.ContentType"/> unset.
        /// <see cref="AccountsFileRequest.Purpose"/> is required by the API, and because it is a
        /// non-nullable enum an unset value is sent as its default,
        /// <see cref="AccountsFilePurpose.AdditionalDocument"/>: always set it explicitly.
        /// </summary>
        /// <param name="entityId">The ID of the sub-entity.</param>
        /// <param name="accountsFileRequest">The purpose of the file upload.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The file ID, the maximum size allowed, the MIME types allowed for the purpose, and the
        /// upload link.</returns>
        Task<UploadFileResponse> UploadFile(
            string entityId,
            AccountsFileRequest accountsFileRequest,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves the details of a sub-entity's file (GET /entities/{entityId}/files/{fileId} on the
        /// Files host).
        /// </summary>
        /// <param name="entityId">The ID of the sub-entity.</param>
        /// <param name="fileId">The ID of the file.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The file's status, size, MIME type, upload date and purpose.</returns>
        Task<FileDetailsResponse> RetrieveFile(
            string entityId,
            string fileId,
            CancellationToken cancellationToken = default);

        Task<ReserveRuleIdResponse> CreateReserveRule(
            string entityId,
            ReserveRuleRequest reserveRuleRequest,
            CancellationToken cancellationToken = default);

        Task<ReserveRulesResponse> GetReserveRules(
            string entityId,
            CancellationToken cancellationToken = default);

        Task<ReserveRuleResponse> GetReserveRuleDetails(
            string entityId,
            string reserveRuleId,
            CancellationToken cancellationToken = default);

        Task<ReserveRuleIdResponse> UpdateReserveRule(
            string entityId,
            string reserveRuleId,
            string etag,
            ReserveRuleRequest reserveRuleRequest,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve the list of pending requirements that the sub-entity must resolve.
        /// </summary>
        Task<EntityRequirementListResponse> GetEntityRequirements(
            string entityId,
            CancellationToken cancellationToken = default,
            string schemaVersion = "3.0");

        /// <summary>
        /// Retrieve detailed information for a single requirement.
        /// </summary>
        Task<EntityRequirementDetailsResponse> GetEntityRequirementDetails(
            string entityId,
            string requirementId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Submit a response to resolve a requirement.
        /// </summary>
        Task<EntityRequirementUpdateResponse> ResolveEntityRequirement(
            string entityId,
            string requirementId,
            EntityRequirementUpdateRequest updateRequest,
            CancellationToken cancellationToken = default);
    }
}