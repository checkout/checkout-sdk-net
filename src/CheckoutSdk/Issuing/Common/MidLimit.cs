using System.Collections.Generic;

namespace Checkout.Issuing.Common
{
    /// <summary>
    /// The merchant identification (MID) code rule, which determines the merchants from whom transactions can be
    /// processed.
    /// </summary>
    public class MidLimit
    {
        /// <summary>
        /// Sets whether to allow or block the list of MIDs supplied.
        /// [Required]
        /// </summary>
        public LimitControlType? Type { get; set; }

        /// <summary>
        /// The list of merchant identification (MID) codes to allow or block transactions from.
        /// [Required]
        /// Each item: ^[A-Za-z0-9{}\[\] ,+\-=.();'\/&amp;@*]{1,15}$
        /// Each item: min 1 characters, max 15 characters
        /// </summary>
        public IList<string> MidList { get; set; }
    }
}
