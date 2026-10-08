using Checkout.Issuing.Common;
using Checkout.Issuing.Controls.Requests.Update;
using Checkout.Issuing.Controls.Responses.Create;
using Checkout.Issuing.Controls.Responses.Query;
using Newtonsoft.Json.Linq;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace Checkout.Issuing.Controls
{
    /// <summary>
    /// Schema validation tests for Checkout.Issuing.Controls.
    /// Grouped by domain; each section below covers one subject.
    /// </summary>
    public class ControlsSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        private const string ControlId = "ctr_gp7vkmxayztufjz6top5bjcdra";
        private const string TargetId = "crd_fa6psq42dcdd6fdn5gifcq1491";
        private const string Date = "2023-03-12T18:20:12.0000000+00:00";

        // ------------------------------------------------------------------------
        // VelocityCardControlResponse
        // Covers the base control fields plus description and velocity_limit
        // (VelocityLimitWithRemainingAmount) against update-control-velocity-limit-response.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldDeserializeSwaggerExampleForVelocityCardControlResponse()
        {
            const string json = @"{
                ""id"": ""ctr_gp7vkmxayztufjz6top5bjcdra"",
                ""target_id"": ""crd_fa6psq42dcdd6fdn5gifcq1491"",
                ""description"": ""Maximum spend of 500 per week for restaurants"",
                ""control_type"": ""velocity_limit"",
                ""is_editable"": true,
                ""created_date"": ""2023-03-12T18:20:12.0000000+00:00"",
                ""last_modified_date"": ""2023-03-12T18:20:12.0000000+00:00"",
                ""velocity_limit"": {
                    ""amount_remaining"": 45000,
                    ""amount_limit"": 50000,
                    ""velocity_window"": { ""type"": ""weekly"" },
                    ""mcc_list"": [4121, 4582]
                }
            }";

            var response = (AbstractCardControlResponse)Serializer.Deserialize(json, typeof(AbstractCardControlResponse));

            var velocity = response.ShouldBeOfType<VelocityCardControlResponse>();
            AssertBase(velocity, IssuingControlType.VelocityLimit, "Maximum spend of 500 per week for restaurants");
            velocity.VelocityLimit.AmountRemaining.ShouldBe(45000);
            velocity.VelocityLimit.AmountLimit.ShouldBe(50000);
            velocity.VelocityLimit.VelocityWindow.Type.ShouldBe(VelocityWindowType.Weekly);
            velocity.VelocityLimit.MccList.ShouldBe(new List<string> { "4121", "4582" });
            velocity.VelocityLimit.MidList.ShouldBeNull();
        }

        [Fact]
        public void ShouldRoundTripAllPropertiesForVelocityCardControlResponse()
        {
            var original = new VelocityCardControlResponse
            {
                VelocityLimit = new VelocityLimit
                {
                    AmountLimit = 50000,
                    AmountRemaining = 45000,
                    VelocityWindow = new VelocityWindow { Type = VelocityWindowType.AllTime },
                    MccList = new List<string> { "4121" },
                    MidList = new List<string> { "593278" }
                }
            };
            PopulateBase(original, "velocity");

            var json = Serializer.Serialize(original);
            var result = (AbstractCardControlResponse)Serializer.Deserialize(json, typeof(AbstractCardControlResponse));

            var velocity = result.ShouldBeOfType<VelocityCardControlResponse>();
            AssertBase(velocity, IssuingControlType.VelocityLimit, "velocity");
            velocity.VelocityLimit.AmountLimit.ShouldBe(50000);
            velocity.VelocityLimit.AmountRemaining.ShouldBe(45000);
            velocity.VelocityLimit.VelocityWindow.Type.ShouldBe(VelocityWindowType.AllTime);
            velocity.VelocityLimit.MccList.ShouldBe(new List<string> { "4121" });
            velocity.VelocityLimit.MidList.ShouldBe(new List<string> { "593278" });
        }

        [Fact]
        public void ShouldNotSendAmountRemainingOnVelocityCardControlUpdate()
        {
            var request = new VelocityCardControlUpdate
            {
                Description = "velocity",
                VelocityLimit = new VelocityLimit
                {
                    AmountLimit = 100,
                    VelocityWindow = new VelocityWindow { Type = VelocityWindowType.Daily }
                }
            };

            var json = JObject.Parse(Serializer.Serialize(request));

            ((JObject)json["velocity_limit"]).ContainsKey("amount_remaining").ShouldBeFalse();
        }

        // ------------------------------------------------------------------------
        // MccCardControlResponse
        // Covers the base control fields plus description and mcc_limit against
        // update-control-mcc-limit-response.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldDeserializeSwaggerExampleForMccCardControlResponse()
        {
            const string json = @"{
                ""id"": ""ctr_gp7vkmxayztufjz6top5bjcdra"",
                ""target_id"": ""crd_fa6psq42dcdd6fdn5gifcq1491"",
                ""description"": ""Allow the card to be used only in restaurants and supermarkets"",
                ""control_type"": ""mcc_limit"",
                ""is_editable"": true,
                ""created_date"": ""2023-03-12T18:20:12.0000000+00:00"",
                ""last_modified_date"": ""2023-03-12T18:20:12.0000000+00:00"",
                ""mcc_limit"": { ""type"": ""allow"", ""mcc_list"": [5932, 5411] }
            }";

            var response = (AbstractCardControlResponse)Serializer.Deserialize(json, typeof(AbstractCardControlResponse));

            var mcc = response.ShouldBeOfType<MccCardControlResponse>();
            AssertBase(mcc, IssuingControlType.MccLimit, "Allow the card to be used only in restaurants and supermarkets");
            mcc.MccLimit.Type.ShouldBe(LimitControlType.Allow);
            mcc.MccLimit.MccList.ShouldBe(new List<string> { "5932", "5411" });
        }

        [Fact]
        public void ShouldRoundTripAllPropertiesForMccCardControlResponse()
        {
            var original = new MccCardControlResponse
            {
                MccLimit = new MccLimit { Type = LimitControlType.Block, MccList = new List<string> { "5932" } }
            };
            PopulateBase(original, "mcc");

            var json = Serializer.Serialize(original);
            var result = (AbstractCardControlResponse)Serializer.Deserialize(json, typeof(AbstractCardControlResponse));

            var mcc = result.ShouldBeOfType<MccCardControlResponse>();
            AssertBase(mcc, IssuingControlType.MccLimit, "mcc");
            mcc.MccLimit.Type.ShouldBe(LimitControlType.Block);
            mcc.MccLimit.MccList.ShouldBe(new List<string> { "5932" });
        }

        // ------------------------------------------------------------------------
        // MidCardControlResponse
        // Covers the base control fields plus description and mid_limit against
        // update-control-mid-limit-response, and the deprecated VelocityLimit alias.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldDeserializeSwaggerExampleForMidCardControlResponse()
        {
            const string json = @"{
                ""id"": ""ctr_gp7vkmxayztufjz6top5bjcdra"",
                ""target_id"": ""crd_fa6psq42dcdd6fdn5gifcq1491"",
                ""description"": ""Allow the card to be used only in AZ Pizza"",
                ""control_type"": ""mid_limit"",
                ""is_editable"": true,
                ""created_date"": ""2023-03-12T18:20:12.0000000+00:00"",
                ""last_modified_date"": ""2023-03-12T18:20:12.0000000+00:00"",
                ""mid_limit"": { ""type"": ""allow"", ""mid_list"": [593278, 541114] }
            }";

            var response = (AbstractCardControlResponse)Serializer.Deserialize(json, typeof(AbstractCardControlResponse));

            var mid = response.ShouldBeOfType<MidCardControlResponse>();
            AssertBase(mid, IssuingControlType.MidLimit, "Allow the card to be used only in AZ Pizza");
            mid.MidLimit.ShouldNotBeNull();
            mid.MidLimit.Type.ShouldBe(LimitControlType.Allow);
            mid.MidLimit.MidList.ShouldBe(new List<string> { "593278", "541114" });
