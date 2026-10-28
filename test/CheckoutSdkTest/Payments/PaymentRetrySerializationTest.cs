using Checkout.HandlePaymentsAndPayouts.Flow.Entities;
using Checkout.HandlePaymentsAndPayouts.Flow.Requests;
using Checkout.Payments.Hosted;
using Checkout.Payments.Links;
using Checkout.Payments.Response;
using Shouldly;
using System;
using Xunit;
using LegacyDowntimeRetry = Checkout.Payments.Request.DowntimeRetryRequest;
using LegacyDunningRetry = Checkout.Payments.Request.DunningRetryRequest;
using LegacyPaymentRetry = Checkout.Payments.Request.PaymentRetryRequest;
using PostPaymentRetry =
    Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.Retry.Retry;

namespace Checkout.Payments
{
    public class PaymentRetrySerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        private static readonly DateTime EndsOn = new DateTime(2026, 11, 25, 10, 0, 0, DateTimeKind.Utc);
        private static readonly DateTime NextAttemptOn = new DateTime(2026, 10, 27, 10, 0, 0, DateTimeKind.Utc);

        private const string PostResponseJson =
            "{\"enabled\":true,\"max_attempts\":4,\"ends_on\":\"2026-11-25T10:00:00Z\"," +
            "\"next_attempt_on\":\"2026-10-27T10:00:00Z\"}";

        private const string GetResponseJson =
            "{\"enabled\":true,\"attempts_made\":1,\"max_attempts\":6,\"ends_on\":\"2026-11-25T10:00:00Z\"," +
            "\"next_attempt_on\":\"2026-10-27T10:00:00Z\"}";

        private static T Deserialize<T>(string json) => (T)Serializer.Deserialize(json, typeof(T));

        private static T RoundTrip<T>(T value) => Deserialize<T>(Serializer.Serialize(value));

        private static void ShouldBeUtc(DateTime? actual, DateTime expected)
        {
            actual.ShouldNotBeNull();
            actual.Value.ToUniversalTime().ShouldBe(expected);
        }

        // ------------------------------------------------------------------------
        // RetryRequest (POST /payments retry)
        // ------------------------------------------------------------------------

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
            var result = RoundTrip(CreateFullyPopulatedRetry());

