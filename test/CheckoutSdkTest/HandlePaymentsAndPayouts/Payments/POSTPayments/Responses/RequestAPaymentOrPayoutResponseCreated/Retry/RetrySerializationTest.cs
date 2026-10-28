using Shouldly;
using System;
using Xunit;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.Retry
{
    public class RetrySerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        private static readonly DateTime EndsOn = new DateTime(2026, 11, 25, 10, 0, 0, DateTimeKind.Utc);
        private static readonly DateTime NextAttemptOn = new DateTime(2026, 10, 27, 10, 0, 0, DateTimeKind.Utc);

        private const string PostResponseJson =
            "{\"enabled\":true,\"max_attempts\":4,\"ends_on\":\"2026-11-25T10:00:00Z\"," +
            "\"next_attempt_on\":\"2026-10-27T10:00:00Z\"}";

        private static Retry Deserialize(string json) => (Retry)Serializer.Deserialize(json, typeof(Retry));

        private static void ShouldMatchPostResponse(Retry result)
        {
            result.Enabled.ShouldBe(true);
            result.MaxAttempts.ShouldBe(4);
            result.EndsOn.ShouldNotBeNull();
            result.EndsOn.Value.ToUniversalTime().ShouldBe(EndsOn);
            result.NextAttemptOn.ShouldNotBeNull();
            result.NextAttemptOn.Value.ToUniversalTime().ShouldBe(NextAttemptOn);
        }

        [Fact]
        public void ShouldDeserializePostPaymentRetryResponse()
        {
            ShouldMatchPostResponse(Deserialize(PostResponseJson));
        }

        [Fact]
        public void ShouldRoundTripPostPaymentRetryResponse()
        {
            ShouldMatchPostResponse(Deserialize(Serializer.Serialize(Deserialize(PostResponseJson))));
        }

        [Fact]
        public void ShouldLeavePostPaymentRetryResponseOptionalsNullWhenAbsent()
        {
            var result = Deserialize("{\"enabled\":false}");

            result.Enabled.ShouldBe(false);
            result.MaxAttempts.ShouldBeNull();
            result.EndsOn.ShouldBeNull();
            result.NextAttemptOn.ShouldBeNull();
        }
    }
}
