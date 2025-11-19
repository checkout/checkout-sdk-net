namespace Checkout.Payments.Setups.Entities
{
    public class Klarna : PaymentMethodBase
    {
        /// <summary>
        /// The account holder information for Klarna payments
        /// </summary>
        public KlarnaAccountHolder AccountHolder { get; set; }

        /// <summary>
        /// Payment method options specific to Klarna
        /// </summary>
        public PaymentMethodOptions PaymentMethodOptions { get; set; }
    }
}