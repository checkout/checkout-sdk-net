using Shouldly;
using Xunit;

namespace Checkout.Payments
{
    public class PaymentProcessingSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        [Fact]
        public void ShouldSerializeWithRequiredProperties()
        {
            var processing = new PaymentProcessing();

            Should.NotThrow(() => Serializer.Serialize(processing));
        }

        [Fact]
        public void ShouldDeserializeSchemeTransactionLinkId()
        {
            const string json = @"{""scheme_transaction_link_id"": ""MTL-001""}";

            var processing = (PaymentProcessing)Serializer.Deserialize(json, typeof(PaymentProcessing));

            processing.ShouldNotBeNull();
            processing.SchemeTransactionLinkId.ShouldBe("MTL-001");
        }

        [Fact]
        public void ShouldSerializeSchemeTransactionLinkIdToSnakeCase()
        {
            var processing = new PaymentProcessing { SchemeTransactionLinkId = "MTL-001" };

            var json = Serializer.Serialize(processing);

            json.ShouldContain("\"scheme_transaction_link_id\":\"MTL-001\"");
        }

        [Fact]
        public void ShouldDeserializeProcessingFieldsWithSchemeTransactionLinkId()
        {
            const string json = @"{
                ""retrieval_reference_number"": ""RRN001"",
                ""acquirer_transaction_id"": ""ACQ001"",
                ""scheme"": ""Mastercard"",
                ""scheme_merchant_id"": 12345,
                ""scheme_transaction_link_id"": ""MTL-XYZ-789""
            }";

            var processing = (PaymentProcessing)Serializer.Deserialize(json, typeof(PaymentProcessing));

            processing.ShouldNotBeNull();
            processing.RetrievalReferenceNumber.ShouldBe("RRN001");
            processing.AcquirerTransactionId.ShouldBe("ACQ001");
            processing.Scheme.ShouldBe("Mastercard");
            processing.SchemeMerchantId.ShouldBe(12345L);
            processing.SchemeTransactionLinkId.ShouldBe("MTL-XYZ-789");
        }
    }
}
