using Checkout.Common;

namespace Checkout.Accounts.Simulator
{
    /// <summary>
    /// Response from the Set entity status simulator endpoint.
    /// </summary>
    public class SimulatorSetStatusResponse : Resource
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
    }
}
