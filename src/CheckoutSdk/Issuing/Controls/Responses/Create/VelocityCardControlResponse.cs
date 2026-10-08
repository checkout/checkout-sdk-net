using Checkout.Issuing.Common;

namespace Checkout.Issuing.Controls.Responses.Create
{
    /// <summary>
    /// A card control of type velocity_limit.
    /// </summary>
    public class VelocityCardControlResponse : AbstractCardControlResponse
    {
        /// <summary>
        /// Initializes a velocity_limit control.
        /// </summary>
        public VelocityCardControlResponse() : base(IssuingControlType.VelocityLimit)
        {
        }

        /// <summary>
        /// The velocity limit, which determines how much a target card can spend over a given timeframe.
        /// On get and update responses it also carries amount_remaining.
        /// [Optional]
        /// </summary>
        public VelocityLimit VelocityLimit { get; set; }
    }
}
