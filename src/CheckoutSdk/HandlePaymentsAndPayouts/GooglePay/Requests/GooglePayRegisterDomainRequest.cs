namespace Checkout.HandlePaymentsAndPayouts.GooglePay.Requests
{
    /// <summary>
    /// Request to register a web domain for an actively enrolled Google Pay entity.
    /// Required (API): webDomain.
    /// </summary>
    public class GooglePayRegisterDomainRequest
    {
        /// <summary>
        /// The web domain to register for an actively enrolled entity.
        /// [Required]
        /// Format: hostname
        /// </summary>
        public string WebDomain { get; set; }
    }
}
