namespace Checkout.Payments.Setups.Entities
{
    public class Bizum : PaymentMethodBase
    {
        /// <summary>
        /// Payment method options specific to Bizum
        /// </summary>
        public PaymentMethodOptions PaymentMethodOptions { get; set; }
    }
}