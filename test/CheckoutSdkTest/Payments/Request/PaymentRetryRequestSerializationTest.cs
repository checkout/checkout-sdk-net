using Shouldly;
using Xunit;

namespace Checkout.Payments.Request
{
#pragma warning disable CS0618
    public class PaymentRetryRequestSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        [Fact]
        public void ShouldStillSerializeObsoletePaymentRetryRequest()
        {
            var request = new PaymentRetryRequest
            {
                Enabled = false,
                Dunning = new DunningRetryRequest { Enabled = true, MaxAttempts = 3, EndAfterDays = 10 },
                Downtime = new DowntimeRetryRequest { Enabled = false }
            };

            var json = Serializer.Serialize(request);

            json.ShouldContain("\"enabled\":false");
            json.ShouldContain("\"dunning\":{\"enabled\":true,\"max_attempts\":3,\"end_after_days\":10}");
            json.ShouldContain("\"downtime\":{\"enabled\":false}");
        }
    }
#pragma warning restore CS0618
}
