using System.Collections.Generic;

namespace Checkout.OnboardingSimulator.Requests
{
    /// <summary>
    /// Request body for marking requirement fields as due on an entity.
    /// </summary>
    public class SimulatorSetRequirementsDueRequest
    {
        /// <summary>
        /// The requirement fields to mark as due. Call the List available requirements endpoint
        /// for a list of valid values.
        /// [Required]
        /// </summary>
        public IList<string> Fields { get; set; }
    }
}
