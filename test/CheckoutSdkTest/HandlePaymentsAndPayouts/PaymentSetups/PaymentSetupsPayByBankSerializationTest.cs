using Checkout.Payments.Setups.Entities;
using Checkout.Payments.Setups.Requests;
using Checkout.Payments.Setups.Responses;
using Newtonsoft.Json.Linq;
using Shouldly;
using Xunit;
using SetupPaymentMethods = Checkout.Payments.Setups.Entities.PaymentMethods;

namespace Checkout.HandlePaymentsAndPayouts.PaymentSetups
{
    /// <summary>
    /// Schema validation tests for the Pay by Bank payment method wire key and the risk settings on
    /// Payment Setups. The tests assert the literal JSON keys, not only the member names.
    /// </summary>
    public class PaymentSetupsPayByBankSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        private const string PayByBankResponseJson = @"{
            ""status"": ""action_required"",
            ""flags"": [""flag_1""],
            ""bank_id"": ""bank_123"",
            ""action"": {
                ""type"": ""select_bank"",
                ""banks"": [
                    {
                        ""bank_id"": ""bank_123"",
                        ""display_name"": ""Example Bank"",
                        ""logo_url"": ""https://example.com/logo.png"",
                        ""available"": true
                    }
                ]
            }
        }";

        private static PaymentSetupsRequest CreateRequest()
        {
            return new PaymentSetupsRequest
            {
                PaymentMethods = new SetupPaymentMethods { PayByBank = new PayByBank { BankId = "bank_123" } },
                Risk = new PaymentSetupRisk { Enabled = false }
            };
        }

        // ------------------------------------------------------------------------
        // PayByBank (request side)
        // payment_methods.paybybank: the wire key is one lowercase word.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializePayByBankUnderSingleWordKey()
        {
            var json = Serializer.Serialize(CreateRequest());
            var methods = (JObject)JObject.Parse(json)["payment_methods"];

            methods.ContainsKey("paybybank").ShouldBeTrue();
            // Ordinal checks: Shouldly's ShouldNotContain ignores case.
            json.Contains("pay_by_bank").ShouldBeFalse();
            json.Contains("payByBank").ShouldBeFalse();
            ((string)methods["paybybank"]["bank_id"]).ShouldBe("bank_123");
        }

        // ------------------------------------------------------------------------
        // PayByBank (response side)
        // payment_methods.paybybank.action with the bank list.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldDeserializePayByBankActionFromResponse()
        {
            var json = @"{ ""id"": ""set_pbb"", ""payment_methods"": { ""paybybank"": " + PayByBankResponseJson + " } }";

            var response = (PaymentSetupsResponse)Serializer.Deserialize(json, typeof(PaymentSetupsResponse));
            AssertPayByBank(response.PaymentMethods.PayByBank);
        }

        [Fact]
        public void ShouldDeserializePayByBankInConfirmResponse()
        {
            var json = @"{ ""id"": ""set_pbb"", ""payment_methods"": { ""paybybank"": " + PayByBankResponseJson + " } }";

            var response = (PaymentSetupsConfirmResponse)Serializer.Deserialize(json, typeof(PaymentSetupsConfirmResponse));
            AssertPayByBank(response.PaymentMethods.PayByBank);
        }

        [Fact]
        public void ShouldIgnoreOldSnakeCaseKeyOnResponse()
        {
            var json = @"{ ""payment_methods"": { ""pay_by_bank"": " + PayByBankResponseJson + " } }";

            var response = (PaymentSetupsResponse)Serializer.Deserialize(json, typeof(PaymentSetupsResponse));
            response.PaymentMethods.PayByBank.ShouldBeNull();
        }

        [Fact]
        public void ShouldRoundTripPayByBankWithEveryProperty()
        {
            var original = (PayByBank)Serializer.Deserialize(PayByBankResponseJson, typeof(PayByBank));
            var json = JObject.Parse(Serializer.Serialize(original));

            // status, flags, bank_id, action, plus the deprecated initialization, still sent by default.
            json.Count.ShouldBe(5);
            var bank = (JObject)json["action"]["banks"][0];
            bank.Count.ShouldBe(4);
            bank.ContainsKey("display_name").ShouldBeTrue();
            bank.ContainsKey("logo_url").ShouldBeTrue();

            AssertPayByBank((PayByBank)Serializer.Deserialize(json.ToString(), typeof(PayByBank)));
        }

        private static void AssertPayByBank(PayByBank payByBank)
        {
            payByBank.ShouldNotBeNull();
            payByBank.Status.ShouldBe(PaymentMethodStatus.ActionRequired);
            payByBank.Flags.ShouldBe(new[] { "flag_1" });
            payByBank.BankId.ShouldBe("bank_123");
            payByBank.Action.Type.ShouldBe("select_bank");
            payByBank.Action.Banks.Count.ShouldBe(1);
            payByBank.Action.Banks[0].BankId.ShouldBe("bank_123");
            payByBank.Action.Banks[0].DisplayName.ShouldBe("Example Bank");
            payByBank.Action.Banks[0].LogoUrl.ShouldBe("https://example.com/logo.png");
            payByBank.Action.Banks[0].Available.ShouldBe(true);
        }

        // ------------------------------------------------------------------------
        // PaymentSetupRisk
        // risk.enabled on request and response.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeRiskEnabled()
        {
            var risk = (JObject)JObject.Parse(Serializer.Serialize(CreateRequest()))["risk"];

            risk.Count.ShouldBe(1);
            ((bool)risk["enabled"]).ShouldBeFalse();
        }

        [Fact]
        public void ShouldOmitRiskWhenNotSet()
        {
            JObject.Parse(Serializer.Serialize(new PaymentSetupsRequest())).ContainsKey("risk").ShouldBeFalse();
            Serializer.Serialize(new PaymentSetupRisk()).ShouldBe("{}");
        }

        [Fact]
        public void ShouldDeserializeRiskFromResponses()
        {
            const string json = @"{ ""id"": ""set_pbb"", ""risk"": { ""enabled"": true } }";

            ((PaymentSetupsResponse)Serializer.Deserialize(json, typeof(PaymentSetupsResponse))).Risk.Enabled.ShouldBe(true);
            ((PaymentSetupsConfirmResponse)Serializer.Deserialize(json, typeof(PaymentSetupsConfirmResponse))).Risk.Enabled.ShouldBe(true);
        }
    }
}
