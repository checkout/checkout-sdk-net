using Checkout.AgenticCommerce.Responses;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace Checkout.AgenticCommerce
{
    public class DelegatedPaymentResponseSerializationTest
    {
        [Fact]
        public void ShouldSerializeWithRequiredProperties()
        {
            var response = new DelegatedPaymentResponse
            {
                Id = "vt_abc123def456ghi789",
                Created = new DateTime(2026, 3, 11, 10, 30, 0),
                Metadata = new Dictionary<string, string> { { "psp", "checkout.com" } }
            };

            Should.NotThrow(() => new JsonSerializer().Serialize(response));
        }

        [Fact]
        public void ShouldDeserializeSwaggerExample()
        {
            const string json = @"{
                ""id"": ""vt_abc123def456ghi789"",
                ""created"": ""2026-03-11T10:30:00Z"",
                ""metadata"": { ""psp"": ""checkout.com"" }
            }";

            var response = (DelegatedPaymentResponse)new JsonSerializer()
                .Deserialize(json, typeof(DelegatedPaymentResponse));

            response.ShouldNotBeNull();
            response.Id.ShouldBe("vt_abc123def456ghi789");
            response.Created.ShouldNotBeNull();
            response.Metadata.ShouldNotBeNull();
            response.Metadata["psp"].ShouldBe("checkout.com");
        }

        [Fact]
        public void ShouldRoundTripSerialize()
        {
            var original = new DelegatedPaymentResponse
            {
                Id = "vt_test123",
                Created = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                Metadata = new Dictionary<string, string> { { "key", "value" } }
            };
            var serializer = new JsonSerializer();

            var json = serializer.Serialize(original);
            var deserialized = (DelegatedPaymentResponse)serializer.Deserialize(json, typeof(DelegatedPaymentResponse));

            deserialized.Id.ShouldBe(original.Id);
            deserialized.Metadata["key"].ShouldBe("value");
        }
    }
}
