using Checkout.Common;
using Checkout.Identities.AddressDocumentVerification.Responses;
using Checkout.Identities.Entities;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace Checkout.Identities.AddressDocumentVerification
{
    /// <summary>
    /// Schema validation tests for AddressDocumentVerificationAttemptAssetsResponse.
    ///
    /// Swagger reference: GET /address-document-verifications/{address_document_verification_id}/attempts/{attempt_id}/assets
    /// Schemas: AdvAttemptAssets, AdvAttemptAsset.
    /// </summary>
    public class AddressDocumentVerificationAttemptAssetsResponseSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        [Fact]
        public void ShouldSerializeWithAllProperties()
        {
            var response = new AddressDocumentVerificationAttemptAssetsResponse
            {
                TotalCount = 1,
                Skip = 0,
                Limit = 10,
                Data = new List<AddressDocumentVerificationAttemptAsset>
                {
                    new AddressDocumentVerificationAttemptAsset
                    {
                        Type = AddressDocumentVerificationAttemptAssetType.Document,
                        Links = new AttemptAssetLinks
                        {
                            AssetUrl = new Link { Href = "https://example.com/address-document.png" }
                        }
                    }
                }
            };

            Should.NotThrow(() => Serializer.Serialize(response));
        }

        /// <summary>
        /// Deserializes the spec's adv_attempt_assets_response_body example verbatim. The next and
        /// previous hrefs are truncated with "?..." in the specification itself.
        /// </summary>
        [Fact]
        public void ShouldDeserializeFromSwaggerExample()
        {
            const string json = @"{
                ""total_count"": 1,
                ""skip"": 0,
                ""limit"": 10,
                ""data"": [
                    {
                        ""type"": ""document"",
                        ""_links"": {
                            ""asset_url"": {
                                ""href"": ""https://storage-b.env.ubble.ai/ubble-ai/NDYOOVHGZPAQ/a54b3393-f02a-47c9-a9c5-2f6ee73560e1/bb603e2f-5de9-40f2-9631-8285a33c24c0/address_document.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Expires=3600""
                            }
                        }
                    }
                ],
                ""_links"": {
                    ""self"": { ""href"": ""https://identity-verification.checkout.com/address-document-verifications/adv_tkoi5db4hryu5cei5vwoabr7we/attempts/adva_tkoi5db4hryu5cei5vwoabr7we/assets"" },
                    ""next"": { ""href"": ""https://identity-verification.checkout.com/address-document-verifications/adv_tkoi5db4hryu5cei5vwoabr7we/attempts/adva_tkoi5db4hryu5cei5vwoabr7we/assets?..."" },
                    ""previous"": { ""href"": ""https://identity-verification.checkout.com/address-document-verifications/adv_tkoi5db4hryu5cei5vwoabr7we/attempts/adva_tkoi5db4hryu5cei5vwoabr7we/assets?..."" }
                }
            }";

            var result = (AddressDocumentVerificationAttemptAssetsResponse)
                Serializer.Deserialize(json, typeof(AddressDocumentVerificationAttemptAssetsResponse));

            result.ShouldNotBeNull();
            result.TotalCount.ShouldBe(1);
            result.Skip.ShouldBe(0);
            result.Limit.ShouldBe(10);
            result.Data.ShouldNotBeNull();
            result.Data.Count.ShouldBe(1);
            result.Data[0].Type.ShouldBe(AddressDocumentVerificationAttemptAssetType.Document);
            result.Data[0].Links.ShouldNotBeNull();
            result.Data[0].Links.AssetUrl.ShouldNotBeNull();
            result.Data[0].Links.AssetUrl.Href.ShouldContain("address_document.png");
            result.GetSelfLink().ShouldNotBeNull();
            result.GetLink("next").ShouldNotBeNull();
            result.GetLink("previous").ShouldNotBeNull();
        }

        [Fact]
        public void ShouldRoundTripSerialize()
        {
            var original = new AddressDocumentVerificationAttemptAssetsResponse
            {
                TotalCount = 3,
                Skip = 6,
                Limit = 5,
                Data = new List<AddressDocumentVerificationAttemptAsset>
                {
                    new AddressDocumentVerificationAttemptAsset
                    {
                        Type = AddressDocumentVerificationAttemptAssetType.Document,
                        Links = new AttemptAssetLinks
                        {
                            AssetUrl = new Link { Href = "https://example.com/address-document.png" }
                        }
                    }
                }
            };

            var json = Serializer.Serialize(original);
            var deserialized = (AddressDocumentVerificationAttemptAssetsResponse)
                Serializer.Deserialize(json, typeof(AddressDocumentVerificationAttemptAssetsResponse));

            json.ShouldContain("\"total_count\":3");
            json.ShouldContain("\"asset_url\":");
            deserialized.TotalCount.ShouldBe(3);
            deserialized.Skip.ShouldBe(6);
            deserialized.Limit.ShouldBe(5);
            deserialized.Data.Count.ShouldBe(1);
            deserialized.Data[0].Type.ShouldBe(AddressDocumentVerificationAttemptAssetType.Document);
            deserialized.Data[0].Links.AssetUrl.Href.ShouldBe("https://example.com/address-document.png");
        }

        /// <summary>
        /// data has minItems 0, so an empty page is valid and must not be asserted non-empty.
        /// </summary>
        [Fact]
        public void ShouldDeserializeEmptyDataPage()
        {
            const string json = @"{ ""total_count"": 0, ""skip"": 0, ""limit"": 10, ""data"": [] }";

            var result = (AddressDocumentVerificationAttemptAssetsResponse)
                Serializer.Deserialize(json, typeof(AddressDocumentVerificationAttemptAssetsResponse));

            result.ShouldNotBeNull();
            result.TotalCount.ShouldBe(0);
            result.Data.ShouldNotBeNull();
            result.Data.ShouldBeEmpty();
        }

        [Fact]
        public void ShouldSerializeTheOnlyAssetTypeToItsSwaggerValue()
        {
            var asset = new AddressDocumentVerificationAttemptAsset
            {
                Type = AddressDocumentVerificationAttemptAssetType.Document
            };

            var json = Serializer.Serialize(asset);

            json.ShouldContain("\"type\":\"document\"");
        }
    }
}
