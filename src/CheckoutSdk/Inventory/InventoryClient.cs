using Checkout.Inventory.Requests;
using Checkout.Inventory.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Inventory
{
    /// <summary>
    /// Manages stock levels, atomic multi-variant reservations and per-variant product knowledge.
    /// All operations require the <c>agentic:inventory</c> OAuth scope (never a secret/public key),
    /// so this client is built with <see cref="SdkAuthorizationType.OAuth"/>, the same pattern used
    /// by <see cref="Checkout.StandaloneAccountUpdater.StandaloneAccountUpdaterClient"/>.
    /// </summary>
    public class InventoryClient : AbstractClient, IInventoryClient
    {
        private const string InventoryPath = "inventory";
        private const string AdjustmentsPath = "inventory/adjustments";
        private const string ReservationsPath = "inventory/reservations";
        private const string CommitPath = "commit";
        private const string ReleasePath = "release";
        private const string ProductPath = "product";

        public InventoryClient(IApiClient apiClient, CheckoutConfiguration configuration)
            : base(apiClient, configuration, SdkAuthorizationType.OAuth)
        {
        }

        public Task<InventoryLevels> AdjustInventory(
            InventoryAdjustmentRequest request,
            string idempotencyKey = null,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("request", request);
            return ApiClient.Post<InventoryLevels>(
                AdjustmentsPath,
                SdkAuthorization(),
                request,
                cancellationToken,
                idempotencyKey);
        }

        public Task<InventoryReservation> CreateInventoryReservation(
            InventoryReservationRequest request,
            string idempotencyKey = null,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("request", request);
            return ApiClient.Post<InventoryReservation>(
                ReservationsPath,
                SdkAuthorization(),
                request,
                cancellationToken,
                idempotencyKey);
        }

        public Task<InventoryReservation> GetInventoryReservation(
            string reservationId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("reservationId", reservationId);
            return ApiClient.Get<InventoryReservation>(
                BuildPath(ReservationsPath, reservationId),
                SdkAuthorization(),
                cancellationToken);
        }

        public Task<InventoryReservation> CommitInventoryReservation(
            string reservationId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("reservationId", reservationId);
            return ApiClient.Post<InventoryReservation>(
                BuildPath(ReservationsPath, reservationId, CommitPath),
                SdkAuthorization(),
                (object)null,
                cancellationToken);
        }

        public Task<InventoryReservation> ReleaseInventoryReservation(
            string reservationId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("reservationId", reservationId);
            return ApiClient.Post<InventoryReservation>(
                BuildPath(ReservationsPath, reservationId, ReleasePath),
                SdkAuthorization(),
                (object)null,
                cancellationToken);
        }

        public Task<InventoryLevels> GetInventoryLevels(
            string variantId,
            InventoryLevelsQueryFilter queryFilter = null,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("variantId", variantId);
            return ApiClient.Query<InventoryLevels>(
                BuildPath(InventoryPath, variantId),
                SdkAuthorization(),
                queryFilter,
                cancellationToken);
        }

        public Task<InventoryLevels> SetInventoryLevels(
            string variantId,
            InventorySetLevelsRequest request,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("variantId", variantId, "request", request);
            return ApiClient.Put<InventoryLevels>(
                BuildPath(InventoryPath, variantId),
                SdkAuthorization(),
                request,
                cancellationToken);
        }

        public Task<InventoryProductKnowledge> GetInventoryProduct(
            string variantId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("variantId", variantId);
            return ApiClient.Get<InventoryProductKnowledge>(
                BuildPath(InventoryPath, variantId, ProductPath),
                SdkAuthorization(),
                cancellationToken);
        }

        public Task<InventoryProductKnowledge> SetInventoryProduct(
            string variantId,
            InventorySetProductRequest request,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("variantId", variantId, "request", request);
            return ApiClient.Put<InventoryProductKnowledge>(
                BuildPath(InventoryPath, variantId, ProductPath),
                SdkAuthorization(),
                request,
                cancellationToken);
        }

        public Task<EmptyResponse> DeleteInventoryProduct(
            string variantId,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("variantId", variantId);
            return ApiClient.Delete<EmptyResponse>(
                BuildPath(InventoryPath, variantId, ProductPath),
                SdkAuthorization(),
                cancellationToken);
        }
    }
}
