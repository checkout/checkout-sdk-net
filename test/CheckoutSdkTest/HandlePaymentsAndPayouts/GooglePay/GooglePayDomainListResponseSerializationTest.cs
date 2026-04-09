using Checkout.HandlePaymentsAndPayouts.GooglePay.Responses;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace Checkout.HandlePaymentsAndPayouts.GooglePay
{
    public class GooglePayDomainListResponseSerializationTest
    {
        [Fact]
        public void ShouldSerializeWithDomains()
        {
            var response = new GooglePayDomainListResponse
            {
                Domains = new List<string> { "example.com", "shop.example.com" }
            };

            Should.NotThrow(() => new JsonSerializer().Serialize(response));
        }

        [Fact]
        public void ShouldDeserializeSwaggerExample()
        {
            const string json = @"{
                ""domains"": [""example.com"", ""shop.example.com""]
            }";

            var response = (GooglePayDomainListResponse)new JsonSerializer()
                .Deserialize(json, typeof(GooglePayDomainListResponse));

            response.ShouldNotBeNull();
            response.Domains.ShouldNotBeNull();
            response.Domains.Count.ShouldBe(2);
            response.Domains[0].ShouldBe("example.com");
        }

        [Fact]
        public void ShouldRoundTripSerialize()
        {
            var original = new GooglePayDomainListResponse
            {
                Domains = new List<string> { "example.com" }
            };
            var serializer = new JsonSerializer();

            var json = serializer.Serialize(original);
            var deserialized = (GooglePayDomainListResponse)serializer.Deserialize(json, typeof(GooglePayDomainListResponse));

            deserialized.Domains.ShouldNotBeNull();
            deserialized.Domains[0].ShouldBe("example.com");
        }
    }
}
