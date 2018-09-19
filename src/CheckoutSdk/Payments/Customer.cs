namespace Checkout.Payments
{
    public class Customer
    {
        /// <summary>
        /// The unique identifier of the customer. This can be passed as a source when making a payment
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// The customer email address
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// The customer name
        /// </summary>
        public string Name { get; set; }
    }
}
