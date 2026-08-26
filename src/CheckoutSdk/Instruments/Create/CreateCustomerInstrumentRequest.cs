using Checkout.Common;

namespace Checkout.Instruments.Create
{
    /// <summary>
    /// The customer's details, used to associate the stored instrument with an existing customer or
    /// to create a new one.
    /// </summary>
    public class CreateCustomerInstrumentRequest
    {
        /// <summary>
        /// The identifier of an existing customer.
        /// [Optional]
        /// Pattern: ^(cus)_(\w{26})$
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// An optional email address to associate with the customer.
        /// [Optional]
        /// Format: email
        /// max 255 characters
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The customer's name. This will only set the name for new customers.
        /// [Optional]
        /// max 255 characters
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The customer's phone number. This will only set the phone number for new customers.
        /// [Optional]
        /// </summary>
        public Phone Phone { get; set; }

        /// <summary>
        /// If true, this instrument will become the default for the customer. If a new customer is
        /// created as a result of this request, the instrument will automatically be the default.
        /// [Optional]
        /// </summary>
        public bool? Default { get; set; }
    }
}
