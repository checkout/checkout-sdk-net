namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.
    Subscription
{
    /// <summary>
    /// subscription
    /// The details of the subscription.
    /// </summary>
    public class Subscription
    {
        /// <summary>
        /// The ID or reference linking a series of recurring payments together.
        /// [Optional]
        /// <= 50
        /// </summary>
        public string Id { get; set; }
    }
}