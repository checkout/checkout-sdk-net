using Checkout.Inventory.Requests;
using Checkout.Inventory.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Inventory
{
    /// <summary>
    /// Manages stock levels, atomic multi-variant reservations and per-variant product knowledge.
    /// All operations require the <c>agentic:inventory</c> OAuth scope.
    /// </summary>
    public interface IInventoryClient
    {
        /// <summary>
        /// Applies a signed adjustment to a variant's on-hand stock.
        /// </summary>
        /// <param name="request">The adjustment request</param>
        /// <param name="idempotencyKey">Optional idempotency key for the request</param>
        /// <param name="cancellationToken">A cancellation token for the operation</param>
        Task<InventoryLevels> AdjustInventory(
            InventoryAdjustmentRequest request,
            string idempotencyKey = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates an atomic, multi-variant inventory reservation (hold).
        /// </summary>
        /// <param name="request">The reservation request</param>
        /// <param name="idempotencyKey">Optional idempotency key for the request</param>
        /// <param name="cancellationToken">A cancellation token for the operation</param>
        Task<InventoryReservation> CreateInventoryReservation(
            InventoryReservationRequest request,
            string idempotencyKey = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves an inventory reservation by ID.
        /// </summary>
        /// <param name="reservationId">Identifier of the reservation</param>
        /// <param name="cancellationToken">A cancellation token for the operation</param>
        Task<InventoryReservation> GetInventoryReservation(
            string reservationId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Commits a held reservation, converting the hold into a permanent stock reduction.
        /// </summary>
        /// <param name="reservationId">Identifier of the reservation</param>
        /// <param name="cancellationToken">A cancellation token for the operation</param>
        Task<InventoryReservation> CommitInventoryReservation(
            string reservationId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Releases a held reservation, returning the reserved quantities to available stock.
        /// </summary>
        /// <param name="reservationId">Identifier of the reservation</param>
        /// <param name="cancellationToken">A cancellation token for the operation</param>
        Task<InventoryReservation> ReleaseInventoryReservation(
            string reservationId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a variant's stock levels. Pass <paramref name="queryFilter"/> with
        /// <c>Expand = "product"</c> to embed the variant's product knowledge, if any.
        /// </summary>
        /// <param name="variantId">Identifier of the variant</param>
        /// <param name="queryFilter">Optional query parameters</param>
        /// <param name="cancellationToken">A cancellation token for the operation</param>
        Task<InventoryLevels> GetInventoryLevels(
            string variantId,
            InventoryLevelsQueryFilter queryFilter = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates or updates a variant's stock levels.
        /// </summary>
        /// <param name="variantId">Identifier of the variant</param>
        /// <param name="request">The levels request</param>
        /// <param name="cancellationToken">A cancellation token for the operation</param>
        Task<InventoryLevels> SetInventoryLevels(
            string variantId,
            InventorySetLevelsRequest request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a variant's product knowledge (merchandising metadata for AI agents). [BETA]
        /// </summary>
        /// <param name="variantId">Identifier of the variant</param>
        /// <param name="cancellationToken">A cancellation token for the operation</param>
        Task<InventoryProductKnowledge> GetInventoryProduct(
            string variantId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates or updates a variant's product knowledge (merchandising metadata for AI agents). [BETA]
        /// </summary>
        /// <param name="variantId">Identifier of the variant</param>
        /// <param name="request">The product knowledge request</param>
        /// <param name="cancellationToken">A cancellation token for the operation</param>
        Task<InventoryProductKnowledge> SetInventoryProduct(
            string variantId,
            InventorySetProductRequest request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a variant's product knowledge. [BETA]
        /// </summary>
        /// <param name="variantId">Identifier of the variant</param>
        /// <param name="cancellationToken">A cancellation token for the operation</param>
        Task<EmptyResponse> DeleteInventoryProduct(
            string variantId,
            CancellationToken cancellationToken = default);
    }
}
