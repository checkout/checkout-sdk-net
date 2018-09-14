using System;

namespace Checkout.Payments
{
    public class GetPaymentResponse
    {
        public string Id { get; set; }
        public DateTime RequestedOn { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string PaymentType { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}