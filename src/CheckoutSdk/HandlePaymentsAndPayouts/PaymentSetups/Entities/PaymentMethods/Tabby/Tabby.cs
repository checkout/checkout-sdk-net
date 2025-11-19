namespace Checkout.Payments.Setups.Entities
{
    public class Tabby : PaymentMethodBase
    {
        /// <summary>
        /// Payment method options specific to Tabby
        /// </summary>
        public PaymentMethodOptions PaymentMethodOptions { get; set; }
    }
}