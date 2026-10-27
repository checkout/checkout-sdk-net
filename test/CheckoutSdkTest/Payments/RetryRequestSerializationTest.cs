using Checkout.HandlePaymentsAndPayouts.Flow.Entities;
using Checkout.Payments.Hosted;
using Checkout.Payments.Links;
using Checkout.HandlePaymentsAndPayouts.Flow.Requests;
using Shouldly;
using Xunit;

namespace Checkout.Payments
{
    public class RetryRequestSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

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
        public void ShouldSerializeEnabledFalse()
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
            var json = Serializer.Serialize(CreateFullyPopulatedRetry());
            var result = (RetryRequest)Serializer.Deserialize(json, typeof(RetryRequest));

            result.Enabled.ShouldBe(true);
            result.Dunning.Enabled.ShouldBeTrue();
            result.Dunning.MaxAttempts.ShouldBe(5);
            result.Dunning.EndAfterDays.ShouldBe(30);
            result.Downtime.Enabled.ShouldBeTrue();
        }

        [Fact]
        public void ShouldDeserializeRetryRequestHandBuiltSpecShape()
        {
            const string json =
                "{\"enabled\":true,\"dunning\":{\"enabled\":true,\"max_attempts\":6,\"end_after_days\":30},\"downtime\":{\"enabled\":false}}";

            var result = (RetryRequest)Serializer.Deserialize(json, typeof(RetryRequest));

            result.Enabled.ShouldBe(true);
            result.Dunning.MaxAttempts.ShouldBe(6);
            result.Dunning.EndAfterDays.ShouldBe(30);
            result.Downtime.Enabled.ShouldBeFalse();
        }

        [Fact]
        public void ShouldSerializeCustomerRetryWithOnlyMaxAttempts()
        {
            var json = Serializer.Serialize(new CustomerRetry { MaxAttempts = 2 });

            json.ShouldBe("{\"max_attempts\":2}");
        }

        [Fact]
        public void ShouldRoundTripCustomerRetrySwaggerExample()
        {
            var result = (CustomerRetry)Serializer.Deserialize("{\"max_attempts\":2}", typeof(CustomerRetry));

            result.MaxAttempts.ShouldBe(2);
            Serializer.Serialize(result).ShouldBe("{\"max_attempts\":2}");
        }

        [Fact]
        public void ShouldSerializeCustomerRetryOnHostedLinksAndPaymentSessions()
        {
            var hosted = Serializer.Serialize(new HostedPaymentRequest
                { CustomerRetry = new CustomerRetry { MaxAttempts = 0 } });
            var links = Serializer.Serialize(new PaymentLinkRequest
                { CustomerRetry = new CustomerRetry { MaxAttempts = 3 } });
            var sessions = Serializer.Serialize(new PaymentSessionCreateRequest
                { CustomerRetry = new CustomerRetry { MaxAttempts = 5 } });

            hosted.ShouldContain("\"customer_retry\":{\"max_attempts\":0}");
            links.ShouldContain("\"customer_retry\":{\"max_attempts\":3}");
            sessions.ShouldContain("\"customer_retry\":{\"max_attempts\":5}");
        }
    #pragma warning disable CS0618
        [Fact]
        public void ShouldStillSerializeObsoletePaymentRetryRequest()
        {
            var request = new Checkout.Payments.Request.PaymentRetryRequest
            {
                Enabled = false,
                Dunning = new Checkout.Payments.Request.DunningRetryRequest
                    { Enabled = true, MaxAttempts = 3, EndAfterDays = 10 },
                Downtime = new Checkout.Payments.Request.DowntimeRetryRequest { Enabled = false }
            };

            var json = Serializer.Serialize(request);

            json.ShouldContain("\"enabled\":false");
            json.ShouldContain("\"dunning\":{\"enabled\":true,\"max_attempts\":3,\"end_after_days\":10}");
            json.ShouldContain("\"downtime\":{\"enabled\":false}");
        }
#pragma warning restore CS0618

        [Fact]
        public void ShouldDeserializePaymentRetryResponseGetShape()
        {
            const string json = "{\"enabled\":true,\"attempts_made\":1,\"max_attempts\":6," +
                                "\"ends_on\":\"2026-11-25T10:00:00Z\",\"next_attempt_on\":\"2026-10-27T10:00:00Z\"}";

            var result = (Checkout.Payments.Response.PaymentRetryResponse)Serializer.Deserialize(json,
                typeof(Checkout.Payments.Response.PaymentRetryResponse));

            result.Enabled.ShouldBe(true);
            result.AttemptsMade.ShouldBe(1);
            result.MaxAttempts.ShouldBe(6);
            result.EndsOn.ShouldNotBeNull();
            result.NextAttemptOn.ShouldNotBeNull();
        }

        [Fact]
        public void ShouldLeavePaymentRetryResponseOptionalsNullWhenAbsent()
        {
            var result = (Checkout.Payments.Response.PaymentRetryResponse)Serializer.Deserialize(
                "{\"enabled\":false}", typeof(Checkout.Payments.Response.PaymentRetryResponse));

            result.Enabled.ShouldBe(false);
            result.AttemptsMade.ShouldBeNull();
            result.MaxAttempts.ShouldBeNull();
            result.EndsOn.ShouldBeNull();
            result.NextAttemptOn.ShouldBeNull();
        }

        [Fact]
        public void ShouldDeserializePostPaymentRetryResponse()
        {
            const string json = "{\"enabled\":true,\"max_attempts\":4,\"ends_on\":\"2026-11-25T10:00:00Z\"," +
                                "\"next_attempt_on\":\"2026-10-27T10:00:00Z\"}";
            var type = typeof(Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses
                .RequestAPaymentOrPayoutResponseCreated.Retry.Retry);

            var result = (Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses
                .RequestAPaymentOrPayoutResponseCreated.Retry.Retry)Serializer.Deserialize(json, type);

            result.Enabled.ShouldBe(true);
            result.MaxAttempts.ShouldBe(4);
            result.EndsOn.ShouldNotBeNull();
            result.NextAttemptOn.ShouldNotBeNull();

            var empty = (Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses
                .RequestAPaymentOrPayoutResponseCreated.Retry.Retry)Serializer.Deserialize("{\"enabled\":false}", type);
            empty.MaxAttempts.ShouldBeNull();
        }
    }
}
