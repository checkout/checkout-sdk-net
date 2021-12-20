using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestKlarnaSource : AbstractRequestSource
    {
        public RequestKlarnaSource() : base(PaymentSourceType.Klarna)
        {
        }

        public string AuthorizationToken { get; set; }

        public string Locale { get; set; }

        public CountryCode? PurchaseCountry { get; set; }

        public long? TaxAmount { get; set; }

        public Address BillingAddress { get; set; }

        public KlarnaCustomer Customer { get; set; }

        public IList<KlarnaCustomer> Products { get; set; }
    }
}