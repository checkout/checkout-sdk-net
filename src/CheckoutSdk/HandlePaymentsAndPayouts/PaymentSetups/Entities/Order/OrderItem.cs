using Checkout.Common;

namespace Checkout.Payments.Setups.Entities
{
    public class OrderItem
    {
        /// <summary>
        /// The name or title of the item
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The quantity of this item in the order
        /// </summary>
        public long? Quantity { get; set; }

        /// <summary>
        /// The price per unit of this item in minor currency units (e.g., pence, cents)
        /// </summary>
        public long? UnitPrice { get; set; }
        
        /// <summary>
        /// The total amount for this item (quantity × unit price) in minor currency units
        /// </summary>
        public long? TotalAmount { get; set; }
        
        /// <summary>
        /// A reference identifier for this item (e.g., SKU, product code)
        /// </summary>
        public string Reference { get; set; }        

        /// <summary>
        /// The discount amount applied to this item in minor currency units
        /// </summary>
        public long? DiscountAmount { get; set; }

        /// <summary>
        /// The URL where more information about this item can be found
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The URL of an image representing this item
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// The type or category of this item (e.g., physical, digital, service)
        /// </summary>
        public string Type { get; set; }
    }
}