#pragma warning disable CS0618
            mid.VelocityLimit.ShouldBeSameAs(mid.MidLimit);
#pragma warning restore CS0618
        }

        [Fact]
        public void ShouldRoundTripAllPropertiesForMidCardControlResponse()
        {
            var original = new MidCardControlResponse
            {
                MidLimit = new MidLimit { Type = LimitControlType.Block, MidList = new List<string> { "AZ-PIZZA" } }
            };
            PopulateBase(original, "mid");

            var json = Serializer.Serialize(original);
            var result = (AbstractCardControlResponse)Serializer.Deserialize(json, typeof(AbstractCardControlResponse));

            var mid = result.ShouldBeOfType<MidCardControlResponse>();
            AssertBase(mid, IssuingControlType.MidLimit, "mid");
            mid.MidLimit.Type.ShouldBe(LimitControlType.Block);
            mid.MidLimit.MidList.ShouldBe(new List<string> { "AZ-PIZZA" });
        }

        [Fact]
        public void ShouldForwardDeprecatedVelocityLimitToMidLimitAndSerializeOnceForMidCardControlResponse()
        {
            var limit = new MidLimit { Type = LimitControlType.Allow, MidList = new List<string> { "593278" } };
#pragma warning disable CS0618
            var response = new MidCardControlResponse { VelocityLimit = limit };
            response.MidLimit.ShouldBeSameAs(limit);
            response.VelocityLimit.ShouldBeSameAs(response.MidLimit);
#pragma warning restore CS0618

            var json = JObject.Parse(Serializer.Serialize(response));

            json.ContainsKey("mid_limit").ShouldBeTrue();
            json.ContainsKey("velocity_limit").ShouldBeFalse();
        }

        // ------------------------------------------------------------------------
        // Create control response (add-control-response)
        // The create operation also returns _links (ControlLinks).
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldDeserializeLinksForCreateCardControlResponse()
        {
            const string json = @"{
                ""id"": ""ctr_gp7vkmxayztufjz6top5bjcdra"",
                ""target_id"": ""crd_fa6psq42dcdd6fdn5gifcq1491"",
                ""description"": ""Allow the card to be used only in AZ Pizza"",
                ""control_type"": ""mid_limit"",
                ""is_editable"": true,
                ""created_date"": ""2023-03-12T18:20:12.0000000+00:00"",
                ""last_modified_date"": ""2023-03-12T18:20:12.0000000+00:00"",
                ""mid_limit"": { ""type"": ""allow"", ""mid_list"": [""593278""] },
                ""_links"": { ""self"": { ""href"": ""https://api.checkout.com/issuing/controls/ctr_gp7vkmxayztufjz6top5bjcdra"" } }
            }";

            var response = (AbstractCardControlResponse)Serializer.Deserialize(json, typeof(AbstractCardControlResponse));

            var mid = response.ShouldBeOfType<MidCardControlResponse>();
            AssertBase(mid, IssuingControlType.MidLimit, "Allow the card to be used only in AZ Pizza");
            mid.GetSelfLink().ShouldNotBeNull();
            mid.GetSelfLink().Href.ShouldBe("https://api.checkout.com/issuing/controls/ctr_gp7vkmxayztufjz6top5bjcdra");
        }

        // ------------------------------------------------------------------------
        // CardControlsQueryResponse
        // The list operation returns the same discriminated subtypes.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldDeserializeEverySubtypeForCardControlsQueryResponse()
        {
            const string json = @"{ ""controls"": [
                { ""id"": ""ctr_gp7vkmxayztufjz6top5bjcdra"", ""control_type"": ""velocity_limit"",
                  ""velocity_limit"": { ""amount_remaining"": 1, ""amount_limit"": 2, ""velocity_window"": { ""type"": ""daily"" } } },
                { ""id"": ""ctr_gp7vkmxayztufjz6top5bjcdra"", ""control_type"": ""mcc_limit"",
                  ""mcc_limit"": { ""type"": ""block"", ""mcc_list"": [""5411""] } },
                { ""id"": ""ctr_gp7vkmxayztufjz6top5bjcdra"", ""control_type"": ""mid_limit"",
                  ""mid_limit"": { ""type"": ""block"", ""mid_list"": [""593278""] } }
            ] }";

            var response = (CardControlsQueryResponse)Serializer.Deserialize(json, typeof(CardControlsQueryResponse));

            response.Controls.Count.ShouldBe(3);
            response.Controls[0].ShouldBeOfType<VelocityCardControlResponse>().VelocityLimit.AmountRemaining.ShouldBe(1);
            response.Controls[1].ShouldBeOfType<MccCardControlResponse>().MccLimit.MccList.ShouldBe(new List<string> { "5411" });
            response.Controls[2].ShouldBeOfType<MidCardControlResponse>().MidLimit.MidList.ShouldBe(new List<string> { "593278" });
        }

        private static void PopulateBase(AbstractCardControlResponse response, string description)
        {
            response.Id = ControlId;
            response.TargetId = TargetId;
            response.IsEditable = true;
            response.CreatedDate = Date;
            response.LastModifiedDate = Date;
            response.Description = description;
        }

        private static void AssertBase(AbstractCardControlResponse response, IssuingControlType type, string description)
        {
            response.ControlType.ShouldBe(type);
            response.Id.ShouldBe(ControlId);
            response.TargetId.ShouldBe(TargetId);
            response.IsEditable.ShouldBe(true);
            response.CreatedDate.ShouldNotBeNullOrEmpty();
            response.LastModifiedDate.ShouldNotBeNullOrEmpty();
            response.Description.ShouldBe(description);
        }
    }
}
