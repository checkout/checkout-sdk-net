using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Apm.Previous.Klarna
{
    public class CreditSessionRequest
    {
        public CountryCode? PurchaseCountry { get; set; }

        public Currency? Currency { get; set; }

        public string Locale { get; set; }

        public long? Amount { get; set; }

        public int? TaxAmount { get; set; }

        public IList<KlarnaProduct> Products { get; set; }
    }
}