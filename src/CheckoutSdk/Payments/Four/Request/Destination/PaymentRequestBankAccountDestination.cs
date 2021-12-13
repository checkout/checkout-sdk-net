using Checkout.Common;
using Checkout.Common.Four;

namespace Checkout.Payments.Four.Request.Destination
{
    public sealed class PaymentBankAccountDestination : PaymentRequestDestination
    {
        public PaymentBankAccountDestination() : base(PaymentDestinationType.BankAccount)
        {
        }

        public AccountType? AccountType { get; set; }

        public string AccountNumber { get; set; }

        public string BankCode { get; set; }

        public string Iban { get; set; }

        public string Bban { get; set; }

        public string SwiftBic { get; set; }

        public CountryCode? Country { get; set; }

        public AccountHolder AccountHolder { get; set; }

        public BankDetails Bank { get; set; }
               
    }
}