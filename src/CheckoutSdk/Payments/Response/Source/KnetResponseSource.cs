using Checkout.Common;

namespace Checkout.Payments.Response.Source
{
    public class KnetResponseSource : AbstractResponseSource, IResponseSource
    {
        public string Language { get; set; }

        public string UserDefinedField1 { get; set; }

        public string UserDefinedField2 { get; set; }

        public string UserDefinedField3 { get; set; }

        public string UserDefinedField4 { get; set; }

        public string UserDefinedField5 { get; set; }

        public string CardToken { get; set; }

        public string Ptlf { get; set; }

        public string KnetPaymentId { get; set; }

        public string KnetResult { get; set; }

        public string InquiryResult { get; set; }

        public string BankReference { get; set; }

        public string KnetTransactionId { get; set; }

        public string AuthCode { get; set; }

        public string AuthResponseCode { get; set; }

        public string PostDate { get; set; }

        public string Avr { get; set; }

        public string Error { get; set; }

        public string ErrorText { get; set; }

        public new PaymentSourceType? Type()
        {
            return base.Type;
        }
    }
}