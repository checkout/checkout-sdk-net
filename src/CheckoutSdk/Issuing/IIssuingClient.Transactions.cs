using Checkout.Issuing.Transactions.Requests.Query;
using Checkout.Issuing.Transactions.Responses.Query;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Issuing
{
    public partial interface IIssuingClient
    {
        Task<TransactionsListResponse> GetListTransactions(TransactionsQueryFilter query,
            CancellationToken cancellationToken = default);

        Task<TransactionSingleResponse> GetSingleTransaction(string transactionId,
            CancellationToken cancellationToken = default);
    }
}