using System;
using System.Collections.Generic;
using System.Linq;
using Checkout.Common;
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
    /// Schema validation tests for the 28 keys of PaymentSetup.payment_methods in the Payment Setups API
    /// specification that the SDK did not model, and for the members deprecated in the same change.
    /// The tests assert the literal JSON keys and values, not only the member names.
    /// </summary>
    public class PaymentSetupsPaymentMethodsSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        private static readonly string[] NewMethodKeys =
        {
            "paynow", "qpay", "eps", "ideal", "knet", "bancontact", "benefit", "vipps", "twint", "alipay_cn",
            "alipay_hk", "gcash", "tng", "dana", "mobilepay", "tamara", "mbway", "multibanco", "wechatpay",
            "kakaopay", "truemoney", "octopus", "p24", "alma", "swish", "sequra", "ach", "sepa"
        };

        private static SetupPaymentMethods CreateAllNewMethods()
        {
            return new SetupPaymentMethods
            {
                PayNow = new PayNow(),
                Qpay = new Qpay(),
                Eps = new Eps(),
                Ideal = new Ideal(),
                Knet = new Knet(),
                Bancontact = new Bancontact(),
                Benefit = new Benefit(),
                Vipps = new Vipps(),
                Twint = new Twint(),
                AlipayCn = new AlipayCn(),
                AlipayHk = new AlipayHk(),
                GCash = new GCash(),
                Tng = new Tng(),
                Dana = new Dana(),
                MobilePay = new MobilePay(),
                Tamara = new Tamara(),
                MbWay = new MbWay(),
                Multibanco = new Multibanco(),
                WeChatPay = new WeChatPay(),
                KakaoPay = new KakaoPay(),
                TrueMoney = new TrueMoney(),
                Octopus = new Octopus(),
                P24 = new P24(),
                Alma = new Alma(),
                Swish = new Swish(),
                Sequra = new Sequra(),
                Ach = new Ach(),
                Sepa = new Sepa()
            };
        }

        // ------------------------------------------------------------------------
        // PaymentMethods wire keys
        // Every new payment method is sent under the exact key of PaymentSetup.payment_methods. Seven of them
        // carry an explicit JsonProperty because the snake case naming strategy would split them.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeEveryNewPaymentMethodUnderItsSpecKey()
        {
            var methods = (JObject)JObject.Parse(Serializer.Serialize(CreateAllNewMethods()));

            methods.Properties().Select(p => p.Name).OrderBy(k => k, StringComparer.Ordinal)
                .ShouldBe(NewMethodKeys.OrderBy(k => k, StringComparer.Ordinal));
        }

        [Theory]
        [InlineData("g_cash")]
        [InlineData("pay_now")]
        [InlineData("mobile_pay")]
        [InlineData("mb_way")]
        [InlineData("we_chat_pay")]
        [InlineData("kakao_pay")]
        [InlineData("true_money")]
        public void ShouldNotSplitSingleWordKeys(string splitKey)
        {
            var json = Serializer.Serialize(CreateAllNewMethods());

            // Ordinal check: Shouldly's ShouldNotContain ignores case.
            json.Contains("\"" + splitKey + "\"").ShouldBeFalse();
        }

        [Fact]
        public void ShouldDeserializeEveryNewPaymentMethodFromItsSpecKey()
        {
            var body = string.Join(",", NewMethodKeys.Select(k => $"\"{k}\": {{ \"status\": \"ready\" }}"));
            var json = "{ \"id\": \"set_new\", \"payment_methods\": { " + body + " } }";

            var methods = ((PaymentSetupsResponse)Serializer.Deserialize(json, typeof(PaymentSetupsResponse)))
                .PaymentMethods;

            var all = new PaymentSetupPaymentMethod[]
            {
                methods.PayNow, methods.Qpay, methods.Eps, methods.Ideal, methods.Knet, methods.Bancontact,
                methods.Benefit, methods.Vipps, methods.Twint, methods.AlipayCn, methods.AlipayHk, methods.GCash,
                methods.Tng, methods.Dana, methods.MobilePay, methods.Tamara, methods.MbWay, methods.Multibanco,
                methods.WeChatPay, methods.KakaoPay, methods.TrueMoney, methods.Octopus, methods.P24, methods.Alma,
                methods.Swish, methods.Sequra, methods.Ach, methods.Sepa
            };
            all.Length.ShouldBe(NewMethodKeys.Length);
            foreach (var method in all)
            {
                method.ShouldNotBeNull();
                method.Status.ShouldBe(PaymentMethodStatus.Ready);
            }
        }

        [Fact]
        public void ShouldSendOnlyStatusAndFlagsForMethodsWithoutOwnProperties()
        {
            var methods = new SetupPaymentMethods
            {
                PayNow = new PayNow { Status = PaymentMethodStatus.Ready, Flags = new List<string> { "flag_1" } }
            };

            var payNow = (JObject)JObject.Parse(Serializer.Serialize(methods))["paynow"];

            payNow.Count.ShouldBe(2);
            ((string)payNow["status"]).ShouldBe("ready");
            payNow["flags"].ToObject<List<string>>().ShouldBe(new List<string> { "flag_1" });
        }

        // ------------------------------------------------------------------------
        // Swagger example
        // The payment methods with own properties, deserialized from the examples in the API specification.
        // ------------------------------------------------------------------------

        private const string SwaggerExampleJson = @"{
            ""qpay"": { ""status"": ""ready"", ""national_id"": ""21234567890"", ""description"": ""Order 123"" },
            ""ideal"": { ""status"": ""ready"", ""description"": ""2 t-shirts"" },
            ""knet"": { ""status"": ""ready"", ""language"": ""ar"" },
            ""bancontact"": { ""status"": ""ready"", ""account_holder_name"": ""John Smith"" },
            ""multibanco"": { ""status"": ""ready"", ""account_holder_name"": ""John Smith"" },
            ""p24"": { ""status"": ""ready"", ""account_holder"": { ""name"": ""John Smith"", ""email"": ""john.smith@example.com"" } },
            ""swish"": { ""status"": ""ready"", ""billing_descriptor"": ""Example Store"", ""account_holder"": { ""first_name"": ""John"", ""last_name"": ""Smith"" } },
            ""alipay_cn"": { ""status"": ""ready"", ""terminal_type"": ""web"", ""os_type"": ""android"" },
            ""tamara"": {
                ""status"": ""ready"",
                ""initialization"": ""enabled"",
                ""risk_assessment"": { ""premium_customer"": false, ""risk_score"": 42, ""order_count_last24h"": 0, ""linked_device_count"": 2 }
            },
            ""ach"": {
                ""status"": ""ready"",
                ""account_type"": ""savings"",
                ""account_holder"": { ""type"": ""individual"", ""first_name"": ""John"", ""last_name"": ""Smith"" },
                ""country"": ""US""
            },
            ""sepa"": {
                ""status"": ""ready"",
                ""account_holder"": { ""type"": ""corporate"", ""company_name"": ""Example GmbH"" },
                ""country"": ""DE"",
                ""currency"": ""EUR"",
                ""mandate"": { ""id"": ""mandate_123"", ""type"": ""b2b"", ""date_of_signature"": ""2026-01-15"" }
            }
        }";

        [Fact]
        public void ShouldDeserializeSwaggerExampleForNewPaymentMethods()
        {
            var methods = (SetupPaymentMethods)Serializer.Deserialize(SwaggerExampleJson, typeof(SetupPaymentMethods));

            methods.Qpay.NationalId.ShouldBe("21234567890");
            methods.Qpay.Description.ShouldBe("Order 123");
            methods.Ideal.Description.ShouldBe("2 t-shirts");
            methods.Knet.Language.ShouldBe(KnetLanguage.Ar);
            methods.Bancontact.AccountHolderName.ShouldBe("John Smith");
            methods.Multibanco.AccountHolderName.ShouldBe("John Smith");
            methods.P24.AccountHolder.Name.ShouldBe("John Smith");
            methods.P24.AccountHolder.Email.ShouldBe("john.smith@example.com");
            methods.Swish.BillingDescriptor.ShouldBe("Example Store");
            methods.Swish.AccountHolder.FirstName.ShouldBe("John");
            methods.Swish.AccountHolder.LastName.ShouldBe("Smith");
            methods.AlipayCn.TerminalType.ShouldBe(PaymentSetupTerminalType.Web);
            methods.AlipayCn.OsType.ShouldBe(PaymentSetupOsType.Android);
            methods.Tamara.Initialization.ShouldBe(PaymentMethodInitialization.Enabled);
            methods.Tamara.RiskAssessment.PremiumCustomer.ShouldBe(false);
            methods.Tamara.RiskAssessment.RiskScore.ShouldBe(42);
            methods.Tamara.RiskAssessment.OrderCountLast24h.ShouldBe(0);
            methods.Tamara.RiskAssessment.LinkedDeviceCount.ShouldBe(2);
            methods.Ach.AccountType.ShouldBe(PaymentSetupAchAccountType.Savings);
            methods.Ach.AccountHolder.Type.ShouldBe(AchAccountHolderType.Individual);
            methods.Ach.AccountHolder.FirstName.ShouldBe("John");
            methods.Ach.AccountHolder.LastName.ShouldBe("Smith");
            methods.Ach.Country.ShouldBe(CountryCode.US);
            methods.Sepa.AccountHolder.Type.ShouldBe(SepaAccountHolderType.Corporate);
            methods.Sepa.AccountHolder.CompanyName.ShouldBe("Example GmbH");
            methods.Sepa.Country.ShouldBe(CountryCode.DE);
            methods.Sepa.Currency.ShouldBe("EUR");
            methods.Sepa.Mandate.Id.ShouldBe("mandate_123");
            methods.Sepa.Mandate.Type.ShouldBe(PaymentSetupSepaMandateType.B2B);
            methods.Sepa.Mandate.DateOfSignature.ShouldBe(new DateTime(2026, 1, 15));
        }

        // ------------------------------------------------------------------------
        // Round trip with every property
        // Every class with own properties, fully populated: each key is asserted literally and the key count
        // proves nothing extra is sent.
        // ------------------------------------------------------------------------

        private static SetupPaymentMethods CreateFullyPopulatedMethods()
        {
            return new SetupPaymentMethods
            {
                Qpay = new Qpay { NationalId = "21234567890", Description = "Order 123" },
                Ideal = new Ideal { Description = "2 t-shirts" },
                Knet = new Knet { Language = KnetLanguage.En },
                Bancontact = new Bancontact { AccountHolderName = "John Smith" },
                Multibanco = new Multibanco { AccountHolderName = "Jane Doe" },
                P24 = new P24 { AccountHolder = new P24AccountHolder { Name = "John Smith", Email = "john@example.com" } },
                Swish = new Swish
                {
                    BillingDescriptor = "Example Store",
                    AccountHolder = new PaymentSetupSwishAccountHolder { FirstName = "John", LastName = "Smith" }
                },
                AlipayCn = new AlipayCn { TerminalType = PaymentSetupTerminalType.App, OsType = PaymentSetupOsType.Ios },
                AlipayHk = new AlipayHk { TerminalType = PaymentSetupTerminalType.Wap, OsType = PaymentSetupOsType.Android },
                GCash = new GCash { TerminalType = PaymentSetupTerminalType.Web },
                Tng = new Tng { TerminalType = PaymentSetupTerminalType.App, OsType = PaymentSetupOsType.Android },
                Dana = new Dana { TerminalType = PaymentSetupTerminalType.App, OsType = PaymentSetupOsType.Ios },
                KakaoPay = new KakaoPay { TerminalType = PaymentSetupTerminalType.Wap, OsType = PaymentSetupOsType.Ios },
                TrueMoney = new TrueMoney { TerminalType = PaymentSetupTerminalType.App, OsType = PaymentSetupOsType.Android },
                Tamara = new Tamara
                {
                    Initialization = PaymentMethodInitialization.Enabled,
                    RiskAssessment = new TamaraRiskAssessment
                    {
                        PremiumCustomer = true, RiskScore = 7, OrderCountLast24h = 1, LinkedDeviceCount = 3
                    }
                },
                Ach = new Ach
                {
                    AccountType = PaymentSetupAchAccountType.Current,
                    AccountHolder = new AchAccountHolder
                    {
                        Type = AchAccountHolderType.Government,
                        FirstName = "John",
                        LastName = "Smith",
                        CompanyName = "Example Inc",
                        DateOfBirth = "1985-05-15",
                        Identification = new AchAccountHolderIdentification
                        {
                            Type = "ssn", IssuingCountry = "US", Number = "123456789"
                        }
                    },
                    AccountNumber = "4099999992",
                    BankCode = "011075150",
                    Country = CountryCode.US
                },
                Sepa = new Sepa
                {
                    AccountHolder = new SepaAccountHolder
                    {
                        Type = SepaAccountHolderType.Individual,
                        FirstName = "Hans",
                        LastName = "Muller",
                        CompanyName = "Example GmbH"
                    },
                    AccountNumber = "DE89370400440532013000",
                    Country = CountryCode.DE,
                    Currency = "EUR",
                    Mandate = new SepaMandate
                    {
                        Id = "mandate_123", Type = PaymentSetupSepaMandateType.Core, DateOfSignature = new DateTime(2026, 1, 15)
                    }
                }
            };
        }

        [Fact]
        public void ShouldSerializeEveryPropertyOfNewPaymentMethodsUnderSpecKeys()
        {
            var json = JObject.Parse(Serializer.Serialize(CreateFullyPopulatedMethods()));

            AssertKeys(json["qpay"], "national_id", "description");
            AssertKeys(json["ideal"], "description");
            AssertKeys(json["knet"], "language");
            ((string)json["knet"]["language"]).ShouldBe("en");
            AssertKeys(json["bancontact"], "account_holder_name");
            AssertKeys(json["multibanco"], "account_holder_name");
            AssertKeys(json["p24"], "account_holder");
            AssertKeys(json["p24"]["account_holder"], "name", "email");
            AssertKeys(json["swish"], "billing_descriptor", "account_holder");
            AssertKeys(json["swish"]["account_holder"], "first_name", "last_name");
            foreach (var wallet in new[] { "alipay_cn", "alipay_hk", "tng", "dana", "kakaopay", "truemoney" })
            {
                AssertKeys(json[wallet], "terminal_type", "os_type");
            }
            AssertKeys(json["gcash"], "terminal_type");
            AssertKeys(json["tamara"], "initialization", "risk_assessment");
            AssertKeys(json["tamara"]["risk_assessment"],
                "premium_customer", "risk_score", "order_count_last24h", "linked_device_count");
            AssertKeys(json["ach"], "account_type", "account_holder", "account_number", "bank_code", "country");
            AssertKeys(json["ach"]["account_holder"],
                "type", "first_name", "last_name", "company_name", "date_of_birth", "identification");
            AssertKeys(json["ach"]["account_holder"]["identification"], "type", "issuing_country", "number");
            AssertKeys(json["sepa"], "account_holder", "account_number", "country", "currency", "mandate");
            AssertKeys(json["sepa"]["account_holder"], "type", "first_name", "last_name", "company_name");
            AssertKeys(json["sepa"]["mandate"], "id", "type", "date_of_signature");
        }

        [Fact]
        public void ShouldRoundTripEveryPropertyOfNewPaymentMethods()
        {
            var json = Serializer.Serialize(CreateFullyPopulatedMethods());
            var m = (SetupPaymentMethods)Serializer.Deserialize(json, typeof(SetupPaymentMethods));

            m.Qpay.NationalId.ShouldBe("21234567890");
            m.Qpay.Description.ShouldBe("Order 123");
            m.Ideal.Description.ShouldBe("2 t-shirts");
            m.Knet.Language.ShouldBe(KnetLanguage.En);
            m.Bancontact.AccountHolderName.ShouldBe("John Smith");
            m.Multibanco.AccountHolderName.ShouldBe("Jane Doe");
            m.P24.AccountHolder.Name.ShouldBe("John Smith");
            m.P24.AccountHolder.Email.ShouldBe("john@example.com");
            m.Swish.BillingDescriptor.ShouldBe("Example Store");
            m.Swish.AccountHolder.FirstName.ShouldBe("John");
            m.Swish.AccountHolder.LastName.ShouldBe("Smith");
            m.AlipayCn.TerminalType.ShouldBe(PaymentSetupTerminalType.App);
            m.AlipayCn.OsType.ShouldBe(PaymentSetupOsType.Ios);
            m.AlipayHk.TerminalType.ShouldBe(PaymentSetupTerminalType.Wap);
            m.AlipayHk.OsType.ShouldBe(PaymentSetupOsType.Android);
            m.GCash.TerminalType.ShouldBe(PaymentSetupTerminalType.Web);
            m.GCash.OsType.ShouldBeNull();
            m.Tng.OsType.ShouldBe(PaymentSetupOsType.Android);
            m.Dana.OsType.ShouldBe(PaymentSetupOsType.Ios);
            m.KakaoPay.TerminalType.ShouldBe(PaymentSetupTerminalType.Wap);
            m.TrueMoney.TerminalType.ShouldBe(PaymentSetupTerminalType.App);
            m.Tamara.Initialization.ShouldBe(PaymentMethodInitialization.Enabled);
            m.Tamara.RiskAssessment.PremiumCustomer.ShouldBe(true);
            m.Tamara.RiskAssessment.RiskScore.ShouldBe(7);
            m.Tamara.RiskAssessment.OrderCountLast24h.ShouldBe(1);
            m.Tamara.RiskAssessment.LinkedDeviceCount.ShouldBe(3);
            m.Ach.AccountType.ShouldBe(PaymentSetupAchAccountType.Current);
            m.Ach.AccountHolder.Type.ShouldBe(AchAccountHolderType.Government);
            m.Ach.AccountHolder.FirstName.ShouldBe("John");
            m.Ach.AccountHolder.LastName.ShouldBe("Smith");
            m.Ach.AccountHolder.CompanyName.ShouldBe("Example Inc");
            m.Ach.AccountHolder.DateOfBirth.ShouldBe("1985-05-15");
            m.Ach.AccountHolder.Identification.Type.ShouldBe("ssn");
            m.Ach.AccountHolder.Identification.IssuingCountry.ShouldBe("US");
            m.Ach.AccountHolder.Identification.Number.ShouldBe("123456789");
            m.Ach.AccountNumber.ShouldBe("4099999992");
            m.Ach.BankCode.ShouldBe("011075150");
            m.Ach.Country.ShouldBe(CountryCode.US);
            m.Sepa.AccountHolder.Type.ShouldBe(SepaAccountHolderType.Individual);
            m.Sepa.AccountHolder.FirstName.ShouldBe("Hans");
            m.Sepa.AccountHolder.LastName.ShouldBe("Muller");
            m.Sepa.AccountHolder.CompanyName.ShouldBe("Example GmbH");
            m.Sepa.AccountNumber.ShouldBe("DE89370400440532013000");
            m.Sepa.Country.ShouldBe(CountryCode.DE);
            m.Sepa.Currency.ShouldBe("EUR");
            m.Sepa.Mandate.Id.ShouldBe("mandate_123");
            m.Sepa.Mandate.Type.ShouldBe(PaymentSetupSepaMandateType.Core);
            m.Sepa.Mandate.DateOfSignature.ShouldBe(new DateTime(2026, 1, 15));
        }

        private static void AssertKeys(JToken token, params string[] keys)
        {
            var obj = (JObject)token;
            obj.Properties().Select(p => p.Name).OrderBy(k => k, StringComparer.Ordinal)
                .ShouldBe(keys.OrderBy(k => k, StringComparer.Ordinal));
        }

        // ------------------------------------------------------------------------
        // Enum wire values
        // The setups enums send the lowercase values of the spec; the payments TerminalType and OsType,
        // which send uppercase values, are not used here.
        // ------------------------------------------------------------------------

        [Theory]
        [InlineData(PaymentSetupTerminalType.Web, "web")]
        [InlineData(PaymentSetupTerminalType.Wap, "wap")]
        [InlineData(PaymentSetupTerminalType.App, "app")]
        public void ShouldSendTerminalTypeSpecValue(PaymentSetupTerminalType value, string wire)
        {
            var json = JObject.Parse(Serializer.Serialize(new AlipayCn { TerminalType = value }));
            ((string)json["terminal_type"]).ShouldBe(wire);
        }

        [Theory]
        [InlineData(PaymentSetupOsType.Android, "android")]
        [InlineData(PaymentSetupOsType.Ios, "ios")]
        public void ShouldSendOsTypeSpecValue(PaymentSetupOsType value, string wire)
        {
            var json = JObject.Parse(Serializer.Serialize(new AlipayCn { OsType = value }));
            ((string)json["os_type"]).ShouldBe(wire);
        }

        [Fact]
        public void ShouldSendAchAndSepaEnumSpecValues()
        {
            var ach = JObject.Parse(Serializer.Serialize(new Ach
            {
                AccountType = PaymentSetupAchAccountType.Cash,
                AccountHolder = new AchAccountHolder { Type = AchAccountHolderType.Corporate }
            }));
            ((string)ach["account_type"]).ShouldBe("cash");
            ((string)ach["account_holder"]["type"]).ShouldBe("corporate");

            var sepa = JObject.Parse(Serializer.Serialize(new Sepa
            {
                AccountHolder = new SepaAccountHolder { Type = SepaAccountHolderType.Individual },
                Mandate = new SepaMandate { Type = PaymentSetupSepaMandateType.B2B, DateOfSignature = new DateTime(2026, 3, 9) }
            }));
            ((string)sepa["account_holder"]["type"]).ShouldBe("individual");
            ((string)sepa["mandate"]["type"]).ShouldBe("b2b");
            ((string)sepa["mandate"]["date_of_signature"]).ShouldBe("2026-03-09");
        }

        // ------------------------------------------------------------------------
        // Deprecated members
        // initialization on Bizum, Blik, PayByBank and Stablecoin (not in their schemas), card_present
        // (removed from the spec) and Bizum.PaymentMethodOptions are deprecated, not removed: they keep
        // sending exactly what they sent before this change.
        // ------------------------------------------------------------------------

        // The deprecated members are used on purpose in this section only, to prove they are still sent
        // as before, so the obsolete warning is suppressed here and restored after it.
