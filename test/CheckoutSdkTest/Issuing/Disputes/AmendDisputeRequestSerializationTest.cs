using System.Collections.Generic;
using Checkout.Issuing.Disputes.Common;
using Checkout.Issuing.Disputes.Requests;
using Checkout.Issuing.Disputes.Responses;
using Shouldly;
using Xunit;

namespace Checkout.Issuing.Disputes
{
    public class AmendDisputeRequestSerializationTest
    {
        [Fact]
        public void ShouldSerializeWithAllProperties()
        {
            var request = new AmendDisputeRequest
            {
                Reason = "4807",
                Amount = 1500,
                Evidence = new List<DisputeEvidence>
                {
                    new DisputeEvidence { Name = "receipt.pdf", Content = "SGVsbG8=", Description = "Receipt" }
                },
                FraudDetails = new IssuingDisputeFraudDetails
                {
                    FraudType = IssuingDisputeFraudType.CardNotPresentFraud,
                    Description = "No online purchases on this date."
                },
                ReasonChangeJustification = "New evidence confirms an unauthorized transaction.",
                ActionResponse = "Updated the reason code as requested."
            };

            var json = new JsonSerializer().Serialize(request);

            json.ShouldContain("\"reason\"");
            json.ShouldContain("\"amount\"");
            json.ShouldContain("\"fraud_details\"");
            json.ShouldContain("\"fraud_type\"");
            json.ShouldContain("card_not_present_fraud");
            json.ShouldContain("\"reason_change_justification\"");
            json.ShouldContain("\"action_response\"");
        }

        [Fact]
        public void ShouldDeserializeSwaggerExample()
        {
            const string swaggerJson = @"{
                ""reason"": ""4807"",
                ""amount"": 1500,
                ""evidence"": [{ ""name"": ""receipt.pdf"", ""content"": ""SGVsbG8="", ""description"": ""Receipt"" }],
                ""fraud_details"": { ""fraud_type"": ""card_not_present_fraud"", ""description"": ""context"" },
                ""reason_change_justification"": ""justification"",
                ""action_response"": ""Updated as requested""
            }";

            var request = (AmendDisputeRequest)new JsonSerializer()
                .Deserialize(swaggerJson, typeof(AmendDisputeRequest));

            request.ShouldNotBeNull();
            request.Reason.ShouldBe("4807");
            request.Amount.ShouldBe(1500);
            request.Evidence.ShouldNotBeNull();
            request.Evidence.Count.ShouldBe(1);
            request.FraudDetails.ShouldNotBeNull();
            request.FraudDetails.FraudType.ShouldBe(IssuingDisputeFraudType.CardNotPresentFraud);
            request.FraudDetails.Description.ShouldBe("context");
            request.ReasonChangeJustification.ShouldBe("justification");
            request.ActionResponse.ShouldBe("Updated as requested");
        }

        [Fact]
        public void ShouldRoundTripSerialize()
        {
            var original = new AmendDisputeRequest
            {
                Reason = "4837",
                Amount = 2000,
                FraudDetails = new IssuingDisputeFraudDetails { FraudType = IssuingDisputeFraudType.AccountTakeover },
                ReasonChangeJustification = "round-trip",
                ActionResponse = "round-trip response"
            };

            var serializer = new JsonSerializer();
            var deserialized = (AmendDisputeRequest)serializer
                .Deserialize(serializer.Serialize(original), typeof(AmendDisputeRequest));

            deserialized.Reason.ShouldBe(original.Reason);
            deserialized.Amount.ShouldBe(original.Amount);
            deserialized.FraudDetails.FraudType.ShouldBe(original.FraudDetails.FraudType);
            deserialized.ReasonChangeJustification.ShouldBe(original.ReasonChangeJustification);
            deserialized.ActionResponse.ShouldBe(original.ActionResponse);
        }

        [Theory]
        [InlineData(IssuingDisputeFraudType.CardLost, "card_lost")]
        [InlineData(IssuingDisputeFraudType.CardStolen, "card_stolen")]
        [InlineData(IssuingDisputeFraudType.CardNeverReceived, "card_never_received")]
        [InlineData(IssuingDisputeFraudType.FraudulentAccount, "fraudulent_account")]
        [InlineData(IssuingDisputeFraudType.CounterfeitCard, "counterfeit_card")]
        [InlineData(IssuingDisputeFraudType.AccountTakeover, "account_takeover")]
        [InlineData(IssuingDisputeFraudType.CardNotPresentFraud, "card_not_present_fraud")]
        [InlineData(IssuingDisputeFraudType.MerchantMisrepresentation, "merchant_misrepresentation")]
        [InlineData(IssuingDisputeFraudType.CardholderManipulation, "cardholder_manipulation")]
        [InlineData(IssuingDisputeFraudType.IncorrectProcessing, "incorrect_processing")]
        [InlineData(IssuingDisputeFraudType.Other, "other")]
        public void ShouldSerializeEachFraudTypeValue(IssuingDisputeFraudType fraudType, string expected)
        {
            var details = new IssuingDisputeFraudDetails { FraudType = fraudType };

            var json = new JsonSerializer().Serialize(details);

            json.ShouldContain($"\"{expected}\"");
        }

        [Fact]
        public void ShouldDeserializeActionDetailsOnDisputeResponse()
        {
            const string swaggerJson = @"{
                ""id"": ""idsp_test"",
                ""action_details"": { ""instructions"": ""Provide a reason code."", ""last_action_response"": ""none"" }
            }";

            var response = (IssuingDisputeResponse)new JsonSerializer()
                .Deserialize(swaggerJson, typeof(IssuingDisputeResponse));

            response.ActionDetails.ShouldNotBeNull();
            response.ActionDetails.Instructions.ShouldBe("Provide a reason code.");
            response.ActionDetails.LastActionResponse.ShouldBe("none");
        }
    }
}
