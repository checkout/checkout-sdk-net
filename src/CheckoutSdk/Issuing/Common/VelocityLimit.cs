using System.Collections.Generic;

namespace Checkout.Issuing.Common
{
    /// <summary>
    /// The velocity limit, which determines how much a target card can spend over a given timeframe.
    /// </summary> 
    public class VelocityLimit
    {
        /// <summary>
        /// The amount the target can spend, in minor units.
        /// >= 0
        /// [Required]
        /// </summary>
        public long? AmountLimit { get; set; }

        /// <summary>
        /// The period of time over which the specified amount_limit can be spent.
        /// [Required]
        /// </summary>
        public VelocityWindow VelocityWindow { get; set; }

        /// <summary>
        /// The list of merchant category codes (MCCs) that the velocity limit applies to, as four-digit ISO 18245 codes.
        /// You can provide either mcc_list or mid_list, but not both.
        /// [Optional]
        /// </summary>
        public IList<string> MccList { get; set; }
        
        /// <summary>
        /// The list of merchant identification (MID) codes to allow or block transactions from.
        /// You can provide either mcc_list or mid_list, but not both.
        /// [Optional]
        /// </summary>
        public IList<string> MidList { get; set; }
    }
}