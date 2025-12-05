namespace Checkout.HandlePaymentsAndPayouts.Flow.Entities
{
    public class Item
    {
        /// <summary>
        /// The descriptive name of the line item.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The number of line items.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// The unit price of the item, in minor currency units.
        /// </summary>
        public long UnitPrice { get; set; }

        /// <summary>
        /// The item reference or product stock keeping unit (SKU).
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// A code identifying a commodity for value-added tax (VAT) purposes.
        /// </summary>
        public string CommodityCode { get; set; }

        /// <summary>
        /// The unit in which the item is measured, as a Unit of Measure (UoM) code.
        /// </summary>
        public string UnitOfMeasure { get; set; }

        /// <summary>
        /// The total cost of the line item, in minor currency units. 
        /// The value should include any tax and discounts applied using the formula: 
        /// value = (quantity x unit_price) - discount_amount.
        /// </summary>
        public long? TotalAmount { get; set; }

        /// <summary>
        /// The total amount of sales tax or value-added tax (VAT) on the total purchase amount. 
        /// Tax should be included in the total purchase amount.
        /// </summary>
        public long? TaxAmount { get; set; }

        /// <summary>
        /// The discount applied to each invoice line item.
        /// </summary>
        public long? DiscountAmount { get; set; }

        /// <summary>
        /// Link to the line item's product page.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Link to the line item's product image.
        /// </summary>
        public string ImageUrl { get; set; }
    }
}