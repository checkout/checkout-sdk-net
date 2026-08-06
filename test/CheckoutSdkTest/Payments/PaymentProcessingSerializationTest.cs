using Checkout.Common;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace Checkout.Payments
{
    public class PaymentProcessingSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        private static PaymentProcessing CreateFullyPopulated()
        {
            return new PaymentProcessing
            {
                RetrievalReferenceNumber = "909913440644",
                AcquirerTransactionId = "440644309099499894406",
                RecommendationCode = "02",
                Scheme = "Mastercard",
                PartnerMerchantAdviceCode = "24",
                PartnerResponseCode = "ER_WRONG_TICKET",
                PartnerOrderId = "5GK24544NA744002L",
                PartnerSessionId = "session_abc",
                PartnerClientToken = "token_abc",
                PartnerPaymentId = "440644309099499894406",
                PanTypeProcessed = PanProcessedType.DPAN,
                ContinuationPayload = "payload_abc",
                Pun = "pun_abc",
                PartnerStatus = "pending",
                PartnerTransactionId = "txn_abc",
                PartnerErrorCodes = new List<string> { "ERR_001", "ERR_002" },
                PartnerErrorMessage = "Payment declined",
                PartnerAuthorizationCode = "auth_123",
                PartnerAuthorizationResponseCode = "00",
                SurchargeAmount = 200L,
                FallbackSourceUsed = false,
                CkoNetworkTokenAvailable = true,
                PurchaseCountry = CountryCode.GB,
                ForeignRetailerAmount = 200L,
                MerchantCategoryCode = "5311",
                SchemeMerchantId = "123456",
                ReconciliationId = "4123495123",
                Aggregator = new ProcessingAggregator
                {
                    SubMerchantId = "9cf70789ba90123",
                    AggregatorIdVisa = "10012345",
                    AggregatorIdMc = "00000123456"
                },
                Aft = true,
                SchemeTransactionLinkId = "MTL-001"
            };
        }

        [Fact]
        public void ShouldSerializeWithRequiredProperties()
        {
            var processing = new PaymentProcessing();

            Should.NotThrow(() => Serializer.Serialize(processing));
        }

        [Fact]
        public void ShouldSerializeWithAllOptionalProperties()
        {
            var processing = CreateFullyPopulated();

            Should.NotThrow(() => Serializer.Serialize(processing));
        }

        [Fact]
        public void ShouldRoundTripSerializeAllProperties()
        {
            var original = CreateFullyPopulated();

            var json = Serializer.Serialize(original);
            var deserialized = (PaymentProcessing)Serializer.Deserialize(json, typeof(PaymentProcessing));

            deserialized.ShouldNotBeNull();
            deserialized.RetrievalReferenceNumber.ShouldBe(original.RetrievalReferenceNumber);
            deserialized.AcquirerTransactionId.ShouldBe(original.AcquirerTransactionId);
            deserialized.RecommendationCode.ShouldBe(original.RecommendationCode);
            deserialized.Scheme.ShouldBe(original.Scheme);
            deserialized.PartnerMerchantAdviceCode.ShouldBe(original.PartnerMerchantAdviceCode);
            deserialized.PartnerResponseCode.ShouldBe(original.PartnerResponseCode);
            deserialized.PartnerOrderId.ShouldBe(original.PartnerOrderId);
            deserialized.PartnerSessionId.ShouldBe(original.PartnerSessionId);
            deserialized.PartnerClientToken.ShouldBe(original.PartnerClientToken);
            deserialized.PartnerPaymentId.ShouldBe(original.PartnerPaymentId);
            deserialized.PanTypeProcessed.ShouldBe(original.PanTypeProcessed);
            deserialized.ContinuationPayload.ShouldBe(original.ContinuationPayload);
            deserialized.Pun.ShouldBe(original.Pun);
            deserialized.PartnerStatus.ShouldBe(original.PartnerStatus);
            deserialized.PartnerTransactionId.ShouldBe(original.PartnerTransactionId);
            deserialized.PartnerErrorCodes.ShouldBe(original.PartnerErrorCodes);
            deserialized.PartnerErrorMessage.ShouldBe(original.PartnerErrorMessage);
            deserialized.PartnerAuthorizationCode.ShouldBe(original.PartnerAuthorizationCode);
            deserialized.PartnerAuthorizationResponseCode.ShouldBe(original.PartnerAuthorizationResponseCode);
            deserialized.SurchargeAmount.ShouldBe(original.SurchargeAmount);
            deserialized.FallbackSourceUsed.ShouldBe(original.FallbackSourceUsed);
            deserialized.CkoNetworkTokenAvailable.ShouldBe(original.CkoNetworkTokenAvailable);
            deserialized.PurchaseCountry.ShouldBe(original.PurchaseCountry);
            deserialized.ForeignRetailerAmount.ShouldBe(original.ForeignRetailerAmount);
            deserialized.MerchantCategoryCode.ShouldBe(original.MerchantCategoryCode);
            deserialized.SchemeMerchantId.ShouldBe(original.SchemeMerchantId);
            deserialized.ReconciliationId.ShouldBe(original.ReconciliationId);
            deserialized.Aggregator.SubMerchantId.ShouldBe("9cf70789ba90123");
            deserialized.Aggregator.AggregatorIdVisa.ShouldBe("10012345");
            deserialized.Aggregator.AggregatorIdMc.ShouldBe("00000123456");
            deserialized.Aft.ShouldBe(original.Aft);
            deserialized.SchemeTransactionLinkId.ShouldBe(original.SchemeTransactionLinkId);
        }

        [Fact]
        public void ShouldDeserializeSwaggerExample()
        {
            const string json = @"{
                ""retrieval_reference_number"": ""909913440644"",
                ""acquirer_transaction_id"": ""440644309099499894406"",
                ""recommendation_code"": ""02"",
                ""scheme"": ""Mastercard"",
                ""partner_merchant_advice_code"": ""24"",
                ""partner_response_code"": ""ER_WRONG_TICKET"",
                ""partner_order_id"": ""5GK24544NA744002L"",
                ""partner_payment_id"": ""440644309099499894406"",
                ""partner_status"": ""pending"",
                ""partner_transaction_id"": ""txn_abc"",
                ""partner_error_codes"": [""ERR_001""],
                ""partner_error_message"": ""Payment declined"",
                ""partner_authorization_code"": ""auth_123"",
                ""partner_authorization_response_code"": ""00"",
                ""surcharge_amount"": 200,
                ""pan_type_processed"": ""dpan"",
                ""fallback_source_used"": false,
                ""cko_network_token_available"": false,
                ""purchase_country"": ""GB"",
                ""foreign_retailer_amount"": 200,
                ""scheme_merchant_id"": ""123456"",
                ""reconciliation_id"": ""4123495123"",
                ""aggregator"": {
                    ""sub_merchant_id"": ""9cf70789ba90123"",
                    ""aggregator_id_visa"": ""10012345"",
                    ""aggregator_id_mc"": ""00000123456""
                },
                ""scheme_transaction_link_id"": ""MTL-001""
            }";

            var processing = (PaymentProcessing)Serializer.Deserialize(json, typeof(PaymentProcessing));

            processing.ShouldNotBeNull();
            processing.RetrievalReferenceNumber.ShouldBe("909913440644");
            processing.AcquirerTransactionId.ShouldBe("440644309099499894406");
            processing.RecommendationCode.ShouldBe("02");
            processing.Scheme.ShouldBe("Mastercard");
            processing.PartnerMerchantAdviceCode.ShouldBe("24");
            processing.PartnerResponseCode.ShouldBe("ER_WRONG_TICKET");
            processing.PartnerOrderId.ShouldBe("5GK24544NA744002L");
            processing.PartnerPaymentId.ShouldBe("440644309099499894406");
            processing.PartnerStatus.ShouldBe("pending");
            processing.PartnerTransactionId.ShouldBe("txn_abc");
            processing.PartnerErrorCodes.ShouldBe(new List<string> { "ERR_001" });
            processing.PartnerErrorMessage.ShouldBe("Payment declined");
            processing.PartnerAuthorizationCode.ShouldBe("auth_123");
            processing.PartnerAuthorizationResponseCode.ShouldBe("00");
            processing.SurchargeAmount.ShouldBe(200L);
            processing.PanTypeProcessed.ShouldBe(PanProcessedType.DPAN);
            processing.FallbackSourceUsed.ShouldBe(false);
            processing.CkoNetworkTokenAvailable.ShouldBe(false);
            processing.PurchaseCountry.ShouldBe(CountryCode.GB);
            processing.ForeignRetailerAmount.ShouldBe(200L);
            processing.SchemeMerchantId.ShouldBe("123456");
            processing.ReconciliationId.ShouldBe("4123495123");
            processing.Aggregator.ShouldNotBeNull();
            processing.Aggregator.SubMerchantId.ShouldBe("9cf70789ba90123");
            processing.SchemeTransactionLinkId.ShouldBe("MTL-001");
        }

        [Fact]
        public void ShouldDeserializeAlphanumericSchemeMerchantId()
        {
            const string json = @"{""scheme_merchant_id"": ""MID-0012AB""}";

            var processing = (PaymentProcessing)Serializer.Deserialize(json, typeof(PaymentProcessing));

            processing.ShouldNotBeNull();
            processing.SchemeMerchantId.ShouldBe("MID-0012AB");
        }

        [Fact]
        public void ShouldSerializeSchemeTransactionLinkIdToSnakeCase()
        {
            var processing = new PaymentProcessing { SchemeTransactionLinkId = "MTL-001" };

            var json = Serializer.Serialize(processing);

            json.ShouldContain("\"scheme_transaction_link_id\":\"MTL-001\"");
        }
    }
}
