namespace Checkout.Payments.Four.Request
{
    public sealed class PaymentInstruction 
    {
        public string Purpose { get; set; }

        public string ChargeBearer { get; set; }

        public bool? Repair { get; set; }

        public InstructionScheme? Scheme { get; set; }

        public string QuoteId { get; set; }
               
    }
}