using Checkout.Common;

namespace Checkout.Payments.Request
{
    public class PaymentCustomerRequest : CustomerRequest
    {
        /// <summary>
        /// The identifier of an existing customer.
        /// [Optional]
        /// Pattern: ^(cus)_(\w{26})$
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The customer's value-added tax (VAT) registration number.
        /// [Optional]
        /// max 13 characters
        /// </summary>
        public string TaxNumber { get; set; }
    }
}