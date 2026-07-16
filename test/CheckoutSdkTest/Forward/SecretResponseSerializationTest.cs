using Checkout.Forward.Responses;
using Shouldly;
using System;
using Xunit;

namespace Checkout.Forward
{
    public class SecretResponseSerializationTest
    {
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
            response.CreatedAt.ShouldNotBeNull();
            response.CreatedAt.Value.ToUniversalTime()
                .ShouldBe(new DateTime(2024, 1, 15, 10, 30, 0, DateTimeKind.Utc));
            response.UpdatedAt.ShouldNotBeNull();
            response.UpdatedAt.Value.ToUniversalTime()
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
        public void ShouldRoundTripSerialize()
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
            deserialized.CreatedAt.Value.ToUniversalTime().ShouldBe(original.CreatedAt.Value.ToUniversalTime());
            deserialized.UpdatedAt.Value.ToUniversalTime().ShouldBe(original.UpdatedAt.Value.ToUniversalTime());
            deserialized.Version.ShouldBe(original.Version);
            deserialized.EntityId.ShouldBe(original.EntityId);
        }
    }
}
