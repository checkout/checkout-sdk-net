using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Apm.Previous.Klarna
{
    public class CreditSessionResponse : Resource
    {
        public string SessionId { get; set; }

        public string ClientToken { get; set; }

        public IList<PaymentMethodCategory> PaymentMethodCategories { get; set; }
    }
}