using System;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The Klarna payment method's details and configuration.
    /// </summary>
    public class Klarna : PaymentMethodBase
    {
        /// <summary>
        /// The account holder details returned by Klarna after the shopper completes verification.
        /// [Optional]
        /// readOnly
        /// </summary>
        public KlarnaAccountHolder AccountHolder { get; set; }

        /// <summary>
        /// The next available action for the payment method. For Klarna the type is "sdk", and the action
        /// carries the client_token and session_id to use with the Klarna SDK.
        /// [Optional]
        /// readOnly
        /// </summary>
        public PaymentMethodAction Action { get; set; }

        /// <summary>
        /// The IBAN of the account used to complete Klarna verification.
        /// [Optional]
        /// readOnly
        /// </summary>
        public string Iban { get; set; }

        /// <summary>
        /// The payment method options.
        /// [Optional]
        /// Deprecated: not part of the API spec; the API no longer sends it and ignores it on requests.
        /// </summary>
        [Obsolete("Not part of the Payment Setups API specification; the API no longer sends it and ignores it on requests. Use Action instead. Will be removed in the next major version.")]
        public PaymentMethodOptions PaymentMethodOptions { get; set; }
    }
}
