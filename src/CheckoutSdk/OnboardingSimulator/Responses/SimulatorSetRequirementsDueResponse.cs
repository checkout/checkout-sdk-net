using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.OnboardingSimulator.Responses
{
    /// <summary>
    /// Response from the Set requirements due simulator endpoint.
    /// </summary>
    public class SimulatorSetRequirementsDueResponse : Resource
    {
        /// <summary>
        /// The ID of the entity.
        /// [Optional]
        /// </summary>
        public string EntityId { get; set; }

        /// <summary>
        /// The status before the update.
        /// [Optional]
        /// </summary>
        public string PreviousStatus { get; set; }

        /// <summary>
        /// The status after the update.
        /// [Optional]
        /// </summary>
        public string CurrentStatus { get; set; }

        /// <summary>
        /// The requirement fields that are now marked as due.
        /// [Optional]
        /// </summary>
        public IList<string> RequirementsDue { get; set; }
    }
}
