using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestSepaSource : AbstractRequestSource
    {
        public RequestSepaSource() : base(PaymentSourceType.Sepa)
        {
        }

        public CountryCode? Country { get; set; }

        public string AccountNumber { get; set; }

        public string BankCode { get; set; }

        public Currency? Currency { get; set; }

        public string MandateId { get; set; }

        public string DateOfSignature { get; set; }

        public AccountHolder AccountHolder { get; set; }
    }
}