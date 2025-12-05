

namespace Checkout.HandlePaymentsAndPayouts.Flow.Entities
{
    public class BillingDescriptor
    {
        /// <summary>
        /// A description for the payment, which will be displayed on the customer's card statement. Only applicable for card payments.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The city from which the payment originated. Only applicable for card payments.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The reference to display on the customer's bank statement. Required for payouts to bank accounts.
        /// </summary>
        public string Reference { get; set; }
    }
}