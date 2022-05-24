using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestPayPalSource : AbstractRequestSource
    {
        public string InvoiceNumber { get; set; }

        public string RecipientName { get; set; }

        public string LogoUrl { get; set; }

        public IDictionary<string, string> Stc { get; set; }

        public RequestPayPalSource() : base(PaymentSourceType.PayPal)
        {
        }
    }
}