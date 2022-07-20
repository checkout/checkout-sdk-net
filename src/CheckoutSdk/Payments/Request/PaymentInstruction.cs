namespace Checkout.Payments.Request
{
    public class PaymentInstruction
    {
        public string Purpose { get; set; }

        public string ChargeBearer { get; set; }

        public bool? Repair { get; set; }

        public InstructionScheme? Scheme { get; set; }

        public string QuoteId { get; set; }

        public bool? SkipExpiry { get; set; }

        public string FundsTransferType { get; set; }

        public string Mvv { get; set; }
    }
}