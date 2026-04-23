using Checkout.Payments;
using Shouldly;
using Xunit;

namespace Checkout.Payments
{
    public class ProcessingAggregatorSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        [Fact]
        public void ShouldRoundTripSerializeAggregator()
        {
            var original = new ProcessingSettings
            {
                Aggregator = new ProcessingAggregator
                {
                    SubMerchantId = "9cf70789ba90123",
                    AggregatorIdVisa = "10012345",
                    AggregatorIdMc = "00000123456"
                },
                ReconciliationId = "4123495123",
                ForeignRetailerAmount = 200,
                ServiceType = AchServiceType.Standard
            };

            var json = Serializer.Serialize(original);
            var deserialized = (ProcessingSettings)Serializer.Deserialize(json, typeof(ProcessingSettings));

            deserialized.Aggregator.ShouldNotBeNull();
            deserialized.Aggregator.SubMerchantId.ShouldBe("9cf70789ba90123");
            deserialized.Aggregator.AggregatorIdVisa.ShouldBe("10012345");
            deserialized.Aggregator.AggregatorIdMc.ShouldBe("00000123456");
            deserialized.ReconciliationId.ShouldBe("4123495123");
            deserialized.ForeignRetailerAmount.ShouldBe(200L);
            deserialized.ServiceType.ShouldBe(AchServiceType.Standard);
        }

        [Fact]
        public void ShouldSerializeSnakeCaseKeys()
        {
            var settings = new ProcessingSettings
            {
                Aggregator = new ProcessingAggregator
                {
                    SubMerchantId = "sub123",
                    AggregatorIdVisa = "visa123",
                    AggregatorIdMc = "mc123"
                },
                ReconciliationId = "rec_001",
                ServiceType = AchServiceType.SameDay
            };

            var json = Serializer.Serialize(settings);

            json.ShouldContain("\"aggregator\"");
            json.ShouldContain("\"sub_merchant_id\"");
            json.ShouldContain("\"aggregator_id_visa\"");
            json.ShouldContain("\"aggregator_id_mc\"");
            json.ShouldContain("\"reconciliation_id\"");
            json.ShouldContain("\"service_type\"");
            json.ShouldContain("same_day");
        }

        [Fact]
        public void ShouldDeserializeSwaggerExample()
        {
            const string json = @"{
                ""aggregator"": {
                    ""sub_merchant_id"": ""9cf70789ba90123"",
                    ""aggregator_id_visa"": ""10012345"",
                    ""aggregator_id_mc"": ""00000123456""
                },
                ""reconciliation_id"": ""4123495123"",
                ""foreign_retailer_amount"": 200,
                ""service_type"": ""standard""
            }";

            var result = (ProcessingSettings)Serializer.Deserialize(json, typeof(ProcessingSettings));

            result.Aggregator.SubMerchantId.ShouldBe("9cf70789ba90123");
            result.Aggregator.AggregatorIdVisa.ShouldBe("10012345");
            result.Aggregator.AggregatorIdMc.ShouldBe("00000123456");
            result.ReconciliationId.ShouldBe("4123495123");
            result.ForeignRetailerAmount.ShouldBe(200L);
            result.ServiceType.ShouldBe(AchServiceType.Standard);
        }
    }
}
