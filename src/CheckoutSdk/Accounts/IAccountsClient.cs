using Checkout.Accounts.Entities.Request;
using Checkout.Accounts.Entities.Requirements;
using Checkout.Accounts.Entities.Response;
using Checkout.Accounts.Payout.Request;
using Checkout.Accounts.Payout.Response;
using Checkout.Accounts.ReserveRules;
using Checkout.Accounts.Simulator;
using Checkout.Common;
using Checkout.Files;
using System;
using System.Collections.Generic;
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
        Task<EntityRequirementListResponse> ListEntityRequirements(
            string entityId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve detailed information for a single requirement.
        /// </summary>
        Task<EntityRequirementDetails> GetEntityRequirement(
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

        /// <summary>
        /// Sandbox only. Marks the specified requirement fields as due on an entity.
        /// </summary>
        Task<SimulatorSetRequirementsDueResponse> SimulatorSetRequirementsDue(
            string entityId,
            SimulatorSetRequirementsDueRequest request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Sandbox only. Executes a pre-defined scenario against an entity.
        /// </summary>
        Task<SimulatorRunScenarioResponse> SimulatorRunScenario(
            string entityId,
            string scenarioId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Sandbox only. Forces the entity to the specified status.
        /// </summary>
        Task<SimulatorSetStatusResponse> SimulatorSetStatus(
            string entityId,
            SimulatorSetStatusRequest request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Sandbox only. Returns all requirement fields that can be set as due on an entity.
        /// </summary>
        Task<ItemsResponse<SimulatorAvailableRequirement>> SimulatorListAvailableRequirements(
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Sandbox only. Returns all pre-defined scenarios available.
        /// </summary>
        Task<ItemsResponse<SimulatorScenario>> SimulatorListScenarios(
            CancellationToken cancellationToken = default);
    }
}