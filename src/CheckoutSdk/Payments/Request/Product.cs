namespace Checkout.Payments.Request
{
    public class Product
    {
        /// <summary>
        /// The item type. For example, digital or physical.
        /// This field is used to provide item level metadata where applicable.
        /// If source.type is sequra, this field is required.
        /// [Optional]
        /// Enum: "digital" "discount" "physical"
        /// </summary>
        public ItemType? Type { get; set; }

        /// <summary>
        /// The digital item type.
        /// If type is set to digital, this field is required.
        /// [Optional]
        /// Enum: "blockchain" "cbdc" "cryptocurrency" "nft" "stablecoin"
        /// </summary>
        public ItemSubType? SubType { get; set; }

        /// <summary>
        /// The descriptive name of the line item.
        /// If source.type is sequra, this field is required.
        /// [Optional]
        /// max 255 characters
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The number of line items.
        /// If source.type is sequra, this field is required.
        /// [Optional]
        /// min 1
        /// </summary>
        public long? Quantity { get; set; }

        /// <summary>
        /// The unit price of the item in the minor currency unit.
        /// If source.type is sequra, this field is required.
        /// [Optional]
        /// min 0
        /// </summary>
        public long? UnitPrice { get; set; }

        /// <summary>
        /// The item reference or product SKU (stock-keeping unit).
        /// For Visa and Mastercard interchange reduction programs, this value has a maximum length of 12 characters.
        /// If source.type is sequra, this field is required.
        /// [Optional]
        /// max 255 characters
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// A code identifying a commodity for value-added tax (VAT) purposes.
        /// [Optional]
        /// max 12 characters
        /// </summary>
        public string CommodityCode { get; set; }

        /// <summary>
        /// Unit of measure code used for an item in transaction.
        /// [Optional]
        /// max 12 characters
        /// </summary>
        public string UnitOfMeasure { get; set; }

        /// <summary>
        /// The total cost of the line item, in minor currency units.
        /// Calculate using: total_amount = (quantity * unit_price) - discount_amount.
        /// If source.type is sequra, this field is required.
        /// [Optional]
        /// </summary>
        public long? TotalAmount { get; set; }

        /// <summary>
        /// The total amount of sales tax or value-added tax (VAT) on the total purchase amount.
        /// Tax should be included in the total purchase.
        /// [Optional]
        /// </summary>
        public long? TaxAmount { get; set; }

        /// <summary>
        /// The tax rate of the item line in minor units.
        /// The percentage value is represented with two implicit decimals. For example, 2000 = 20%.
        /// [Optional]
        /// </summary>
        public long? TaxRate { get; set; }

        /// <summary>
        /// The discount applied to each invoice line item.
        /// [Optional]
        /// </summary>
        public long? DiscountAmount { get; set; }

        /// <summary>
        /// Unified product number defined by WeChat Pay.
        /// [Optional]
        /// max 32 characters
        /// </summary>
        public string WxpayGoodsId { get; set; }

        /// <summary>
        /// Link to the line item product image.
        /// [Optional]
        /// max 1024 characters
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Link to the line item product page.
        /// [Optional]
        /// max 1024 characters
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The item SKU (stock-keeping unit).
        /// [Optional]
        /// </summary>
        public string Sku { get; set; }
    }
}