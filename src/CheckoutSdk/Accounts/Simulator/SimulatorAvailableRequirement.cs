namespace Checkout.Accounts.Simulator
{
    /// <summary>
    /// A requirement field available to mark as due in the Onboarding Simulator.
    /// </summary>
    public class SimulatorAvailableRequirement
    {
        /// <summary>
        /// The public path of the requirement field.
        /// [Optional]
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// The data type of the field.
        /// [Optional]
        /// </summary>
        public string Type { get; set; }
    }
}
