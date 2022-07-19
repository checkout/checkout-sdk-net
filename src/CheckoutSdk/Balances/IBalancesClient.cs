using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Balances
{
    public interface IBalancesClient
    {
        Task<BalancesResponse> RetrieveEntityBalances(string entityId, BalancesQuery balancesQuery,
            CancellationToken cancellationToken = default);
    }
}