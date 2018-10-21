namespace Checkout.Payments
{
    /// <summary>
    /// Represents an Alternative Payment source for a payment request.
    /// </summary>
    public class AlternativePaymentSource : IAlternativePaymentRequestSource
    {
        /// <summary>
        /// Creates a new <see cref="AlternativePaymentSource"/> instance.
        /// </summary>
        public AlternativePaymentSource() { }

        /// <summary>
        /// Gets or sets the type of source.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the BIC (8 or 11-digits).
        /// </summary>
        public string Bic { get; set; }

        /// <summary>
        /// Gets or sets the IBAN without whitespaces.
        /// </summary>
        public string Iban { get; set; }

        /// <summary>
        /// Gets or sets info fields (e.g. giropay).
        /// </summary>
        public object[] InfoFields { get; set; }

        /// <summary>
        /// Gets or sets the issuer id of the payment.
        /// </summary>
        public string Issuer_id { get; set; }

        /// <summary>
        /// Gets or sets the purpose of the payment.
        /// </summary>
        public string Purpose { get; set; }
    }
}
