using System;
using Checkout.Issuing.Cards.Requests.Update;
using Checkout.Issuing.Cards.Responses.Update;
using Shouldly;
using Xunit;

namespace Checkout.Issuing.Cards
{
    /// <summary>
    /// Schema validation tests for CardUpdateResponse and CardUpdateHeaders.
    ///
    /// Swagger reference: PATCH /issuing/cards/{cardId}
    /// Schema: update-card-response. Only last_modified_date is required; encrypted_cvv is
    /// returned solely when the return-encrypted-cvv header is set to true.
    /// </summary>
    public class CardUpdateResponseSerializationTest
    {
        private readonly JsonSerializer _serializer = new JsonSerializer();

        [Fact]
        public void ShouldDeserializeWithEncryptedCvv()
        {
            const string json = @"{
                ""last_modified_date"": ""2026-06-01T10:00:00Z"",
                ""encrypted_cvv"": ""ZW5jcnlwdGVkLWN2dg=="",
                ""_links"": { ""self"": { ""href"": ""https://api.checkout.com/issuing/cards/crd_test"" } }
            }";

            var response = (CardUpdateResponse)_serializer.Deserialize(json, typeof(CardUpdateResponse));

            response.ShouldNotBeNull();
            response.LastModifiedDate.ShouldBe(DateTime.Parse("2026-06-01T10:00:00Z").ToUniversalTime());
            response.EncryptedCvv.ShouldBe("ZW5jcnlwdGVkLWN2dg==");
            response.GetSelfLink().ShouldNotBeNull();
        }

        /// <summary>
        /// encrypted_cvv is optional: a plain update that did not opt in must leave it null.
        /// </summary>
        [Fact]
        public void ShouldDeserializeWithoutEncryptedCvv()
        {
            const string json = @"{ ""last_modified_date"": ""2026-06-01T10:00:00Z"" }";

            var response = (CardUpdateResponse)_serializer.Deserialize(json, typeof(CardUpdateResponse));

            response.ShouldNotBeNull();
            response.LastModifiedDate.ShouldNotBeNull();
            response.EncryptedCvv.ShouldBeNull();
        }

        [Fact]
        public void ShouldRoundTripAllProperties()
        {
            var original = new CardUpdateResponse
            {
                LastModifiedDate = DateTime.Parse("2026-06-01T10:00:00Z").ToUniversalTime(),
                EncryptedCvv = "ZW5jcnlwdGVkLWN2dg=="
            };

            var json = _serializer.Serialize(original);
            var deserialized = (CardUpdateResponse)_serializer.Deserialize(json, typeof(CardUpdateResponse));

            json.ShouldContain("\"last_modified_date\":");
            json.ShouldContain("\"encrypted_cvv\":\"ZW5jcnlwdGVkLWN2dg==\"");
            deserialized.LastModifiedDate.ShouldBe(original.LastModifiedDate);
            deserialized.EncryptedCvv.ShouldBe(original.EncryptedCvv);
        }

        [Fact]
        public void ShouldSerializeTheUpdateHeadersUsingTheExactSwaggerHeaderNames()
        {
            var headers = new CardUpdateHeaders
            {
                ReturnEncryptedCvv = true,
                EncryptionKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8A"
            };

            var json = _serializer.Serialize(headers);

            // Header names are case-sensitive: return-encrypted-cvv is lower case,
            // Encryption-Key is title case.
            json.ShouldContain("\"return-encrypted-cvv\":true");
            json.ShouldContain("\"Encryption-Key\":\"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8A\"");
        }
    }
}
