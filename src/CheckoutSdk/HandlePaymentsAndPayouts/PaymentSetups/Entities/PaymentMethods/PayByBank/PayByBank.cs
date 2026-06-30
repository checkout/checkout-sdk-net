namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The Pay by Bank (Open Banking) payment method's details and configuration.
    /// </summary>
    public class PayByBank : PaymentMethodBase
    {
        /// <summary>
        /// The identifier of the bank the customer has selected for the payment.
        /// [Optional]
        /// </summary>
        public string BankId { get; set; }

        /// <summary>
        /// The next available action for the payment method (response only).
        /// [Optional]
        /// </summary>
        public PayByBankAction Action { get; set; }
    }
}
