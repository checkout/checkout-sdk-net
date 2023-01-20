using Checkout.Common;

namespace Checkout.Payments.Sender
{
    public class PaymentIndividualSender : PaymentSender
    {
        public PaymentIndividualSender() : base(PaymentSenderType.Individual)
        {
        }

        public string FirstName { get; set; }
        
        public string MiddleName { get; set; }

        public string LastName { get; set; }
        
        public string Dob { get; set; }

        public Address Address { get; set; }

        public AccountHolderIdentification AccountHolderIdentification { get; set; }
        
        public string ReferenceType { get; set; }

        public string DateOfBirth { get; set; }

        public SourceOfFunds? SourceOfFunds { get; set; }

        public CountryCode? CountryOfBirth { get; set; }

        public CountryCode? Nationality { get; set; }
    }
}