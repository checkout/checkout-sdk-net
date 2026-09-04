using Checkout.HandlePaymentsAndPayouts.GooglePay.Entities;
using Checkout.HandlePaymentsAndPayouts.GooglePay.Requests;
using Checkout.HandlePaymentsAndPayouts.GooglePay.Responses;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace Checkout.HandlePaymentsAndPayouts.GooglePay
{
    /// <summary>
    /// Schema validation tests for Checkout.HandlePaymentsAndPayouts.GooglePay.
    /// Grouped by domain; each section below covers one subject.
    /// </summary>
    public class GooglePaySerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        // ------------------------------------------------------------------------
        // GooglePayEnrollmentRequest
        // ------------------------------------------------------------------------

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
        public void ShouldRoundTripSerializeForGooglePayEnrollmentRequest()
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
        public void ShouldDeserializeSwaggerExampleForGooglePayEnrollmentRequest()
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

        // ------------------------------------------------------------------------
        // GooglePayEnrollmentResponse
        // Covers the Google Pay enrollment response against the body the API really returns.
        // <para>
        // The spec declares only tosAcceptedTime and state, with additionalProperties false, so
        // merchant_id was missing from the model. A merchant reported it. These tests deserialize
        // the real sandbox 201 body rather than the swagger example, because the swagger example is
        // the thing that was wrong.
        // </para>
        // ------------------------------------------------------------------------

        // Captured from a real POST /googlepay/enrollments 201 in sandbox.
        private const string RealResponseBody = @"{
            ""merchant_id"": ""12345678901234567890"",
            ""tos_accepted_time"": ""2026-08-13T09:12:41Z"",
            ""state"": ""ACTIVE""
        }";

        [Fact]
        public void ShouldDeserializeTheRealEnrollmentResponse()
        {
            var response = (GooglePayEnrollmentResponse)Serializer
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
            var response = (GooglePayEnrollmentResponse)Serializer
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

            var response = (GooglePayEnrollmentResponse)Serializer
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

            var json = Serializer.Serialize(response);

            json.ShouldContain("\"merchant_id\":\"12345678901234567890\"");
        }

        // ------------------------------------------------------------------------
        // GooglePayDomainListResponse
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeWithDomains()
        {
            var response = new GooglePayDomainListResponse
            {
                Domains = new List<string> { "example.com", "shop.example.com" }
            };

            Should.NotThrow(() => new JsonSerializer().Serialize(response));
        }

        [Fact]
        public void ShouldDeserializeSwaggerExampleForGooglePayDomainListResponse()
        {
            const string json = @"{
                ""domains"": [""example.com"", ""shop.example.com""]
            }";

            var response = (GooglePayDomainListResponse)new JsonSerializer()
                .Deserialize(json, typeof(GooglePayDomainListResponse));

            response.ShouldNotBeNull();
            response.Domains.ShouldNotBeNull();
            response.Domains.Count.ShouldBe(2);
            response.Domains[0].ShouldBe("example.com");
        }

        [Fact]
        public void ShouldRoundTripSerializeForGooglePayDomainListResponse()
        {
            var original = new GooglePayDomainListResponse
            {
                Domains = new List<string> { "example.com" }
            };
            var serializer = new JsonSerializer();

            var json = serializer.Serialize(original);
            var deserialized = (GooglePayDomainListResponse)serializer.Deserialize(json, typeof(GooglePayDomainListResponse));

            deserialized.Domains.ShouldNotBeNull();
            deserialized.Domains[0].ShouldBe("example.com");
        }
    }
}
