namespace Checkout.Payments.Setups.Entities
{
    public class Settings
    {
        /// <summary>
        /// The URL to redirect the customer to, if the payment is successful.
        /// For payment methods with a redirect, this value overrides the default success redirect URL configured on your account.
        /// &lt;= 255 characters
        /// </summary>
        public string SuccessUrl { get; set; }

        /// <summary>
        /// The URL to redirect the customer to, if the payment is unsuccessful.
        /// For payment methods with a redirect, this value overrides the default failure redirect URL configured on your account.
        /// &lt;= 255 characters
        /// </summary>
        public string FailureUrl { get; set; }
    }
}