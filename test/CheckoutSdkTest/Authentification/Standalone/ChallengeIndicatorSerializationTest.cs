using Checkout.Payments;
using Shouldly;
using System;
using Xunit;
using CommonChallengeIndicatorType = Checkout.Common.ChallengeIndicatorType;
using GetSessionDetailsResponseOk =
    Checkout.Authentication.Standalone.GETSessionsId.Responses.GetSessionDetailsResponseOk.GetSessionDetailsResponseOk;
using RequestASessionRequestBody =
    Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.RequestASessionRequest;
using RequestASessionResponseAccepted =
    Checkout.Authentication.Standalone.POSTSessions.Responses.RequestASessionResponseAccepted.
    RequestASessionResponseAccepted;
using RequestASessionResponseCreated =
    Checkout.Authentication.Standalone.POSTSessions.Responses.RequestASessionResponseCreated.
    RequestASessionResponseCreated;
using RequestChallengeIndicatorType =
    Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.ChallengeIndicatorType;
using ResponseChallengeIndicatorType = Checkout.Authentication.Standalone.Common.Responses.ChallengeIndicatorType;
using UpdateASessionResponseOk =
    Checkout.Authentication.Standalone.PUTSessionsIdCollectData.Responses.UpdateASessionResponseOk.
    UpdateASessionResponseOk;

namespace Checkout.Authentification.Standalone
{
    /// <summary>
    /// Covers the three challenge-indicator enums and their call sites:
    /// the nine-value request enum on POST /sessions, the nine-value response enum returned by the
    /// session responses, and the four-value shared enum used by the payments 3ds field.
    /// </summary>
    public class ChallengeIndicatorSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        [Theory]
        [InlineData(RequestChallengeIndicatorType.NoPreference, "no_preference")]
        [InlineData(RequestChallengeIndicatorType.NoChallengeRequested, "no_challenge_requested")]
        [InlineData(RequestChallengeIndicatorType.ChallengeRequested, "challenge_requested")]
        [InlineData(RequestChallengeIndicatorType.ChallengeRequestedMandate, "challenge_requested_mandate")]
        [InlineData(RequestChallengeIndicatorType.LowValue, "low_value")]
        [InlineData(RequestChallengeIndicatorType.TrustedListing, "trusted_listing")]
        [InlineData(RequestChallengeIndicatorType.TrustedListingPrompt, "trusted_listing_prompt")]
        [InlineData(RequestChallengeIndicatorType.TransactionRiskAssessment, "transaction_risk_assessment")]
        [InlineData(RequestChallengeIndicatorType.DataShare, "data_share")]
        public void ShouldSerializeEveryRequestValueOnSessionRequest(
            RequestChallengeIndicatorType value,
            string expected)
        {
            var request = new RequestASessionRequestBody { ChallengeIndicator = value };

            var json = Serializer.Serialize(request);

            json.ShouldContain("\"challenge_indicator\":\"" + expected + "\"");
        }

        [Theory]
        [InlineData(RequestChallengeIndicatorType.NoPreference)]
        [InlineData(RequestChallengeIndicatorType.NoChallengeRequested)]
        [InlineData(RequestChallengeIndicatorType.ChallengeRequested)]
        [InlineData(RequestChallengeIndicatorType.ChallengeRequestedMandate)]
        [InlineData(RequestChallengeIndicatorType.LowValue)]
        [InlineData(RequestChallengeIndicatorType.TrustedListing)]
        [InlineData(RequestChallengeIndicatorType.TrustedListingPrompt)]
        [InlineData(RequestChallengeIndicatorType.TransactionRiskAssessment)]
        [InlineData(RequestChallengeIndicatorType.DataShare)]
        public void ShouldRoundTripEveryRequestValue(RequestChallengeIndicatorType value)
        {
            var json = Serializer.Serialize(new RequestASessionRequestBody { ChallengeIndicator = value });

            var deserialized = (RequestASessionRequestBody)Serializer.Deserialize(
                json, typeof(RequestASessionRequestBody));

            deserialized.ChallengeIndicator.ShouldBe(value);
        }

        [Fact]
        public void ShouldDefaultRequestChallengeIndicatorToNoPreference()
        {
            var request = new RequestASessionRequestBody();

            request.ChallengeIndicator.ShouldBe(RequestChallengeIndicatorType.NoPreference);
            Serializer.Serialize(request).ShouldContain("\"challenge_indicator\":\"no_preference\"");
        }

