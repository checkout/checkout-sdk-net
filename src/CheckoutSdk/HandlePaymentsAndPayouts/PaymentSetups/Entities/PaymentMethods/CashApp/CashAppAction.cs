namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The next available action for the Cash App payment method (response only).
    /// </summary>
    public class CashAppAction
    {
        /// <summary>
        /// The type of action.
        /// [Optional]
        /// readOnly
        /// Enum: "redirect"
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The URL to redirect the customer to so they can authorize the payment with Cash App.
        /// [Optional]
        /// readOnly
        /// Format: uri
        /// </summary>
        public string RedirectUrl { get; set; }
    }
}
