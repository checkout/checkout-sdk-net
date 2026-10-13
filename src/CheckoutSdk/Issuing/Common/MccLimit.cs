using System.Collections.Generic;

namespace Checkout.Issuing.Common
{
    /// <summary>
    /// The merchant category code (MCC) rule, which determines the types of businesses transactions can be
    /// processed from.
    /// </summary>
    public class MccLimit
    {
        /// <summary>
        /// Sets whether to allow or block the list of MCCs supplied.
        /// [Required]
        /// </summary>
        public LimitControlType? Type { get; set; }

        /// <summary>
        /// The list of MCCs to allow or block transactions from, as 4-digit ISO 18245 codes.
        /// [Required]
        /// </summary>
        public IList<string> MccList { get; set; }
    }
}
