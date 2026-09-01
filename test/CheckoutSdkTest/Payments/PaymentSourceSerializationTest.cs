using System;
using Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated;
using Checkout.HandlePaymentsAndPayouts.Payments.Common.Source;
using Checkout.Common;
using Checkout.Payments.Request.Source.Apm;
using Checkout.Payments.Request.Source;
using Checkout.Payments.Request;
using Checkout.Payments.Response.Source;
using Checkout.Payments.Response;
using Shouldly;
using Xunit;

namespace Checkout.Payments
{
    /// <summary>
    /// Schema validation tests for Checkout.Payments.
    /// Grouped by domain; each section below covers one subject.
    /// </summary>
    public class PaymentSourceSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        // ------------------------------------------------------------------------
        // PaymentSourceType
        // Schema validation tests for PaymentSourceType, value by value in both directions.
        // ------------------------------------------------------------------------

        [Theory]
        [InlineData(PaymentSourceType.Card, "card")]
        [InlineData(PaymentSourceType.Id, "id")]
        [InlineData(PaymentSourceType.NetworkToken, "network_token")]
        [InlineData(PaymentSourceType.Token, "token")]
        [InlineData(PaymentSourceType.Ach, "ach")]
        [InlineData(PaymentSourceType.Customer, "customer")]
        [InlineData(PaymentSourceType.ProviderToken, "provider_token")]
        [InlineData(PaymentSourceType.DLocal, "dLocal")]
        [InlineData(PaymentSourceType.CurrencyAccount, "currency_account")]
        [InlineData(PaymentSourceType.Boleto, "boleto")]
        [InlineData(PaymentSourceType.Fawry, "fawry")]
        [InlineData(PaymentSourceType.Giropay, "giropay")]
        [InlineData(PaymentSourceType.Ideal, "ideal")]
        [InlineData(PaymentSourceType.Oxxo, "oxxo")]
        [InlineData(PaymentSourceType.PagoFacil, "pagofacil")]
        [InlineData(PaymentSourceType.RapiPago, "rapipago")]
        [InlineData(PaymentSourceType.Klarna, "klarna")]
        [InlineData(PaymentSourceType.Sofort, "sofort")]
        [InlineData(PaymentSourceType.Knet, "knet")]
        [InlineData(PaymentSourceType.QPay, "qpay")]
        [InlineData(PaymentSourceType.Alipay, "alipay")]
        [InlineData(PaymentSourceType.PayPal, "paypal")]
        [InlineData(PaymentSourceType.Multibanco, "multibanco")]
        [InlineData(PaymentSourceType.Octopus, "octopus")]
        [InlineData(PaymentSourceType.Plaid, "plaid")]
        [InlineData(PaymentSourceType.EPS, "eps")]
        [InlineData(PaymentSourceType.Illicado, "illicado")]
        [InlineData(PaymentSourceType.Poli, "poli")]
        [InlineData(PaymentSourceType.Przelewy24, "p24")]
        [InlineData(PaymentSourceType.BenefitPay, "benefitpay")]
        [InlineData(PaymentSourceType.Bizum, "bizum")]
        [InlineData(PaymentSourceType.Bancontact, "bancontact")]
        [InlineData(PaymentSourceType.Blik, "blik")]
        [InlineData(PaymentSourceType.Tamara, "tamara")]
        [InlineData(PaymentSourceType.BankAccount, "bank_account")]
        [InlineData(PaymentSourceType.AlipayHk, "alipay_hk")]
        [InlineData(PaymentSourceType.AlipayCn, "alipay_cn")]
        [InlineData(PaymentSourceType.AlipayPlus, "alipay_plus")]
        [InlineData(PaymentSourceType.Gcash, "gcash")]
        [InlineData(PaymentSourceType.Wechatpay, "wechatpay")]
        [InlineData(PaymentSourceType.Dana, "dana")]
        [InlineData(PaymentSourceType.Kakaopay, "kakaopay")]
        [InlineData(PaymentSourceType.Truemoney, "truemoney")]
        [InlineData(PaymentSourceType.Tng, "tng")]
        [InlineData(PaymentSourceType.Afterpay, "afterpay")]
        [InlineData(PaymentSourceType.Benefit, "benefit")]
        [InlineData(PaymentSourceType.Mbway, "mbway")]
        [InlineData(PaymentSourceType.Postfinance, "postfinance")]
        [InlineData(PaymentSourceType.Stcpay, "stcpay")]
        [InlineData(PaymentSourceType.Alma, "alma")]
        [InlineData(PaymentSourceType.Trustly, "trustly")]
        [InlineData(PaymentSourceType.Cvconnect, "cvconnect")]
        [InlineData(PaymentSourceType.Sepa, "sepa")]
        [InlineData(PaymentSourceType.Sequra, "sequra")]
        [InlineData(PaymentSourceType.Tabby, "tabby")]
        [InlineData(PaymentSourceType.Applepay, "applepay")]
        [InlineData(PaymentSourceType.Googlepay, "googlepay")]
        [InlineData(PaymentSourceType.Bacs, "bacs")]
        [InlineData(PaymentSourceType.Mobilepay, "mobilepay")]
        [InlineData(PaymentSourceType.Paynow, "paynow")]
        [InlineData(PaymentSourceType.Swish, "swish")]
        [InlineData(PaymentSourceType.Twint, "twint")]
        [InlineData(PaymentSourceType.Vipps, "vipps")]
        public void ShouldMapEveryPaymentSourceTypeToItsWireValue(
            PaymentSourceType sourceType,
            string wireValue)
        {
            CheckoutUtils.GetEnumMemberValue(sourceType).ShouldBe(wireValue);
            CheckoutUtils.GetEnumFromStringMemberValue<PaymentSourceType>(wireValue).ShouldBe(sourceType);
        }

