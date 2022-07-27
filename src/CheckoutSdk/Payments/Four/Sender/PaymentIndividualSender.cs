using Checkout.Common;
using Checkout.Common.Four;

namespace Checkout.Payments.Four.Sender
{
    public class PaymentIndividualSender : PaymentSender
    {
        public PaymentIndividualSender() : base(PaymentSenderType.Individual)
        {
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Address Address { get; set; }

        public Identification Identification { get; set; }
        
        public string MiddleName { get; set; }
        
        public string ReferenceType { get; set; }
        
        public string DateOfBirth { get; set; }
        
        public SourceOfFunds? SourceOfFunds { get; set; }
        
        public CountryCode? CountryOfBirth { get; set; }
        
        public CountryCode? Nationality { get; set; }
    }
}