namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The Swish payment method's details and configuration.
    /// </summary>
    public class Swish : PaymentSetupPaymentMethod
    {
        /// <summary>
        /// A description that appears on the customer's billing statement.
        /// [Optional]
        /// </summary>
        public string BillingDescriptor { get; set; }

        /// <summary>
        /// The account holder's details.
        /// [Optional]
        /// </summary>
        public PaymentSetupSwishAccountHolder AccountHolder { get; set; }
    }
}
