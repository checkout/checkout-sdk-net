using Shouldly;
using Xunit;

namespace Checkout.Payments
{
    public class RetryRequestSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        private static RetryRequest Deserialize(string json) =>
            (RetryRequest)Serializer.Deserialize(json, typeof(RetryRequest));

        private static RetryRequest CreateFullyPopulatedRetry()
        {
            return new RetryRequest
            {
                Enabled = true,
                Dunning = new Dunning { Enabled = true, MaxAttempts = 5, EndAfterDays = 30 },
                Downtime = new Downtime { Enabled = true }
            };
        }

        [Fact]
        public void ShouldSerializeRetryRequestAllPropertiesToSnakeCase()
        {
            var json = Serializer.Serialize(CreateFullyPopulatedRetry());

            json.ShouldContain("\"enabled\":true");
            json.ShouldContain("\"dunning\":{\"enabled\":true,\"max_attempts\":5,\"end_after_days\":30}");
            json.ShouldContain("\"downtime\":{\"enabled\":true}");
            json.ShouldNotContain("customer_retry");
        }

        [Fact]
        public void ShouldSerializeRetryRequestEnabledFalse()
        {
            var request = new RetryRequest
            {
                Enabled = false,
                Dunning = new Dunning { Enabled = false },
                Downtime = new Downtime { Enabled = false }
            };

            var json = Serializer.Serialize(request);

            json.ShouldContain("\"enabled\":false,\"dunning\":{\"enabled\":false");
            json.ShouldContain("\"downtime\":{\"enabled\":false}");
        }

        [Fact]
        public void ShouldRoundTripRetryRequest()
        {
            var result = Deserialize(Serializer.Serialize(CreateFullyPopulatedRetry()));

            result.Enabled.ShouldBe(true);
            result.Dunning.Enabled.ShouldBeTrue();
            result.Dunning.MaxAttempts.ShouldBe(5);
            result.Dunning.EndAfterDays.ShouldBe(30);
            result.Downtime.Enabled.ShouldBeTrue();
        }

        [Fact]
        public void ShouldDeserializeRetryRequestSpecShape()
        {
            var result = Deserialize(
                "{\"enabled\":true,\"dunning\":{\"enabled\":true,\"max_attempts\":6,\"end_after_days\":30}," +
                "\"downtime\":{\"enabled\":false}}");

            result.Enabled.ShouldBe(true);
            result.Dunning.MaxAttempts.ShouldBe(6);
            result.Dunning.EndAfterDays.ShouldBe(30);
            result.Downtime.Enabled.ShouldBeFalse();
        }
    }
}
