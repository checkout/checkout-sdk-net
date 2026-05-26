using System.Threading;
using System.Threading.Tasks;
using Checkout.OnboardingSimulator.Entities;
using Checkout.OnboardingSimulator.Requests;
using Checkout.OnboardingSimulator.Responses;

namespace Checkout.OnboardingSimulator
{
    /// <summary>
    /// Onboarding Simulator client. Sandbox only — endpoints are not registered in production.
    /// </summary>
    public interface IOnboardingSimulatorClient
    {
        /// <summary>
        /// Marks the specified requirement fields as due on an entity.
        /// </summary>
        Task<SimulatorSetRequirementsDueResponse> SetRequirementsDue(
            string entityId,
            SimulatorSetRequirementsDueRequest request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Executes a pre-defined scenario against an entity.
        /// </summary>
        Task<SimulatorRunScenarioResponse> RunScenario(
            string entityId,
            string scenarioId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Forces the entity to the specified status.
        /// </summary>
        Task<SimulatorSetStatusResponse> SetStatus(
            string entityId,
            SimulatorSetStatusRequest request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns all requirement fields that can be set as due on an entity.
        /// </summary>
        Task<ItemsResponse<SimulatorAvailableRequirement>> ListAvailableRequirements(
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns all pre-defined scenarios available.
        /// </summary>
        Task<ItemsResponse<SimulatorScenario>> ListScenarios(
            CancellationToken cancellationToken = default);
    }
}
