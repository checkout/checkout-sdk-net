namespace Checkout.Payments.Setups.Entities
{
    public class Klarna : PaymentMethodBase
    {
        /// <summary>
        /// The account holder information for Klarna payments
        /// </summary>
        public KlarnaAccountHolder AccountHolder { get; set; }

        /// <summary>
        /// The next available action for the Klarna payment method (response only).
        /// Contains client_token and session_id for the Klarna SDK.
        /// </summary>
        public PaymentMethodAction Action { get; set; }

        /// <summary>
        /// Payment method options specific to Klarna
        /// </summary>
        public PaymentMethodOptions PaymentMethodOptions { get; set; }
    }
}