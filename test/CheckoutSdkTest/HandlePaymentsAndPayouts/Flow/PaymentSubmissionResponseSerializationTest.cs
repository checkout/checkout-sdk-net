using Checkout.HandlePaymentsAndPayouts.Flow.Entities;
using Checkout.HandlePaymentsAndPayouts.Flow.Responses;
using Shouldly;
using Xunit;

namespace Checkout.HandlePaymentsAndPayouts.Flow
{
    public class PaymentSubmissionResponseSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        [Fact]
        public void ShouldSerializeWithRequiredProperties()
        {
            var response = new PaymentSubmissionResponse();

            Should.NotThrow(() => Serializer.Serialize(response));
        }

        [Fact]
        public void ShouldSerializeWithAllOptionalProperties()
        {
            var response = new PaymentSubmissionResponse
            {
                Id = "pay_abc123",
                Status = "Approved",
                Type = PaymentMethod.Card,
                DeclineReason = "Insufficient funds",
                Action = new { type = "redirect", url = "https://example.com" },
                PaymentSessionId = "ps_xyz",
                PaymentSessionSecret = "secret_abc"
            };

            Should.NotThrow(() => Serializer.Serialize(response));
        }

        [Fact]
        public void ShouldDeserializeApprovedResponse()
        {
            const string json = @"{
                ""id"": ""pay_abc123"",
                ""status"": ""Approved"",
                ""type"": ""card"",
                ""payment_session_id"": ""ps_xyz"",
                ""payment_session_secret"": ""secret_abc""
            }";

            var response = (PaymentSubmissionResponse)Serializer.Deserialize(json, typeof(PaymentSubmissionResponse));

            response.ShouldNotBeNull();
            response.Id.ShouldBe("pay_abc123");
            response.Status.ShouldBe("Approved");
            response.PaymentSessionId.ShouldBe("ps_xyz");
            response.PaymentSessionSecret.ShouldBe("secret_abc");
        }

        [Fact]
        public void ShouldDeserializeDeclinedResponse()
        {
            const string json = @"{
                ""id"": ""pay_def456"",
                ""status"": ""Declined"",
                ""type"": ""card"",
                ""decline_reason"": ""Insufficient funds""
            }";

            var response = (PaymentSubmissionResponse)Serializer.Deserialize(json, typeof(PaymentSubmissionResponse));

            response.ShouldNotBeNull();
            response.Id.ShouldBe("pay_def456");
            response.Status.ShouldBe("Declined");
            response.DeclineReason.ShouldBe("Insufficient funds");
        }

        [Fact]
        public void ShouldDeserializeActionRequiredResponse()
        {
            const string json = @"{
                ""id"": ""pay_ghi789"",
                ""status"": ""Action Required"",
                ""type"": ""card"",
                ""action"": { ""type"": ""redirect"" }
            }";

            var response = (PaymentSubmissionResponse)Serializer.Deserialize(json, typeof(PaymentSubmissionResponse));

            response.ShouldNotBeNull();
            response.Id.ShouldBe("pay_ghi789");
            response.Status.ShouldBe("Action Required");
            response.Action.ShouldNotBeNull();
        }

        [Fact]
        public void ShouldRoundTripSerialize()
        {
            var original = new PaymentSubmissionResponse
            {
                Id = "pay_abc123",
                Status = "Approved",
                Type = PaymentMethod.Card,
                PaymentSessionId = "ps_xyz",
                PaymentSessionSecret = "secret_abc",
                DeclineReason = null,
                Action = null
            };

            var json = Serializer.Serialize(original);
            var deserialized = (PaymentSubmissionResponse)Serializer.Deserialize(json, typeof(PaymentSubmissionResponse));

            deserialized.Id.ShouldBe(original.Id);
            deserialized.Status.ShouldBe(original.Status);
            deserialized.Type.ShouldBe(original.Type);
            deserialized.PaymentSessionId.ShouldBe(original.PaymentSessionId);
            deserialized.PaymentSessionSecret.ShouldBe(original.PaymentSessionSecret);
        }
    }
}
