using Checkout.Payments.Request;
using Checkout.Payments.Request.Source;
using Shouldly;
using Xunit;

namespace Checkout.Payments
{
    public class PaymentRequestFallbackSourceSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        [Fact]
        public void ShouldSerializeFallbackSource()
        {
            var request = new PaymentRequest
            {
                FallbackSource = new RequestCardSource
                {
                    Number = "4543474002249996",
                    ExpiryMonth = 6,
                    ExpiryYear = 2030,
                    Cvv = "956"
                }
            };

            var json = Serializer.Serialize(request);

            json.ShouldContain("\"fallback_source\"");
            json.ShouldContain("4543474002249996");
        }

        [Fact]
        public void ShouldRoundTripSerializeFallbackSource()
        {
            var original = new PaymentRequest
            {
                FallbackSource = new RequestCardSource
                {
                    Number = "4543474002249996",
                    ExpiryMonth = 6,
                    ExpiryYear = 2030,
                    Cvv = "956",
                    Name = "Bruce Wayne"
                }
            };

            var json = Serializer.Serialize(original);
            var deserialized = (PaymentRequest)Serializer.Deserialize(json, typeof(PaymentRequest));

            deserialized.FallbackSource.ShouldNotBeNull();
            deserialized.FallbackSource.Number.ShouldBe("4543474002249996");
            deserialized.FallbackSource.ExpiryMonth.ShouldBe(6);
            deserialized.FallbackSource.ExpiryYear.ShouldBe(2030);
            deserialized.FallbackSource.Cvv.ShouldBe("956");
            deserialized.FallbackSource.Name.ShouldBe("Bruce Wayne");
        }

        [Fact]
        public void ShouldDeserializeSwaggerExample()
        {
            const string json = @"{
                ""fallback_source"": {
                    ""type"": ""card"",
                    ""number"": ""4543474002249996"",
                    ""expiry_month"": 6,
                    ""expiry_year"": 2030,
                    ""cvv"": ""956""
                }
            }";

            var request = (PaymentRequest)Serializer.Deserialize(json, typeof(PaymentRequest));

            request.FallbackSource.ShouldNotBeNull();
            request.FallbackSource.Number.ShouldBe("4543474002249996");
            request.FallbackSource.ExpiryMonth.ShouldBe(6);
            request.FallbackSource.ExpiryYear.ShouldBe(2030);
        }
    }
}
