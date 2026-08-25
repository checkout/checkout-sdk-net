using Checkout.Common;
using Checkout.Payments.Response;
using Checkout.Payments;
using Shouldly;
using System.Collections.Generic;
using System;
using Xunit;

namespace Checkout.Payments
{
    /// <summary>
    /// Schema validation tests for Checkout.Payments.
    /// Grouped by domain; each section below covers one subject.
    /// </summary>
    public class PaymentProcessingSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        // ------------------------------------------------------------------------
        // PaymentProcessing
        // ------------------------------------------------------------------------

        private static PaymentProcessing CreateFullyPopulatedPaymentProcessing()
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
        public void ShouldSerializeWithRequiredPropertiesForPaymentProcessing()
        {
            var processing = new PaymentProcessing();

            Should.NotThrow(() => Serializer.Serialize(processing));
        }

        [Fact]
        public void ShouldSerializeWithAllOptionalPropertiesForPaymentProcessing()
        {
            var processing = CreateFullyPopulatedPaymentProcessing();

            Should.NotThrow(() => Serializer.Serialize(processing));
        }

        [Fact]
        public void ShouldRoundTripSerializeAllPropertiesForPaymentProcessing()
        {
            var original = CreateFullyPopulatedPaymentProcessing();

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
        public void ShouldDeserializeSwaggerExampleForPaymentProcessing()
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

        // ------------------------------------------------------------------------
        // ProcessingData
        // ------------------------------------------------------------------------

        private static ProcessingData CreateFullyPopulatedProcessingData()
        {
            return new ProcessingData
            {
                PreferredScheme = PreferredSchema.Visa,
                AppId = "com.iap.linker_portal",
                PartnerCustomerId = "2102209000001106125F8",
                PartnerPaymentId = "440644309099499894406",
                TaxAmount = 1000L,
                PurchaseCountry = CountryCode.GB,
                Locale = "en-US",
                RetrievalReferenceNumber = "909913440644",
                PartnerOrderId = "ord_abc",
                PartnerStatus = "pending",
                PartnerTransactionId = "txn_abc",
                PartnerErrorCodes = new List<string> { "ERR_001", "ERR_002" },
                PartnerErrorMessage = "Payment declined",
                PartnerAuthorizationCode = "auth_123",
                PartnerAuthorizationResponseCode = "00",
                FraudStatus = "approved",
                ProviderAuthorizedPaymentMethod = new ProviderAuthorizedPaymentMethod
                {
                    Type = "pay_later",
                    Description = "Pay in 30 days",
                    NumberOfInstallments = 3L,
                    NumberOfDays = 30L
                },
                CustomPaymentMethodIds = new List<string> { "cpm_001" },
                Aft = true,
                MerchantCategoryCode = "5311",
                SchemeMerchantId = "123456",
                PanTypeProcessed = PanProcessedType.FPAN,
                CkoNetworkTokenAvailable = true,
                FallbackSourceUsed = false,
                FailureCode = "partner_error",
                PartnerCode = "999111",
                PartnerResponseCode = "ER_WRONG_TICKET",
                Scheme = "ACCEL",
                PartnerFraudStatus = "Pending",
                PartnerMerchantAdviceCode = "24",
                AccommodationData = new List<AccommodationData>
                {
                    new AccommodationData
                    {
                        Name = "Hotel California",
                        BookingReference = "BR-001",
                        CheckInDate = DateTime.Parse("2026-08-01"),
                        CheckOutDate = DateTime.Parse("2026-08-05"),
                        City = "London",
                        Country = CountryCode.GB,
                        NumberOfRooms = 2
                    }
                },
                AirlineData = new List<AirlineData>
                {
                    new AirlineData
                    {
                        Ticket = new Ticket
                        {
                            Number = "045-21351455",
                            IssueDate = "2026-08-01",
                            IssuingCarrierCode = "AA"
                        }
                    }
                },
                SchemeTransactionLinkId = "MTL-001"
            };
        }

        [Fact]
        public void ShouldSerializeWithRequiredPropertiesForProcessingData()
        {
            var data = new ProcessingData();

            Should.NotThrow(() => Serializer.Serialize(data));
        }

        [Fact]
        public void ShouldSerializeWithAllOptionalPropertiesForProcessingData()
        {
            var data = CreateFullyPopulatedProcessingData();

            Should.NotThrow(() => Serializer.Serialize(data));
        }

        [Fact]
        public void ShouldRoundTripSerializeAllPropertiesForProcessingData()
        {
            var original = CreateFullyPopulatedProcessingData();

            var json = Serializer.Serialize(original);
            var deserialized = (ProcessingData)Serializer.Deserialize(json, typeof(ProcessingData));

            deserialized.ShouldNotBeNull();
            deserialized.PreferredScheme.ShouldBe(original.PreferredScheme);
            deserialized.AppId.ShouldBe(original.AppId);
            deserialized.PartnerCustomerId.ShouldBe(original.PartnerCustomerId);
            deserialized.PartnerPaymentId.ShouldBe(original.PartnerPaymentId);
            deserialized.TaxAmount.ShouldBe(original.TaxAmount);
            deserialized.PurchaseCountry.ShouldBe(original.PurchaseCountry);
            deserialized.Locale.ShouldBe(original.Locale);
            deserialized.RetrievalReferenceNumber.ShouldBe(original.RetrievalReferenceNumber);
            deserialized.PartnerOrderId.ShouldBe(original.PartnerOrderId);
            deserialized.PartnerStatus.ShouldBe(original.PartnerStatus);
            deserialized.PartnerTransactionId.ShouldBe(original.PartnerTransactionId);
            deserialized.PartnerErrorCodes.ShouldBe(original.PartnerErrorCodes);
            deserialized.PartnerErrorMessage.ShouldBe(original.PartnerErrorMessage);
            deserialized.PartnerAuthorizationCode.ShouldBe(original.PartnerAuthorizationCode);
            deserialized.PartnerAuthorizationResponseCode.ShouldBe(original.PartnerAuthorizationResponseCode);
            deserialized.FraudStatus.ShouldBe(original.FraudStatus);
            deserialized.ProviderAuthorizedPaymentMethod.Type.ShouldBe("pay_later");
            deserialized.ProviderAuthorizedPaymentMethod.Description.ShouldBe("Pay in 30 days");
            deserialized.ProviderAuthorizedPaymentMethod.NumberOfInstallments.ShouldBe(3L);
            deserialized.ProviderAuthorizedPaymentMethod.NumberOfDays.ShouldBe(30L);
            deserialized.CustomPaymentMethodIds.ShouldBe(original.CustomPaymentMethodIds);
            deserialized.Aft.ShouldBe(original.Aft);
            deserialized.MerchantCategoryCode.ShouldBe(original.MerchantCategoryCode);
            deserialized.SchemeMerchantId.ShouldBe(original.SchemeMerchantId);
            deserialized.PanTypeProcessed.ShouldBe(original.PanTypeProcessed);
            deserialized.CkoNetworkTokenAvailable.ShouldBe(original.CkoNetworkTokenAvailable);
            deserialized.FallbackSourceUsed.ShouldBe(original.FallbackSourceUsed);
            deserialized.FailureCode.ShouldBe(original.FailureCode);
            deserialized.PartnerCode.ShouldBe(original.PartnerCode);
            deserialized.PartnerResponseCode.ShouldBe(original.PartnerResponseCode);
            deserialized.Scheme.ShouldBe(original.Scheme);
            deserialized.PartnerFraudStatus.ShouldBe(original.PartnerFraudStatus);
            deserialized.PartnerMerchantAdviceCode.ShouldBe(original.PartnerMerchantAdviceCode);
            deserialized.AccommodationData.Count.ShouldBe(1);
            deserialized.AccommodationData[0].Name.ShouldBe("Hotel California");
            deserialized.AccommodationData[0].City.ShouldBe("London");
            deserialized.AirlineData.Count.ShouldBe(1);
            deserialized.AirlineData[0].Ticket.Number.ShouldBe("045-21351455");
            deserialized.SchemeTransactionLinkId.ShouldBe(original.SchemeTransactionLinkId);
        }

        [Fact]
        public void ShouldDeserializeSwaggerExampleForProcessingData()
        {
            const string json = @"{
                ""preferred_scheme"": ""visa"",
                ""app_id"": ""com.iap.linker_portal"",
                ""partner_customer_id"": ""2102209000001106125F8"",
                ""partner_payment_id"": ""440644309099499894406"",
                ""tax_amount"": 1000,
                ""locale"": ""en-US"",
                ""retrieval_reference_number"": ""909913440644"",
                ""partner_order_id"": ""ord_abc"",
                ""partner_status"": ""pending"",
                ""partner_transaction_id"": ""txn_abc"",
                ""partner_error_codes"": [""ERR_001""],
                ""partner_error_message"": ""Payment declined"",
                ""partner_authorization_code"": ""auth_123"",
                ""partner_authorization_response_code"": ""00"",
                ""custom_payment_method_ids"": [""cpm_001""],
                ""aft"": true,
                ""merchant_category_code"": ""5311"",
                ""scheme_merchant_id"": ""123456"",
                ""pan_type_processed"": ""fpan"",
                ""fallback_source_used"": false,
                ""failure_code"": ""partner_error"",
                ""partner_code"": ""999111"",
                ""partner_response_code"": ""ER_WRONG_TICKET"",
                ""scheme"": ""ACCEL"",
                ""partner_fraud_status"": ""Pending"",
                ""partner_merchant_advice_code"": ""24"",
                ""scheme_transaction_link_id"": ""MTL-001""
            }";

            var result = (ProcessingData)Serializer.Deserialize(json, typeof(ProcessingData));

            result.ShouldNotBeNull();
            result.PreferredScheme.ShouldBe(PreferredSchema.Visa);
            result.AppId.ShouldBe("com.iap.linker_portal");
            result.PartnerCustomerId.ShouldBe("2102209000001106125F8");
            result.PartnerPaymentId.ShouldBe("440644309099499894406");
            result.TaxAmount.ShouldBe(1000L);
            result.Locale.ShouldBe("en-US");
            result.RetrievalReferenceNumber.ShouldBe("909913440644");
            result.PartnerOrderId.ShouldBe("ord_abc");
            result.PartnerStatus.ShouldBe("pending");
            result.PartnerTransactionId.ShouldBe("txn_abc");
            result.PartnerErrorCodes.ShouldBe(new List<string> { "ERR_001" });
            result.PartnerErrorMessage.ShouldBe("Payment declined");
            result.PartnerAuthorizationCode.ShouldBe("auth_123");
            result.PartnerAuthorizationResponseCode.ShouldBe("00");
            result.CustomPaymentMethodIds.ShouldBe(new List<string> { "cpm_001" });
            result.Aft.ShouldBe(true);
            result.MerchantCategoryCode.ShouldBe("5311");
            result.SchemeMerchantId.ShouldBe("123456");
            result.PanTypeProcessed.ShouldBe(PanProcessedType.FPAN);
            result.FallbackSourceUsed.ShouldBe(false);
            result.FailureCode.ShouldBe("partner_error");
            result.PartnerCode.ShouldBe("999111");
            result.PartnerResponseCode.ShouldBe("ER_WRONG_TICKET");
            result.Scheme.ShouldBe("ACCEL");
            result.PartnerFraudStatus.ShouldBe("Pending");
            result.PartnerMerchantAdviceCode.ShouldBe("24");
            result.SchemeTransactionLinkId.ShouldBe("MTL-001");
        }

