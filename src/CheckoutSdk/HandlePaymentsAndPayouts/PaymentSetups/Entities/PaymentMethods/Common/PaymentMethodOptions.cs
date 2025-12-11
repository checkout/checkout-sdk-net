namespace Checkout.Payments.Setups.Entities
{
    public class PaymentMethodOptions
    {
        /// <summary>
        /// Klarna SDK configuration options
        /// </summary>
        public PaymentMethodOption Sdk { get; set; }

        /// <summary>
        /// STC Pay full payment option configuration
        /// </summary>
        public PaymentMethodOption PayInFull { get; set; }

        /// <summary>
        /// Tabby installments payment option configuration
        /// </summary>
        public PaymentMethodOption Installments { get; set; }

        /// <summary>
        /// Bizum immediate payment option configuration
        /// </summary>
        public PaymentMethodOption PayNow { get; set; }
    }
}