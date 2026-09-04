using Checkout.Common;

namespace Checkout.Instruments
{
    /// <summary>
    /// The account holder details returned on a stored instrument.
    /// This deliberately does not derive from Checkout.Common.AccountHolderResponse, which is a
    /// superset carrying fields the instrument account holder response does not return.
    /// </summary>
    public class InstrumentAccountHolderResponse
    {
        /// <summary>
        /// The account holder's first name.
        /// [Optional]
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The account holder's last name.
        /// [Optional]
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The account holder's billing address.
        /// [Optional]
        /// </summary>
        public Address BillingAddress { get; set; }

        /// <summary>
        /// The account holder's phone number.
        /// [Optional]
        /// </summary>
        public Phone Phone { get; set; }
    }
}
