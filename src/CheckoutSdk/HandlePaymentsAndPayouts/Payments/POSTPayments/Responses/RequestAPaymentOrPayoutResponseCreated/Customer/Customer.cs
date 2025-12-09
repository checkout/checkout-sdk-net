namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.
    Customer
{
    /// <summary>
    /// customer
    /// The customer associated with the payment, if provided in the request
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// The customer's unique identifier. This can be passed as a source when making a payment.
        /// [Required]
        /// ^(cus)_(\w{26})$
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The customer's email address.
        /// [Optional]
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The customer's name.
        /// [Optional]
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The customer's phone number.
        /// [Optional]
        /// </summary>
        public Phone.Phone Phone { get; set; }

        /// <summary>
        /// The customer’s value-added tax (VAT) registration number.
        /// </summary>
        public string TaxNumber { get; set; }

        /// <summary>
        /// Summary of the customer's transaction history.  Used for risk assessment when source.type is Tamara
        /// [Optional]
        /// </summary>
        public Summary.Summary Summary { get; set; }
    }
}