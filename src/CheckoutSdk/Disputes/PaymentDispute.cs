using Checkout.Common;
using System;

namespace Checkout.Disputes
{
    public class PaymentDispute
    {
        public string Id { get; set; }

        public string ActionId { get; set; }

        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public string Method { get; set; }

        public string Arn { get; set; }

        public DateTime? ProcessedOn { get; set; }
    }
}