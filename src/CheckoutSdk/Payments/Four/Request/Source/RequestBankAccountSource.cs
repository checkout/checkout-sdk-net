using Checkout.Common;
using Checkout.Common.Four;

namespace Checkout.Payments.Four.Request.Source
{
    public class RequestBankAccountSource : AbstractRequestSource
    {
        public string PaymentMethod { get; set; }

        public AccountType? AccountType { get; set; }

        public CountryCode? Country { get; set; }

        public string AccountNumber { get; set; }

        public string BankCode { get; set; }

        public AccountHolder AccountHolder { get; set; }

        public RequestBankAccountSource() : base(PaymentSourceType.BankAccount)
        {
        }
    }
}