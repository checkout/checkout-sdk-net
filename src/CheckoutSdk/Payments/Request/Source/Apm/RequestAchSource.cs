using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestAchSource : AbstractRequestSource
    {
        public AccountType? AccountType { get; set; }

        public CountryCode? Country { get; set; }

        public string AccountNumber { get; set; }

        public string BankCode { get; set; }

        public AccountHolder AccountHolder { get; set; }

        public RequestAchSource() : base(PaymentSourceType.Ach)
        {
        }
    }
}