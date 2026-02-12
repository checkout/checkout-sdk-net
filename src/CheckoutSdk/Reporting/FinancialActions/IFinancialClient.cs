using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Financial
{
    public interface IFinancialClient
    {
        Task<FinancialActionsQueryResponse> Query(
            FinancialActionsQueryFilter query,
            CancellationToken cancellationToken = default);
    }
}
