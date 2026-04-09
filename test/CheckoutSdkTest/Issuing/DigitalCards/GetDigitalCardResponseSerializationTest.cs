using Checkout.Issuing.DigitalCards.Responses;
using Shouldly;
using System;
using Xunit;

namespace Checkout.Issuing.DigitalCards
{
    public class GetDigitalCardResponseSerializationTest
    {
        [Fact]
        public void ShouldSerializeWithRequiredProperties()
        {
            var response = new GetDigitalCardResponse
            {
                Id = "dcr_5ngxzsynm2me3oxf73esbhda6q",
                CardId = "crd_test123",
                ClientId = "cli_test123",
                EntityId = "ent_test123",
                LastFour = "4242",
                Status = IssuingDigitalCardStatus.Active
            };

            Should.NotThrow(() => new JsonSerializer().Serialize(response));
        }

        [Fact]
        public void ShouldSerializeWithAllOptionalProperties()
        {
            var response = new GetDigitalCardResponse
            {
                Id = "dcr_5ngxzsynm2me3oxf73esbhda6q",
                CardId = "crd_test123",
                ClientId = "cli_test123",
                EntityId = "ent_test123",
                LastFour = "4242",
                Status = IssuingDigitalCardStatus.Active,
                Type = IssuingDigitalCardType.SecureElement,
                SchemeCardId = "FAPLMC00002572060f2b2213c3964c44b218665ef9dd0d72",
                ProvisionedOn = new DateTime(2025, 1, 1),
                Requestor = new IssuingDigitalCardRequestor
                {
                    Id = "501103353211",
                    Name = "APPLE PAY"
                },
                Device = new IssuingDigitalCardDevice
                {
                    Id = "04431C7B514E80018306224737548585F11415F67C4E2EB6",
                    Type = IssuingDigitalCardDeviceType.Iphone,
                    Manufacturer = "ios",
                    Brand = "Apple",
                    Model = "iPhone 14",
                    OsVersion = "16",
                    FirmwareVersion = "1.0",
                    PhoneNumber = "447505551234",
                    DeviceName = "John's iPhone",
                    Language = "en",
                    TimeZone = "Europe/London",
                    TimeZoneSetting = IssuingDigitalCardDeviceTimeZoneSetting.NetworkSet,
                    Imei = "000000000000024",
                    NetworkType = IssuingDigitalCardDeviceNetworkType.Wifi
                }
            };

            Should.NotThrow(() => new JsonSerializer().Serialize(response));
        }

        [Fact]
        public void ShouldRoundTripSerialize()
        {
            var original = new GetDigitalCardResponse
            {
                Id = "dcr_5ngxzsynm2me3oxf73esbhda6q",
                CardId = "crd_test123",
                LastFour = "4242",
                Status = IssuingDigitalCardStatus.Active,
                Type = IssuingDigitalCardType.HostCardEmulation
            };
            var serializer = new JsonSerializer();

            var json = serializer.Serialize(original);
            var deserialized = (GetDigitalCardResponse)serializer.Deserialize(json, typeof(GetDigitalCardResponse));

            deserialized.Id.ShouldBe(original.Id);
            deserialized.CardId.ShouldBe(original.CardId);
            deserialized.LastFour.ShouldBe(original.LastFour);
            deserialized.Status.ShouldBe(original.Status);
            deserialized.Type.ShouldBe(original.Type);
        }

        [Fact]
        public void ShouldDeserializeSwaggerExample()
        {
            const string json = @"{
                ""id"": ""dcr_5ngxzsynm2me3oxf73esbhda6q"",
                ""card_id"": ""crd_test123"",
                ""client_id"": ""cli_test123"",
                ""entity_id"": ""ent_test123"",
                ""last_four"": ""4242"",
                ""status"": ""active"",
                ""type"": ""secure_element"",
                ""scheme_card_id"": ""FAPLMC00002572060f2b2213c3964c44b218665ef9dd0d72"",
                ""provisioned_on"": ""2025-01-01T00:00:00Z"",
                ""requestor"": { ""id"": ""501103353211"", ""name"": ""APPLE PAY"" },
                ""device"": { ""id"": ""04431C7B"", ""type"": ""iphone"", ""manufacturer"": ""ios"" }
            }";

            var response = (GetDigitalCardResponse)new JsonSerializer()
                .Deserialize(json, typeof(GetDigitalCardResponse));

            response.ShouldNotBeNull();
            response.Id.ShouldBe("dcr_5ngxzsynm2me3oxf73esbhda6q");
            response.Status.ShouldBe(IssuingDigitalCardStatus.Active);
            response.Type.ShouldBe(IssuingDigitalCardType.SecureElement);
            response.Requestor.ShouldNotBeNull();
            response.Requestor.Name.ShouldBe("APPLE PAY");
            response.Device.ShouldNotBeNull();
            response.Device.Type.ShouldBe(IssuingDigitalCardDeviceType.Iphone);
        }
    }
}
