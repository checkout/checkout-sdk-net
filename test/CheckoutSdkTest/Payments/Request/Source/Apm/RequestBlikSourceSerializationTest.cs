using Checkout.Payments.Request.Source.Apm;
using Shouldly;
using Xunit;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestBlikSourceSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        [Fact]
        public void ShouldSerializeTypeDiscriminator()
        {
            var json = Serializer.Serialize(new RequestBlikSource());
            json.ShouldContain("\"type\":\"blik\"");
        }

        [Fact]
        public void ShouldSerializePartnerAgreementIdInSnakeCase()
        {
            var json = Serializer.Serialize(new RequestBlikSource { PartnerAgreementId = "blik_payid_123456789" });
            json.ShouldContain("\"partner_agreement_id\":\"blik_payid_123456789\"");
        }

        [Fact]
        public void ShouldRoundTripSerialize()
        {
            var original = new RequestBlikSource { PartnerAgreementId = "blik_payid_123456789" };
            var json = Serializer.Serialize(original);
            var deserialized = (RequestBlikSource)Serializer.Deserialize(json, typeof(RequestBlikSource));

            deserialized.ShouldNotBeNull();
            deserialized.Type.ShouldBe(Common.PaymentSourceType.Blik);
            deserialized.PartnerAgreementId.ShouldBe("blik_payid_123456789");
        }

        [Fact]
        public void ShouldDeserializeSwaggerExample()
        {
            const string swaggerJson = "{\"type\":\"blik\",\"partner_agreement_id\":\"blik_payid_123456789\"}";
            var source = (RequestBlikSource)Serializer.Deserialize(swaggerJson, typeof(RequestBlikSource));

            source.Type.ShouldBe(Common.PaymentSourceType.Blik);
            source.PartnerAgreementId.ShouldBe("blik_payid_123456789");
        }
    }
}
