using Checkout.Issuing.Cards.Requests.Create;
using Checkout.Issuing.Cards.Requests.Update;
using Checkout.Issuing.Cards.Responses.Activate;
using Checkout.Issuing.Cards.Responses.Create;
using Checkout.Issuing.Cards.Responses.Update;
using Checkout.Issuing.Common;
using Checkout.Issuing.Common.Responses;
using Shouldly;
using System;
using Xunit;

namespace Checkout.Issuing.Cards
{
    /// <summary>
    /// Covers INT-1700: `scheduled_revocation_date` (add/update-card-request, get-card-response),
    /// `status` (update-card-request), and `last_activated_on` (add-card-response,
    /// get-card-response, activate-card-response).
    /// </summary>
    public class CardScheduledRevocationAndActivationSerializationTest
    {
        private readonly JsonSerializer _serializer = new JsonSerializer();

        [Fact]
        public void ShouldRoundTripSerializeScheduledRevocationDateOnCreateRequest()
        {
            var original = new VirtualCardCreateRequest
            {
                CardholderId = "crh_test",
                ScheduledRevocationDate = "2027-03-12"
            };

            var deserialized = (VirtualCardCreateRequest)_serializer
                .Deserialize(_serializer.Serialize(original), typeof(VirtualCardCreateRequest));

            deserialized.ScheduledRevocationDate.ShouldBe(original.ScheduledRevocationDate);
        }

        [Fact]
        public void ShouldDeserializeScheduledRevocationDateSwaggerExampleOnCreateRequest()
        {
            const string json = @"{ ""cardholder_id"": ""crh_test"", ""scheduled_revocation_date"": ""2027-03-12"" }";

            var request = (VirtualCardCreateRequest)_serializer.Deserialize(json, typeof(VirtualCardCreateRequest));

            request.ScheduledRevocationDate.ShouldBe("2027-03-12");
        }

        [Fact]
        public void ShouldRoundTripSerializeStatusAndScheduledRevocationDateOnUpdateRequest()
        {
            var original = new CardsUpdateRequest
            {
                Status = CardStatus.Active,
                ScheduledRevocationDate = "2027-03-12"
            };

            var deserialized = (CardsUpdateRequest)_serializer
                .Deserialize(_serializer.Serialize(original), typeof(CardsUpdateRequest));

            deserialized.Status.ShouldBe(original.Status);
            deserialized.ScheduledRevocationDate.ShouldBe(original.ScheduledRevocationDate);
        }

        [Fact]
        public void ShouldDeserializeStatusAndScheduledRevocationDateSwaggerExampleOnUpdateRequest()
        {
            const string json = @"{ ""status"": ""active"", ""scheduled_revocation_date"": ""2027-03-12"" }";

            var request = (CardsUpdateRequest)_serializer.Deserialize(json, typeof(CardsUpdateRequest));

            request.Status.ShouldBe(CardStatus.Active);
            request.ScheduledRevocationDate.ShouldBe("2027-03-12");
        }

        [Fact]
        public void ShouldSerializeStatusOnUpdateRequest()
        {
            var request = new CardsUpdateRequest { Status = CardStatus.Active };

            var json = _serializer.Serialize(request);

            json.ShouldContain("\"status\"");
            json.ShouldContain("active");
        }

        [Fact]
        public void ShouldRoundTripSerializeScheduledRevocationDateAndLastActivatedOnOnCardResponse()
        {
            var original = new VirtualCardResponse
            {
                Id = "crd_test",
                ScheduledRevocationDate = "2027-03-12",
                LastActivatedOn = new DateTime(2019, 9, 10, 10, 11, 12, DateTimeKind.Utc)
            };

            var deserialized = (AbstractCardResponse)_serializer
                .Deserialize(_serializer.Serialize(original), typeof(AbstractCardResponse));

            deserialized.ScheduledRevocationDate.ShouldBe(original.ScheduledRevocationDate);
            deserialized.LastActivatedOn.ShouldBe(original.LastActivatedOn);
        }

        [Fact]
        public void ShouldDeserializeScheduledRevocationDateAndLastActivatedOnSwaggerExampleOnGetCardResponse()
        {
            const string json = @"{
                ""type"": ""virtual"",
                ""id"": ""crd_test"",
                ""scheduled_revocation_date"": ""2027-03-12"",
                ""last_activated_on"": ""2019-09-10T10:11:12Z""
            }";

