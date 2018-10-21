namespace Checkout.Payments
{
    /// <summary>
    /// Defines an Alternative Payment source specified in a payment request.
    /// </summary>
    public interface IAlternativePaymentRequestSource : IRequestSource
    {
        /// <summary>
        /// Gets the BIC (8 or 11-digits).
        /// </summary>
        string Bic { get; }

        /// <summary>
        /// Gets the IBAN without whitespaces.
        /// </summary>
        string Iban { get; }

        /// <summary>
        /// Gets the info fields (e.g. giropay).
        /// </summary>
        object[] InfoFields { get; }

        /// <summary>
        /// Gets the issuer id of the payment.
        /// </summary>
        string Issuer_id { get; }

        /// <summary>
        /// Gets the purpose of the payment.
        /// </summary>
        string Purpose { get; }
    }
}
