using Checkout.HandlePaymentsAndPayouts.GooglePay.Requests;
using Shouldly;
using Xunit;

namespace Checkout.HandlePaymentsAndPayouts.GooglePay
{
    public class GooglePayEnrollmentRequestSerializationTest
    {
        [Fact]
        public void ShouldSerializeWithRequiredProperties()
        {
            var request = new GooglePayEnrollmentRequest
            {
                EntityId = "ent_uzm3uxtssvmuxnyrfdffcyjxeu",
                EmailAddress = "test@gmail.com",
                AcceptTermsOfService = true
            };

            Should.NotThrow(() => new JsonSerializer().Serialize(request));
        }

        [Fact]
        public void ShouldRoundTripSerialize()
        {
            var original = new GooglePayEnrollmentRequest
            {
                EntityId = "ent_uzm3uxtssvmuxnyrfdffcyjxeu",
                EmailAddress = "test@gmail.com",
                AcceptTermsOfService = true
            };
            var serializer = new JsonSerializer();

            var json = serializer.Serialize(original);
            var deserialized = (GooglePayEnrollmentRequest)serializer.Deserialize(json, typeof(GooglePayEnrollmentRequest));

            deserialized.EntityId.ShouldBe(original.EntityId);
            deserialized.EmailAddress.ShouldBe(original.EmailAddress);
            deserialized.AcceptTermsOfService.ShouldBe(original.AcceptTermsOfService);
        }

        [Fact]
        public void ShouldDeserializeSwaggerExample()
        {
            const string json = @"{
                ""entity_id"": ""ent_uzm3uxtssvmuxnyrfdffcyjxeu"",
                ""email_address"": ""test@gmail.com"",
                ""accept_terms_of_service"": true
            }";

            var request = (GooglePayEnrollmentRequest)new JsonSerializer()
                .Deserialize(json, typeof(GooglePayEnrollmentRequest));

            request.ShouldNotBeNull();
            request.EntityId.ShouldBe("ent_uzm3uxtssvmuxnyrfdffcyjxeu");
            request.EmailAddress.ShouldBe("test@gmail.com");
            request.AcceptTermsOfService.ShouldBeTrue();
        }
    }
}
