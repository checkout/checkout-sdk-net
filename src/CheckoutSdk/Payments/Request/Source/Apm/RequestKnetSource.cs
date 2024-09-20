using Checkout.Common;
using Checkout.Tokens;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestKnetSource : AbstractRequestSource
    {
        public string Language { get; set; }

        public string UserDefinedField1 { get; set; }

        public string UserDefinedField2 { get; set; }

        public string UserDefinedField3 { get; set; }

        public string UserDefinedField4 { get; set; }

        public string UserDefinedField5 { get; set; }

        public string CardToken { get; set; }

        public string Ptlf { get; set; }
        
        public string TokenType { get; set; }

        public ApplePayTokenData TokenData { get; set; }

        public PaymentMethodDetails PaymentMethodDetails { get; set; }

        public RequestKnetSource() : base(PaymentSourceType.KNet)
        {
        }
    }
}