        // ------------------------------------------------------------------------
        // ApmRequestSource
        // Schema validation tests for the alternative payment method request sources added to close the
        // gap between PaymentSourceType and the typed request source classes.
        // ------------------------------------------------------------------------

        [Theory]
        [InlineData(typeof(RequestMobilePaySource), "mobilepay")]
        [InlineData(typeof(RequestPayNowSource), "paynow")]
        [InlineData(typeof(RequestTwintSource), "twint")]
        [InlineData(typeof(RequestVippsSource), "vipps")]
        public void ShouldSerializeTypeOnlySources(System.Type sourceType, string wireType)
        {
            var source = (AbstractRequestSource)System.Activator.CreateInstance(sourceType);

            var json = Serializer.Serialize(source);

            json.ShouldBe("{\"type\":\"" + wireType + "\"}");
        }

        [Fact]
        public void ShouldRoundTripSerializeSwishSource()
        {
            var original = new RequestSwishSource
            {
                PaymentCountry = CountryCode.SE,
                AccountHolder = new SwishAccountHolder
                {
                    FirstName = "Bruce",
                    LastName = "Wayne"
                },
                BillingDescriptor = new SwishBillingDescriptor { Name = "CKO Store" }
            };

            var json = Serializer.Serialize(original);
            var d = (RequestSwishSource)Serializer.Deserialize(json, typeof(RequestSwishSource));

            json.ShouldContain("\"type\":\"swish\"");
            json.ShouldContain("\"payment_country\":\"SE\"");
            d.Type.ShouldBe(PaymentSourceType.Swish);
            d.PaymentCountry.ShouldBe(CountryCode.SE);
            d.AccountHolder.FirstName.ShouldBe(original.AccountHolder.FirstName);
            d.AccountHolder.LastName.ShouldBe(original.AccountHolder.LastName);
            d.BillingDescriptor.Name.ShouldBe(original.BillingDescriptor.Name);
        }

        [Fact]
        public void ShouldDeserializeSwishSwaggerExample()
        {
            const string json = @"{
                ""type"": ""swish"",
                ""payment_country"": ""SE"",
                ""account_holder"": { ""first_name"": ""Bruce"", ""last_name"": ""Wayne"" },
                ""billing_descriptor"": { ""name"": ""CKO Store"" }
            }";

            var s = (RequestSwishSource)Serializer.Deserialize(json, typeof(RequestSwishSource));

