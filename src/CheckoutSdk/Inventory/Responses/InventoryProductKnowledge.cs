using Checkout.Inventory.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Checkout.Inventory.Responses
{
    /// <summary>
    /// Per-variant merchandising metadata for AI agents. Returned by getInventoryProduct and
    /// setInventoryProduct, and embedded (allOf) in <see cref="InventoryLevels.Product"/> when
    /// <c>?expand=product</c> is requested. [BETA]
    /// </summary>
    public class InventoryProductKnowledge : HttpMetadata
    {
        /// <summary> Identifier of the variant this product knowledge describes. (Required) </summary>
        public string VariantId { get; set; }

        /// <summary> The product's title. (Required) </summary>
        public string Title { get; set; }

        /// <summary> The product's description. (Required) </summary>
        public string Description { get; set; }

        /// <summary> URL of the product page. (Required) </summary>
        public string ProductUrl { get; set; }

        /// <summary> URL of the primary product image. (Required) </summary>
        public string ImageUrl { get; set; }

        /// <summary> Additional product image URLs. (Optional) </summary>
        public List<string> AdditionalImageUrls { get; set; }

        /// <summary> URL of a product video. (Optional) </summary>
        public string VideoUrl { get; set; }

        /// <summary> URL of a 3D model of the product. (Optional) </summary>
        public string Model3dUrl { get; set; }

        /// <summary> The merchant's SKU for the product. (Optional) </summary>
        public string Sku { get; set; }

        /// <summary> The product's Global Trade Item Number. (Optional) </summary>
        public string Gtin { get; set; }

        /// <summary> The product's Manufacturer Part Number. (Optional) </summary>
        public string Mpn { get; set; }

        /// <summary> The product's brand. (Optional) </summary>
        public string Brand { get; set; }

        /// <summary> The product's category. (Optional) </summary>
        public string Category { get; set; }

        /// <summary> The product's list price. (Optional) </summary>
        public InventoryMoney Price { get; set; }

        /// <summary> The product's sale price. (Optional) </summary>
        public InventoryMoney SalePrice { get; set; }

        /// <summary> When the sale price becomes active. Pairs with sale_price. (Optional) </summary>
        public DateTime? SalePriceStartsAt { get; set; }

        /// <summary> When the sale price stops being active. Pairs with sale_price. (Optional) </summary>
        public DateTime? SalePriceEndsAt { get; set; }

        /// <summary> Groups variants of the same product. (Optional) </summary>
        public string GroupId { get; set; }

        /// <summary> Display title for the variant group. (Optional) </summary>
        public string GroupTitle { get; set; }

        /// <summary> The variant's color. (Optional) </summary>
        public string Color { get; set; }

        /// <summary> The variant's size. (Optional) </summary>
        public string Size { get; set; }

        /// <summary> The sizing system the size value belongs to. (Optional) </summary>
        public string SizeSystem { get; set; }

        /// <summary> The product's target gender. (Optional) </summary>
        public string Gender { get; set; }

        /// <summary> The product's condition. Defaults to <c>new</c>. (Required) </summary>
        public InventoryProductCondition? Condition { get; set; }

        /// <summary> The product's material. (Optional) </summary>
        public string Material { get; set; }

        /// <summary> The product's target age group. (Optional) </summary>
        public string AgeGroup { get; set; }

        /// <summary> The product's length. (Optional) </summary>
        public double? Length { get; set; }

        /// <summary> The product's width. (Optional) </summary>
        public double? Width { get; set; }

        /// <summary> The product's height. (Optional) </summary>
        public double? Height { get; set; }

        /// <summary> The unit the length/width/height values are expressed in. (Optional) </summary>
        public string DimensionUnit { get; set; }

        /// <summary> The product's weight. (Optional) </summary>
        public double? Weight { get; set; }

        /// <summary> The unit the weight value is expressed in. (Optional) </summary>
        public string WeightUnit { get; set; }

        /// <summary> When the product expires. (Optional) </summary>
        public DateTime? ExpirationDate { get; set; }

        /// <summary> The product's harmonized system code. (Optional) </summary>
        public string HarmonizedSystemCode { get; set; }

        /// <summary> 2-letter ISO 3166-1 alpha-2 country of origin. (Optional) </summary>
        public string CountryOfOrigin { get; set; }

        /// <summary> The seller's display name. (Optional) </summary>
        public string SellerName { get; set; }

        /// <summary> The seller's URL. (Optional) </summary>
        public string SellerUrl { get; set; }

        /// <summary> The seller's privacy policy URL. (Optional) </summary>
        public string SellerPrivacyPolicy { get; set; }

        /// <summary> The seller's terms of service URL. (Optional) </summary>
        public string SellerTos { get; set; }

        /// <summary> When the product knowledge was created. (Required) </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary> When the product knowledge was last modified. (Required) </summary>
        public DateTime? ModifiedOn { get; set; }

        /// <summary>
        /// HAL links for this resource (self, set, delete). (Required)
        /// </summary>
        [JsonProperty(PropertyName = "_links")]
        public IDictionary<string, InventoryHalLink> Links { get; set; }
    }
}
