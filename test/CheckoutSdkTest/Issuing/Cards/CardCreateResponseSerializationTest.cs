using Checkout.Common;
using Checkout.Issuing.Cards.Responses.Create;
using Checkout.Issuing.Common;
using Newtonsoft.Json.Linq;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace Checkout.Issuing.Cards
{
    /// <summary>
    /// Schema validation tests for the card create response (add-card-response) in Checkout.Issuing.Cards.
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

        // ------------------------------------------------------------------------
        // VirtualCardCreateResponse / PhysicalCardCreateResponse
        // Covers every property of add-virtual-card-response and
        // add-physical-card-response, including _links and scheme.
        // ------------------------------------------------------------------------

        private const string SwaggerBody = @"{
            ""id"": ""crd_fa6psq242dcd6fdn5gifcq1491"",
            ""client_id"": ""cli_vkuhvk4vjn2edkps7dfsq6emqm"",
            ""entity_id"": ""ent_fa6psq242dcd6fdn5gifcq1491"",
            ""last_four"": 1234,
            ""expiry_month"": 5,
            ""expiry_year"": 2025,
            ""billing_currency"": ""USD"",
            ""issuing_country"": ""US"",
            ""status"": ""active"",
            ""type"": ""TYPE"",
            ""scheme"": ""mastercard"",
            ""created_date"": ""2019-09-10T10:11:12.0000000+00:00""
        }";

        [Theory]
        [InlineData("virtual", typeof(VirtualCardCreateResponse))]
        [InlineData("physical", typeof(PhysicalCardCreateResponse))]
        public void ShouldDeserializeSwaggerExampleForCardCreateResponse(string type, Type expected)
        {
            var response = (AbstractCardCreateResponse)Serializer.Deserialize(
                SwaggerBody.Replace("TYPE", type), typeof(AbstractCardCreateResponse));

            response.ShouldBeOfType(expected);
            response.Id.ShouldBe("crd_fa6psq242dcd6fdn5gifcq1491");
            response.ClientId.ShouldBe("cli_vkuhvk4vjn2edkps7dfsq6emqm");
            response.EntityId.ShouldBe("ent_fa6psq242dcd6fdn5gifcq1491");
            response.LastFour.ShouldBe("1234");
            response.ExpiryMonth.ShouldBe(5);
            response.ExpiryYear.ShouldBe(2025);
            response.BillingCurrency.ShouldBe(Currency.USD);
            response.IssuingCountry.ShouldBe(CountryCode.US);
            response.Status.ShouldBe(CardStatus.Active);
            response.Scheme.ShouldBe(IssuingScheme.Mastercard);
            response.CreatedDate.ShouldNotBeNull();
        }

        [Fact]
        public void ShouldRoundTripAllPropertiesForVirtualCardCreateResponse()
        {
            var original = new VirtualCardCreateResponse
            {
                Credentials = new Credentials { Number = "4242424242424242", Cvc2 = "604" },
                Controls = new List<AbstractCardControlsResponse>
                {
                    new MidCardControlsResponse
                    {
                        Id = "ctr_s6tfwc6jpieexgcet2pljss6tu", Description = "mid",
                        MidLimit = new MidLimit { Type = LimitControlType.Allow, MidList = new List<string> { "593278" } }
                    }
                }
            };
            PopulateCard(original);

            var result = (AbstractCardCreateResponse)Serializer.Deserialize(
                Serializer.Serialize(original), typeof(AbstractCardCreateResponse));

            var card = result.ShouldBeOfType<VirtualCardCreateResponse>();
            AssertCard(card);
            card.Credentials.Number.ShouldBe("4242424242424242");
            card.Credentials.Cvc2.ShouldBe("604");
            card.Controls.Count.ShouldBe(1);
            card.Controls[0].ShouldBeOfType<MidCardControlsResponse>().MidLimit.MidList.ShouldBe(new List<string> { "593278" });
        }

        [Fact]
        public void ShouldRoundTripAllPropertiesForPhysicalCardCreateResponse()
        {
            var original = new PhysicalCardCreateResponse();
            PopulateCard(original);

            var result = (AbstractCardCreateResponse)Serializer.Deserialize(
                Serializer.Serialize(original), typeof(AbstractCardCreateResponse));

            AssertCard(result.ShouldBeOfType<PhysicalCardCreateResponse>());
        }

        [Fact]
        public void ShouldLeaveDeprecatedFieldsNullForSpecBodyOfCardControlsResponse()
        {
            const string json = @"{ ""id"": ""ctr_s6tfwc6jpieexgcet2pljss6tu"", ""description"": ""mcc"", ""control_type"": ""mcc_limit"",
                ""mcc_limit"": { ""type"": ""block"", ""mcc_list"": [""5411""] } }";

            var control = (AbstractCardControlsResponse)Serializer.Deserialize(json, typeof(AbstractCardControlsResponse));

            control.ShouldBeOfType<MccCardControlsResponse>();
