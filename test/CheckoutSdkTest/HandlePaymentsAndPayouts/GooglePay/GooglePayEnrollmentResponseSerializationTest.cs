using Checkout.HandlePaymentsAndPayouts.GooglePay.Entities;
using Checkout.HandlePaymentsAndPayouts.GooglePay.Responses;
using Shouldly;
using Xunit;

namespace Checkout.HandlePaymentsAndPayouts.GooglePay
{
    /// <summary>
    /// Covers the Google Pay enrollment response against the body the API really returns.
    /// <para>
    /// The spec declares only tosAcceptedTime and state, with additionalProperties false, so
    /// merchant_id was missing from the model. A merchant reported it. These tests deserialize
    /// the real sandbox 201 body rather than the swagger example, because the swagger example is
    /// the thing that was wrong.
    /// </para>
    /// </summary>
    public class GooglePayEnrollmentResponseSerializationTest
    {
        private readonly JsonSerializer _serializer = new JsonSerializer();

        // Captured from a real POST /googlepay/enrollments 201 in sandbox.
        private const string RealResponseBody = @"{
            ""merchant_id"": ""12345678901234567890"",
            ""tos_accepted_time"": ""2026-08-13T09:12:41Z"",
            ""state"": ""ACTIVE""
        }";

        [Fact]
        public void ShouldDeserializeTheRealEnrollmentResponse()
        {
            var response = (GooglePayEnrollmentResponse)_serializer
                .Deserialize(RealResponseBody, typeof(GooglePayEnrollmentResponse));

            response.ShouldNotBeNull();
            response.MerchantId.ShouldBe("12345678901234567890");
            response.TosAcceptedTime.ShouldNotBeNull();
            response.State.ShouldBe(GooglePayEnrollmentState.Active);
        }

        /// <summary>
        /// merchant_id is what the caller needs to initialise Google Pay on the client, so
        /// losing it is the whole defect. Asserted on its own to make that unmissable.
        /// </summary>
        [Fact]
        public void ShouldNotSilentlyDropMerchantId()
        {
            var response = (GooglePayEnrollmentResponse)_serializer
                .Deserialize(RealResponseBody, typeof(GooglePayEnrollmentResponse));

            response.MerchantId.ShouldNotBeNullOrEmpty();
        }

        /// <summary>
        /// The API does not always return every field, and a missing merchant_id must come back
        /// null rather than empty: a caller has to be able to tell "not returned" from "blank".
        /// </summary>
        [Fact]
        public void ShouldLeaveMerchantIdNullWhenAbsent()
        {
            const string json = @"{
                ""tos_accepted_time"": ""2026-08-13T09:12:41Z"",
                ""state"": ""ACTIVE""
            }";

            var response = (GooglePayEnrollmentResponse)_serializer
                .Deserialize(json, typeof(GooglePayEnrollmentResponse));

            response.MerchantId.ShouldBeNull();
            response.State.ShouldBe(GooglePayEnrollmentState.Active);
        }

        [Fact]
        public void ShouldSerializeMerchantIdAsSnakeCase()
        {
            var response = new GooglePayEnrollmentResponse
            {
                MerchantId = "12345678901234567890",
                State = GooglePayEnrollmentState.Active
            };

            var json = _serializer.Serialize(response);

            json.ShouldContain("\"merchant_id\":\"12345678901234567890\"");
        }
    }
}
