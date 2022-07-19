using Checkout.Accounts.Payout.Request;
using Checkout.Accounts.Payout.Response;
using Checkout.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Accounts
{
    public interface IAccountsClient
    {
        Task<OnboardEntityResponse> CreateEntity(OnboardEntityRequest entityRequest,
            CancellationToken cancellationToken = default);

        Task<OnboardEntityDetailsResponse> GetEntity(string entityId, CancellationToken cancellationToken = default);

        Task<OnboardEntityResponse> UpdateEntity(string entityId, OnboardEntityRequest entityRequest,
            CancellationToken cancellationToken = default);

        Task<EmptyResponse> CreatePaymentInstrument(string entityId, AccountsPaymentInstrument accountsPaymentInstrument,
            CancellationToken cancellationToken = default);

        Task<IdResponse> SubmitFile(AccountsFileRequest accountsFileRequest,
            CancellationToken cancellationToken = default);

        Task<EmptyResponse> UpdatePayoutSchedule(string entityId, Currency currency,
            UpdateScheduleRequest updateScheduleRequest, CancellationToken cancellationToken = default);

        Task<GetScheduleResponse>
            RetrievePayoutSchedule(string entityId, CancellationToken cancellationToken = default);
    }
}