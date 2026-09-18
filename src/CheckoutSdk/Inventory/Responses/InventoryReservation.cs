using Checkout.Inventory.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Checkout.Inventory.Responses
{
    /// <summary>
    /// An atomic, multi-variant stock reservation (hold). Returned by createInventoryReservation,
    /// getInventoryReservation, commitInventoryReservation and releaseInventoryReservation.
    /// </summary>
    public class InventoryReservation : HttpMetadata
    {
        /// <summary> Identifier of the reservation, in the form <c>rsv_{base32-encoded GUID}</c>. (Optional) </summary>
        public string Id { get; set; }

        /// <summary>
        /// The reservation's state. A held reservation past its expires_at reports as expired. (Optional)
        /// </summary>
        public InventoryReservationState? State { get; set; }

        /// <summary> Echo of the owner_type supplied on creation. (Optional) </summary>
        public string OwnerType { get; set; }

        /// <summary> Echo of the owner_reference supplied on creation. (Optional) </summary>
        public string OwnerReference { get; set; }

        /// <summary> The reserved variants and quantities. (Optional) </summary>
        public List<InventoryReservationItem> Items { get; set; }

        /// <summary> When the hold expires. (Optional) </summary>
        public DateTime? ExpiresAt { get; set; }

        /// <summary> When the reservation was created. (Optional) </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// HAL links for this resource. A held reservation exposes self, commit and release;
        /// terminal states (committed, released, expired) expose self only. (Optional)
        /// </summary>
        [JsonProperty(PropertyName = "_links")]
        public IDictionary<string, InventoryHalLink> Links { get; set; }
    }
}
