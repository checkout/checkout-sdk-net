using Checkout.Payments;
using Checkout.Payments.Links;
using Shouldly;
using Xunit;

namespace Checkout.Payments.Links
{
    public class PaymentLinkRequestSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        [Fact]
        public void ShouldSerializeWithPaymentPlanAndAuthorizationType()
        {
            var request = new PaymentLinkRequest
            {
                AuthorizationType = AuthorizationType.Estimated,
                PaymentPlan = new PaymentPlan
                {
                    AmountVariability = AmountVariabilityType.Variable,
                    Amount = 1234L,
                    DaysBetweenPayments = 28,
                    TotalNumberOfPayments = 5,
                    CurrentPaymentNumber = 3,
                    Expiry = "20251031",
                    Name = "Subscription 1234",
                    StartDate = "20260507"
                }
            };

            Should.NotThrow(() => Serializer.Serialize(request));
        }

        [Fact]
        public void ShouldRoundTripSerializeAuthorizationType()
        {
            var original = new PaymentLinkRequest { AuthorizationType = AuthorizationType.Estimated };

            var json = Serializer.Serialize(original);
            var deserialized = (PaymentLinkRequest)Serializer.Deserialize(json, typeof(PaymentLinkRequest));

            json.ShouldContain("\"authorization_type\":\"Estimated\"");
            deserialized.AuthorizationType.ShouldBe(AuthorizationType.Estimated);
        }

        [Fact]
        public void ShouldRoundTripSerializePaymentPlan()
        {
            var original = new PaymentLinkRequest
            {
                PaymentPlan = new PaymentPlan
                {
                    AmountVariability = AmountVariabilityType.Variable,
                    Amount = 1234L,
                    DaysBetweenPayments = 28,
                    TotalNumberOfPayments = 5,
                    CurrentPaymentNumber = 3,
                    Expiry = "20251031",
                    Name = "Subscription 1234",
                    StartDate = "20260507"
                }
            };

            var json = Serializer.Serialize(original);
            var deserialized = (PaymentLinkRequest)Serializer.Deserialize(json, typeof(PaymentLinkRequest));

            json.ShouldContain("\"payment_plan\":");
            json.ShouldContain("\"amount_variability\":\"Variable\"");
            deserialized.PaymentPlan.ShouldNotBeNull();
            deserialized.PaymentPlan.AmountVariability.ShouldBe(AmountVariabilityType.Variable);
            deserialized.PaymentPlan.Amount.ShouldBe(1234L);
            deserialized.PaymentPlan.DaysBetweenPayments.ShouldBe(28);
            deserialized.PaymentPlan.TotalNumberOfPayments.ShouldBe(5);
            deserialized.PaymentPlan.CurrentPaymentNumber.ShouldBe(3);
            deserialized.PaymentPlan.Expiry.ShouldBe("20251031");
            deserialized.PaymentPlan.Name.ShouldBe("Subscription 1234");
            deserialized.PaymentPlan.StartDate.ShouldBe("20260507");
        }

        [Fact]
        public void ShouldDeserializePaymentPlanAndAuthorizationType()
        {
            const string json = @"{
                ""authorization_type"": ""Estimated"",
                ""payment_plan"": {
                    ""amount_variability"": ""Fixed"",
                    ""amount"": 999,
                    ""days_between_payments"": 30,
                    ""total_number_of_payments"": 12,
                    ""name"": ""Plan A""
                }
            }";

            var result = (PaymentLinkRequest)Serializer.Deserialize(json, typeof(PaymentLinkRequest));

            result.ShouldNotBeNull();
            result.AuthorizationType.ShouldBe(AuthorizationType.Estimated);
            result.PaymentPlan.ShouldNotBeNull();
            result.PaymentPlan.AmountVariability.ShouldBe(AmountVariabilityType.Fixed);
            result.PaymentPlan.Amount.ShouldBe(999L);
            result.PaymentPlan.DaysBetweenPayments.ShouldBe(30);
            result.PaymentPlan.TotalNumberOfPayments.ShouldBe(12);
            result.PaymentPlan.Name.ShouldBe("Plan A");
        }
    }
}
