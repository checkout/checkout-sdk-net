namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The billing descriptor for payment.
    /// </summary>
    public class PaymentSetupBillingDescriptor
    {
        /// <summary>
        /// A dynamic description of the payment.
        /// [Optional]
        /// &lt;= 25 characters
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The city from which the payment was made.
        /// [Optional]
        /// &lt;= 13 characters
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The reference shown on the statement.
        /// [Optional]
        /// &lt;= 50 characters
        /// </summary>
        public string Reference { get; set; }
    }
}
