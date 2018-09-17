using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Checkout.Payments
{
    public class PaymentRequest<TSource> where TSource : IPaymentSource
    {
        public PaymentRequest(TSource source, string currency, int? amount)
        {
            Source = source;
            Amount = amount;
            Currency = currency;
        }

        public int? Amount { get; }
        public string Currency { get; }
        public TSource Source { get; }
        public PaymentType? PaymentType { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public bool? Capture { get; set; }
        public DateTimeOffset? CaptureOn { get; set; }
        public Customer Customer { get; set; }
        public BillingDescriptor BillingDescriptor { get; set; }

        [JsonProperty(PropertyName = "3ds")]
        public bool? ThreeDs { get; set; }
        public bool? AttemptN3d { get; set; }
        public bool? SkipRiskCheck { get; set; }
        public string SuccessUrl { get; set; }
        public string FailureUrl { get; set; }
        public string PaymentIp { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
        public IEnumerable<PaymentDestination> Destinations { get; set; }
        public PaymentRecipient Recipient { get; set; }
        public ShippingDetails Shipping { get; set; }
    }
}