            s.Type.ShouldBe(PaymentSourceType.Swish);
            s.PaymentCountry.ShouldBe(CountryCode.SE);
            s.AccountHolder.FirstName.ShouldBe("Bruce");
            s.AccountHolder.LastName.ShouldBe("Wayne");
            s.BillingDescriptor.Name.ShouldBe("CKO Store");
        }

        // ------------------------------------------------------------------------
        // BacsPaymentSource
        // Schema validation tests for the bacs payment source, covering both
        // PaymentRequestBacsSource and PaymentGetResponseBacsSource.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldRoundTripSerializeRequestSource()
        {
            var original = new RequestBacsSource
            {
                Id = "src_wmlfc3zyhqzehihu7giusaaawu"
            };

            var json = Serializer.Serialize(original);
            var deserialized = (RequestBacsSource)Serializer
                .Deserialize(json, typeof(RequestBacsSource));

            json.ShouldContain("\"type\":\"bacs\"");
            json.ShouldContain("\"id\":\"src_wmlfc3zyhqzehihu7giusaaawu\"");
            deserialized.Type.ShouldBe(PaymentSourceType.Bacs);
            deserialized.Id.ShouldBe(original.Id);
        }

        [Fact]
        public void ShouldDeserializeResponseSourceToTypedClass()
        {
            const string json = @"{
                ""id"": ""pay_mbabizu24mvu3mela5njyhpit4"",
                ""source"": {
                    ""type"": ""bacs"",
                    ""id"": ""src_wmlfc3zyhqzehihu7giusaaawu""
                }
            }";

            var response = (GetPaymentResponse)Serializer.Deserialize(json, typeof(GetPaymentResponse));

            var bacsSource = response.Source.ShouldBeOfType<BacsResponseSource>();
            bacsSource.SourceType.ShouldBe(PaymentSourceType.Bacs);
            bacsSource.Type().ShouldBe(PaymentSourceType.Bacs);
            bacsSource.Id.ShouldBe("src_wmlfc3zyhqzehihu7giusaaawu");
        }

        [Fact]
        public void ShouldRoundTripSerializeResponseSource()
        {
            var original = new BacsResponseSource
            {
                SourceType = PaymentSourceType.Bacs,
                Id = "src_wmlfc3zyhqzehihu7giusaaawu"
            };

            var json = Serializer.Serialize(original);
            var deserialized = (BacsResponseSource)Serializer
                .Deserialize(json, typeof(BacsResponseSource));

            json.ShouldContain("\"type\":\"bacs\"");
            json.ShouldContain("\"id\":\"src_wmlfc3zyhqzehihu7giusaaawu\"");
            deserialized.SourceType.ShouldBe(original.SourceType);
            deserialized.Id.ShouldBe(original.Id);
        }

        // ------------------------------------------------------------------------
        // PaymentRequestFallbackSource
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeFallbackSource()
        {
            var request = new PaymentRequest
            {
                FallbackSource = new RequestCardSource
                {
                    Number = "4543474002249996",
                    ExpiryMonth = 6,
                    ExpiryYear = 2030,
                    Cvv = "956"
                }
            };

            var json = Serializer.Serialize(request);

            json.ShouldContain("\"fallback_source\"");
            json.ShouldContain("4543474002249996");
        }

        [Fact]
        public void ShouldRoundTripSerializeFallbackSource()
        {
            var original = new PaymentRequest
            {
                FallbackSource = new RequestCardSource
                {
                    Number = "4543474002249996",
                    ExpiryMonth = 6,
                    ExpiryYear = 2030,
                    Cvv = "956",
                    Name = "Bruce Wayne"
                }
            };

            var json = Serializer.Serialize(original);
            var deserialized = (PaymentRequest)Serializer.Deserialize(json, typeof(PaymentRequest));

            deserialized.FallbackSource.ShouldNotBeNull();
            deserialized.FallbackSource.Number.ShouldBe("4543474002249996");
            deserialized.FallbackSource.ExpiryMonth.ShouldBe(6);
            deserialized.FallbackSource.ExpiryYear.ShouldBe(2030);
            deserialized.FallbackSource.Cvv.ShouldBe("956");
            deserialized.FallbackSource.Name.ShouldBe("Bruce Wayne");
        }

        [Fact]
        public void ShouldDeserializeSwaggerExample()
        {
            const string json = @"{
                ""fallback_source"": {
                    ""type"": ""card"",
                    ""number"": ""4543474002249996"",
                    ""expiry_month"": 6,
                    ""expiry_year"": 2030,
                    ""cvv"": ""956""
                }
            }";

            var request = (PaymentRequest)Serializer.Deserialize(json, typeof(PaymentRequest));

            request.FallbackSource.ShouldNotBeNull();
            request.FallbackSource.Number.ShouldBe("4543474002249996");
            request.FallbackSource.ExpiryMonth.ShouldBe(6);
            request.FallbackSource.ExpiryYear.ShouldBe(2030);
        }

        // ------------------------------------------------------------------------
        // PaymentCreateResponseSourceDispatch
        // Schema validation tests for the POST /payments 201 response source. PaymentResponseSource
        // maps card, ach, alipay_cn, bank_account, sepa and bacs. The converter throws
        // CheckoutApiException for a type it does not recognise, so every mapped type must resolve.
        // ------------------------------------------------------------------------

        [Theory]
        [InlineData("card")]
        [InlineData("ach")]
        [InlineData("alipay_cn")]
        [InlineData("bank_account")]
        [InlineData("sepa")]
        [InlineData("bacs")]
        public void ShouldResolveEveryMappedCreateResponseSourceType(string type)
        {
            var json = @"{""id"":""pay_mbabizu24mvu3mela5njyhpit4"",""source"":{""type"":""" +
                       type + @""",""id"":""src_wmlfc3zyhqzehihu7giusaaawu""}}";

            var response = (RequestAPaymentOrPayoutResponseCreated)Serializer
                .Deserialize(json, typeof(RequestAPaymentOrPayoutResponseCreated));

            response.Source.ShouldNotBeNull();
            response.Source.Type.ShouldNotBeNull();
            CheckoutUtils.GetEnumMemberValue(response.Source.Type.GetValueOrDefault()).ShouldBe(type);
        }

        // Every type that PaymentResponseSource maps to PaymentDeclinedSourceResponse must keep the
        // id, which that schema declares required alongside type.
        [Theory]
        [InlineData("ach", SourceType.Ach,
            typeof(HandlePaymentsAndPayouts.Payments.Common.Source.AchSource.AchSource))]
        [InlineData("alipay_cn", SourceType.AlipayCn,
            typeof(HandlePaymentsAndPayouts.Payments.Common.Source.AlipayCnSource.AlipayCnSource))]
        [InlineData("bank_account", SourceType.BankAccount,
            typeof(HandlePaymentsAndPayouts.Payments.Common.Source.BankAccountSource.BankAccountSource))]
        [InlineData("sepa", SourceType.Sepa,
            typeof(HandlePaymentsAndPayouts.Payments.Common.Source.SepaSource.SepaSource))]
        [InlineData("bacs", SourceType.Bacs,
            typeof(HandlePaymentsAndPayouts.Payments.Common.Source.BacsSource.BacsSource))]
        public void ShouldDeserializeTheIdOnEveryDeclinedCreateResponseSource(
            string wireType,
            SourceType expectedType,
            Type expectedSourceType)
        {
            var json = @"{
                ""id"": ""pay_mbabizu24mvu3mela5njyhpit4"",
                ""source"": { ""type"": """ + wireType + @""", ""id"": ""src_wmlfc3zyhqzehihu7giusaaawu"" }
            }";

            var response = (RequestAPaymentOrPayoutResponseCreated)Serializer
                .Deserialize(json, typeof(RequestAPaymentOrPayoutResponseCreated));

            var source = response.Source;
            source.ShouldBeOfType(expectedSourceType);
            source.Type.ShouldBe(expectedType);

            var id = expectedSourceType.GetProperty("Id");
            id.ShouldNotBeNull();
            id.GetValue(source).ShouldBe("src_wmlfc3zyhqzehihu7giusaaawu");
        }

        // ------------------------------------------------------------------------
        // ApmRequestSourceCoverage
        // Every wire value in the PaymentRequestSource discriminator must have a request-source
        // class, and each must serialize its own type.
        // ------------------------------------------------------------------------

        [Theory]
        [InlineData(typeof(RequestAlipayCnSource), "alipay_cn")]
        [InlineData(typeof(RequestAlipayHkSource), "alipay_hk")]
        [InlineData(typeof(RequestDanaSource), "dana")]
        [InlineData(typeof(RequestGcashSource), "gcash")]
        [InlineData(typeof(RequestKakaopaySource), "kakaopay")]
        [InlineData(typeof(RequestTngSource), "tng")]
        [InlineData(typeof(RequestTruemoneySource), "truemoney")]
        [InlineData(typeof(RequestBacsSource), "bacs")]
        [InlineData(typeof(RequestMobilePaySource), "mobilepay")]
        [InlineData(typeof(RequestPayNowSource), "paynow")]
        [InlineData(typeof(RequestSwishSource), "swish")]
        [InlineData(typeof(RequestTwintSource), "twint")]
        [InlineData(typeof(RequestVippsSource), "vipps")]
        public void ShouldSerializeEachApmRequestSourceWithItsOwnType(Type sourceType, string wire)
        {
            var source = (AbstractRequestSource)Activator.CreateInstance(sourceType);

            Serializer.Serialize(source).ShouldContain(@"""type"":""" + wire + @"""");
        }


        // ------------------------------------------------------------------------
        // RequestSepaSource
        // Schema validation tests for RequestSepaSource against PaymentRequestSEPAV4Source, which
        // declares type, country, account_number, currency, mandate_id, mandate_type,
        // date_of_signature and account_holder. mandate_type was previously absent from the SDK.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeTheSepaSourceMandateType()
        {
            var source = new RequestSepaSource
            {
                Country = CountryCode.FR,
                AccountNumber = "FR7630006000011234567890189",
                Currency = Common.Currency.EUR,
                MandateId = "123456",
                MandateType = SepaMandateType.B2B,
                DateOfSignature = "2022-08-02"
            };

            var json = Serializer.Serialize(source);

            json.ShouldContain(@"""type"":""sepa""");
            json.ShouldContain(@"""country"":""FR""");
            json.ShouldContain(@"""account_number"":""FR7630006000011234567890189""");
            json.ShouldContain(@"""currency"":""EUR""");
            json.ShouldContain(@"""mandate_id"":""123456""");
            json.ShouldContain(@"""mandate_type"":""B2B""");
            json.ShouldContain(@"""date_of_signature"":""2022-08-02""");
        }

        [Theory]
        [InlineData(SepaMandateType.Core, "Core")]
        [InlineData(SepaMandateType.B2B, "B2B")]
        public void ShouldMapEverySepaMandateTypeToItsWireValue(SepaMandateType type, string wire)
        {
            CheckoutUtils.GetEnumMemberValue(type).ShouldBe(wire);
            CheckoutUtils.GetEnumFromStringMemberValue<SepaMandateType>(wire).ShouldBe(type);
        }

        [Fact]
        public void ShouldNotSendAMandateTypeThatWasNotSet()
        {
            var source = new RequestSepaSource { Country = CountryCode.FR };

            Serializer.Serialize(source).ShouldNotContain("mandate_type");
        }

        [Fact]
        public void ShouldKeepTheSepaMandateEnumsSeparate()
        {
            // The payments source enum and the instruments enum carry the same two values today but
            // belong to independent schemas. The previous platform's MandateType is a different set
            // entirely, which is why neither is shared.
            CheckoutUtils.GetEnumMemberValue(SepaMandateType.Core).ShouldBe("Core");
            CheckoutUtils.GetEnumMemberValue(Instruments.SepaMandateType.Core).ShouldBe("Core");
            CheckoutUtils.GetEnumMemberValue(Sources.Previous.MandateType.Single).ShouldBe("single");
            CheckoutUtils.GetEnumMemberValue(Sources.Previous.MandateType.Recurring).ShouldBe("recurring");
        }

    }
}