            var response = (AbstractCardResponse)_serializer.Deserialize(json, typeof(AbstractCardResponse));

            response.ScheduledRevocationDate.ShouldBe("2027-03-12");
            response.LastActivatedOn.ShouldBe(new DateTime(2019, 9, 10, 10, 11, 12, DateTimeKind.Utc));
        }

        [Fact]
        public void ShouldDeserializeNullLastActivatedOnOnGetCardResponse()
        {
            const string json = @"{ ""type"": ""virtual"", ""id"": ""crd_test"", ""last_activated_on"": null }";

            var response = (AbstractCardResponse)_serializer.Deserialize(json, typeof(AbstractCardResponse));

            response.LastActivatedOn.ShouldBeNull();
        }

        [Fact]
        public void ShouldRoundTripSerializeScheduledRevocationDateAndLastActivatedOnOnCreateResponse()
        {
            var original = new VirtualCardCreateResponse
            {
                Id = "crd_test",
                ScheduledRevocationDate = "2027-03-12",
                LastActivatedOn = new DateTime(2019, 9, 10, 10, 11, 12, DateTimeKind.Utc)
            };

            var deserialized = (VirtualCardCreateResponse)_serializer
                .Deserialize(_serializer.Serialize(original), typeof(VirtualCardCreateResponse));

            deserialized.ScheduledRevocationDate.ShouldBe(original.ScheduledRevocationDate);
            deserialized.LastActivatedOn.ShouldBe(original.LastActivatedOn);
        }

        [Fact]
        public void ShouldRoundTripSerializeLastActivatedOnOnActivateCardResponse()
        {
            var original = new ActivateCardResponse
            {
                LastActivatedOn = new DateTime(2019, 9, 10, 10, 11, 12, DateTimeKind.Utc)
            };

            var deserialized = (ActivateCardResponse)_serializer
                .Deserialize(_serializer.Serialize(original), typeof(ActivateCardResponse));

            deserialized.LastActivatedOn.ShouldBe(original.LastActivatedOn);
        }

        [Fact]
        public void ShouldDeserializeLastActivatedOnSwaggerExampleOnActivateCardResponse()
        {
            const string json = @"{ ""last_activated_on"": ""2019-09-10T10:11:12Z"" }";

            var response = (ActivateCardResponse)_serializer.Deserialize(json, typeof(ActivateCardResponse));

            response.LastActivatedOn.ShouldBe(new DateTime(2019, 9, 10, 10, 11, 12, DateTimeKind.Utc));
        }

        [Fact]
        public void ShouldDeserializeUpdateCardResponseWithFullGetCardResponseFieldSet()
        {
            const string json = @"{
                ""type"": ""virtual"",
                ""id"": ""crd_test"",
                ""client_id"": ""cli_test"",
                ""entity_id"": ""ent_test"",
                ""cardholder_id"": ""crh_test"",
                ""card_product_id"": ""pro_test"",
                ""last_four"": ""1234"",
                ""expiry_month"": 12,
                ""expiry_year"": 2030,
                ""status"": ""active"",
                ""billing_currency"": ""USD"",
                ""issuing_country"": ""US"",
                ""scheme"": ""visa"",
                ""scheduled_revocation_date"": ""2027-03-12"",
                ""last_activated_on"": ""2019-09-10T10:11:12Z"",
                ""last_modified_date"": ""2026-09-17T10:11:12Z""
            }";

            var response = (CardsUpdateResponse)_serializer.Deserialize(json, typeof(CardsUpdateResponse));

            response.Id.ShouldBe("crd_test");
            response.ClientId.ShouldBe("cli_test");
            response.EntityId.ShouldBe("ent_test");
            response.CardholderId.ShouldBe("crh_test");
            response.CardProductId.ShouldBe("pro_test");
            response.LastFour.ShouldBe("1234");
            response.ExpiryMonth.ShouldBe(12);
            response.ExpiryYear.ShouldBe(2030);
            response.ScheduledRevocationDate.ShouldBe("2027-03-12");
            response.LastActivatedOn.ShouldBe(new DateTime(2019, 9, 10, 10, 11, 12, DateTimeKind.Utc));
            response.LastModifiedDate.ShouldBe(new DateTime(2026, 9, 17, 10, 11, 12, DateTimeKind.Utc));
        }
    }
}
