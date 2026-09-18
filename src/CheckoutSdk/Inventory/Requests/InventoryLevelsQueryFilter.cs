namespace Checkout.Inventory.Requests
{
    /// <summary> Query parameters for getInventoryLevels. </summary>
    public class InventoryLevelsQueryFilter
    {
        /// <summary>
        /// When set to "product", embeds the variant's product knowledge in the response, if any
        /// exists. (Optional)
        /// </summary>
        public string Expand { get; set; }
    }
}
