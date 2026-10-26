namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The account holder's details.
    /// </summary>
    public class PaymentSetupSwishAccountHolder
    {
        /// <summary>
        /// The account holder's first name.
        /// [Optional]
        /// min 1 character
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The account holder's last name.
        /// [Optional]
        /// min 1 character
        /// </summary>
        public string LastName { get; set; }
    }
}