#pragma warning disable CS0618
        [Fact]
        public void ShouldStillSendDeprecatedInitializationAsDisabledByDefault()
        {
            var methods = new SetupPaymentMethods
            {
                Bizum = new Bizum(),
                Blik = new Blik(),
                PayByBank = new PayByBank(),
                Stablecoin = new Stablecoin()
            };

            var json = JObject.Parse(Serializer.Serialize(methods));

            foreach (var key in new[] { "bizum", "blik", "paybybank", "stablecoin" })
            {
                AssertKeys(json[key], "initialization");
                ((string)json[key]["initialization"]).ShouldBe("disabled");
            }
        }

        [Fact]
        public void ShouldRoundTripDeprecatedInitialization()
        {
            var methods = new SetupPaymentMethods
            {
                Bizum = new Bizum { Initialization = PaymentMethodInitialization.Enabled },
                Blik = new Blik { Initialization = PaymentMethodInitialization.Enabled, PartnerCode = "999111" },
                PayByBank = new PayByBank { Initialization = PaymentMethodInitialization.Enabled, BankId = "ob-natwest" },
                Stablecoin = new Stablecoin { Initialization = PaymentMethodInitialization.Enabled }
            };

            var m = (SetupPaymentMethods)Serializer.Deserialize(Serializer.Serialize(methods), typeof(SetupPaymentMethods));

            m.Bizum.Initialization.ShouldBe(PaymentMethodInitialization.Enabled);
            m.Blik.Initialization.ShouldBe(PaymentMethodInitialization.Enabled);
            m.Blik.PartnerCode.ShouldBe("999111");
            m.PayByBank.Initialization.ShouldBe(PaymentMethodInitialization.Enabled);
            m.PayByBank.BankId.ShouldBe("ob-natwest");
            m.Stablecoin.Initialization.ShouldBe(PaymentMethodInitialization.Enabled);
        }

        [Fact]
        public void ShouldShareDeprecatedInitializationWithPaymentMethodBase()
        {
            // Bizum, Blik, PayByBank and Stablecoin still derive from PaymentMethodBase, so code that uses them
            // through the base class keeps compiling. The obsolete property hides the base one and delegates
            // to it: both views read and write the same value, and only one "initialization" key is sent.
            var bizum = new Bizum { Initialization = PaymentMethodInitialization.Enabled };
            PaymentMethodBase[] methods =
            {
                bizum, new Blik { PartnerCode = "999111" }, new PayByBank { BankId = "ob-natwest" }, new Stablecoin()
            };

            ((PaymentMethodBase)bizum).Initialization.ShouldBe(PaymentMethodInitialization.Enabled);
            foreach (var method in methods)
            {
                method.Initialization = PaymentMethodInitialization.Enabled;
            }

            var json = JObject.Parse(Serializer.Serialize(new SetupPaymentMethods
            {
                Bizum = bizum,
                Blik = (Blik)methods[1],
                PayByBank = (PayByBank)methods[2],
                Stablecoin = (Stablecoin)methods[3]
            }));

            ((Blik)methods[1]).Initialization.ShouldBe(PaymentMethodInitialization.Enabled);
            ((PayByBank)methods[2]).Initialization.ShouldBe(PaymentMethodInitialization.Enabled);
            ((Stablecoin)methods[3]).Initialization.ShouldBe(PaymentMethodInitialization.Enabled);
            AssertKeys(json["bizum"], "initialization");
            AssertKeys(json["blik"], "initialization", "partner_code");
            AssertKeys(json["paybybank"], "initialization", "bank_id");
            AssertKeys(json["stablecoin"], "initialization");
            foreach (var key in new[] { "bizum", "blik", "paybybank", "stablecoin" })
            {
                ((string)json[key]["initialization"]).ShouldBe("enabled");
            }
        }

        [Fact]
        public void ShouldStillSendDeprecatedCardPresentAndBizumPaymentMethodOptions()
        {
            var methods = new SetupPaymentMethods
            {
                CardPresent = new CardPresent { EntryMode = "chip" },
                Bizum = new Bizum { PaymentMethodOptions = new PaymentMethodOptions() }
            };

            var json = JObject.Parse(Serializer.Serialize(methods));

            ((string)json["card_present"]["entry_mode"]).ShouldBe("chip");
            ((JObject)json["bizum"]).ContainsKey("payment_method_options").ShouldBeTrue();
        }

        [Fact]
        public void ShouldDeserializeDeprecatedMembersInConfirmResponse()
        {
            const string json = @"{
                ""id"": ""set_dep"",
                ""payment_methods"": {
                    ""bizum"": { ""status"": ""ready"", ""initialization"": ""enabled"" },
                    ""card_present"": { ""status"": ""ready"" }
                }
            }";

            var response = (PaymentSetupsConfirmResponse)Serializer.Deserialize(json, typeof(PaymentSetupsConfirmResponse));

            response.PaymentMethods.Bizum.Status.ShouldBe(PaymentMethodStatus.Ready);
            response.PaymentMethods.Bizum.Initialization.ShouldBe(PaymentMethodInitialization.Enabled);
            response.PaymentMethods.CardPresent.Status.ShouldBe(PaymentMethodStatus.Ready);
        }

#pragma warning restore CS0618

        // ------------------------------------------------------------------------
        // New payment methods on create requests
        // A request with the new methods keeps them under payment_methods.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSendNewPaymentMethodsInsideRequest()
        {
            var request = new PaymentSetupsRequest
            {
                PaymentMethods = new SetupPaymentMethods { Ideal = new Ideal { Description = "2 t-shirts" } }
            };

            var json = JObject.Parse(Serializer.Serialize(request));

            ((string)json["payment_methods"]["ideal"]["description"]).ShouldBe("2 t-shirts");
        }
    }
}
