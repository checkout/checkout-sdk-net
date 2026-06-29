using Checkout.Payments.Request;
using Shouldly;
using Xunit;

namespace Checkout.Payments
{
    public class ProductSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        [Fact]
        public void ShouldOmitTypeAndSubTypeWhenNull()
        {
            var product = new Product
            {
                Name = "Gold Necklace",
                Quantity = 1,
                UnitPrice = 5000
            };

            var json = Serializer.Serialize(product);

            json.ShouldNotContain("\"type\"");
            json.ShouldNotContain("\"sub_type\"");
        }

        [Fact]
        public void ShouldSerializeTypeWhenSet()
        {
            var product = new Product
            {
                Type = ItemType.Physical,
                Name = "Gold Necklace",
                Quantity = 1,
                UnitPrice = 5000
            };

            var json = Serializer.Serialize(product);

            json.ShouldContain("\"type\":\"physical\"");
        }

        [Fact]
        public void ShouldSerializeSubTypeWhenSet()
        {
            var product = new Product
            {
                Type = ItemType.Digital,
                SubType = ItemSubType.Cryptocurrency,
                Name = "Bitcoin",
                Quantity = 1,
                UnitPrice = 10000
            };

            var json = Serializer.Serialize(product);

            json.ShouldContain("\"type\":\"digital\"");
            json.ShouldContain("\"sub_type\":\"cryptocurrency\"");
        }

        [Fact]
        public void ShouldRoundTripAllProperties()
        {
            var original = new Product
            {
                Type = ItemType.Physical,
                SubType = ItemSubType.Nft,
                Name = "Gold Necklace",
                Quantity = 2,
                UnitPrice = 5000,
                Reference = "858818ac",
                CommodityCode = "DEF123",
                UnitOfMeasure = "metres",
                TotalAmount = 29000,
                TaxAmount = 1000,
                TaxRate = 2000,
                DiscountAmount = 1000,
                WxpayGoodsId = "1001",
                ImageUrl = "https://example.com/image.jpg",
                Url = "https://example.com/product",
                Sku = "SKU-001"
            };

            var json = Serializer.Serialize(original);
            var deserialized = (Product)Serializer.Deserialize(json, typeof(Product));

            deserialized.ShouldNotBeNull();
            deserialized.Type.ShouldBe(ItemType.Physical);
            deserialized.SubType.ShouldBe(ItemSubType.Nft);
            deserialized.Name.ShouldBe("Gold Necklace");
            deserialized.Quantity.ShouldBe(2L);
            deserialized.UnitPrice.ShouldBe(5000L);
            deserialized.Reference.ShouldBe("858818ac");
            deserialized.CommodityCode.ShouldBe("DEF123");
            deserialized.UnitOfMeasure.ShouldBe("metres");
            deserialized.TotalAmount.ShouldBe(29000L);
            deserialized.TaxAmount.ShouldBe(1000L);
            deserialized.TaxRate.ShouldBe(2000L);
            deserialized.DiscountAmount.ShouldBe(1000L);
            deserialized.WxpayGoodsId.ShouldBe("1001");
            deserialized.ImageUrl.ShouldBe("https://example.com/image.jpg");
            deserialized.Url.ShouldBe("https://example.com/product");
            deserialized.Sku.ShouldBe("SKU-001");
        }

        [Fact]
        public void ShouldDeserializeTypeAndSubTypeFromJson()
        {
            const string json = @"{
                ""type"": ""digital"",
                ""sub_type"": ""stablecoin"",
                ""name"": ""USDC"",
                ""quantity"": 1,
                ""unit_price"": 50
            }";

            var product = (Product)Serializer.Deserialize(json, typeof(Product));

            product.ShouldNotBeNull();
            product.Type.ShouldBe(ItemType.Digital);
            product.SubType.ShouldBe(ItemSubType.Stablecoin);
            product.Name.ShouldBe("USDC");
        }

        [Fact]
        public void ShouldDeserializeNullTypeAndSubTypeFromJsonWithoutThoseFields()
        {
            const string json = @"{""name"": ""Widget"", ""quantity"": 3, ""unit_price"": 100}";

            var product = (Product)Serializer.Deserialize(json, typeof(Product));

            product.ShouldNotBeNull();
            product.Type.ShouldBeNull();
            product.SubType.ShouldBeNull();
            product.Name.ShouldBe("Widget");
        }
    }
}
