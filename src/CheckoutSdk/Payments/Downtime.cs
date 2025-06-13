namespace Checkout.Payments
{
    public class Downtime
    {
        /// <summary>
        /// Indicates if Checkout.com retries the payment when it's declined due to issuer or acquirer downtime
        /// (Required)
        /// </summary>
        public bool Enabled { get; set; }
    }
}