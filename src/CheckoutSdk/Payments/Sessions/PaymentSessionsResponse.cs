using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Payments.Sessions
{
    public class PaymentSessionsResponse : Resource
    {
        public string Id { get; set; }

        public string PaymentSessionToken { get; set; }
    }
}