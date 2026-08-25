using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace Checkout.Payments
{
    public class VoidRequestSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        private static VoidRequest CreateFullyPopulated()
        {
            return new VoidRequest
            {
                Amount = 100L,
                Reference = "ORD-5023-4E89",
                Metadata = new Dictionary<string, object> { { "coupon_code", "NY2018" } }
            };
        }

        [Fact]
        public void ShouldSerializeWithRequiredProperties()
        {
            var request = new VoidRequest();

            Should.NotThrow(() => Serializer.Serialize(request));
        }

        [Fact]
        public void ShouldSerializeAllOptionalPropertiesToSnakeCase()
        {
            var request = CreateFullyPopulated();

            var json = Serializer.Serialize(request);

            json.ShouldContain("\"amount\":100");
            json.ShouldContain("\"reference\":\"ORD-5023-4E89\"");
            json.ShouldContain("\"metadata\":{\"coupon_code\":\"NY2018\"}");
        }

        [Fact]
        public void ShouldOmitAmountWhenNotSet()
        {
            var request = new VoidRequest { Reference = "ORD-5023-4E89" };

            var json = Serializer.Serialize(request);

            json.ShouldNotContain("amount");
        }

        [Fact]
        public void ShouldRoundTripSerializeAllProperties()
        {
            var original = CreateFullyPopulated();

            var json = Serializer.Serialize(original);
            var deserialized = (VoidRequest)Serializer.Deserialize(json, typeof(VoidRequest));

            deserialized.ShouldNotBeNull();
            deserialized.Amount.ShouldBe(original.Amount);
            deserialized.Reference.ShouldBe(original.Reference);
            deserialized.Metadata.ShouldContainKeyAndValue("coupon_code", "NY2018");
        }

        [Fact]
        public void ShouldDeserializeSwaggerExample()
        {
            const string json = @"{
                ""amount"": 100,
                ""reference"": ""ORD-5023-4E89"",
                ""metadata"": {
                    ""coupon_code"": ""NY2018""
                }
            }";

            var request = (VoidRequest)Serializer.Deserialize(json, typeof(VoidRequest));

            request.ShouldNotBeNull();
            request.Amount.ShouldBe(100L);
            request.Reference.ShouldBe("ORD-5023-4E89");
            request.Metadata.ShouldContainKeyAndValue("coupon_code", "NY2018");
        }
    }
}
