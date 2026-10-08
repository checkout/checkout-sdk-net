using Checkout.Issuing.Common;
using Newtonsoft.Json;
using System;

namespace Checkout.Issuing.Cards.Responses.Create
{
    /// <summary>
    /// A card control of type mid_limit set when the virtual card was created.
    /// </summary>
    public class MidCardControlsResponse : AbstractCardControlsResponse
    {
        /// <summary>
        /// Initializes a mid_limit control.
        /// </summary>
        public MidCardControlsResponse() : base(IssuingControlType.MidLimit)
        {
        }

        /// <summary>
        /// The merchant identification (MID) code rule, which determines the merchants from whom transactions can
        /// be processed.
        /// [Required]
        /// </summary>
        public MidLimit MidLimit { get; set; }

        /// <summary>
        /// Former name of <see cref="MidLimit"/>. It was mapped to velocity_limit, so it was never populated.
        /// It now reads and writes <see cref="MidLimit"/> and is not serialized.
        /// [Optional]
        /// </summary>
        [Obsolete("Use MidLimit instead. Will be removed in the next major version.")]
        [JsonIgnore]
        public MidLimit VelocityLimit
        {
            get => MidLimit;
            set => MidLimit = value;
        }
    }
}
