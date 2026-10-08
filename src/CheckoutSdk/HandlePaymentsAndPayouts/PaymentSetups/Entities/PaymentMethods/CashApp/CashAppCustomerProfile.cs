namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The customer's Cash App profile that they consented to share. Included in the response when
    /// customer_profile_sharing is enabled. Cash App releases this profile only once: it is present in
    /// the first successful response when you get the payment setup after the customer authorizes the
    /// payment, and every subsequent response omits it.
    /// </summary>
    public class CashAppCustomerProfile
    {
        /// <summary>
        /// Cash App's identifier for the customer. This is not a Checkout.com customer identifier.
        /// [Optional]
        /// readOnly
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// The customer's $Cashtag.
        /// [Optional]
        /// readOnly
        /// </summary>
        public string Cashtag { get; set; }

        /// <summary>
        /// Cash App's reference for the customer profile.
        /// [Optional]
        /// readOnly
        /// </summary>
        public string ReferenceId { get; set; }

        /// <summary>
        /// The customer's full name.
        /// [Optional]
        /// readOnly
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// The customer's given name.
        /// [Optional]
        /// readOnly
        /// </summary>
        public string GivenName { get; set; }

        /// <summary>
        /// The customer's middle name.
        /// [Optional]
        /// readOnly
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// The customer's family name.
        /// [Optional]
        /// readOnly
        /// </summary>
        public string FamilyName { get; set; }

        /// <summary>
        /// The suffix of the customer's name.
        /// [Optional]
        /// readOnly
        /// </summary>
        public string Suffix { get; set; }

        /// <summary>
        /// The customer's date of birth. Kept as the raw string returned by the API, because the
        /// provider's value is not always a plain yyyy-MM-dd date.
        /// [Optional]
        /// readOnly
        /// Format: date
        /// </summary>
        public string BirthDate { get; set; }

        /// <summary>
        /// The customer's address.
        /// [Optional]
        /// readOnly
        /// </summary>
        public CashAppAddress Address { get; set; }

        /// <summary>
        /// The customer's phone number.
        /// [Optional]
        /// readOnly
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The customer's email address.
        /// [Optional]
        /// readOnly
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// The date and time the customer's Cash App account was created. Kept as the raw string
        /// returned by the API, so that a provider format the SDK does not expect cannot fail the
        /// whole response and lose the profile, which is only returned once.
        /// [Optional]
        /// readOnly
        /// Format: date-time
        /// </summary>
        public string CustomerSince { get; set; }
    }
}
