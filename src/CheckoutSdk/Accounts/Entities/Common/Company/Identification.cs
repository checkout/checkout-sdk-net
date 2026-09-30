using System;

namespace Checkout.Accounts.Entities.Common.Company
{
    /// <summary>
    /// The identification of a representative or individual on the Accounts API v2.0 US variants.
    /// </summary>
    public class Identification
    {
        /// <summary>
        /// Social Security Number (SSN), or Individual Taxpayer Identification Number (ITIN) for
        /// non-US citizens.
        /// [Required]
        /// ^\d{9}$
        /// 9 characters
        /// </summary>
        public string NationalIdNumber { get; set; }

        /// <summary>
        /// Not defined by the Accounts API: the identification object carries
        /// <c>national_id_number</c> only. Retained so existing code keeps compiling.
        /// </summary>
        [Obsolete("Not defined by any Accounts API schema: identification carries national_id_number only. Will be removed in a future major version.")]
        public Documents.Documents Document { get; set; }
    }
}