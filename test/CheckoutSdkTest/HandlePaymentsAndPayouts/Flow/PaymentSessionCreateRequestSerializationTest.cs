using Checkout.HandlePaymentsAndPayouts.Flow.Requests;
using Checkout.Payments;
using Shouldly;
using Xunit;

namespace Checkout.HandlePaymentsAndPayouts.Flow
{
    public class PaymentSessionCreateRequestSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        [Fact]
        public void ShouldSerializeWithPaymentPlanAndAuthorizationType()
        {
            var request = new PaymentSessionCreateRequest
            {
                AuthorizationType = AuthorizationType.Estimated,
                PaymentPlan = new PaymentPlan
                {
                    AmountVariability = AmountVariabilityType.Variable,
                    Amount = 1234L,
                    DaysBetweenPayments = 28,
                    TotalNumberOfPayments = 5,
                    Name = "Subscription 1234"
                }
            };

            Should.NotThrow(() => Serializer.Serialize(request));
        }

        [Fact]
        public void ShouldRoundTripSerializeAuthorizationTypeAndPaymentPlan()
        {
            var original = new PaymentSessionCreateRequest
            {
                AuthorizationType = AuthorizationType.Estimated,
                PaymentPlan = new PaymentPlan
                {
                    AmountVariability = AmountVariabilityType.Variable,
                    Amount = 1234L,
                    Name = "Subscription 1234"
                }
            };

            var json = Serializer.Serialize(original);
            var deserialized = (PaymentSessionCreateRequest)Serializer.Deserialize(json, typeof(PaymentSessionCreateRequest));

            json.ShouldContain("\"authorization_type\":\"Estimated\"");
            json.ShouldContain("\"payment_plan\":");
            deserialized.AuthorizationType.ShouldBe(AuthorizationType.Estimated);
            deserialized.PaymentPlan.ShouldNotBeNull();
            deserialized.PaymentPlan.AmountVariability.ShouldBe(AmountVariabilityType.Variable);
            deserialized.PaymentPlan.Amount.ShouldBe(1234L);
            deserialized.PaymentPlan.Name.ShouldBe("Subscription 1234");
        }

        [Fact]
        public void ShouldDeserializeAuthorizationTypeAndPaymentPlan()
        {
            const string json = @"{
                ""authorization_type"": ""Final"",
                ""payment_plan"": {
                    ""amount_variability"": ""Fixed"",
                    ""amount"": 500,
                    ""total_number_of_payments"": 6
                }
            }";

            var result = (PaymentSessionCreateRequest)Serializer.Deserialize(json, typeof(PaymentSessionCreateRequest));

            result.ShouldNotBeNull();
            result.AuthorizationType.ShouldBe(AuthorizationType.Final);
            result.PaymentPlan.ShouldNotBeNull();
            result.PaymentPlan.AmountVariability.ShouldBe(AmountVariabilityType.Fixed);
            result.PaymentPlan.Amount.ShouldBe(500L);
            result.PaymentPlan.TotalNumberOfPayments.ShouldBe(6);
        }
    }
}
