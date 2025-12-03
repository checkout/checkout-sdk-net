using System;

namespace Checkout.Payments.Contexts
{
    public class PaymentContextsGuests
    {
        /// <summary>
        /// The guest's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The guest's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The guest's date of birth in YYYY-MM-DD format
        /// </summary>
        public DateTime DateOfBirth { get; set; }
    }
}