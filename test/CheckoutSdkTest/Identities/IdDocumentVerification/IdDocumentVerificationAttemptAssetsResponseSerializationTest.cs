using Checkout.Common;
using Checkout.Identities.Entities;
using Checkout.Identities.IdDocumentVerification.Responses;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace Checkout.Identities.IdDocumentVerification
{
    /// <summary>
    /// Schema validation tests for IdDocumentVerificationAttemptAssetsResponse.
    ///
    /// Swagger reference: GET /id-document-verifications/{id_document_verification_id}/attempts/{attempt_id}/assets
    /// Schemas: IddvAttemptAssets, IddvAttemptAsset.
    /// </summary>
    public class IdDocumentVerificationAttemptAssetsResponseSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        [Fact]
        public void ShouldSerializeWithAllProperties()
        {
            var response = new IdDocumentVerificationAttemptAssetsResponse
            {
                TotalCount = 2,
                Skip = 0,
                Limit = 10,
                Data = new List<IdDocumentVerificationAttemptAsset>
                {
                    new IdDocumentVerificationAttemptAsset
                    {
                        Type = IdDocumentVerificationAttemptAssetType.DocumentFrontImage,
                        Links = new AttemptAssetLinks
                        {
                            AssetUrl = new Link { Href = "https://example.com/document_front.png" }
                        }
                    },
                    new IdDocumentVerificationAttemptAsset
                    {
                        Type = IdDocumentVerificationAttemptAssetType.DocumentBackImage,
                        Links = new AttemptAssetLinks
                        {
                            AssetUrl = new Link { Href = "https://example.com/document_back.png" }
                        }
                    }
                }
            };

            Should.NotThrow(() => Serializer.Serialize(response));
        }

        /// <summary>
        /// Deserializes the spec's iddv_attempt_assets_response_body example verbatim. The next and
        /// previous hrefs are truncated with "?..." in the specification itself.
        /// </summary>
        [Fact]
        public void ShouldDeserializeFromSwaggerExample()
        {
            const string json = @"{
                ""total_count"": 2,
                ""skip"": 0,
                ""limit"": 10,
                ""data"": [
                    {
                        ""type"": ""document_front_image"",
                        ""_links"": {
                            ""asset_url"": {
                                ""href"": ""https://storage-b.env.ubble.ai/ubble-ai/NDYOOVHGZPAQ/a54b3393-f02a-47c9-a9c5-2f6ee73560e1/bb603e2f-5de9-40f2-9631-8285a33c24c0/document_front.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Expires=3600""
                            }
                        }
                    },
                    {
                        ""type"": ""document_back_image"",
                        ""_links"": {
                            ""asset_url"": {
                                ""href"": ""https://storage-b.env.ubble.ai/ubble-ai/NDYOOVHGZPAQ/a54b3393-f02a-47c9-a9c5-2f6ee73560e1/bb603e2f-5de9-40f2-9631-8285a33c24c0/document_back.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Expires=3600""
                            }
                        }
                    }
                ],
                ""_links"": {
                    ""self"": { ""href"": ""https://identity-verification.checkout.com/id-document-verifications/iddv_tkoi5db4hryu5cei5vwoabr7we/attempts/datp_tkoi5db4hryu5cei5vwoabraio/assets"" },
                    ""next"": { ""href"": ""https://identity-verification.checkout.com/id-document-verifications/iddv_tkoi5db4hryu5cei5vwoabr7we/attempts/datp_tkoi5db4hryu5cei5vwoabraio/assets?..."" },
                    ""previous"": { ""href"": ""https://identity-verification.checkout.com/id-document-verifications/iddv_tkoi5db4hryu5cei5vwoabr7we/attempts/datp_tkoi5db4hryu5cei5vwoabraio/assets?..."" }
                }
            }";

            var result = (IdDocumentVerificationAttemptAssetsResponse)
                Serializer.Deserialize(json, typeof(IdDocumentVerificationAttemptAssetsResponse));

            result.ShouldNotBeNull();
            result.TotalCount.ShouldBe(2);
            result.Skip.ShouldBe(0);
            result.Limit.ShouldBe(10);
            result.Data.Count.ShouldBe(2);
            result.Data[0].Type.ShouldBe(IdDocumentVerificationAttemptAssetType.DocumentFrontImage);
            result.Data[0].Links.AssetUrl.Href.ShouldContain("document_front.png");
            result.Data[1].Type.ShouldBe(IdDocumentVerificationAttemptAssetType.DocumentBackImage);
            result.Data[1].Links.AssetUrl.Href.ShouldContain("document_back.png");
            result.GetSelfLink().ShouldNotBeNull();
            result.GetLink("next").ShouldNotBeNull();
            result.GetLink("previous").ShouldNotBeNull();
        }

        [Fact]
        public void ShouldRoundTripSerialize()
        {
            var original = new IdDocumentVerificationAttemptAssetsResponse
            {
                TotalCount = 2,
                Skip = 1,
                Limit = 4,
                Data = new List<IdDocumentVerificationAttemptAsset>
                {
                    new IdDocumentVerificationAttemptAsset
                    {
                        Type = IdDocumentVerificationAttemptAssetType.DocumentBackImage,
                        Links = new AttemptAssetLinks
                        {
                            AssetUrl = new Link { Href = "https://example.com/document_back.png" }
                        }
                    }
                }
            };

            var json = Serializer.Serialize(original);
            var deserialized = (IdDocumentVerificationAttemptAssetsResponse)
                Serializer.Deserialize(json, typeof(IdDocumentVerificationAttemptAssetsResponse));

            json.ShouldContain("\"total_count\":2");
            json.ShouldContain("\"asset_url\":");
            deserialized.TotalCount.ShouldBe(2);
            deserialized.Skip.ShouldBe(1);
            deserialized.Limit.ShouldBe(4);
            deserialized.Data.Count.ShouldBe(1);
            deserialized.Data[0].Type.ShouldBe(IdDocumentVerificationAttemptAssetType.DocumentBackImage);
            deserialized.Data[0].Links.AssetUrl.Href.ShouldBe("https://example.com/document_back.png");
        }

        /// <summary>
        /// data has minItems 0, so an empty page is valid and must not be asserted non-empty.
        /// </summary>
        [Fact]
        public void ShouldDeserializeEmptyDataPage()
        {
            const string json = @"{ ""total_count"": 0, ""skip"": 0, ""limit"": 10, ""data"": [] }";

            var result = (IdDocumentVerificationAttemptAssetsResponse)
                Serializer.Deserialize(json, typeof(IdDocumentVerificationAttemptAssetsResponse));

            result.ShouldNotBeNull();
            result.TotalCount.ShouldBe(0);
            result.Data.ShouldBeEmpty();
        }

        [Theory]
        [InlineData(IdDocumentVerificationAttemptAssetType.DocumentFrontImage, "document_front_image")]
        [InlineData(IdDocumentVerificationAttemptAssetType.DocumentBackImage, "document_back_image")]
        public void ShouldSerializeEachAssetTypeToSwaggerValue(IdDocumentVerificationAttemptAssetType type, string expected)
        {
            var asset = new IdDocumentVerificationAttemptAsset { Type = type };

            var json = Serializer.Serialize(asset);

            json.ShouldContain($"\"type\":\"{expected}\"");
        }
    }
}
