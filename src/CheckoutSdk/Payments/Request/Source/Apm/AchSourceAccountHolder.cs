using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    /// <summary>
    /// The account holder's details on an ACH payment source.
    /// Maps the AccountHolderAch schema exactly. Deliberately not Checkout.Common.AccountHolder,
    /// which is a superset, and distinct from Checkout.Instruments.Create.CreateAchAccountHolder,
    /// which declares four properties only - the instrument schema has no billing address, date of
    /// birth or identification.
    /// BillingAddress reuses Checkout.Common.Address because that schema's six properties are
    /// exactly what this position references. Identification reuses
    /// Checkout.Common.AccountHolderIdentification, which carries one extra property, DateOfExpiry,
    /// that this position does not declare - do not set it.
    /// </summary>
    public class AchSourceAccountHolder
    {
        /// <summary>
        /// The type of account holder.
        /// [Required]
        /// Enum: "individual" "corporate" "government"
        /// </summary>
        public AccountHolderType? Type { get; set; }

        /// <summary>
        /// The account holder's first name.
        /// [Required]
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The account holder's last name.
        /// [Required]
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The account holder's company name.
        /// [Optional]
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// The account holder's billing address.
        /// [Optional]
        /// </summary>
        public Address BillingAddress { get; set; }

        /// <summary>
        /// The account holder's date of birth.
        /// [Optional]
        /// </summary>
        public string DateOfBirth { get; set; }

        /// <summary>
        /// The account holder's identification.
        /// [Optional]
        /// </summary>
        public AccountHolderIdentification Identification { get; set; }
    }
}
