using Checkout.Payments.Response;
using Shouldly;
using Xunit;

namespace Checkout.Payments
{
    /// <summary>
    /// Schema validation tests for Checkout.Payments.RiskAssessment.
    /// Grouped by domain; each section below covers one subject.
    /// </summary>
    public class RiskAssessmentSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        // ------------------------------------------------------------------------
        // RiskAssessment
        // Returns the payment's risk assessment results. Covers both properties
        // (flagged, score) against the risk object of the PaymentResponse and
        // PaymentDetails schemas, where score is type number, min 0, max 100.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldDeserializeFractionalScoreWithoutRounding()
        {
            var risk = (RiskAssessment)Serializer.Deserialize("{\"flagged\":true,\"score\":22.5}", typeof(RiskAssessment));

            risk.Flagged.ShouldBe(true);
            risk.Score.ShouldBe(22.5);
        }

        [Fact]
        public void ShouldDeserializeWholeNumberScore()
        {
            var risk = (RiskAssessment)Serializer.Deserialize("{\"flagged\":false,\"score\":22}", typeof(RiskAssessment));

            risk.Flagged.ShouldBe(false);
            risk.Score.ShouldBe(22.0);
        }

        [Fact]
        public void ShouldDeserializeBoundaryScores()
        {
            ((RiskAssessment)Serializer.Deserialize("{\"score\":0}", typeof(RiskAssessment))).Score.ShouldBe(0.0);
            ((RiskAssessment)Serializer.Deserialize("{\"score\":100}", typeof(RiskAssessment))).Score.ShouldBe(100.0);
        }

        [Fact]
        public void ShouldDeserializeWithNullOptionalProperties()
        {
            var risk = (RiskAssessment)Serializer.Deserialize("{}", typeof(RiskAssessment));

            risk.Flagged.ShouldBeNull();
            risk.Score.ShouldBeNull();
        }

        [Fact]
        public void ShouldSerializeFractionalScore()
        {
            var json = Serializer.Serialize(new RiskAssessment { Flagged = true, Score = 22.5 });

            json.ShouldContain("\"flagged\":true");
            json.ShouldContain("\"score\":22.5");
        }

        [Fact]
        public void ShouldRoundTripAllProperties()
        {
            var original = new RiskAssessment { Flagged = true, Score = 22.5 };

            var deserialized = (RiskAssessment)Serializer.Deserialize(Serializer.Serialize(original), typeof(RiskAssessment));

            deserialized.Flagged.ShouldBe(original.Flagged);
            deserialized.Score.ShouldBe(original.Score);
        }

        [Fact]
        public void ShouldDeserializeSwaggerExampleInGetPaymentResponse()
        {
            var json = "{\"id\":\"pay_mbabizu24mvu3mela5njyhpit4\",\"risk\":{\"flagged\":true,\"score\":22}}";

            var response = (GetPaymentResponse)Serializer.Deserialize(json, typeof(GetPaymentResponse));

            response.Risk.ShouldNotBeNull();
            response.Risk.Flagged.ShouldBe(true);
            response.Risk.Score.ShouldBe(22.0);
        }

        [Fact]
        public void ShouldDeserializeFractionalScoreInGetPaymentResponse()
        {
            var json = "{\"id\":\"pay_123\",\"risk\":{\"flagged\":true,\"score\":22.5}}";

            var response = (GetPaymentResponse)Serializer.Deserialize(json, typeof(GetPaymentResponse));

            response.Risk.Score.ShouldBe(22.5);
        }

        [Fact]
        public void ShouldDeserializeFractionalScoreInPaymentResponse()
        {
            var json = "{\"id\":\"pay_123\",\"risk\":{\"flagged\":false,\"score\":22.7}}";

            var response = (PaymentResponse)Serializer.Deserialize(json, typeof(PaymentResponse));

            response.Risk.Score.ShouldBe(22.7);
        }
    }
}