        /// <summary>
        /// The API Reference specifies only the four base values for the session response fields, but
        /// the request accepts nine. An exemption value echoed back must still deserialize: because
        /// the response property is a non-nullable enum, an unrecognised value would otherwise be
        /// tolerated into the default NoPreference and become indistinguishable from a real
        /// no_preference.
        /// </summary>
        [Theory]
        [InlineData(ResponseChallengeIndicatorType.NoPreference, "no_preference")]
        [InlineData(ResponseChallengeIndicatorType.NoChallengeRequested, "no_challenge_requested")]
        [InlineData(ResponseChallengeIndicatorType.ChallengeRequested, "challenge_requested")]
        [InlineData(ResponseChallengeIndicatorType.ChallengeRequestedMandate, "challenge_requested_mandate")]
        [InlineData(ResponseChallengeIndicatorType.LowValue, "low_value")]
        [InlineData(ResponseChallengeIndicatorType.TrustedListing, "trusted_listing")]
        [InlineData(ResponseChallengeIndicatorType.TrustedListingPrompt, "trusted_listing_prompt")]
        [InlineData(ResponseChallengeIndicatorType.TransactionRiskAssessment, "transaction_risk_assessment")]
        [InlineData(ResponseChallengeIndicatorType.DataShare, "data_share")]
        public void ShouldDeserializeEveryResponseValueOnEverySessionResponse(
            ResponseChallengeIndicatorType expected,
            string wireValue)
        {
            var json = "{\"challenge_indicator\":\"" + wireValue + "\"}";

            var created = (RequestASessionResponseCreated)Serializer.Deserialize(
                json, typeof(RequestASessionResponseCreated));
            var accepted = (RequestASessionResponseAccepted)Serializer.Deserialize(
                json, typeof(RequestASessionResponseAccepted));
            var details = (GetSessionDetailsResponseOk)Serializer.Deserialize(
                json, typeof(GetSessionDetailsResponseOk));
            var updated = (UpdateASessionResponseOk)Serializer.Deserialize(
                json, typeof(UpdateASessionResponseOk));

            created.ChallengeIndicator.ShouldBe(expected);
            accepted.ChallengeIndicator.ShouldBe(expected);
            details.ChallengeIndicator.ShouldBe(expected);
            updated.ChallengeIndicator.ShouldBe(expected);
        }

        [Theory]
        [InlineData(CommonChallengeIndicatorType.NoPreference, "no_preference")]
        [InlineData(CommonChallengeIndicatorType.NoChallengeRequested, "no_challenge_requested")]
        [InlineData(CommonChallengeIndicatorType.ChallengeRequested, "challenge_requested")]
        [InlineData(CommonChallengeIndicatorType.ChallengeRequestedMandate, "challenge_requested_mandate")]
        public void ShouldRoundTripEverySharedPaymentsValue(CommonChallengeIndicatorType value, string wireValue)
        {
            var request = new ThreeDsRequest { ChallengeIndicator = value };

            var json = Serializer.Serialize(request);

            json.ShouldContain("\"challenge_indicator\":\"" + wireValue + "\"");

            var deserialized = (ThreeDsRequest)Serializer.Deserialize(json, typeof(ThreeDsRequest));
            deserialized.ChallengeIndicator.ShouldBe(value);
        }

        /// <summary>
        /// The payments 3ds field must stay bound to the four-value shared enum. Assigning a sessions
        /// exemption value to it should not compile, so this guards the binding by type identity: if
        /// ThreeDsRequest.ChallengeIndicator were retyped to either sessions enum, this fails.
        /// </summary>
        [Fact]
        public void ShouldBindPaymentsThreeDsToTheSharedFourValueEnum()
        {
            var property = typeof(ThreeDsRequest).GetProperty(nameof(ThreeDsRequest.ChallengeIndicator));

            property.ShouldNotBeNull();
            Nullable.GetUnderlyingType(property.PropertyType).ShouldBe(typeof(CommonChallengeIndicatorType));
            Enum.GetValues(typeof(CommonChallengeIndicatorType)).Length.ShouldBe(4);
        }

        /// <summary>
        /// Guards the split: the shared payments enum must expose only the four base values, while
        /// both sessions enums expose all nine. If the exemption values leak back onto the shared
        /// enum they would be offered on POST /payments, where the API rejects them.
        /// </summary>
        [Fact]
        public void ShouldKeepTheSharedPaymentsEnumNarrowAndTheSessionsEnumsWide()
        {
            Enum.GetValues(typeof(CommonChallengeIndicatorType)).Length.ShouldBe(4);
            Enum.GetValues(typeof(RequestChallengeIndicatorType)).Length.ShouldBe(9);
            Enum.GetValues(typeof(ResponseChallengeIndicatorType)).Length.ShouldBe(9);
        }
    }
}
