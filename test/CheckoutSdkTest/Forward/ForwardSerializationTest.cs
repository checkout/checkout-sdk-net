using Checkout.Forward.Requests;
using Checkout.Forward.Responses;
using Newtonsoft.Json.Linq;
using Shouldly;
using System;
using Xunit.Sdk;
using Xunit;

namespace Checkout.Forward
{
    /// <summary>
    /// Schema validation tests for Checkout.Forward.
    /// Grouped by domain; each section below covers one subject.
    /// </summary>
    public class ForwardSerializationTest
    {
        // ------------------------------------------------------------------------
        // SecretRequest
        // ------------------------------------------------------------------------

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
        public void ShouldRoundTripSerializeForSecretRequest()
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

        // ------------------------------------------------------------------------
        // SecretResponse
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldDeserializeSwaggerExample()
        {
            const string json = @"{
                ""name"": ""my_secret"",
                ""created_at"": ""2024-01-15T10:30:00Z"",
                ""updated_at"": ""2024-02-20T14:45:00Z"",
                ""version"": 3,
                ""entity_id"": ""ent_123""
            }";

            var response = (SecretResponse)new JsonSerializer()
                .Deserialize(json, typeof(SecretResponse));

            response.ShouldNotBeNull();
            response.Name.ShouldBe("my_secret");
            var createdAt = response.CreatedAt ?? throw new XunitException("created_at was null");
            createdAt.ToUniversalTime()
                .ShouldBe(new DateTime(2024, 1, 15, 10, 30, 0, DateTimeKind.Utc));
            var updatedAt = response.UpdatedAt ?? throw new XunitException("updated_at was null");
            updatedAt.ToUniversalTime()
                .ShouldBe(new DateTime(2024, 2, 20, 14, 45, 0, DateTimeKind.Utc));
            response.Version.ShouldBe(3);
            response.EntityId.ShouldBe("ent_123");
        }

        [Fact]
        public void ShouldDeserializeWithoutOptionalEntityId()
        {
            const string json = @"{
                ""name"": ""my_secret"",
                ""created_at"": ""2024-01-15T10:30:00Z"",
                ""updated_at"": ""2024-02-20T14:45:00Z"",
                ""version"": 1
            }";

            var response = (SecretResponse)new JsonSerializer()
                .Deserialize(json, typeof(SecretResponse));

            response.ShouldNotBeNull();
            response.Name.ShouldBe("my_secret");
            response.Version.ShouldBe(1);
            response.EntityId.ShouldBeNull();
        }

        [Fact]
        public void ShouldRoundTripSerializeForSecretResponse()
        {
            var original = new SecretResponse
            {
                Name = "my_secret",
                CreatedAt = new DateTime(2024, 1, 15, 10, 30, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 2, 20, 14, 45, 0, DateTimeKind.Utc),
                Version = 3,
                EntityId = "ent_123"
            };
            var serializer = new JsonSerializer();

            var json = serializer.Serialize(original);
            var deserialized = (SecretResponse)serializer.Deserialize(json, typeof(SecretResponse));

            deserialized.Name.ShouldBe(original.Name);
            var deserializedCreatedAt = deserialized.CreatedAt ?? throw new XunitException("created_at was null");
            var originalCreatedAt = original.CreatedAt ?? throw new XunitException("created_at was null");
            deserializedCreatedAt.ToUniversalTime().ShouldBe(originalCreatedAt.ToUniversalTime());
            var deserializedUpdatedAt = deserialized.UpdatedAt ?? throw new XunitException("updated_at was null");
            var originalUpdatedAt = original.UpdatedAt ?? throw new XunitException("updated_at was null");
            deserializedUpdatedAt.ToUniversalTime().ShouldBe(originalUpdatedAt.ToUniversalTime());
            deserialized.Version.ShouldBe(original.Version);
            deserialized.EntityId.ShouldBe(original.EntityId);
        }
    }
}
