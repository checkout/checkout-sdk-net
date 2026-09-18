using Checkout.Inventory.Entities;
using Checkout.Inventory.Requests;
using Checkout.Inventory.Responses;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace Checkout.Inventory
{
    /// <summary>
    /// Schema validation tests for Checkout.Inventory.
    /// Grouped by domain; each section below covers one subject.
    /// </summary>
    public class InventorySerializationTest
    {
        // ------------------------------------------------------------------------
        // InventoryAdjustmentRequest
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeWithRequiredPropertiesForInventoryAdjustmentRequest()
        {
            var request = new InventoryAdjustmentRequest
            {
                VariantId = "var_123",
                Delta = -5,
                Reason = "damaged in transit"
            };

            Should.NotThrow(() => new JsonSerializer().Serialize(request));
        }

        [Fact]
        public void ShouldRoundTripSerializeForInventoryAdjustmentRequest()
        {
            var original = new InventoryAdjustmentRequest
            {
                VariantId = "var_123",
                Delta = 10,
                Reason = "restock"
            };
            var serializer = new JsonSerializer();

            var json = serializer.Serialize(original);
            var deserialized = (InventoryAdjustmentRequest)serializer.Deserialize(json, typeof(InventoryAdjustmentRequest));

            deserialized.VariantId.ShouldBe(original.VariantId);
            deserialized.Delta.ShouldBe(original.Delta);
            deserialized.Reason.ShouldBe(original.Reason);
        }

        [Fact]
        public void ShouldDeserializeSwaggerExampleForInventoryLevels()
        {
            const string json = @"{
                ""variant_id"": ""var_123"",
                ""on_hand"": 100,
                ""reserved"": 10,
                ""safety_stock"": 5,
                ""available"": 85,
                ""state"": ""in_stock"",
                ""source"": ""managed"",
                ""created_on"": ""2026-09-10T09:15:30Z"",
                ""modified_on"": ""2026-09-10T09:15:30Z"",
                ""_links"": {
                    ""self"": { ""href"": ""https://api.checkout.com/inventory/var_123"", ""actions"": [ ""GET"" ], ""types"": [ ""application/json"" ] },
                    ""set"": { ""href"": ""https://api.checkout.com/inventory/var_123"", ""actions"": [ ""PUT"" ], ""types"": [ ""application/json"" ] }
                }
            }";

            var response = (InventoryLevels)new JsonSerializer().Deserialize(json, typeof(InventoryLevels));

            response.ShouldNotBeNull();
            response.VariantId.ShouldBe("var_123");
            response.OnHand.ShouldBe(100);
            response.Available.ShouldBe(85);
            response.State.ShouldBe(InventoryLevelState.InStock);
            response.Source.ShouldBe(InventorySource.Managed);
            response.Links.ShouldNotBeNull();
            response.Links["self"].Href.ShouldBe("https://api.checkout.com/inventory/var_123");
            response.Links["set"].Actions.ShouldContain("PUT");
        }

        // ------------------------------------------------------------------------
        // InventoryLevels (with embedded product)
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeWithAllOptionalPropertiesForInventoryLevels()
        {
            var response = new InventoryLevels
            {
                VariantId = "var_123",
                OnHand = 100,
                Reserved = 10,
                SafetyStock = 5,
                Available = 85,
                State = InventoryLevelState.Limited,
                Source = InventorySource.Sync,
                CreatedOn = new DateTime(2026, 9, 10, 9, 15, 30, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2026, 9, 10, 9, 15, 30, DateTimeKind.Utc),
                Product = CreateFullyPopulatedProductKnowledge(),
                Links = new Dictionary<string, InventoryHalLink>
                {
                    { "self", new InventoryHalLink { Href = "https://api.checkout.com/inventory/var_123", Actions = new[] { "GET" }, Types = new[] { "application/json" } } },
                    { "set", new InventoryHalLink { Href = "https://api.checkout.com/inventory/var_123", Actions = new[] { "PUT" }, Types = new[] { "application/json" } } }
                }
            };

            Should.NotThrow(() => new JsonSerializer().Serialize(response));
        }

        [Fact]
        public void ShouldRoundTripSerializeForInventoryLevels()
        {
            var original = new InventoryLevels
            {
                VariantId = "var_123",
                OnHand = 100,
                Reserved = 10,
                SafetyStock = 5,
                Available = 85,
                State = InventoryLevelState.OutOfStock,
                Source = InventorySource.Managed,
                CreatedOn = new DateTime(2026, 9, 10, 9, 15, 30, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2026, 9, 10, 9, 15, 30, DateTimeKind.Utc)
            };
            var serializer = new JsonSerializer();

            var json = serializer.Serialize(original);
            var deserialized = (InventoryLevels)serializer.Deserialize(json, typeof(InventoryLevels));

            deserialized.VariantId.ShouldBe(original.VariantId);
            deserialized.State.ShouldBe(InventoryLevelState.OutOfStock);
            deserialized.Source.ShouldBe(InventorySource.Managed);
            deserialized.Available.ShouldBe(original.Available);
        }

        // ------------------------------------------------------------------------
        // InventoryReservationRequest / InventoryReservationItem / InventoryReservation
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeWithRequiredPropertiesForInventoryReservationRequest()
        {
            var request = new InventoryReservationRequest
            {
                OwnerType = "order",
                OwnerReference = "order_123",
                Items = new List<InventoryReservationItem>
                {
                    new InventoryReservationItem { VariantId = "var_123", Quantity = 2 }
                }
            };

            Should.NotThrow(() => new JsonSerializer().Serialize(request));
        }

        [Fact]
        public void ShouldSerializeWithAllOptionalPropertiesForInventoryReservationRequest()
        {
            var request = new InventoryReservationRequest
            {
                OwnerType = "order",
                OwnerReference = "order_123",
                Items = new List<InventoryReservationItem>
                {
                    new InventoryReservationItem { VariantId = "var_123", Quantity = 2 },
                    new InventoryReservationItem { VariantId = "var_456", Quantity = 1 }
                },
                TtlSeconds = 1800
            };

            Should.NotThrow(() => new JsonSerializer().Serialize(request));
        }

        [Fact]
        public void ShouldRoundTripSerializeForInventoryReservationRequest()
        {
            var original = new InventoryReservationRequest
            {
                OwnerType = "cart",
                OwnerReference = "cart_789",
                Items = new List<InventoryReservationItem>
                {
                    new InventoryReservationItem { VariantId = "var_123", Quantity = 3 }
                },
                TtlSeconds = 900
            };
            var serializer = new JsonSerializer();

            var json = serializer.Serialize(original);
            var deserialized = (InventoryReservationRequest)serializer.Deserialize(json, typeof(InventoryReservationRequest));

            deserialized.OwnerType.ShouldBe(original.OwnerType);
            deserialized.OwnerReference.ShouldBe(original.OwnerReference);
            deserialized.Items.Count.ShouldBe(1);
            deserialized.Items[0].VariantId.ShouldBe("var_123");
            deserialized.TtlSeconds.ShouldBe(900);
        }

        [Fact]
        public void ShouldDeserializeSwaggerExampleForInventoryReservation()
        {
            const string json = @"{
                ""id"": ""rsv_abc123"",
                ""state"": ""held"",
                ""owner_type"": ""order"",
                ""owner_reference"": ""order_123"",
                ""items"": [ { ""variant_id"": ""var_123"", ""quantity"": 2 } ],
                ""expires_at"": ""2026-09-10T09:30:30Z"",
                ""created_on"": ""2026-09-10T09:15:30Z"",
                ""_links"": {
                    ""self"": { ""href"": ""https://api.checkout.com/inventory/reservations/rsv_abc123"" },
                    ""commit"": { ""href"": ""https://api.checkout.com/inventory/reservations/rsv_abc123/commit"" },
                    ""release"": { ""href"": ""https://api.checkout.com/inventory/reservations/rsv_abc123/release"" }
                }
            }";

            var response = (InventoryReservation)new JsonSerializer().Deserialize(json, typeof(InventoryReservation));

            response.ShouldNotBeNull();
            response.Id.ShouldBe("rsv_abc123");
            response.State.ShouldBe(InventoryReservationState.Held);
            response.Items.Count.ShouldBe(1);
            response.Items[0].Quantity.ShouldBe(2);
            response.Links["commit"].ShouldNotBeNull();
        }

        [Fact]
        public void ShouldRoundTripSerializeForInventoryReservation()
        {
            var original = new InventoryReservation
            {
                Id = "rsv_abc123",
                State = InventoryReservationState.Committed,
                OwnerType = "order",
                OwnerReference = "order_123",
                Items = new List<InventoryReservationItem>
                {
                    new InventoryReservationItem { VariantId = "var_123", Quantity = 2 }
                },
                ExpiresAt = new DateTime(2026, 9, 10, 9, 30, 30, DateTimeKind.Utc),
                CreatedOn = new DateTime(2026, 9, 10, 9, 15, 30, DateTimeKind.Utc)
            };
            var serializer = new JsonSerializer();

            var json = serializer.Serialize(original);
            var deserialized = (InventoryReservation)serializer.Deserialize(json, typeof(InventoryReservation));

            deserialized.Id.ShouldBe(original.Id);
            deserialized.State.ShouldBe(InventoryReservationState.Committed);
            deserialized.Items[0].VariantId.ShouldBe("var_123");
        }

        // ------------------------------------------------------------------------
        // InventorySetLevelsRequest
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeWithRequiredPropertiesForInventorySetLevelsRequest()
        {
            var request = new InventorySetLevelsRequest { OnHand = 50 };

            Should.NotThrow(() => new JsonSerializer().Serialize(request));
        }

        [Fact]
        public void ShouldSerializeWithAllOptionalPropertiesForInventorySetLevelsRequest()
        {
            var request = new InventorySetLevelsRequest
            {
                OnHand = 50,
                SafetyStock = 5,
                Reason = "initial stock load"
            };

            Should.NotThrow(() => new JsonSerializer().Serialize(request));
        }

        [Fact]
        public void ShouldRoundTripSerializeForInventorySetLevelsRequest()
        {
            var original = new InventorySetLevelsRequest
            {
                OnHand = 75,
                SafetyStock = 10,
                Reason = "quarterly recount"
            };
            var serializer = new JsonSerializer();

            var json = serializer.Serialize(original);
            var deserialized = (InventorySetLevelsRequest)serializer.Deserialize(json, typeof(InventorySetLevelsRequest));

            deserialized.OnHand.ShouldBe(original.OnHand);
            deserialized.SafetyStock.ShouldBe(original.SafetyStock);
            deserialized.Reason.ShouldBe(original.Reason);
        }

        // ------------------------------------------------------------------------
        // InventorySetProductRequest / InventoryProductKnowledge
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeWithRequiredPropertiesForInventorySetProductRequest()
        {
            var request = new InventorySetProductRequest
            {
                Title = "Blue T-Shirt",
                Description = "A comfortable cotton t-shirt.",
                ProductUrl = "https://shop.example.com/products/blue-tshirt",
                ImageUrl = "https://shop.example.com/images/blue-tshirt.png"
            };

            Should.NotThrow(() => new JsonSerializer().Serialize(request));
        }

        [Fact]
        public void ShouldSerializeWithAllOptionalPropertiesForInventorySetProductRequest()
        {
            var request = new InventorySetProductRequest
            {
                Title = "Blue T-Shirt",
                Description = "A comfortable cotton t-shirt.",
                ProductUrl = "https://shop.example.com/products/blue-tshirt",
                ImageUrl = "https://shop.example.com/images/blue-tshirt.png",
                AdditionalImageUrls = new List<string> { "https://shop.example.com/images/blue-tshirt-2.png" },
                VideoUrl = "https://shop.example.com/videos/blue-tshirt.mp4",
                Model3dUrl = "https://shop.example.com/models/blue-tshirt.glb",
                Sku = "SKU-123",
                Gtin = "00012345678905",
                Mpn = "MPN-123",
                Brand = "ExampleBrand",
                Category = "Apparel",
                Price = new InventoryMoney { Amount = 2000, Currency = "USD" },
                SalePrice = new InventoryMoney { Amount = 1500, Currency = "USD" },
                SalePriceStartsAt = new DateTime(2026, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                SalePriceEndsAt = new DateTime(2026, 9, 30, 0, 0, 0, DateTimeKind.Utc),
                GroupId = "grp_123",
                GroupTitle = "Blue T-Shirt (all sizes)",
                Color = "blue",
                Size = "M",
                SizeSystem = "US",
                Gender = "unisex",
                Condition = InventoryProductCondition.New,
                Material = "cotton",
                AgeGroup = "adult",
                Length = 10.5,
                Width = 5.5,
                Height = 1.2,
                DimensionUnit = "in",
                Weight = 0.3,
                WeightUnit = "kg",
                ExpirationDate = new DateTime(2027, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                HarmonizedSystemCode = "6109.10",
                CountryOfOrigin = "US",
                SellerName = "Example Seller",
                SellerUrl = "https://shop.example.com",
                SellerPrivacyPolicy = "https://shop.example.com/privacy",
                SellerTos = "https://shop.example.com/tos"
            };

            Should.NotThrow(() => new JsonSerializer().Serialize(request));
        }

        [Fact]
        public void ShouldRoundTripSerializeForInventorySetProductRequest()
        {
            var original = new InventorySetProductRequest
            {
                Title = "Blue T-Shirt",
                Description = "A comfortable cotton t-shirt.",
                ProductUrl = "https://shop.example.com/products/blue-tshirt",
                ImageUrl = "https://shop.example.com/images/blue-tshirt.png",
                Price = new InventoryMoney { Amount = 2000, Currency = "USD" },
                Condition = InventoryProductCondition.Refurbished
            };
            var serializer = new JsonSerializer();

            var json = serializer.Serialize(original);
            var deserialized = (InventorySetProductRequest)serializer.Deserialize(json, typeof(InventorySetProductRequest));

            deserialized.Title.ShouldBe(original.Title);
            deserialized.Price.Amount.ShouldBe(2000);
            deserialized.Price.Currency.ShouldBe("USD");
            deserialized.Condition.ShouldBe(InventoryProductCondition.Refurbished);
        }

        [Fact]
        public void ShouldDeserializeSwaggerExampleForInventoryProductKnowledge()
        {
            const string json = @"{
                ""variant_id"": ""var_123"",
                ""title"": ""Blue T-Shirt"",
                ""description"": ""A comfortable cotton t-shirt."",
                ""product_url"": ""https://shop.example.com/products/blue-tshirt"",
                ""image_url"": ""https://shop.example.com/images/blue-tshirt.png"",
                ""price"": { ""amount"": 2000, ""currency"": ""USD"" },
                ""condition"": ""new"",
                ""created_on"": ""2026-09-10T09:15:30Z"",
                ""modified_on"": ""2026-09-10T09:15:30Z"",
                ""_links"": {
                    ""self"": { ""href"": ""https://api.checkout.com/inventory/var_123/product"" },
                    ""set"": { ""href"": ""https://api.checkout.com/inventory/var_123/product"" },
                    ""delete"": { ""href"": ""https://api.checkout.com/inventory/var_123/product"" }
                }
            }";

            var response = (InventoryProductKnowledge)new JsonSerializer()
                .Deserialize(json, typeof(InventoryProductKnowledge));

            response.ShouldNotBeNull();
            response.VariantId.ShouldBe("var_123");
            response.Title.ShouldBe("Blue T-Shirt");
            response.Price.Amount.ShouldBe(2000);
            response.Condition.ShouldBe(InventoryProductCondition.New);
            response.Links["delete"].ShouldNotBeNull();
        }

        [Fact]
        public void ShouldRoundTripSerializeForInventoryProductKnowledge()
        {
            var original = CreateFullyPopulatedProductKnowledge();
            var serializer = new JsonSerializer();

            var json = serializer.Serialize(original);
            var deserialized = (InventoryProductKnowledge)serializer.Deserialize(json, typeof(InventoryProductKnowledge));

            deserialized.VariantId.ShouldBe(original.VariantId);
            deserialized.Title.ShouldBe(original.Title);
            deserialized.Price.Amount.ShouldBe(original.Price.Amount);
            deserialized.Condition.ShouldBe(original.Condition);
        }

        private static InventoryProductKnowledge CreateFullyPopulatedProductKnowledge()
        {
            return new InventoryProductKnowledge
            {
                VariantId = "var_123",
                Title = "Blue T-Shirt",
                Description = "A comfortable cotton t-shirt.",
                ProductUrl = "https://shop.example.com/products/blue-tshirt",
                ImageUrl = "https://shop.example.com/images/blue-tshirt.png",
                AdditionalImageUrls = new List<string> { "https://shop.example.com/images/blue-tshirt-2.png" },
                VideoUrl = "https://shop.example.com/videos/blue-tshirt.mp4",
                Model3dUrl = "https://shop.example.com/models/blue-tshirt.glb",
                Sku = "SKU-123",
                Gtin = "00012345678905",
                Mpn = "MPN-123",
                Brand = "ExampleBrand",
                Category = "Apparel",
                Price = new InventoryMoney { Amount = 2000, Currency = "USD" },
                SalePrice = new InventoryMoney { Amount = 1500, Currency = "USD" },
                SalePriceStartsAt = new DateTime(2026, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                SalePriceEndsAt = new DateTime(2026, 9, 30, 0, 0, 0, DateTimeKind.Utc),
                GroupId = "grp_123",
                GroupTitle = "Blue T-Shirt (all sizes)",
                Color = "blue",
                Size = "M",
                SizeSystem = "US",
                Gender = "unisex",
                Condition = InventoryProductCondition.New,
                Material = "cotton",
                AgeGroup = "adult",
                Length = 10.5,
                Width = 5.5,
                Height = 1.2,
                DimensionUnit = "in",
                Weight = 0.3,
                WeightUnit = "kg",
                ExpirationDate = new DateTime(2027, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                HarmonizedSystemCode = "6109.10",
                CountryOfOrigin = "US",
                SellerName = "Example Seller",
                SellerUrl = "https://shop.example.com",
                SellerPrivacyPolicy = "https://shop.example.com/privacy",
                SellerTos = "https://shop.example.com/tos",
                CreatedOn = new DateTime(2026, 9, 10, 9, 15, 30, DateTimeKind.Utc),
                ModifiedOn = new DateTime(2026, 9, 10, 9, 15, 30, DateTimeKind.Utc),
                Links = new Dictionary<string, InventoryHalLink>
                {
                    { "self", new InventoryHalLink { Href = "https://api.checkout.com/inventory/var_123/product" } },
                    { "set", new InventoryHalLink { Href = "https://api.checkout.com/inventory/var_123/product" } },
                    { "delete", new InventoryHalLink { Href = "https://api.checkout.com/inventory/var_123/product" } }
                }
            };
        }

        // ------------------------------------------------------------------------
        // InventoryLevelsQueryFilter
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeWithAllOptionalPropertiesForInventoryLevelsQueryFilter()
        {
            var filter = new InventoryLevelsQueryFilter { Expand = "product" };

            Should.NotThrow(() => new JsonSerializer().Serialize(filter));
        }
    }
}
