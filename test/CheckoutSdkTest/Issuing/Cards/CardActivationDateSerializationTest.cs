using Checkout.Issuing.Cards.Requests.Create;
using Checkout.Issuing.Cards.Requests.Update;
using Checkout.Issuing.Common.Responses;
using Shouldly;
using Xunit;

namespace Checkout.Issuing.Cards
{
    public class CardActivationDateSerializationTest
    {
        private readonly JsonSerializer _serializer = new JsonSerializer();

        [Fact]
        public void ShouldSerializeActivationDateOnCreateRequest()
        {
            var request = new VirtualCardCreateRequest
            {
                CardholderId = "crh_test",
                ActivationDate = "2026-06-01T10:00Z",
                RevocationDate = "2026-07-01"
            };

            var json = _serializer.Serialize(request);

            json.ShouldContain("\"activation_date\"");
            json.ShouldContain("2026-06-01T10:00Z");
            // revocation_date is a string, so date-only values (which DateTime cannot round-trip) are supported
            json.ShouldContain("\"revocation_date\"");
            json.ShouldContain("2026-07-01");
        }

        [Fact]
        public void ShouldSerializeActivationAndRevocationDateOnUpdateRequest()
        {
            var request = new CardsUpdateRequest
            {
                Reference = "X-123",
                // activation_date supports a round-hour datetime; revocation_date is date-only (yyyy-MM-dd)
                ActivationDate = "2026-06-01T10:00Z",
                RevocationDate = "2026-07-01"
            };

            var json = _serializer.Serialize(request);

            json.ShouldContain("\"activation_date\"");
            json.ShouldContain("2026-06-01T10:00Z");
            json.ShouldContain("\"revocation_date\"");
            json.ShouldContain("2026-07-01");
        }

        [Fact]
        public void ShouldDeserializeActivationDateOnUpdateRequest()
        {
            const string json = @"{ ""reference"": ""X-123"", ""activation_date"": ""2026-06-01T10:00Z"", ""revocation_date"": ""2026-07-01"" }";

            var request = (CardsUpdateRequest)_serializer.Deserialize(json, typeof(CardsUpdateRequest));

            request.ShouldNotBeNull();
            request.ActivationDate.ShouldBe("2026-06-01T10:00Z");
            request.RevocationDate.ShouldBe("2026-07-01");
        }

        [Fact]
        public void ShouldDeserializeActivationDateOnCardResponse()
        {
            const string json = @"{ ""type"": ""virtual"", ""id"": ""crd_test"", ""user_id"": ""usr_test"", ""scheme"": ""mastercard"", ""activation_date"": ""2026-06-01T10:00Z"", ""revocation_date"": ""2026-07-01"" }";

            var response = (AbstractCardResponse)_serializer.Deserialize(json, typeof(AbstractCardResponse));

            response.ShouldNotBeNull();
            response.UserId.ShouldBe("usr_test");
            response.Scheme.ShouldBe(Common.IssuingScheme.Mastercard);
            response.ActivationDate.ShouldBe("2026-06-01T10:00Z");
            response.RevocationDate.ShouldBe("2026-07-01");
        }

        [Fact]
        public void ShouldRoundTripActivationDateOnUpdateRequest()
        {
            var original = new CardsUpdateRequest { ActivationDate = "2026-06-01T10:00Z", RevocationDate = "2026-07-01" };

            var deserialized = (CardsUpdateRequest)_serializer
                .Deserialize(_serializer.Serialize(original), typeof(CardsUpdateRequest));

            deserialized.ActivationDate.ShouldBe(original.ActivationDate);
            deserialized.RevocationDate.ShouldBe(original.RevocationDate);
        }
    }
}
