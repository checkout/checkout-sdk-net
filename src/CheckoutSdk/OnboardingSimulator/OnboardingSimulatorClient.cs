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
    public class OnboardingSimulatorClient : AbstractClient, IOnboardingSimulatorClient
    {
        private const string SimulatePath = "simulate";
        private const string EntitiesPath = "entities";
        private const string RequirementsDuePath = "requirements-due";
        private const string ScenariosPath = "scenarios";
        private const string StatusPath = "status";

        public OnboardingSimulatorClient(IApiClient apiClient, CheckoutConfiguration configuration)
            : base(apiClient, configuration, SdkAuthorizationType.OAuth)
        {
        }

        public Task<SimulatorSetRequirementsDueResponse> SetRequirementsDue(
            string entityId,
            SimulatorSetRequirementsDueRequest request,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("entityId", entityId, "request", request);
            return ApiClient.Post<SimulatorSetRequirementsDueResponse>(
                BuildPath(SimulatePath, EntitiesPath, entityId, RequirementsDuePath),
                SdkAuthorization(),
                request,
                cancellationToken);
        }

        public Task<SimulatorRunScenarioResponse> RunScenario(
            string entityId,
            string scenarioId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("entityId", entityId, "scenarioId", scenarioId);
            return ApiClient.Post<SimulatorRunScenarioResponse>(
                BuildPath(SimulatePath, EntitiesPath, entityId, ScenariosPath, scenarioId),
                SdkAuthorization(),
                (object)null,
                cancellationToken);
        }

        public Task<SimulatorSetStatusResponse> SetEntityStatus(
            string entityId,
            SimulatorSetStatusRequest request,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("entityId", entityId, "request", request);
            return ApiClient.Post<SimulatorSetStatusResponse>(
                BuildPath(SimulatePath, EntitiesPath, entityId, StatusPath),
                SdkAuthorization(),
                request,
                cancellationToken);
        }

        public Task<ItemsResponse<SimulatorAvailableRequirement>> ListAvailableRequirements(
            CancellationToken cancellationToken = default)
        {
            return ApiClient.Get<ItemsResponse<SimulatorAvailableRequirement>>(
                BuildPath(SimulatePath, RequirementsDuePath),
                SdkAuthorization(),
                cancellationToken);
        }

        public Task<ItemsResponse<SimulatorScenario>> ListScenarios(
            CancellationToken cancellationToken = default)
        {
            return ApiClient.Get<ItemsResponse<SimulatorScenario>>(
                BuildPath(SimulatePath, ScenariosPath),
                SdkAuthorization(),
                cancellationToken);
        }
    }
}
