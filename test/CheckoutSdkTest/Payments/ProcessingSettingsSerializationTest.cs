using Shouldly;
using Xunit;

namespace Checkout.Payments
{
    public class ProcessingSettingsSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

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
    }
}
