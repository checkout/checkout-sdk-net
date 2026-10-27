namespace Checkout.Payments.Request
{
    /// <summary>
    /// Configuration of Downtime retries.
    /// </summary>
    [System.Obsolete("Use Checkout.Payments.Downtime instead. This type will be removed in the next major version.")]
    public class DowntimeRetryRequest
    {
        /// <summary>
        /// Indicates if Checkout.com retries the payment when it's declined due to issuer or acquirer downtime.
        /// [Required]
        /// </summary>
        public bool? Enabled { get; set; }
    }
}
