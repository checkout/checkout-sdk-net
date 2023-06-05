using Checkout.Common;

namespace Checkout.Issuing.Testing.Requests
{
    public class TransactionSimulation
    {
        public TransactionType? Type { get; set; }

        public int? Amount { get; set; }

        public Currency? Currency { get; set; }

        public TransactionMerchant Merchant { get; set; }

        public TransactionAuthorizationType? Transaction { get; set; }
    }
}