using Checkout.Issuing.Common;

namespace Checkout.Issuing.Cards.Responses.Create
{
    /// <summary>
    /// A card control of type velocity_limit set when the virtual card was created.
    /// </summary>
    public class VelocityCardControlsResponse : AbstractCardControlsResponse
    {
        /// <summary>
        /// Initializes a velocity_limit control.
        /// </summary>
        public VelocityCardControlsResponse() : base(IssuingControlType.VelocityLimit)
        {
        }

        /// <summary>
        /// The velocity limit, which determines how much a target card can spend over a given timeframe.
        /// [Required]
        /// </summary>
        public VelocityLimit VelocityLimit { get; set; }
    }
}
