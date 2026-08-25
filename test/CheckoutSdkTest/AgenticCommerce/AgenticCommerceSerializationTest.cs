using Checkout.AgenticCommerce.Entities;
using Checkout.AgenticCommerce.Requests;
using Checkout.AgenticCommerce.Responses;
using Checkout.Common;
using Shouldly;
using System.Collections.Generic;
using System;
using Xunit;

namespace Checkout.AgenticCommerce
{
    /// <summary>
    /// Schema validation tests for Checkout.AgenticCommerce.
    /// Grouped by domain; each section below covers one subject.
    /// </summary>
    public class AgenticCommerceSerializationTest
    {
        // ------------------------------------------------------------------------
        // DelegatedPaymentRequest
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeWithRequiredPropertiesForDelegatedPaymentRequest()
        {
            var request = new DelegatedPaymentRequest
            {
                PaymentMethod = new DelegatedPaymentMethodCard
                {
                    CardNumberType = DelegatedCardNumberType.Fpan,
                    Number = "4242424242424242",
                    Metadata = new Dictionary<string, string> { { "issuing_bank", "test" } }
                },
                Allowance = new DelegatedPaymentAllowance
                {
                    Reason = DelegatedPaymentAllowanceReason.OneTime,
                    MaxAmount = 10000,
                    Currency = Currency.USD,
                    MerchantId = "cli_test",
                    CheckoutSessionId = "1PQrsT",
                    ExpiresAt = DateTime.UtcNow.AddHours(1)
                },
                RiskSignals = new List<DelegatedPaymentRiskSignal>
                {
                    new DelegatedPaymentRiskSignal { Type = "card_testing", Score = 10, Action = "blocked" }
                },
                Metadata = new Dictionary<string, string> { { "campaign", "q4" } }
            };

            Should.NotThrow(() => new JsonSerializer().Serialize(request));
        }

        [Fact]
        public void ShouldSerializeWithAllOptionalProperties()
        {
            var request = new DelegatedPaymentRequest
            {
                PaymentMethod = new DelegatedPaymentMethodCard
                {
                    CardNumberType = DelegatedCardNumberType.NetworkToken,
                    Number = "4242424242424242",
                    ExpMonth = "11",
                    ExpYear = "2026",
                    Name = "Jane Doe",
                    Cvc = "223",
                    Cryptogram = "gXc5UCLnM6ckD7pjM1TdPA==",
                    EciValue = "07",
                    ChecksPerformed = new List<string> { "avs", "cvv" },
                    Iin = "424242",
                    DisplayCardFundingType = DelegatedCardFundingType.Credit,
                    DisplayWalletType = "wallet",
                    DisplayBrand = "Visa",
                    DisplayLast4 = "4242",
                    Metadata = new Dictionary<string, string> { { "issuing_bank", "test" } }
                },
                Allowance = new DelegatedPaymentAllowance
                {
                    Reason = DelegatedPaymentAllowanceReason.OneTime,
                    MaxAmount = 10000,
                    Currency = Currency.USD,
                    MerchantId = "cli_vkuhvk4vjn2edkps7dfsq6emqm",
                    CheckoutSessionId = "1PQrsT",
                    ExpiresAt = new DateTime(2025, 10, 9, 7, 20, 50)
                },
                BillingAddress = new DelegatedPaymentBillingAddress
                {
                    Name = "John Doe",
                    LineOne = "123 Fake St.",
                    LineTwo = "Unit 1",
                    City = "San Francisco",
                    State = "CA",
                    PostalCode = "12345",
                    Country = CountryCode.US
                },
                RiskSignals = new List<DelegatedPaymentRiskSignal>
                {
                    new DelegatedPaymentRiskSignal { Type = "card_testing", Score = 10, Action = "blocked" }
                },
                Metadata = new Dictionary<string, string> { { "campaign", "q4" } }
            };

            Should.NotThrow(() => new JsonSerializer().Serialize(request));
        }

        [Fact]
        public void ShouldRoundTripSerializeForDelegatedPaymentRequest()
        {
            var original = new DelegatedPaymentRequest
            {
                PaymentMethod = new DelegatedPaymentMethodCard
                {
                    CardNumberType = DelegatedCardNumberType.Fpan,
                    Number = "4242424242424242",
                    Metadata = new Dictionary<string, string>()
                },
                Allowance = new DelegatedPaymentAllowance
                {
                    Reason = DelegatedPaymentAllowanceReason.OneTime,
                    MaxAmount = 5000,
                    Currency = Currency.GBP,
                    MerchantId = "cli_test",
                    CheckoutSessionId = "sess123",
                    ExpiresAt = new DateTime(2026, 1, 1)
                },
                RiskSignals = new List<DelegatedPaymentRiskSignal>(),
                Metadata = new Dictionary<string, string> { { "key", "value" } }
            };
            var serializer = new JsonSerializer();

            var json = serializer.Serialize(original);
            var deserialized = (DelegatedPaymentRequest)serializer.Deserialize(json, typeof(DelegatedPaymentRequest));

            deserialized.Allowance.Currency.ShouldBe(Currency.GBP);
            deserialized.Allowance.MaxAmount.ShouldBe(5000L);
            deserialized.PaymentMethod.Number.ShouldBe("4242424242424242");
            deserialized.Metadata["key"].ShouldBe("value");
        }

        // ------------------------------------------------------------------------
        // DelegatedPaymentResponse
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeWithRequiredPropertiesForDelegatedPaymentResponse()
        {
            var response = new DelegatedPaymentResponse
            {
                Id = "vt_abc123def456ghi789",
                Created = new DateTime(2026, 3, 11, 10, 30, 0),
                Metadata = new Dictionary<string, string> { { "psp", "checkout.com" } }
            };

            Should.NotThrow(() => new JsonSerializer().Serialize(response));
        }

        [Fact]
        public void ShouldDeserializeSwaggerExample()
        {
            const string json = @"{
                ""id"": ""vt_abc123def456ghi789"",
                ""created"": ""2026-03-11T10:30:00Z"",
                ""metadata"": { ""psp"": ""checkout.com"" }
            }";

            var response = (DelegatedPaymentResponse)new JsonSerializer()
                .Deserialize(json, typeof(DelegatedPaymentResponse));

            response.ShouldNotBeNull();
            response.Id.ShouldBe("vt_abc123def456ghi789");
            response.Created.ShouldNotBe(default);
            response.Metadata.ShouldNotBeNull();
            response.Metadata["psp"].ShouldBe("checkout.com");
        }

        [Fact]
        public void ShouldRoundTripSerializeForDelegatedPaymentResponse()
        {
            var original = new DelegatedPaymentResponse
            {
                Id = "vt_test123",
                Created = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                Metadata = new Dictionary<string, string> { { "key", "value" } }
            };
            var serializer = new JsonSerializer();

            var json = serializer.Serialize(original);
            var deserialized = (DelegatedPaymentResponse)serializer.Deserialize(json, typeof(DelegatedPaymentResponse));

            deserialized.Id.ShouldBe(original.Id);
            deserialized.Metadata["key"].ShouldBe("value");
        }
    }
}
