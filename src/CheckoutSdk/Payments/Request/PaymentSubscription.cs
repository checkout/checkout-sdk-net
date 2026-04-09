namespace Checkout.Payments.Request
{
    public class PaymentSubscription
    {
        /// <summary>
        /// The ID or reference linking a series of recurring payments together.
        /// [Optional]
        /// &lt;= 50 characters
        /// </summary>
        public string Id { get; set; }
    }
}
