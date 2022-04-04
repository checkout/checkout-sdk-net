using Checkout.Common;
using Checkout.Payments.Request.Destination;
using System;
using System.Collections.Generic;

namespace Checkout.Payments.Request
{
    public class PayoutRequest
    {
        public PaymentRequestDestination Destination { get; set; }

        public long? Amount { get; set; }

        public FundTransferType? FundTransferType { get; set; }

        public Currency? Currency { get; set; }

        public PaymentType? PaymentType { get; set; }

        public string Reference { get; set; }

        public string Description { get; set; }

        public bool? Capture { get; set; } = false;

        public DateTime? CaptureOn { get; set; }

        public CustomerRequest Customer { get; set; }

        public BillingDescriptor BillingDescriptor { get; set; }

        public ShippingDetails Shipping { get; set; }

        public string PreviousPaymentId { get; set; }

        public RiskRequest Risk { get; set; }

        public string SuccessUrl { get; set; }

        public string FailureUrl { get; set; }

        public string PaymentIp { get; set; }

        public PaymentRecipient Recipient { get; set; }

        public Purpose? Purpose { get; set; }

        public IDictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();

        public IDictionary<string, object> Processing { get; set; } = new Dictionary<string, object>();
    }
}