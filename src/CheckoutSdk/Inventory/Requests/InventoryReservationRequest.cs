using Checkout.Inventory.Entities;
using System.Collections.Generic;

namespace Checkout.Inventory.Requests
{
    /// <summary> Request body for creating an atomic, multi-variant inventory reservation (hold). </summary>
    public class InventoryReservationRequest
    {
        /// <summary> Type of the owner placing the reservation. (Required, constraints: maxLength 64) </summary>
        public string OwnerType { get; set; }

        /// <summary> Reference of the owner placing the reservation. (Required, constraints: maxLength 256) </summary>
        public string OwnerReference { get; set; }

        /// <summary>
        /// The variants and quantities to reserve.
        /// (Required, constraints: minItems 1, maxItems 45, variant_ids must be unique within the request)
        /// </summary>
        public List<InventoryReservationItem> Items { get; set; }

        /// <summary>
        /// How long the hold is valid for, in seconds, before it reports as expired.
        /// (Optional, constraints: minimum 60, maximum 3600, default 900)
        /// </summary>
        public int? TtlSeconds { get; set; }
    }
}
