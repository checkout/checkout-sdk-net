namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The Cash App Pay payment method's details and configuration. To authorize the payment, send
    /// the customer to the redirect URL returned in the action. The customer device client is
    /// required when using this payment method.
    /// </summary>
    public class CashApp : PaymentMethodBase
    {
        /// <summary>
        /// Indicates whether the customer consents to share their Cash App customer profile with
        /// Checkout.com. When enabled, the customer profile is returned once, after the customer
        /// authorizes the payment.
        /// [Optional]
        /// </summary>
        public bool? CustomerProfileSharing { get; set; }

        /// <summary>
        /// The customer's Cash App profile that they consented to share. Included in the response when
        /// customer_profile_sharing is enabled. Cash App releases this profile only once: it is present
        /// in the first successful response when you get the payment setup after the customer
        /// authorizes the payment, and every subsequent response omits it, so store it on first read.
        /// [Optional]
        /// readOnly
        /// </summary>
        public CashAppCustomerProfile CustomerProfile { get; set; }

        /// <summary>
        /// A reference for the Cash App Pay transaction, returned by the provider.
        /// [Optional]
        /// readOnly
        /// max 80 characters
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// The next available action for the payment method. When its type is redirect, send the
        /// customer to its redirect URL to authorize the payment with Cash App.
        /// [Optional]
        /// readOnly
        /// </summary>
        public CashAppAction Action { get; set; }
    }
}
