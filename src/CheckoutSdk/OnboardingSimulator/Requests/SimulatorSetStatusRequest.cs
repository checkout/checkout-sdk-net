using Checkout.OnboardingSimulator.Entities;

namespace Checkout.OnboardingSimulator.Requests
{
    /// <summary>
    /// Request body for forcing the entity to a specific status.
    /// </summary>
    public class SimulatorSetStatusRequest
    {
        /// <summary>
        /// The status to set on the entity.
        /// [Required]
        /// Enum: "draft" "requirements_due" "pending" "active" "restricted" "rejected" "inactive"
        /// </summary>
        public SimulatorEntityStatus? Status { get; set; }
    }
}
