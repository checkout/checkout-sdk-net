using Checkout.Common;

namespace Checkout.Issuing.Cardholders.Requests
{
    public class CardholderRequest
    {
        /// <summary>
        /// The type of cardholder to create.
        /// [Required] (on create)
        /// </summary>
        public CardholderType? Type { get; set; }

        /// <summary>
        /// The cardholder's first name.
        /// [Optional]
        /// min 1 character, max 40 characters
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The cardholder's last name.
        /// [Optional]
        /// min 1 character, max 40 characters
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The cardholder's billing address.
        /// [Optional]
        /// </summary>
        public Address BillingAddress { get; set; }

        /// <summary>
        /// The entity's unique identifier.
        /// [Required] (on create)
        /// ^ent_[a-z0-9]{26}$
        /// min 30 characters, max 30 characters
        /// </summary>
        public string EntityId { get; set; }

        /// <summary>
        /// Your reference.
        /// [Optional]
        /// &lt;= 256 characters
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// The cardholder's middle name. To set this field to null, pass null in your request.
        /// [Optional]
        /// min 1 character, max 40 characters
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// The cardholder's email address. To set this field to null, pass null in your request.
        /// [Optional]
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The cardholder's phone number.
        /// [Optional]
        /// </summary>
        public Phone PhoneNumber { get; set; }

        /// <summary>
        /// The cardholder's date of birth in the YYYY-MM-DD format. To set this field to null, pass null in
        /// your request.
        /// [Optional]
        /// Format: yyyy-MM-dd
        /// </summary>
        public string DateOfBirth { get; set; }

        /// <summary>
        /// The cardholder's residency address. To set this field to null, pass null in your request.
        /// [Optional]
        /// </summary>
        public Address ResidencyAddress { get; set; }

    }
}