namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The TNG payment method's details and configuration.
    /// </summary>
    public class Tng : PaymentSetupPaymentMethod
    {
        /// <summary>
        /// The client-side terminal type. Indicates whether the customer is using a PC browser, mobile
        /// browser, or mobile application.
        /// [Optional]
        /// </summary>
        public PaymentSetupTerminalType? TerminalType { get; set; }

        /// <summary>
        /// The customer's operating system type. Required when terminal_type is not web.
        /// [Optional]
        /// </summary>
        public PaymentSetupOsType? OsType { get; set; }
    }
}
