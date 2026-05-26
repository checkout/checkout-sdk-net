using System.Collections.Generic;

namespace Checkout.Accounts.Simulator
{
    /// <summary>
    /// A pre-defined scenario available in the Onboarding Simulator.
    /// </summary>
    public class SimulatorScenario
    {
        /// <summary>
        /// The unique identifier of the scenario.
        /// [Optional]
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Human-readable name of the scenario.
        /// [Optional]
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of what the scenario does.
        /// [Optional]
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The action type performed by this scenario.
        /// [Optional]
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// The resulting entity status after the scenario runs. Empty string for non-status actions.
        /// [Optional]
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Requirement fields applied when the scenario runs, if any.
        /// [Optional]
        /// </summary>
        public IList<string> RequirementsDue { get; set; }
    }
}
