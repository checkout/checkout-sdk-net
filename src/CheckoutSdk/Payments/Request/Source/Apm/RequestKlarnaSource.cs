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

        public bool? AutoCapture { get; set; }

        public IDictionary<string, string> BillingAddress { get; set; }

        public IDictionary<string, string> ShippingAddress { get; set; }

        public long? TaxAmount { get; set; }

        public IDictionary<string, string> Products { get; set; }

        public IDictionary<string, string> Customer { get; set; }

        public string MerchantReference1 { get; set; }

        public string MerchantReference2 { get; set; }

        public string MerchantData { get; set; }

        public string Attachment { get; set; }
    }
}