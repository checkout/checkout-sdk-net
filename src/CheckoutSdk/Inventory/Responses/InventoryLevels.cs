using Checkout.Inventory.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Checkout.Inventory.Responses
{
    /// <summary>
    /// A variant's stock levels. Returned by getInventoryLevels, setInventoryLevels and
    /// adjustInventory. adjustInventory and createInventoryReservation-style replays may return
    /// this with HTTP 200 (idempotent replay, with a <c>Cache-Control</c> response header) instead
    /// of 201; both status codes share this schema, available via <see cref="HttpMetadata.ResponseHeaders"/>.
    /// </summary>
    public class InventoryLevels : HttpMetadata
    {
        /// <summary> Identifier of the variant. (Optional) </summary>
        public string VariantId { get; set; }

        /// <summary> Physical stock quantity. (Optional) </summary>
        public int? OnHand { get; set; }

        /// <summary> Sum of active holds against this variant. (Optional) </summary>
        public int? Reserved { get; set; }

        /// <summary> Buffer withheld from sale. (Optional) </summary>
        public int? SafetyStock { get; set; }

        /// <summary> max(0, on_hand - reserved - safety_stock). (Optional) </summary>
        public int? Available { get; set; }

        /// <summary> The stock state. (Optional) </summary>
        public InventoryLevelState? State { get; set; }

        /// <summary> Where the level is managed. (Optional) </summary>
        public InventorySource? Source { get; set; }

        /// <summary> When the level record was created. (Optional) </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary> When the level record was last modified. (Optional) </summary>
        public DateTime? ModifiedOn { get; set; }

        /// <summary>
        /// The variant's product knowledge, only present when <c>?expand=product</c> was passed and
        /// product knowledge exists for the variant. (Optional)
        /// </summary>
        public InventoryProductKnowledge Product { get; set; }

        /// <summary> HAL links for this resource (self, set). (Optional) </summary>
        [JsonProperty(PropertyName = "_links")]
        public IDictionary<string, InventoryHalLink> Links { get; set; }
    }
}
