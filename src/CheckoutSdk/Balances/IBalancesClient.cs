using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Balances
{
    public interface IBalancesClient
    {
        /// <summary>
        /// Retrieves the balances for each sub-account belonging to an entity.
        /// </summary>
        /// <param name="entityId">The ID of the entity.</param>
        /// <param name="balancesQuery">The query filter.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task representing the balances response.</returns>
        Task<BalancesResponse> RetrieveEntityBalances(string entityId, BalancesQuery balancesQuery,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves the bank details required to top up a sub-account, along with the payment
        /// reference that attributes an incoming payment to that sub-account.
        /// Note: The sub-account is referred to as currency account in the API.
        /// </summary>
        /// <param name="entityId">
        /// The ID of the entity that owns the sub-account, or of an entity above it in your
        /// hierarchy. A platform can use its own entity ID to reach the sub-accounts of any
        /// entity beneath it.
        /// </param>
        /// <param name="currencyAccountId">The ID of the sub-account to retrieve top-up instructions for.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task representing the top-up instructions response.</returns>
        Task<TopUpInstructionsResponse> RetrieveTopUpInstructions(string entityId, string currencyAccountId,
            CancellationToken cancellationToken = default);
    }
}
