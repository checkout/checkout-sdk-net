using Checkout.Common;
using Checkout.HandlePaymentsAndPayouts.Payments.Common.Source;
using CardSource =
    Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.CardSource.CardSource;
using KlarnaSource =
    Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.KlarnaSource.KlarnaSource;
using PaypalSource =
    Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.PaypalSource.PaypalSource;
using CurrencyAccountSource =
    Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.CurrencyAccountSource.CurrencyAccountSource;
using SepaSource =
    Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.SepaSource.SepaSource;
using Shouldly;
using Xunit;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated
{
    public class RequestAPaymentOrPayoutResponseCreatedSerializationTest : JsonTestFixture
    {
        [Fact]
        public void ShouldDeserializeCardSource()
        {
            const string json = @"{
                ""id"": ""pay_123"",
                ""amount"": 1000,
                ""currency"": ""USD"",
                ""approved"": true,
                ""status"": ""Authorized"",
                ""response_code"": ""10000"",
                ""processed_on"": ""2021-06-08T12:25:01Z"",
                ""source"": {
                    ""type"": ""card"",
                    ""id"": ""src_123"",
                    ""last4"": ""1234"",
                    ""fingerprint"": ""abc123"",
                    ""bin"": ""123456"",
                    ""scheme"": ""Visa"",
                    ""card_type"": ""Credit"",
                    ""expiry_month"": 12,
                    ""expiry_year"": 2025
                }
            }";

            var response = (RequestAPaymentOrPayoutResponseCreated)new JsonSerializer()
                .Deserialize(json, typeof(RequestAPaymentOrPayoutResponseCreated));

            response.ShouldNotBeNull();
            response.Source.ShouldNotBeNull();
            response.Source.ShouldBeOfType<CardSource>();
            response.Source.Type.ShouldBe(SourceType.Card);

            var cardSource = (CardSource)response.Source;
            cardSource.Last4.ShouldBe("1234");
            cardSource.Fingerprint.ShouldBe("abc123");
            cardSource.Bin.ShouldBe("123456");
            cardSource.ExpiryMonth.ShouldBe(12);
            cardSource.ExpiryYear.ShouldBe(2025);
        }

        [Fact]
        public void ShouldDeserializeKlarnaSource()
        {
            const string json = @"{
                ""id"": ""pay_123"",
                ""amount"": 1000,
                ""currency"": ""EUR"",
                ""approved"": true,
                ""status"": ""Authorized"",
                ""response_code"": ""10000"",
                ""processed_on"": ""2021-06-08T12:25:01Z"",
                ""source"": {
                    ""type"": ""klarna""
                }
            }";

            var response = (RequestAPaymentOrPayoutResponseCreated)new JsonSerializer()
                .Deserialize(json, typeof(RequestAPaymentOrPayoutResponseCreated));

            response.ShouldNotBeNull();
            response.Source.ShouldNotBeNull();
            response.Source.ShouldBeOfType<KlarnaSource>();
            response.Source.Type.ShouldBe(SourceType.Klarna);
        }

        [Fact]
        public void ShouldDeserializePaypalSource()
        {
            const string json = @"{
                ""id"": ""pay_123"",
                ""amount"": 1000,
                ""currency"": ""USD"",
                ""approved"": true,
                ""status"": ""Authorized"",
                ""response_code"": ""10000"",
                ""processed_on"": ""2021-06-08T12:25:01Z"",
                ""source"": {
                    ""type"": ""paypal""
                }
            }";

            var response = (RequestAPaymentOrPayoutResponseCreated)new JsonSerializer()
                .Deserialize(json, typeof(RequestAPaymentOrPayoutResponseCreated));

            response.ShouldNotBeNull();
            response.Source.ShouldNotBeNull();
            response.Source.ShouldBeOfType<PaypalSource>();
            response.Source.Type.ShouldBe(SourceType.Paypal);
        }

        [Fact]
        public void ShouldDeserializeCurrencyAccountSource()
        {
            const string json = @"{
                ""id"": ""pay_123"",
                ""amount"": 1000,
                ""currency"": ""USD"",
                ""approved"": true,
                ""status"": ""Authorized"",
                ""response_code"": ""10000"",
                ""processed_on"": ""2021-06-08T12:25:01Z"",
                ""source"": {
                    ""type"": ""currency_account"",
                    ""id"": ""ca_123"",
                    ""amount"": 500
                }
            }";

            var response = (RequestAPaymentOrPayoutResponseCreated)new JsonSerializer()
                .Deserialize(json, typeof(RequestAPaymentOrPayoutResponseCreated));

            response.ShouldNotBeNull();
            response.Source.ShouldNotBeNull();
            response.Source.ShouldBeOfType<CurrencyAccountSource>();
            response.Source.Type.ShouldBe(SourceType.CurrencyAccount);

            var currencyAccountSource = (CurrencyAccountSource)response.Source;
            currencyAccountSource.Id.ShouldBe("ca_123");
            currencyAccountSource.Amount.ShouldBe(500);
        }

        [Fact]
        public void ShouldDeserializeSepaSource()
        {
            const string json = @"{
                ""id"": ""pay_123"",
                ""amount"": 1000,
                ""currency"": ""EUR"",
                ""approved"": true,
                ""status"": ""Authorized"",
                ""response_code"": ""10000"",
                ""processed_on"": ""2021-06-08T12:25:01Z"",
                ""source"": {
                    ""type"": ""sepa"",
                    ""id"": ""src_sepa_123""
                }
            }";

            var response = (RequestAPaymentOrPayoutResponseCreated)new JsonSerializer()
                .Deserialize(json, typeof(RequestAPaymentOrPayoutResponseCreated));

            response.ShouldNotBeNull();
            response.Source.ShouldNotBeNull();
            response.Source.ShouldBeOfType<SepaSource>();
            response.Source.Type.ShouldBe(SourceType.Sepa);

            var sepaSource = (SepaSource)response.Source;
            sepaSource.Id.ShouldBe("src_sepa_123");
        }

        [Fact]
        public void ShouldThrowExceptionForUnknownSourceType()
        {
            const string json = @"{
                ""id"": ""pay_123"",
                ""amount"": 1000,
                ""currency"": ""USD"",
                ""approved"": true,
                ""status"": ""Authorized"",
                ""response_code"": ""10000"",
                ""processed_on"": ""2021-06-08T12:25:01Z"",
                ""source"": {
                    ""type"": ""unknown_payment_method"",
                    ""some_property"": ""some_value""
                }
            }";

            Should.Throw<CheckoutApiException>(() =>
            {
                new JsonSerializer().Deserialize(json, typeof(RequestAPaymentOrPayoutResponseCreated));
            });
        }

        [Fact]
        public void ShouldDeserializeCompletePaymentResponse()
        {
            const string json = @"{
                ""id"": ""pay_y3oqhf46pyzuxjbcn2giaqnb44"",
                ""action_id"": ""act_y3oqhf46pyzuxjbcn2giaqnb44"",
                ""amount"": 6540,
                ""currency"": ""USD"",
                ""approved"": true,
                ""status"": ""Authorized"",
                ""auth_code"": ""858188"",
                ""response_code"": ""10000"",
                ""response_summary"": ""Approved"",
                ""processed_on"": ""2019-08-24T14:15:22Z"",
                ""reference"": ""ORD-5023-4E89"",
                ""source"": {
                    ""type"": ""card"",
                    ""id"": ""src_nwd3m4in3hkuddfpjsaevunhdy"",
                    ""billing_address"": {
                        ""address_line1"": ""Checkout.com"",
                        ""address_line2"": ""90 Tottenham Court Road"",
                        ""city"": ""London"",
                        ""state"": ""London"",
                        ""zip"": ""W1T 4TJ"",
                        ""country"": ""GB""
                    },
                    ""expiry_month"": 6,
                    ""expiry_year"": 2025,
                    ""name"": ""Visa"",
                    ""scheme"": ""Visa"",
                    ""last_four"": ""4242"",
                    ""fingerprint"": ""F639CAB2745BEE4140BF86DF6B6D6"",
                    ""bin"": ""424242"",
                    ""card_type"": ""Credit"",
                    ""card_category"": ""Consumer"",
                    ""issuer"": ""JPMORGAN CHASE BANK NA"",
                    ""issuer_country"": ""US"",
                    ""product_id"": ""A"",
                    ""product_type"": ""Visa Traditional"",
                    ""avs_check"": ""S"",
                    ""cvv_check"": ""Y""
                },
                ""customer"": {
                    ""id"": ""cus_y3oqhf46pyzuxjbcn2giaqnb44"",
                    ""email"": ""brucewayne@gmail.com"",
                    ""name"": ""Bruce Wayne""
                }
            }";

            var response = (RequestAPaymentOrPayoutResponseCreated)new JsonSerializer()
                .Deserialize(json, typeof(RequestAPaymentOrPayoutResponseCreated));

            response.ShouldNotBeNull();
            response.Id.ShouldBe("pay_y3oqhf46pyzuxjbcn2giaqnb44");
            response.Amount.ShouldBe(6540);
            response.Currency.ShouldBe(Currency.USD);
            response.Approved.ShouldBeTrue();
            response.Status.ShouldBe(StatusType.Authorized);
            response.AuthCode.ShouldBe("858188");
            response.ResponseCode.ShouldBe("10000");
            response.ResponseSummary.ShouldBe("Approved");
            response.Reference.ShouldBe("ORD-5023-4E89");

            response.Source.ShouldNotBeNull();
            response.Source.ShouldBeOfType<CardSource>();
            var cardSource = (CardSource)response.Source;
            cardSource.Id.ShouldBe("src_nwd3m4in3hkuddfpjsaevunhdy");
            cardSource.ExpiryMonth.ShouldBe(6);
            cardSource.ExpiryYear.ShouldBe(2025);
            cardSource.Scheme.ShouldBe("Visa");

            response.Customer.ShouldNotBeNull();
            response.Customer.Id.ShouldBe("cus_y3oqhf46pyzuxjbcn2giaqnb44");
            response.Customer.Email.ShouldBe("brucewayne@gmail.com");
            response.Customer.Name.ShouldBe("Bruce Wayne");
        }
    }
}