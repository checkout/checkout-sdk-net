using Checkout.NetworkTokens.Common.Responses;
using Shouldly;
using Xunit;

namespace Checkout.NetworkTokens
{
    public class NetworkTokenResponseSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        [Fact]
        public void ShouldSerializeWithRequiredProperties()
        {
            var response = new NetworkTokenResponse
            {
                Card = new Card
                {
                    Last4 = "1234",
                    ExpiryMonth = "06",
                    ExpiryYear = "2030"
                },
                NetworkToken = new NetworkToken
                {
                    Id = "dtoken_abc123",
                    State = StateType.Active,
                    Type = NetworkTokenType.Vts,
                    CreatedOn = "2024-01-15T10:00:00Z",
                    ModifiedOn = "2024-01-15T10:00:00Z"
                }
            };

            Should.NotThrow(() => Serializer.Serialize(response));
        }

        [Fact]
        public void ShouldSerializeWithAllOptionalProperties()
        {
            var response = new NetworkTokenResponse
            {
                Card = new Card
                {
                    Last4 = "9996",
                    ExpiryMonth = "06",
                    ExpiryYear = "2030"
                },
                NetworkToken = new NetworkToken
                {
                    Id = "dtoken_abc123",
                    State = StateType.Active,
                    Number = "4543474002249996",
                    ExpiryMonth = "06",
                    ExpiryYear = "2030",
                    Type = NetworkTokenType.Mdes,
                    CreatedOn = "2024-01-15T10:00:00Z",
                    ModifiedOn = "2024-06-01T12:00:00Z",
                    PaymentAccountReference = "V001234ABCDE567"
                },
                TokenRequestorId = "40010030273",
                TokenSchemeId = "vProvisionedTokenID_123"
            };

            Should.NotThrow(() => Serializer.Serialize(response));
        }

        [Fact]
        public void ShouldRoundTripSerialize()
        {
            var original = new NetworkTokenResponse
            {
                Card = new Card
                {
                    Last4 = "9996",
                    ExpiryMonth = "06",
                    ExpiryYear = "2030"
                },
                NetworkToken = new NetworkToken
                {
                    Id = "dtoken_abc123",
                    State = StateType.Active,
                    Number = "4543474002249996",
                    ExpiryMonth = "06",
                    ExpiryYear = "2030",
                    Type = NetworkTokenType.Vts,
                    CreatedOn = "2024-01-15T10:00:00Z",
                    ModifiedOn = "2024-06-01T12:00:00Z",
                    PaymentAccountReference = "V001234ABCDE567"
                },
                TokenRequestorId = "40010030273",
                TokenSchemeId = "vProvisionedTokenID_123"
            };

            var json = Serializer.Serialize(original);
            var deserialized = (NetworkTokenResponse)Serializer.Deserialize(json, typeof(NetworkTokenResponse));

            deserialized.Card.ShouldNotBeNull();
            deserialized.Card.Last4.ShouldBe("9996");
            deserialized.Card.ExpiryMonth.ShouldBe("06");
            deserialized.Card.ExpiryYear.ShouldBe("2030");
            deserialized.NetworkToken.ShouldNotBeNull();
            deserialized.NetworkToken.Id.ShouldBe("dtoken_abc123");
            deserialized.NetworkToken.State.ShouldBe(StateType.Active);
            deserialized.NetworkToken.Type.ShouldBe(NetworkTokenType.Vts);
            deserialized.NetworkToken.PaymentAccountReference.ShouldBe("V001234ABCDE567");
            deserialized.TokenRequestorId.ShouldBe("40010030273");
            deserialized.TokenSchemeId.ShouldBe("vProvisionedTokenID_123");
        }

        [Fact]
        public void ShouldDeserializeSwaggerExample()
        {
            const string json = @"{
                ""card"": {
                    ""last4"": ""9996"",
                    ""expiry_month"": ""06"",
                    ""expiry_year"": ""2030""
                },
                ""network_token"": {
                    ""id"": ""dtoken_abc123"",
                    ""state"": ""active"",
                    ""number"": ""4543474002249996"",
                    ""expiry_month"": ""06"",
                    ""expiry_year"": ""2030"",
                    ""type"": ""vts"",
                    ""created_on"": ""2024-01-15T10:00:00Z"",
                    ""modified_on"": ""2024-06-01T12:00:00Z"",
                    ""payment_account_reference"": ""V001234ABCDE567""
                },
                ""token_requestor_id"": ""40010030273"",
                ""token_scheme_id"": ""vProvisionedTokenID_123""
            }";

            var result = (NetworkTokenResponse)Serializer.Deserialize(json, typeof(NetworkTokenResponse));

            result.ShouldNotBeNull();
            result.Card.Last4.ShouldBe("9996");
            result.NetworkToken.Id.ShouldBe("dtoken_abc123");
            result.NetworkToken.State.ShouldBe(StateType.Active);
            result.NetworkToken.PaymentAccountReference.ShouldBe("V001234ABCDE567");
            result.TokenRequestorId.ShouldBe("40010030273");
            result.TokenSchemeId.ShouldBe("vProvisionedTokenID_123");
        }
    }
}
