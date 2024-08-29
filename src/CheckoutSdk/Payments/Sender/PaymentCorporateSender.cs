using Checkout.Common;

namespace Checkout.Payments.Sender
{
    public class PaymentCorporateSender : PaymentSender
    {
        public PaymentCorporateSender() : base(PaymentSenderType.Corporate)
        {
        }

        public string CompanyName { get; set; }

        public Address Address { get; set; }
        
        public string ReferenceType { get; set; }
        
        public SourceOfFunds? SourceOfFunds { get; set; }
    }
}