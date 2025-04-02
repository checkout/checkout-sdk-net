using Checkout.Issuing.Transactions.Requests.Query;
using Checkout.Issuing.Transactions.Responses.Query;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Issuing
{
    public partial class IssuingClient
    {
        public Task<TransactionsListResponse> GetListTransactions(
            TransactionsQueryFilter query,
            CancellationToken cancellationToken = default
        )
        {
            CheckoutUtils.ValidateParams("query", query);
            return ApiClient.Query<TransactionsListResponse>(
                BuildPath(IssuingPath, TransactionsPath),
                SdkAuthorization(),
                query,
                cancellationToken);
        }

        public Task<TransactionSingleResponse> GetSingleTransaction(
            string transactionId,
            CancellationToken cancellationToken = default
        )
        {
            CheckoutUtils.ValidateParams("transactionId", transactionId);
            return ApiClient.Get<TransactionSingleResponse>(
                BuildPath(IssuingPath, TransactionsPath, transactionId),
                SdkAuthorization(),
                cancellationToken);
        }
    }
}