            result.Enabled.ShouldBe(true);
            result.Dunning.Enabled.ShouldBeTrue();
            result.Dunning.MaxAttempts.ShouldBe(5);
            result.Dunning.EndAfterDays.ShouldBe(30);
            result.Downtime.Enabled.ShouldBeTrue();
        }

        [Fact]
        public void ShouldDeserializeRetryRequestSpecShape()
        {
            var result = Deserialize<RetryRequest>(
                "{\"enabled\":true,\"dunning\":{\"enabled\":true,\"max_attempts\":6,\"end_after_days\":30}," +
                "\"downtime\":{\"enabled\":false}}");

            result.Enabled.ShouldBe(true);
            result.Dunning.MaxAttempts.ShouldBe(6);
            result.Dunning.EndAfterDays.ShouldBe(30);
            result.Downtime.Enabled.ShouldBeFalse();
        }

        // ------------------------------------------------------------------------
        // CustomerRetry (hosted payments, payment links, payment sessions)
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeCustomerRetryWithOnlyMaxAttempts()
        {
            Serializer.Serialize(new CustomerRetry { MaxAttempts = 2 }).ShouldBe("{\"max_attempts\":2}");
        }

        [Fact]
        public void ShouldRoundTripCustomerRetrySwaggerExample()
        {
            var result = Deserialize<CustomerRetry>("{\"max_attempts\":2}");

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

        // ------------------------------------------------------------------------
        // Deprecated PaymentRetryRequest (compatibility)
        // ------------------------------------------------------------------------

#pragma warning disable CS0618
        [Fact]
        public void ShouldConvertLegacyPaymentRetryToCustomerRetry()
        {
            var request = new HostedPaymentRequest { CustomerRetry = new LegacyPaymentRetry { MaxAttempts = 2 } };

            Serializer.Serialize(request.CustomerRetry).ShouldBe("{\"max_attempts\":2}");
        }

        [Fact]
        public void ShouldConvertNullLegacyPaymentRetryAndMaxAttempts()
        {
            CustomerRetry fromNull = (LegacyPaymentRetry)null;
            CustomerRetry fromEmpty = new LegacyPaymentRetry();

            fromNull.ShouldBeNull();
            fromEmpty.MaxAttempts.ShouldBeNull();
            Serializer.Serialize(fromEmpty).ShouldBe("{}");
        }

        [Fact]
        public void ShouldStillSerializeObsoletePaymentRetryRequest()
        {
            var request = new LegacyPaymentRetry
            {
                Enabled = false,
                Dunning = new LegacyDunningRetry { Enabled = true, MaxAttempts = 3, EndAfterDays = 10 },
                Downtime = new LegacyDowntimeRetry { Enabled = false }
            };

            var json = Serializer.Serialize(request);

            json.ShouldContain("\"enabled\":false");
            json.ShouldContain("\"dunning\":{\"enabled\":true,\"max_attempts\":3,\"end_after_days\":10}");
            json.ShouldContain("\"downtime\":{\"enabled\":false}");
        }
#pragma warning restore CS0618

        // ------------------------------------------------------------------------
        // PaymentRetryResponse (GET /payments/{id} and POST /payments retry)
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldDeserializePaymentRetryResponseGetShape()
        {
            var result = Deserialize<PaymentRetryResponse>(GetResponseJson);

            result.Enabled.ShouldBe(true);
            result.AttemptsMade.ShouldBe(1);
            result.MaxAttempts.ShouldBe(6);
            ShouldBeUtc(result.EndsOn, EndsOn);
            ShouldBeUtc(result.NextAttemptOn, NextAttemptOn);
        }

        [Fact]
        public void ShouldRoundTripPaymentRetryResponse()
        {
            var result = RoundTrip(Deserialize<PaymentRetryResponse>(GetResponseJson));

            result.Enabled.ShouldBe(true);
            result.AttemptsMade.ShouldBe(1);
            result.MaxAttempts.ShouldBe(6);
            ShouldBeUtc(result.EndsOn, EndsOn);
            ShouldBeUtc(result.NextAttemptOn, NextAttemptOn);
        }

        [Fact]
        public void ShouldLeavePaymentRetryResponseOptionalsNullWhenAbsent()
        {
            var result = Deserialize<PaymentRetryResponse>("{\"enabled\":false}");

            result.Enabled.ShouldBe(false);
            result.AttemptsMade.ShouldBeNull();
            result.MaxAttempts.ShouldBeNull();
            result.EndsOn.ShouldBeNull();
            result.NextAttemptOn.ShouldBeNull();
        }

        // ------------------------------------------------------------------------
        // Retry (POST /payments 201 response)
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldDeserializePostPaymentRetryResponse()
        {
            var result = Deserialize<PostPaymentRetry>(PostResponseJson);

            result.Enabled.ShouldBe(true);
            result.MaxAttempts.ShouldBe(4);
            ShouldBeUtc(result.EndsOn, EndsOn);
            ShouldBeUtc(result.NextAttemptOn, NextAttemptOn);
        }

        [Fact]
        public void ShouldRoundTripPostPaymentRetryResponse()
        {
            var result = RoundTrip(Deserialize<PostPaymentRetry>(PostResponseJson));

            result.Enabled.ShouldBe(true);
            result.MaxAttempts.ShouldBe(4);
            ShouldBeUtc(result.EndsOn, EndsOn);
            ShouldBeUtc(result.NextAttemptOn, NextAttemptOn);
        }

        [Fact]
        public void ShouldLeavePostPaymentRetryResponseOptionalsNullWhenAbsent()
        {
            var result = Deserialize<PostPaymentRetry>("{\"enabled\":false}");

            result.Enabled.ShouldBe(false);
            result.MaxAttempts.ShouldBeNull();
            result.EndsOn.ShouldBeNull();
            result.NextAttemptOn.ShouldBeNull();
        }
    }
}
