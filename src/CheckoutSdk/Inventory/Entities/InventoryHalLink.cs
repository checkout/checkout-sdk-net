namespace Checkout.Inventory.Entities
{
    /// <summary>
    /// A HAL-style link returned on Inventory resources. Unlike the generic
    /// <see cref="Checkout.Common.Link"/> used elsewhere in the SDK, Inventory links describe the
    /// HTTP methods and media types the linked action supports instead of a title or wallet redirect.
    /// </summary>
    public class InventoryHalLink
    {
        /// <summary> The absolute URI for the link. [Optional] </summary>
        public string Href { get; set; }

        /// <summary> The HTTP methods supported by the link (e.g. "GET", "PUT"). [Optional] </summary>
        public string[] Actions { get; set; }

        /// <summary> The media types supported by the link. [Optional] </summary>
        public string[] Types { get; set; }
    }
}
