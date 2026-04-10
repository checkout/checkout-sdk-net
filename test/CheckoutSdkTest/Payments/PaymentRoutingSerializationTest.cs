using Checkout.Payments.Request;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace Checkout.Payments
{
    public class PaymentRoutingSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        [Fact]
        public void ShouldSerializePaymentRoutingWithAllSchemes()
        {
            var routing = new PaymentRouting
            {
                Attempts = new List<PaymentRoutingAttempt>
                {
                    new PaymentRoutingAttempt { Scheme = PaymentRoutingScheme.Mastercard },
                    new PaymentRoutingAttempt { Scheme = PaymentRoutingScheme.Visa }
                }
            };

            var json = Serializer.Serialize(routing);

            json.ShouldContain("\"mastercard\"");
            json.ShouldContain("\"visa\"");
        }

        [Fact]
        public void ShouldRoundTripSerializePaymentRouting()
        {
            var original = new PaymentRouting
            {
                Attempts = new List<PaymentRoutingAttempt>
                {
                    new PaymentRoutingAttempt { Scheme = PaymentRoutingScheme.Mastercard },
                    new PaymentRoutingAttempt { Scheme = PaymentRoutingScheme.Visa }
                }
            };

            var json = Serializer.Serialize(original);
            var result = (PaymentRouting)Serializer.Deserialize(json, typeof(PaymentRouting));

            result.ShouldNotBeNull();
            result.Attempts.Count.ShouldBe(2);
            result.Attempts[0].Scheme.ShouldBe(PaymentRoutingScheme.Mastercard);
            result.Attempts[1].Scheme.ShouldBe(PaymentRoutingScheme.Visa);
        }

        [Fact]
        public void ShouldDeserializeAllSchemeValues()
        {
            const string json = @"{
                ""attempts"": [
                    { ""scheme"": ""accel"" },
                    { ""scheme"": ""amex"" },
                    { ""scheme"": ""cartes_bancaires"" },
                    { ""scheme"": ""diners"" },
                    { ""scheme"": ""discover"" },
                    { ""scheme"": ""jcb"" },
                    { ""scheme"": ""mada"" },
                    { ""scheme"": ""maestro"" },
                    { ""scheme"": ""mastercard"" },
                    { ""scheme"": ""nyce"" },
                    { ""scheme"": ""omannet"" },
                    { ""scheme"": ""pulse"" },
                    { ""scheme"": ""shazam"" },
                    { ""scheme"": ""star"" },
                    { ""scheme"": ""upi"" },
                    { ""scheme"": ""visa"" }
                ]
            }";

            var result = (PaymentRouting)Serializer.Deserialize(json, typeof(PaymentRouting));

            result.ShouldNotBeNull();
            result.Attempts.Count.ShouldBe(16);
            result.Attempts[0].Scheme.ShouldBe(PaymentRoutingScheme.Accel);
            result.Attempts[1].Scheme.ShouldBe(PaymentRoutingScheme.Amex);
            result.Attempts[2].Scheme.ShouldBe(PaymentRoutingScheme.CartesBancaires);
            result.Attempts[3].Scheme.ShouldBe(PaymentRoutingScheme.Diners);
            result.Attempts[4].Scheme.ShouldBe(PaymentRoutingScheme.Discover);
            result.Attempts[5].Scheme.ShouldBe(PaymentRoutingScheme.Jcb);
            result.Attempts[6].Scheme.ShouldBe(PaymentRoutingScheme.Mada);
            result.Attempts[7].Scheme.ShouldBe(PaymentRoutingScheme.Maestro);
            result.Attempts[8].Scheme.ShouldBe(PaymentRoutingScheme.Mastercard);
            result.Attempts[9].Scheme.ShouldBe(PaymentRoutingScheme.Nyce);
            result.Attempts[10].Scheme.ShouldBe(PaymentRoutingScheme.Omannet);
            result.Attempts[11].Scheme.ShouldBe(PaymentRoutingScheme.Pulse);
            result.Attempts[12].Scheme.ShouldBe(PaymentRoutingScheme.Shazam);
            result.Attempts[13].Scheme.ShouldBe(PaymentRoutingScheme.Star);
            result.Attempts[14].Scheme.ShouldBe(PaymentRoutingScheme.Upi);
            result.Attempts[15].Scheme.ShouldBe(PaymentRoutingScheme.Visa);
        }

        [Fact]
        public void ShouldSerializeRoutingInsidePaymentRequest()
        {
            var request = new PaymentRequest
            {
                Amount = 1000,
                Currency = Common.Currency.GBP,
                Routing = new PaymentRouting
                {
                    Attempts = new List<PaymentRoutingAttempt>
                    {
                        new PaymentRoutingAttempt { Scheme = PaymentRoutingScheme.Mastercard },
                        new PaymentRoutingAttempt { Scheme = PaymentRoutingScheme.Visa }
                    }
                }
            };

            var json = Serializer.Serialize(request);

            json.ShouldContain("\"routing\"");
            json.ShouldContain("\"attempts\"");
            json.ShouldContain("\"mastercard\"");
            json.ShouldContain("\"visa\"");
        }
    }
}
