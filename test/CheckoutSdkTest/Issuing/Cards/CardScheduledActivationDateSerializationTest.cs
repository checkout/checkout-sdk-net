using Checkout.Issuing.Cards.Requests.Create;
using Checkout.Issuing.Cards.Requests.Update;
using Checkout.Issuing.Common.Responses;
using Shouldly;
using Xunit;

namespace Checkout.Issuing.Cards
{
    /// <summary>
    /// Schema validation tests for the issuing card scheduled_activation_date and revocation_date
    /// fields.
    ///
    /// Swagger 2026-09-02 replaced activation_date with scheduled_activation_date on
    /// add-card-request, get-card-response and update-card-request. The old key is gone from the
    /// API, so these tests assert the new key is emitted and the old one is not.
    /// </summary>
    public class CardScheduledActivationDateSerializationTest
    {
        private readonly JsonSerializer _serializer = new JsonSerializer();

        [Fact]
        public void ShouldSerializeScheduledActivationDateOnCreateRequest()
        {
            var request = new VirtualCardCreateRequest
            {
                CardholderId = "crh_test",
                ScheduledActivationDate = "2026-06-01T10:00Z",
                RevocationDate = "2026-07-01"
            };

            var json = _serializer.Serialize(request);

            json.ShouldContain("\"scheduled_activation_date\"");
            json.ShouldContain("2026-06-01T10:00Z");
            // revocation_date is a string, so date-only values (which DateTime cannot round-trip) are supported
            json.ShouldContain("\"revocation_date\"");
            json.ShouldContain("2026-07-01");
        }

        [Fact]
        public void ShouldNotSerializeTheRemovedActivationDateKeyOnCreateRequest()
        {
            var request = new VirtualCardCreateRequest
            {
                CardholderId = "crh_test",
                ScheduledActivationDate = "2026-06-01T10:00Z"
            };

            var json = _serializer.Serialize(request);

            json.ShouldNotContain("\"activation_date\"");
        }

        [Fact]
        public void ShouldSerializeScheduledActivationAndRevocationDateOnUpdateRequest()
        {
            var request = new CardsUpdateRequest
            {
                Reference = "X-123",
                // scheduled_activation_date supports a round-hour datetime; revocation_date is date-only (yyyy-MM-dd)
                ScheduledActivationDate = "2026-06-01T10:00Z",
                RevocationDate = "2026-07-01"
            };

            var json = _serializer.Serialize(request);

            json.ShouldContain("\"scheduled_activation_date\"");
            json.ShouldContain("2026-06-01T10:00Z");
            json.ShouldContain("\"revocation_date\"");
            json.ShouldContain("2026-07-01");
        }

        [Fact]
        public void ShouldNotSerializeTheRemovedActivationDateKeyOnUpdateRequest()
        {
            var request = new CardsUpdateRequest { ScheduledActivationDate = "2026-06-01T10:00Z" };

            var json = _serializer.Serialize(request);

            json.ShouldNotContain("\"activation_date\"");
        }

        [Fact]
        public void ShouldDeserializeScheduledActivationDateOnUpdateRequest()
        {
            const string json = @"{ ""reference"": ""X-123"", ""scheduled_activation_date"": ""2026-06-01T10:00Z"", ""revocation_date"": ""2026-07-01"" }";

            var request = (CardsUpdateRequest)_serializer.Deserialize(json, typeof(CardsUpdateRequest));

            request.ShouldNotBeNull();
            request.ScheduledActivationDate.ShouldBe("2026-06-01T10:00Z");
            request.RevocationDate.ShouldBe("2026-07-01");
        }

        [Fact]
        public void ShouldNotMapTheRemovedActivationDateKeyOnUpdateRequest()
        {
            const string json = @"{ ""activation_date"": ""2026-06-01T10:00Z"" }";

            var request = (CardsUpdateRequest)_serializer.Deserialize(json, typeof(CardsUpdateRequest));

            request.ShouldNotBeNull();
            request.ScheduledActivationDate.ShouldBeNull();
        }

        [Fact]
        public void ShouldDeserializeScheduledActivationDateOnCardResponse()
        {
            const string json = @"{ ""type"": ""virtual"", ""id"": ""crd_test"", ""user_id"": ""usr_test"", ""scheme"": ""mastercard"", ""scheduled_activation_date"": ""2026-06-01T10:00Z"", ""revocation_date"": ""2026-07-01"" }";

            var response = (AbstractCardResponse)_serializer.Deserialize(json, typeof(AbstractCardResponse));

            response.ShouldNotBeNull();
            response.UserId.ShouldBe("usr_test");
            response.Scheme.ShouldBe(Common.IssuingScheme.Mastercard);
            response.ScheduledActivationDate.ShouldBe("2026-06-01T10:00Z");
            response.RevocationDate.ShouldBe("2026-07-01");
        }

        [Fact]
        public void ShouldRoundTripScheduledActivationDateOnUpdateRequest()
        {
            var original = new CardsUpdateRequest { ScheduledActivationDate = "2026-06-01T10:00Z", RevocationDate = "2026-07-01" };

            var deserialized = (CardsUpdateRequest)_serializer
                .Deserialize(_serializer.Serialize(original), typeof(CardsUpdateRequest));

            deserialized.ScheduledActivationDate.ShouldBe(original.ScheduledActivationDate);
            deserialized.RevocationDate.ShouldBe(original.RevocationDate);
        }
    }
}
