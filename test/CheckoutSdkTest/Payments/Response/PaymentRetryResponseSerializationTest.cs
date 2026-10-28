using Shouldly;
using System;
using Xunit;

namespace Checkout.Payments.Response
{
    public class PaymentRetryResponseSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        private static readonly DateTime EndsOn = new DateTime(2026, 11, 25, 10, 0, 0, DateTimeKind.Utc);
        private static readonly DateTime NextAttemptOn = new DateTime(2026, 10, 27, 10, 0, 0, DateTimeKind.Utc);

        private const string GetResponseJson =
            "{\"enabled\":true,\"attempts_made\":1,\"max_attempts\":6,\"ends_on\":\"2026-11-25T10:00:00Z\"," +
            "\"next_attempt_on\":\"2026-10-27T10:00:00Z\"}";

        private static PaymentRetryResponse Deserialize(string json) =>
            (PaymentRetryResponse)Serializer.Deserialize(json, typeof(PaymentRetryResponse));

        private static void ShouldMatchGetResponse(PaymentRetryResponse result)
        {
            result.Enabled.ShouldBe(true);
            result.AttemptsMade.ShouldBe(1);
            result.MaxAttempts.ShouldBe(6);
            result.EndsOn.ShouldNotBeNull();
            result.EndsOn.Value.ToUniversalTime().ShouldBe(EndsOn);
            result.NextAttemptOn.ShouldNotBeNull();
            result.NextAttemptOn.Value.ToUniversalTime().ShouldBe(NextAttemptOn);
        }

        [Fact]
        public void ShouldDeserializePaymentRetryResponseGetShape()
        {
            ShouldMatchGetResponse(Deserialize(GetResponseJson));
        }

        [Fact]
        public void ShouldRoundTripPaymentRetryResponse()
        {
            ShouldMatchGetResponse(Deserialize(Serializer.Serialize(Deserialize(GetResponseJson))));
        }

        [Fact]
        public void ShouldLeavePaymentRetryResponseOptionalsNullWhenAbsent()
        {
            var result = Deserialize("{\"enabled\":false}");

            result.Enabled.ShouldBe(false);
            result.AttemptsMade.ShouldBeNull();
            result.MaxAttempts.ShouldBeNull();
            result.EndsOn.ShouldBeNull();
            result.NextAttemptOn.ShouldBeNull();
        }
    }
}
