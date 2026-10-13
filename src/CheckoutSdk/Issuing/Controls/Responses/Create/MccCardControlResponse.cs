using Checkout.Issuing.Common;

namespace Checkout.Issuing.Controls.Responses.Create
{
    /// <summary>
    /// A card control of type mcc_limit.
    /// </summary>
    public class MccCardControlResponse : AbstractCardControlResponse
    {
        /// <summary>
        /// Initializes an mcc_limit control.
        /// </summary>
        public MccCardControlResponse() : base(IssuingControlType.MccLimit)
        {
        }

        /// <summary>
        /// The merchant category code (MCC) rule, which determines the types of businesses transactions can be
        /// processed from.
        /// [Optional]
        /// </summary>
        public MccLimit MccLimit { get; set; }
    }
}