        [Fact]
        public void ShouldRoundTripSerializeSchemeAndPartnerResponseFields()
        {
            var original = new ProcessingData
            {
                Scheme = "ACCEL",
                PartnerFraudStatus = "Accepted",
                PartnerMerchantAdviceCode = "24"
            };

            var json = Serializer.Serialize(original);
            var deserialized = (ProcessingData)Serializer.Deserialize(json, typeof(ProcessingData));

            json.ShouldContain("\"scheme\":\"ACCEL\"");
            json.ShouldContain("\"partner_fraud_status\":\"Accepted\"");
            json.ShouldContain("\"partner_merchant_advice_code\":\"24\"");
            deserialized.Scheme.ShouldBe("ACCEL");
            deserialized.PartnerFraudStatus.ShouldBe("Accepted");
            deserialized.PartnerMerchantAdviceCode.ShouldBe("24");
        }

        [Fact]
        public void ShouldDeserializeFallbackSourceUsed()
        {
            const string json = @"{
                ""fallback_source_used"": true,
                ""app_id"": ""app_123"",
                ""retrieval_reference_number"": ""rrn_456""
            }";

            var result = (ProcessingData)Serializer.Deserialize(json, typeof(ProcessingData));

            result.ShouldNotBeNull();
            result.FallbackSourceUsed.ShouldBe(true);
            result.AppId.ShouldBe("app_123");
            result.RetrievalReferenceNumber.ShouldBe("rrn_456");
        }

        // ------------------------------------------------------------------------
        // ProcessingSettings
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeWithSchemeTransactionLinkId()
        {
            var settings = new ProcessingSettings { SchemeTransactionLinkId = "MTL-001" };

            Should.NotThrow(() => Serializer.Serialize(settings));
        }

        [Fact]
        public void ShouldDeserializeSchemeTransactionLinkId()
        {
            const string json = @"{""scheme_transaction_link_id"": ""MTL-001""}";

            var result = (ProcessingSettings)Serializer.Deserialize(json, typeof(ProcessingSettings));

            result.ShouldNotBeNull();
            result.SchemeTransactionLinkId.ShouldBe("MTL-001");
        }

        [Fact]
        public void ShouldRoundTripSerializeSchemeTransactionLinkId()
        {
            var original = new ProcessingSettings { SchemeTransactionLinkId = "MTL-XYZ-789" };

            var json = Serializer.Serialize(original);
            var deserialized = (ProcessingSettings)Serializer.Deserialize(json, typeof(ProcessingSettings));

            json.ShouldContain("\"scheme_transaction_link_id\":\"MTL-XYZ-789\"");
            deserialized.SchemeTransactionLinkId.ShouldBe("MTL-XYZ-789");
        }

        // ------------------------------------------------------------------------
        // ProcessingAggregator
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldRoundTripSerializeAggregator()
        {
            var original = new ProcessingSettings
            {
                Aggregator = new ProcessingAggregator
                {
                    SubMerchantId = "9cf70789ba90123",
                    AggregatorIdVisa = "10012345",
                    AggregatorIdMc = "00000123456"
                },
                ReconciliationId = "4123495123",
                ForeignRetailerAmount = 200,
                ServiceType = AchServiceType.Standard
            };

            var json = Serializer.Serialize(original);
            var deserialized = (ProcessingSettings)Serializer.Deserialize(json, typeof(ProcessingSettings));

            deserialized.Aggregator.ShouldNotBeNull();
            deserialized.Aggregator.SubMerchantId.ShouldBe("9cf70789ba90123");
            deserialized.Aggregator.AggregatorIdVisa.ShouldBe("10012345");
            deserialized.Aggregator.AggregatorIdMc.ShouldBe("00000123456");
            deserialized.ReconciliationId.ShouldBe("4123495123");
            deserialized.ForeignRetailerAmount.ShouldBe(200L);
            deserialized.ServiceType.ShouldBe(AchServiceType.Standard);
        }

        [Fact]
        public void ShouldSerializeSnakeCaseKeys()
        {
            var settings = new ProcessingSettings
            {
                Aggregator = new ProcessingAggregator
                {
                    SubMerchantId = "sub123",
                    AggregatorIdVisa = "visa123",
                    AggregatorIdMc = "mc123"
                },
                ReconciliationId = "rec_001",
                ServiceType = AchServiceType.SameDay
            };

            var json = Serializer.Serialize(settings);

            json.ShouldContain("\"aggregator\"");
            json.ShouldContain("\"sub_merchant_id\"");
            json.ShouldContain("\"aggregator_id_visa\"");
            json.ShouldContain("\"aggregator_id_mc\"");
            json.ShouldContain("\"reconciliation_id\"");
            json.ShouldContain("\"service_type\"");
            json.ShouldContain("same_day");
        }

        [Fact]
        public void ShouldDeserializeSwaggerExampleForProcessingAggregator()
        {
            const string json = @"{
                ""aggregator"": {
                    ""sub_merchant_id"": ""9cf70789ba90123"",
                    ""aggregator_id_visa"": ""10012345"",
                    ""aggregator_id_mc"": ""00000123456""
                },
                ""reconciliation_id"": ""4123495123"",
                ""foreign_retailer_amount"": 200,
                ""service_type"": ""standard""
            }";

            var result = (ProcessingSettings)Serializer.Deserialize(json, typeof(ProcessingSettings));

            result.Aggregator.SubMerchantId.ShouldBe("9cf70789ba90123");
            result.Aggregator.AggregatorIdVisa.ShouldBe("10012345");
            result.Aggregator.AggregatorIdMc.ShouldBe("00000123456");
            result.ReconciliationId.ShouldBe("4123495123");
            result.ForeignRetailerAmount.ShouldBe(200L);
            result.ServiceType.ShouldBe(AchServiceType.Standard);
        }
    }
}
