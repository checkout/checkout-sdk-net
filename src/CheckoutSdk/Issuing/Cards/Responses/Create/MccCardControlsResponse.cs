using Checkout.Issuing.Common;

namespace Checkout.Issuing.Cards.Responses.Create
{
    /// <summary>
    /// A card control of type mcc_limit set when the virtual card was created.
    /// </summary>
    public class MccCardControlsResponse : AbstractCardControlsResponse
    {
        /// <summary>
        /// Initializes an mcc_limit control.
        /// </summary>
        public MccCardControlsResponse() : base(IssuingControlType.MccLimit)
        {
        }

        /// <summary>
        /// The merchant category code (MCC) rule, which determines the types of businesses transactions can be
        /// processed from.
        /// [Required]
        /// </summary>
        public MccLimit MccLimit { get; set; }
    }
}
