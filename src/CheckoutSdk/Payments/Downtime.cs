namespace Checkout.Payments
{
    /// <summary>
    /// Configuration of Downtime retries.
    /// </summary>
    public class Downtime
    {
        /// <summary>
        /// Indicates if Checkout.com retries the payment when it's declined due to issuer or acquirer downtime.
        /// [Required]
        /// </summary>
        public bool Enabled { get; set; }
    }
}
