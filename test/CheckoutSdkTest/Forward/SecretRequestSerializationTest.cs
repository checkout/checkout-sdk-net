using Checkout.Forward.Requests;
using Newtonsoft.Json.Linq;
using Shouldly;
using Xunit;

namespace Checkout.Forward
{
    public class SecretRequestSerializationTest
    {
        [Fact]
        public void ShouldSerializeWithRequiredProperties()
        {
            var request = new SecretRequest
            {
                Name = "my_secret",
                Value = "s3cr3t-value"
            };

            Should.NotThrow(() => new JsonSerializer().Serialize(request));
        }

        [Fact]
        public void ShouldSerializeWithAllOptionalProperties()
        {
            var request = new SecretRequest
            {
                Name = "my_secret",
                Value = "s3cr3t-value",
                EntityId = "ent_123"
            };

            Should.NotThrow(() => new JsonSerializer().Serialize(request));
        }

        [Fact]
        public void ShouldMapPropertiesToSnakeCaseJson()
        {
            var request = new SecretRequest
            {
                Name = "my_secret",
                Value = "s3cr3t-value",
                EntityId = "ent_123"
            };

            var json = JObject.Parse(new JsonSerializer().Serialize(request));

            json["name"].Value<string>().ShouldBe("my_secret");
            json["value"].Value<string>().ShouldBe("s3cr3t-value");
            json["entity_id"].Value<string>().ShouldBe("ent_123");
        }

        [Fact]
        public void ShouldOmitNullOptionalProperties()
        {
            // UpdateSecretRequest allows value + entity_id only; name may be omitted.
            var request = new SecretRequest
            {
                Value = "new-value"
            };

            var json = JObject.Parse(new JsonSerializer().Serialize(request));

            json["value"].Value<string>().ShouldBe("new-value");
            json.ContainsKey("name").ShouldBeFalse();
            json.ContainsKey("entity_id").ShouldBeFalse();
        }

        [Fact]
        public void ShouldRoundTripSerialize()
        {
            var original = new SecretRequest
            {
                Name = "my_secret",
                Value = "s3cr3t-value",
                EntityId = "ent_123"
            };
            var serializer = new JsonSerializer();

            var json = serializer.Serialize(original);
            var deserialized = (SecretRequest)serializer.Deserialize(json, typeof(SecretRequest));

            deserialized.Name.ShouldBe(original.Name);
            deserialized.Value.ShouldBe(original.Value);
            deserialized.EntityId.ShouldBe(original.EntityId);
        }
    }
}
