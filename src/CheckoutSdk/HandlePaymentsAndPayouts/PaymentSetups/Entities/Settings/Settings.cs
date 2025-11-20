namespace Checkout.Payments.Setups.Entities
{
    public class Settings
    {
        /// <summary>
        /// The URL to redirect the customer to after a successful payment setup
        /// </summary>
        public string SuccessUrl { get; set; }

        /// <summary>
        /// The URL to redirect the customer to after a failed payment setup
        /// </summary>
        public string FailureUrl { get; set; }
    }
}