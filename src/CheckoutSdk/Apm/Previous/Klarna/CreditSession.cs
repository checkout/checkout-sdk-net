using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Apm.Previous.Klarna
{
    public class CreditSession : Resource
    {
        public string ClientToken { get; set; }

        public string PurchaseCountry { get; set; }

        public string Currency { get; set; }

        public string Locale { get; set; }

        public long? Amount { get; set; }

        public int? TaxAmount { get; set; }

        public IList<KlarnaProduct> Products { get; set; }
    }
}