#pragma warning disable CS0618
            control.TargetId.ShouldBeNull();
            control.CreatedDate.ShouldBeNull();
            control.LastModifiedDate.ShouldBeNull();
#pragma warning restore CS0618
        }

        [Fact]
        public void ShouldStillDeserializeDeprecatedFieldsWhenPresentOnCardControlsResponse()
        {
            const string json = @"{ ""id"": ""ctr_s6tfwc6jpieexgcet2pljss6tu"", ""control_type"": ""mcc_limit"",
                ""target_id"": ""crd_fa6psq42dcdd6fdn5gifcq1491"", ""created_date"": ""a"", ""last_modified_date"": ""b"" }";

            var control = (AbstractCardControlsResponse)Serializer.Deserialize(json, typeof(AbstractCardControlsResponse));

#pragma warning disable CS0618
            control.TargetId.ShouldBe("crd_fa6psq42dcdd6fdn5gifcq1491");
            control.CreatedDate.ShouldBe("a");
            control.LastModifiedDate.ShouldBe("b");
#pragma warning restore CS0618
        }

        private static void PopulateCard(AbstractCardCreateResponse card)
        {
            card.Id = "crd_fa6psq242dcd6fdn5gifcq1491";
            card.ClientId = "cli_vkuhvk4vjn2edkps7dfsq6emqm";
            card.EntityId = "ent_fa6psq242dcd6fdn5gifcq1491";
            card.DisplayName = "JOHN KENNEDY";
            card.LastFour = "1234";
            card.ExpiryMonth = 5;
            card.ExpiryYear = 2025;
            card.BillingCurrency = Currency.USD;
            card.IssuingCountry = CountryCode.US;
            card.Scheme = IssuingScheme.Visa;
            card.CreatedDate = new DateTime(2019, 9, 10, 10, 11, 12, DateTimeKind.Utc);
            card.Status = CardStatus.Suspended;
            card.Reference = "X-123456-N11";
            card.ScheduledRevocationDate = "2027-03-12";
            card.LastActivatedOn = new DateTime(2019, 9, 11, 10, 11, 12, DateTimeKind.Utc);
            card.Links = new Dictionary<string, Link> { { "self", new Link { Href = "https://api.checkout.com/issuing/cards/crd_fa6psq242dcd6fdn5gifcq1491" } } };
        }

        private static void AssertCard(AbstractCardCreateResponse card)
        {
            card.Id.ShouldBe("crd_fa6psq242dcd6fdn5gifcq1491");
            card.ClientId.ShouldBe("cli_vkuhvk4vjn2edkps7dfsq6emqm");
            card.EntityId.ShouldBe("ent_fa6psq242dcd6fdn5gifcq1491");
            card.DisplayName.ShouldBe("JOHN KENNEDY");
            card.LastFour.ShouldBe("1234");
            card.ExpiryMonth.ShouldBe(5);
            card.ExpiryYear.ShouldBe(2025);
            card.BillingCurrency.ShouldBe(Currency.USD);
            card.IssuingCountry.ShouldBe(CountryCode.US);
            card.Scheme.ShouldBe(IssuingScheme.Visa);
            card.CreatedDate.ShouldBe(new DateTime(2019, 9, 10, 10, 11, 12, DateTimeKind.Utc));
            card.Status.ShouldBe(CardStatus.Suspended);
            card.Reference.ShouldBe("X-123456-N11");
            card.ScheduledRevocationDate.ShouldBe("2027-03-12");
            card.LastActivatedOn.ShouldBe(new DateTime(2019, 9, 11, 10, 11, 12, DateTimeKind.Utc));
            card.GetSelfLink().Href.ShouldBe("https://api.checkout.com/issuing/cards/crd_fa6psq242dcd6fdn5gifcq1491");
        }
    }
}
