using Checkout.Issuing.Transactions.Requests.Query;
using Checkout.Issuing.Transactions.Responses;
using Checkout.Issuing.Transactions.Responses.Query;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Issuing.Transactions
{
    public class TransactionsIntegrationTest : IssuingCommon
    {
        [Fact]
        private async Task ShouldGetListTransactions()
        {
            TransactionsQueryFilter query = new TransactionsQueryFilter();
            TransactionsListResponse response = await Api.IssuingClient().GetListTransactions(query);

            response.ShouldNotBeNull();
            response.Limit.ShouldNotBeNull();
            response.Skip.ShouldNotBeNull();
            response.TotalCount.ShouldNotBeNull();
            response.Data.ShouldNotBeNull();
            response.Data.Count.ShouldBePositive();
        }

        [Fact]
        private async Task ShouldGetSingleTransaction()
        {
            TransactionsQueryFilter query = new TransactionsQueryFilter();
            TransactionsListResponse transactions = await Api.IssuingClient().GetListTransactions(query);
            
            TransactionSingleResponse response = await Api.IssuingClient().GetSingleTransaction(transactions.Data[0].Id);

            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNullOrEmpty();
            response.CreatedOn.ShouldNotBeNull();
            response.Status.ShouldNotBeNull();
            response.TransactionType.ShouldNotBeNull();
            response.Client.ShouldNotBeNull();
            response.Entity.ShouldNotBeNull();
            response.Card.ShouldNotBeNull();
            response.Cardholder.ShouldNotBeNull();
            response.Amounts.ShouldNotBeNull();
            response.Merchant.ShouldNotBeNull();
            response.Messages.ShouldNotBeNull();
            response.Links.ShouldNotBeNull();
        }
    }
}