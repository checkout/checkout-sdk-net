using Checkout.Issuing.Cards.Responses.Create;
using Checkout.Issuing.Common;
using Newtonsoft.Json.Linq;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace Checkout.Issuing.Cards
{
    /// <summary>
    /// Schema validation tests for the card create response in Checkout.Issuing.Cards.
    /// </summary>
    public class CardCreateResponseSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        // ------------------------------------------------------------------------
        // AbstractCardControlsResponse subtypes
        // Covers add-virtual-card-response-control-response (id, description,
        // control_type) and its velocity, mcc and mid subtypes.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldDeserializeControlsForVirtualCardCreateResponse()
        {
            const string json = @"{
                ""id"": ""crd_fa6psq42dcdd6fdn5gifcq1491"",
                ""type"": ""virtual"",
                ""last_four"": ""1234"",
                ""controls"": [
                    { ""id"": ""ctr_s6tfwc6jpieexgcet2pljss6tu"", ""description"": ""velocity"", ""control_type"": ""velocity_limit"",
                      ""velocity_limit"": { ""amount_limit"": 5000, ""velocity_window"": { ""type"": ""weekly"" }, ""mcc_list"": [""4121""] } },
                    { ""id"": ""ctr_s6tfwc6jpieexgcet2pljss6tu"", ""description"": ""mcc"", ""control_type"": ""mcc_limit"",
                      ""mcc_limit"": { ""type"": ""allow"", ""mcc_list"": [5932, 5411] } },
                    { ""id"": ""ctr_s6tfwc6jpieexgcet2pljss6tu"", ""description"": ""Allow the card to be used only in AZ Pizza"", ""control_type"": ""mid_limit"",
                      ""mid_limit"": { ""type"": ""allow"", ""mid_list"": [593278, 541114] } }
                ]
            }";

            var response = (AbstractCardCreateResponse)Serializer.Deserialize(json, typeof(AbstractCardCreateResponse));

            var card = response.ShouldBeOfType<VirtualCardCreateResponse>();
            card.Controls.Count.ShouldBe(3);

            var velocity = card.Controls[0].ShouldBeOfType<VelocityCardControlsResponse>();
            velocity.Id.ShouldBe("ctr_s6tfwc6jpieexgcet2pljss6tu");
            velocity.Description.ShouldBe("velocity");
            velocity.ControlType.ShouldBe(IssuingControlType.VelocityLimit);
            velocity.VelocityLimit.AmountLimit.ShouldBe(5000);
            velocity.VelocityLimit.VelocityWindow.Type.ShouldBe(VelocityWindowType.Weekly);
            velocity.VelocityLimit.MccList.ShouldBe(new List<string> { "4121" });

            var mcc = card.Controls[1].ShouldBeOfType<MccCardControlsResponse>();
            mcc.ControlType.ShouldBe(IssuingControlType.MccLimit);
            mcc.MccLimit.Type.ShouldBe(LimitControlType.Allow);
            mcc.MccLimit.MccList.ShouldBe(new List<string> { "5932", "5411" });

            var mid = card.Controls[2].ShouldBeOfType<MidCardControlsResponse>();
            mid.ControlType.ShouldBe(IssuingControlType.MidLimit);
            mid.Description.ShouldBe("Allow the card to be used only in AZ Pizza");
            mid.MidLimit.ShouldNotBeNull();
            mid.MidLimit.Type.ShouldBe(LimitControlType.Allow);
            mid.MidLimit.MidList.ShouldBe(new List<string> { "593278", "541114" });
#pragma warning disable CS0618
            mid.VelocityLimit.ShouldBeSameAs(mid.MidLimit);
#pragma warning restore CS0618
        }

        [Fact]
        public void ShouldForwardDeprecatedVelocityLimitToMidLimitAndSerializeOnceForMidCardControlsResponse()
        {
            var limit = new MidLimit { Type = LimitControlType.Block, MidList = new List<string> { "593278" } };
#pragma warning disable CS0618
            var control = new MidCardControlsResponse { Id = "ctr_s6tfwc6jpieexgcet2pljss6tu", Description = "mid", VelocityLimit = limit };
            control.MidLimit.ShouldBeSameAs(limit);
#pragma warning restore CS0618

            var json = JObject.Parse(Serializer.Serialize(control));

            json.ContainsKey("mid_limit").ShouldBeTrue();
            json.ContainsKey("velocity_limit").ShouldBeFalse();
            json["control_type"].Value<string>().ShouldBe("mid_limit");
        }
    }
}
