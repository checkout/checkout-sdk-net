using Checkout.Common;
using Checkout.HandlePaymentsAndPayouts.Flow.Requests;
using Checkout.Payments;
using Shouldly;
using Xunit;

namespace Checkout.HandlePaymentsAndPayouts.Flow
{
    /// <summary>
    /// Regression tests for the capture and payment_type defaults on the Flow payment session requests.
    ///
    /// The session creation requests (POST /payment-sessions and POST /payment-sessions/complete) must
    /// keep sending the API defaults, because the caller is supplying the payment's values in that same
    /// call. The submit request (POST /payment-sessions/{id}/submit) must send neither property unless
    /// the caller sets it, because any value present in the submit body is applied to the payment
    /// attempt and would overwrite the value provided when the payment session was created.
    /// </summary>
    public class PaymentSessionCaptureDefaultsTest
    {
        [Fact]
        public void ShouldOnlySerializeSessionDataWhenNothingElseIsSet()
        {
            var request = new PaymentSessionSubmitRequest
            {
                SessionData = "SD"
            };

            var json = new JsonSerializer().Serialize(request);

            json.ShouldBe("{\"session_data\":\"SD\"}");
        }

        [Fact]
        public void ShouldLeaveCaptureAndPaymentTypeNullOnSubmitWhenNotSet()
        {
            var request = new PaymentSessionSubmitRequest
            {
                SessionData = "SD"
            };

            request.Capture.ShouldBeNull();
            request.PaymentType.ShouldBeNull();
        }

        [Fact]
        public void ShouldSerializeCaptureFalseOnSubmitWhenExplicitlySet()
        {
            var request = new PaymentSessionSubmitRequest
            {
                SessionData = "SD",
                Capture = false
            };

            var json = new JsonSerializer().Serialize(request);

            json.ShouldContain("\"capture\":false");
        }

        [Fact]
        public void ShouldSerializeCaptureAndPaymentTypeOnSubmitWhenExplicitlySet()
        {
            var request = new PaymentSessionSubmitRequest
            {
                SessionData = "SD",
                Capture = true,
                PaymentType = PaymentType.Recurring
            };

            var json = new JsonSerializer().Serialize(request);

            json.ShouldContain("\"capture\":true");
            json.ShouldContain("\"payment_type\":\"Recurring\"");
        }

        [Fact]
        public void ShouldRoundTripCaptureAndPaymentTypeOnSubmit()
        {
            var original = new PaymentSessionSubmitRequest
            {
                SessionData = "SD",
                Capture = false,
                PaymentType = PaymentType.Moto,
                Amount = 2500,
                Currency = Currency.USD
            };

            var serializer = new JsonSerializer();
            var json = serializer.Serialize(original);
            var deserialized = (PaymentSessionSubmitRequest)serializer.Deserialize(json, typeof(PaymentSessionSubmitRequest));

            deserialized.SessionData.ShouldBe("SD");
            deserialized.Capture.ShouldBe(false);
            deserialized.PaymentType.ShouldBe(PaymentType.Moto);
            deserialized.Amount.ShouldBe(2500L);
            deserialized.Currency.ShouldBe(Currency.USD);
        }

        [Fact]
        public void ShouldNotSerializeSessionCreationOnlyFieldsOnSubmit()
        {
            var request = new PaymentSessionSubmitRequest
            {
                SessionData = "SD",
                Amount = 1000,
                Currency = Currency.GBP,
                Capture = true,
                PaymentType = PaymentType.Regular
            };

            var json = new JsonSerializer().Serialize(request);

            json.ShouldNotContain("\"locale\"");
            json.ShouldNotContain("\"description\"");
            json.ShouldNotContain("\"display_name\"");
            json.ShouldNotContain("\"authorization_type\"");
            json.ShouldNotContain("\"payment_plan\"");
            json.ShouldNotContain("\"risk\"");
        }

        [Fact]
        public void ShouldSerializeTheApiDefaultsOnCreate()
        {
            var request = new PaymentSessionCreateRequest
            {
                Amount = 1000,
                Currency = Currency.GBP
            };

            var json = new JsonSerializer().Serialize(request);

            json.ShouldContain("\"capture\":true");
            json.ShouldContain("\"payment_type\":\"Regular\"");
            json.ShouldContain("\"locale\":\"en-GB\"");
        }

        [Fact]
        public void ShouldSerializeTheApiDefaultsOnComplete()
        {
            var request = new PaymentSessionCompleteRequest
            {
                SessionData = "SD",
                Amount = 2000,
                Currency = Currency.USD
            };

            var json = new JsonSerializer().Serialize(request);

            json.ShouldContain("\"capture\":true");
            json.ShouldContain("\"payment_type\":\"Regular\"");
            json.ShouldContain("\"locale\":\"en-GB\"");
        }

        [Fact]
        public void ShouldStillAllowOverridingTheDefaultsOnCreate()
        {
            var request = new PaymentSessionCreateRequest
            {
                Amount = 1000,
                Currency = Currency.GBP,
                Capture = false,
                PaymentType = PaymentType.Unscheduled,
                Locale = LocaleType.FrFr
            };

            var json = new JsonSerializer().Serialize(request);

            json.ShouldContain("\"capture\":false");
            json.ShouldContain("\"payment_type\":\"Unscheduled\"");
            json.ShouldContain("\"locale\":\"fr-FR\"");
        }

        [Fact]
        public void ShouldRoundTripEverySessionCreationOnlyFieldOnCreate()
        {
            var original = new PaymentSessionCreateRequest
            {
                Capture = false,
                PaymentType = PaymentType.Installment,
                Locale = LocaleType.DeDe,
                AuthorizationType = AuthorizationType.Estimated,
                Description = "Payment for gold necklace",
                DisplayName = "Example Store",
                PaymentPlan = new PaymentPlan
                {
                    AmountVariability = AmountVariabilityType.Fixed,
                    DaysBetweenPayments = 28
                },
                Risk = new RiskRequest
                {
                    Enabled = false
                }
            };

            var serializer = new JsonSerializer();
            var json = serializer.Serialize(original);
            var deserialized = (PaymentSessionCreateRequest)serializer.Deserialize(json, typeof(PaymentSessionCreateRequest));

            deserialized.Capture.ShouldBe(false);
            deserialized.PaymentType.ShouldBe(PaymentType.Installment);
            deserialized.Locale.ShouldBe(LocaleType.DeDe);
            deserialized.AuthorizationType.ShouldBe(AuthorizationType.Estimated);
            deserialized.Description.ShouldBe("Payment for gold necklace");
            deserialized.DisplayName.ShouldBe("Example Store");
            deserialized.PaymentPlan.AmountVariability.ShouldBe(AmountVariabilityType.Fixed);
            deserialized.PaymentPlan.DaysBetweenPayments.ShouldBe(28);
            deserialized.Risk.Enabled.ShouldBe(false);
        }

        [Fact]
        public void ShouldDeserializeTheDocumentedSubmitExample()
        {
            const string json = "{\"session_data\":\"{SESSION_DATA_FROM_FLOW}\",\"3ds\":{\"enabled\":true}}";

            var request = (PaymentSessionSubmitRequest)new JsonSerializer().Deserialize(json, typeof(PaymentSessionSubmitRequest));

            request.SessionData.ShouldBe("{SESSION_DATA_FROM_FLOW}");
            request.ThreeDS.Enabled.ShouldBe(true);
            request.Capture.ShouldBeNull();
            request.PaymentType.ShouldBeNull();
        }
    }
}
