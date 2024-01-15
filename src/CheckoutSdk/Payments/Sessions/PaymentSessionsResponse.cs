using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Payments.Sessions
{
    public class PaymentSessionsResponse : Resource
    {
        public string Id { get; set; }

        public long? Amount { get; set; }

        public string Locale { get; set; }

        public Currency Currency { get; set; }

        public CustomerResponse Customer { get; set; }

        public IList<PaymentMethods> PaymentMethods { get; set; }
    }
}