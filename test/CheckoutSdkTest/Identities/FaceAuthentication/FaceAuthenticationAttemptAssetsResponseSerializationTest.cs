using Checkout.Common;
using Checkout.Identities.Entities;
using Checkout.Identities.FaceAuthentication.Responses;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace Checkout.Identities.FaceAuthentication
{
    public class FaceAuthenticationAttemptAssetsResponseSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        [Fact]
        public void ShouldSerializeWithAllProperties()
        {
            var response = new FaceAuthenticationAttemptAssetsResponse
            {
                TotalCount = 1,
                Skip = 0,
                Limit = 10,
                Data = new List<FaceAuthenticationAttemptAsset>
                {
                    new FaceAuthenticationAttemptAsset
                    {
                        Type = FaceAuthenticationAttemptAssetType.FaceImage,
                        Links = new AttemptAssetLinks
                        {
                            AssetUrl = new Link { Href = "https://example.com/face-image.jpg" }
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
                    { ""type"": ""face_image"", ""_links"": { ""asset_url"": { ""href"": ""https://example.com/face-image.jpg"" } } },
                    { ""type"": ""face_video"", ""_links"": { ""asset_url"": { ""href"": ""https://example.com/face-video.mp4"" } } }
                ],
                ""_links"": {
                    ""self"": { ""href"": ""https://example.com/assets"" },
                    ""next"": { ""href"": ""https://example.com/assets?skip=10"" }
                }
            }";

            var result = (FaceAuthenticationAttemptAssetsResponse)Serializer.Deserialize(json, typeof(FaceAuthenticationAttemptAssetsResponse));

            result.ShouldNotBeNull();
            result.TotalCount.ShouldBe(2);
            result.Skip.ShouldBe(0);
            result.Limit.ShouldBe(10);
            result.Data.ShouldNotBeNull();
            result.Data.Count.ShouldBe(2);
            result.Data[0].Type.ShouldBe(FaceAuthenticationAttemptAssetType.FaceImage);
            result.Data[0].Links.ShouldNotBeNull();
            result.Data[0].Links.AssetUrl.ShouldNotBeNull();
            result.Data[0].Links.AssetUrl.Href.ShouldBe("https://example.com/face-image.jpg");
            result.Data[1].Type.ShouldBe(FaceAuthenticationAttemptAssetType.FaceVideo);
            result.GetSelfLink().ShouldNotBeNull();
        }

        [Fact]
        public void ShouldRoundTripSerialize()
        {
            var original = new FaceAuthenticationAttemptAssetsResponse
            {
                TotalCount = 1,
                Skip = 5,
                Limit = 20,
                Data = new List<FaceAuthenticationAttemptAsset>
                {
                    new FaceAuthenticationAttemptAsset
                    {
                        Type = FaceAuthenticationAttemptAssetType.FaceVideo,
                        Links = new AttemptAssetLinks
                        {
                            AssetUrl = new Link { Href = "https://example.com/face-video.mp4" }
                        }
                    }
                }
            };

            var json = Serializer.Serialize(original);
            var deserialized = (FaceAuthenticationAttemptAssetsResponse)Serializer.Deserialize(json, typeof(FaceAuthenticationAttemptAssetsResponse));

            json.ShouldContain("\"total_count\":1");
            json.ShouldContain("\"asset_url\":");
            deserialized.TotalCount.ShouldBe(1);
            deserialized.Skip.ShouldBe(5);
            deserialized.Limit.ShouldBe(20);
            deserialized.Data.Count.ShouldBe(1);
            deserialized.Data[0].Type.ShouldBe(FaceAuthenticationAttemptAssetType.FaceVideo);
            deserialized.Data[0].Links.AssetUrl.Href.ShouldBe("https://example.com/face-video.mp4");
        }

        [Theory]
        [InlineData(FaceAuthenticationAttemptAssetType.FaceImage, "face_image")]
        [InlineData(FaceAuthenticationAttemptAssetType.FaceVideo, "face_video")]
        public void ShouldSerializeEachAssetTypeToSwaggerValue(FaceAuthenticationAttemptAssetType type, string expected)
        {
            var asset = new FaceAuthenticationAttemptAsset { Type = type };

            var json = Serializer.Serialize(asset);

            json.ShouldContain($"\"type\":\"{expected}\"");
        }
    }
}
