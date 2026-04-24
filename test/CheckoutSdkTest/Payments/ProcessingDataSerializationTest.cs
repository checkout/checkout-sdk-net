using Checkout.Common;
using Checkout.Payments.Response;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace Checkout.Payments
{
    public class ProcessingDataSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        [Fact]
        public void ShouldSerializeWithRequiredProperties()
        {
            var data = new ProcessingData();

            Should.NotThrow(() => Serializer.Serialize(data));
        }

        [Fact]
        public void ShouldSerializeWithAllOptionalProperties()
        {
            var data = new ProcessingData
            {
                PreferredScheme = PreferredSchema.Visa,
                AppId = "app_123",
                PartnerCustomerId = "cust_abc",
                PartnerPaymentId = "pay_abc",
                TaxAmount = 100L,
                PurchaseCountry = CountryCode.GB,
                Locale = "en-GB",
                RetrievalReferenceNumber = "rrn_456",
                PartnerOrderId = "ord_abc",
                PartnerStatus = "pending",
                PartnerTransactionId = "txn_abc",
                PartnerErrorCodes = new List<string> { "ERR_001", "ERR_002" },
                PartnerErrorMessage = "Payment declined",
                PartnerAuthorizationCode = "auth_123",
                PartnerAuthorizationResponseCode = "00",
                FraudStatus = "approved",
                CustomPaymentMethodIds = new List<string> { "cpm_001" },
                Aft = true,
                MerchantCategoryCode = "5411",
                SchemeMerchantId = "scheme_merchant_001",
                PanTypeProcessed = PanProcessedType.FPAN,
                CkoNetworkTokenAvailable = true,
                FallbackSourceUsed = false
            };

            Should.NotThrow(() => Serializer.Serialize(data));
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

        [Fact]
        public void ShouldRoundTripSerializeFallbackSourceUsed()
        {
            var original = new ProcessingData
            {
                FallbackSourceUsed = false,
                AppId = "app_abc",
                MerchantCategoryCode = "5411"
            };

            var json = Serializer.Serialize(original);
            var deserialized = (ProcessingData)Serializer.Deserialize(json, typeof(ProcessingData));

            deserialized.FallbackSourceUsed.ShouldBe(false);
            deserialized.AppId.ShouldBe("app_abc");
            deserialized.MerchantCategoryCode.ShouldBe("5411");
        }
    }
}
