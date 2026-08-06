using Checkout.Common;
using Checkout.Payments.Response;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace Checkout.Payments
{
    public class ProcessingDataSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        private static ProcessingData CreateFullyPopulated()
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
        public void ShouldSerializeWithRequiredProperties()
        {
            var data = new ProcessingData();

            Should.NotThrow(() => Serializer.Serialize(data));
        }

        [Fact]
        public void ShouldSerializeWithAllOptionalProperties()
        {
            var data = CreateFullyPopulated();

            Should.NotThrow(() => Serializer.Serialize(data));
        }

        [Fact]
        public void ShouldRoundTripSerializeAllProperties()
        {
            var original = CreateFullyPopulated();

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
        public void ShouldDeserializeSwaggerExample()
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
    }
}
