using Checkout.Issuing.Transactions.Responses;
using Checkout.Issuing.Transactions.Responses.Query;
using System;

namespace Checkout.Issuing.Transactions.Requests.Query
{
    public class TransactionsQueryFilter
    {
        public int? Limit { get; set; }

        public int? Skip { get; set; }

        public string CardholderId { get; set; }

        public string CardId { get; set; }

        public TransactionStatusType? Status { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }
    }
}