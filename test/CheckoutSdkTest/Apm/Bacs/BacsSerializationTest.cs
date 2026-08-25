using Checkout.Common;
using Shouldly;
using Xunit;

namespace Checkout.Apm.Bacs
{
    /// <summary>
    /// Schema validation tests for Checkout.Apm.Bacs.
    /// Grouped by domain; each section below covers one subject.
    /// </summary>
    public class BacsSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        // ------------------------------------------------------------------------
        // BacsNotificationRequest
        // Schema validation tests for BacsNotificationRequest.
        // Covers all 10 properties against the BacsNotificationRequest swagger schema.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeAllPropertiesToSnakeCaseKeys()
        {
            var request = CreateFullyPopulatedRequest();

            var json = Serializer.Serialize(request);

            json.ShouldContain("\"source_id\":\"src_wmlfc3zyhqzehihu7giusaaawu\"");
            json.ShouldContain("\"notification_type\":\"advance_notice\"");
            json.ShouldContain("\"collection_date\":\"2026-07-15\"");
            json.ShouldContain("\"amount\":4999");
            json.ShouldContain("\"currency\":\"GBP\"");
            json.ShouldContain("\"reference\":\"INV-12345\"");
            json.ShouldContain("\"customer_email\":\"customer@example.com\"");
            json.ShouldContain("\"billing_descriptor\":\"CHECKOUT\"");
            json.ShouldContain("\"support_email\":\"support@test.com\"");
            json.ShouldContain("\"support_phone\":\"+447700900123\"");
        }

        [Fact]
        public void ShouldRoundTripSerializeAllProperties()
        {
            var original = CreateFullyPopulatedRequest();

            var json = Serializer.Serialize(original);
            var deserialized = (BacsNotificationRequest)Serializer
                .Deserialize(json, typeof(BacsNotificationRequest));

            deserialized.SourceId.ShouldBe(original.SourceId);
            deserialized.NotificationType.ShouldBe(original.NotificationType);
            deserialized.CollectionDate.ShouldBe(original.CollectionDate);
            deserialized.Amount.ShouldBe(original.Amount);
            deserialized.Currency.ShouldBe(original.Currency);
            deserialized.Reference.ShouldBe(original.Reference);
            deserialized.CustomerEmail.ShouldBe(original.CustomerEmail);
            deserialized.BillingDescriptor.ShouldBe(original.BillingDescriptor);
            deserialized.SupportEmail.ShouldBe(original.SupportEmail);
            deserialized.SupportPhone.ShouldBe(original.SupportPhone);
        }

        [Fact]
        public void ShouldDeserializeSwaggerExampleForBacsNotificationRequest()
        {
            const string json = @"{
                ""source_id"": ""src_wmlfc3zyhqzehihu7giusaaawu"",
                ""notification_type"": ""advance_notice"",
                ""collection_date"": ""2026-07-15"",
                ""amount"": 4999,
                ""currency"": ""GBP"",
                ""reference"": ""INV-12345"",
                ""customer_email"": ""customer@example.com"",
                ""billing_descriptor"": ""CHECKOUT"",
                ""support_email"": ""support@test.com"",
                ""support_phone"": ""+447700900123""
            }";

            var request = (BacsNotificationRequest)Serializer
                .Deserialize(json, typeof(BacsNotificationRequest));

            request.ShouldNotBeNull();
            request.SourceId.ShouldBe("src_wmlfc3zyhqzehihu7giusaaawu");
            request.NotificationType.ShouldBe(BacsNotificationType.AdvanceNotice);
            request.CollectionDate.ShouldBe("2026-07-15");
            request.Amount.ShouldBe(4999L);
            request.Currency.ShouldBe(Currency.GBP);
            request.Reference.ShouldBe("INV-12345");
            request.CustomerEmail.ShouldBe("customer@example.com");
            request.BillingDescriptor.ShouldBe("CHECKOUT");
            request.SupportEmail.ShouldBe("support@test.com");
            request.SupportPhone.ShouldBe("+447700900123");
        }

        [Fact]
        public void ShouldOmitOptionalPropertiesWhenNotSet()
        {
            var request = new BacsNotificationRequest
            {
                SourceId = "src_wmlfc3zyhqzehihu7giusaaawu",
                NotificationType = BacsNotificationType.AdvanceNotice,
                CollectionDate = "2026-07-15",
                Amount = 4999L,
                Currency = Currency.GBP,
                CustomerEmail = "customer@example.com",
                BillingDescriptor = "CHECKOUT",
                SupportEmail = "support@test.com"
            };

            var json = Serializer.Serialize(request);

            json.ShouldNotContain("reference");
            json.ShouldNotContain("support_phone");
            json.ShouldNotContain("null");
        }

        private static BacsNotificationRequest CreateFullyPopulatedRequest()
        {
            return new BacsNotificationRequest
            {
                SourceId = "src_wmlfc3zyhqzehihu7giusaaawu",
                NotificationType = BacsNotificationType.AdvanceNotice,
                CollectionDate = "2026-07-15",
                Amount = 4999L,
                Currency = Currency.GBP,
                Reference = "INV-12345",
                CustomerEmail = "customer@example.com",
                BillingDescriptor = "CHECKOUT",
                SupportEmail = "support@test.com",
                SupportPhone = "+447700900123"
            };
        }

        // ------------------------------------------------------------------------
        // BacsNotificationResponse
        // Schema validation tests for BacsNotificationResponse.
        // Covers the single property of the BacsNotificationResponse swagger schema.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldDeserializeSwaggerExampleForBacsNotificationResponse()
        {
            const string json = @"{ ""event_id"": ""evt_lzr4csdtddwetactr6phd3kea4"" }";

            var response = (BacsNotificationResponse)Serializer
                .Deserialize(json, typeof(BacsNotificationResponse));

            response.ShouldNotBeNull();
            response.EventId.ShouldBe("evt_lzr4csdtddwetactr6phd3kea4");
        }

        [Fact]
        public void ShouldRoundTripSerialize()
        {
            var original = new BacsNotificationResponse
            {
                EventId = "evt_lzr4csdtddwetactr6phd3kea4"
            };

            var json = Serializer.Serialize(original);
            var deserialized = (BacsNotificationResponse)Serializer
                .Deserialize(json, typeof(BacsNotificationResponse));

            json.ShouldContain("\"event_id\":\"evt_lzr4csdtddwetactr6phd3kea4\"");
            deserialized.EventId.ShouldBe(original.EventId);
        }

        // ------------------------------------------------------------------------
        // BacsNotificationType
        // Schema validation tests for BacsNotificationType, value by value in both directions.
        // ------------------------------------------------------------------------

        [Theory]
        [InlineData(BacsNotificationType.AdvanceNotice, "advance_notice")]
        public void ShouldMapEveryNotificationTypeToItsWireValue(
            BacsNotificationType notificationType,
            string wireValue)
        {
            CheckoutUtils.GetEnumMemberValue(notificationType).ShouldBe(wireValue);
            CheckoutUtils.GetEnumFromStringMemberValue<BacsNotificationType>(wireValue)
                .ShouldBe(notificationType);
        }
    }
}
