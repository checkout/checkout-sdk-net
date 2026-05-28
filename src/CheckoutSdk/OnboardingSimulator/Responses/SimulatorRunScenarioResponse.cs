using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.OnboardingSimulator.Responses
{
    /// <summary>
    /// Result of running a simulator scenario.
    /// </summary>
    public class SimulatorRunScenarioResponse : Resource
    {
        /// <summary>
        /// The ID of the entity.
        /// [Optional]
        /// </summary>
        public string EntityId { get; set; }

        /// <summary>
        /// The ID of the scenario that was run.
        /// [Optional]
        /// </summary>
        public string ScenarioId { get; set; }

        /// <summary>
        /// The name of the scenario that was run.
        /// [Optional]
        /// </summary>
        public string ScenarioName { get; set; }

        /// <summary>
        /// The entity status before the scenario ran.
        /// [Optional]
        /// </summary>
        public string PreviousStatus { get; set; }

        /// <summary>
        /// The entity status after the scenario ran.
        /// [Optional]
        /// </summary>
        public string CurrentStatus { get; set; }

        /// <summary>
        /// Requirement fields applied by the scenario, if any.
        /// [Optional]
        /// </summary>
        public IList<string> RequirementsDue { get; set; }
    }
}
