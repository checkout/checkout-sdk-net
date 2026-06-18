using Checkout.Common;
using Checkout.Identities.Entities;
using Checkout.Identities.IdentityVerification.Responses;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace Checkout.Identities.IdentityVerification
{
    public class IdentityVerificationAttemptAssetsResponseSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        [Fact]
        public void ShouldSerializeWithAllProperties()
        {
            var response = new IdentityVerificationAttemptAssetsResponse
            {
                TotalCount = 1,
                Skip = 0,
                Limit = 10,
                Data = new List<IdentityVerificationAttemptAsset>
                {
                    new IdentityVerificationAttemptAsset
                    {
                        Type = IdentityVerificationAttemptAssetType.DocumentFrontImage,
                        Links = new AttemptAssetLinks
                        {
                            AssetUrl = new Link { Href = "https://example.com/document-front.jpg" }
                        }
                    }
                }
            };

            Should.NotThrow(() => Serializer.Serialize(response));
        }

        [Fact]
        public void ShouldDeserializeFromSwaggerExample()
        {
            const string json = @"{
                ""total_count"": 2,
                ""skip"": 0,
                ""limit"": 10,
                ""data"": [
                    { ""type"": ""document_front_image"", ""_links"": { ""asset_url"": { ""href"": ""https://example.com/document-front.jpg"" } } },
                    { ""type"": ""face_image"", ""_links"": { ""asset_url"": { ""href"": ""https://example.com/face-image.jpg"" } } }
                ],
                ""_links"": {
                    ""self"": { ""href"": ""https://example.com/assets"" },
                    ""previous"": { ""href"": ""https://example.com/assets?skip=0"" }
                }
            }";

            var result = (IdentityVerificationAttemptAssetsResponse)Serializer.Deserialize(json, typeof(IdentityVerificationAttemptAssetsResponse));

            result.ShouldNotBeNull();
            result.TotalCount.ShouldBe(2);
            result.Skip.ShouldBe(0);
            result.Limit.ShouldBe(10);
            result.Data.ShouldNotBeNull();
            result.Data.Count.ShouldBe(2);
            result.Data[0].Type.ShouldBe(IdentityVerificationAttemptAssetType.DocumentFrontImage);
            result.Data[0].Links.ShouldNotBeNull();
            result.Data[0].Links.AssetUrl.ShouldNotBeNull();
            result.Data[0].Links.AssetUrl.Href.ShouldBe("https://example.com/document-front.jpg");
            result.Data[1].Type.ShouldBe(IdentityVerificationAttemptAssetType.FaceImage);
            result.GetSelfLink().ShouldNotBeNull();
        }

        [Fact]
        public void ShouldRoundTripSerialize()
        {
            var original = new IdentityVerificationAttemptAssetsResponse
            {
                TotalCount = 1,
                Skip = 5,
                Limit = 20,
                Data = new List<IdentityVerificationAttemptAsset>
                {
                    new IdentityVerificationAttemptAsset
                    {
                        Type = IdentityVerificationAttemptAssetType.SecondaryDocumentSignatureImage,
                        Links = new AttemptAssetLinks
                        {
                            AssetUrl = new Link { Href = "https://example.com/signature.jpg" }
                        }
                    }
                }
            };

            var json = Serializer.Serialize(original);
            var deserialized = (IdentityVerificationAttemptAssetsResponse)Serializer.Deserialize(json, typeof(IdentityVerificationAttemptAssetsResponse));

            json.ShouldContain("\"total_count\":1");
            json.ShouldContain("\"asset_url\":");
            deserialized.TotalCount.ShouldBe(1);
            deserialized.Skip.ShouldBe(5);
            deserialized.Limit.ShouldBe(20);
            deserialized.Data.Count.ShouldBe(1);
            deserialized.Data[0].Type.ShouldBe(IdentityVerificationAttemptAssetType.SecondaryDocumentSignatureImage);
            deserialized.Data[0].Links.AssetUrl.Href.ShouldBe("https://example.com/signature.jpg");
        }

        [Theory]
        [InlineData(IdentityVerificationAttemptAssetType.FaceImage, "face_image")]
        [InlineData(IdentityVerificationAttemptAssetType.FaceVideo, "face_video")]
        [InlineData(IdentityVerificationAttemptAssetType.DocumentFrontImage, "document_front_image")]
        [InlineData(IdentityVerificationAttemptAssetType.DocumentBackImage, "document_back_image")]
        [InlineData(IdentityVerificationAttemptAssetType.DocumentFrontVideo, "document_front_video")]
        [InlineData(IdentityVerificationAttemptAssetType.DocumentBackVideo, "document_back_video")]
        [InlineData(IdentityVerificationAttemptAssetType.DocumentSignatureImage, "document_signature_image")]
        [InlineData(IdentityVerificationAttemptAssetType.SecondaryDocumentFrontImage, "secondary_document_front_image")]
        [InlineData(IdentityVerificationAttemptAssetType.SecondaryDocumentBackImage, "secondary_document_back_image")]
        [InlineData(IdentityVerificationAttemptAssetType.SecondaryDocumentFrontVideo, "secondary_document_front_video")]
        [InlineData(IdentityVerificationAttemptAssetType.SecondaryDocumentBackVideo, "secondary_document_back_video")]
        [InlineData(IdentityVerificationAttemptAssetType.SecondaryDocumentSignatureImage, "secondary_document_signature_image")]
        public void ShouldSerializeEachAssetTypeToSwaggerValue(IdentityVerificationAttemptAssetType type, string expected)
        {
            var asset = new IdentityVerificationAttemptAsset { Type = type };

            var json = Serializer.Serialize(asset);

            json.ShouldContain($"\"type\":\"{expected}\"");
        }
    }
}
