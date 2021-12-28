namespace Checkout.Payments.Four.Sender
{
    public class Identification
    {
        public IdentificationType? Type { get; set; }

        public string Number { get; set; }

        public string IssuingCountry { get; set; }
    }
}