namespace Checkout.Payments
{
    public class PaymentDestination
    {
        /// <summary>
        /// For OpenPay payments, destinations determine the proportion of the payment amount that should be credited to other OpenPay accounts
        /// </summary>
        /// <param name="id">The OpenPay account identifier</param>
        /// <param name="amount">The amount to be credited to the destination in the major currency unit</param>
        public PaymentDestination(string id, int amount)
        {
            Id = id;
            Amount = amount;
        }

        /// <summary>
        /// The OpenPay account identifier
        /// </summary>
        public string Id { get; }
        /// <summary>
        /// The amount to be credited to the destination in the major currency unit
        /// </summary>
        public int Amount { get; }
    }
}