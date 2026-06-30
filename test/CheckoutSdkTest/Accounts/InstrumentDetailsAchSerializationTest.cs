using Shouldly;
using Xunit;

namespace Checkout.Accounts
{
    public class InstrumentDetailsAchSerializationTest
    {
        private readonly JsonSerializer _serializer = new JsonSerializer();

        [Fact]
        public void ShouldSerializeWithAllProperties()
        {
            var details = new InstrumentDetailsAch
            {
                AccountNumber = "12345100",
                RoutingNumber = "026009593",
                AccountType = InstrumentAccountType.Savings
            };

            var json = _serializer.Serialize(details);

            json.ShouldContain("\"account_number\"");
            json.ShouldContain("12345100");
            json.ShouldContain("\"routing_number\"");
            json.ShouldContain("026009593");
            json.ShouldContain("\"account_type\"");
            json.ShouldContain("savings");
        }

        [Fact]
        public void ShouldDeserializeSwaggerExample()
        {
            const string json = @"{ ""account_number"": ""12345100"", ""routing_number"": ""026009593"", ""account_type"": ""checking"" }";

            var details = (InstrumentDetailsAch)_serializer.Deserialize(json, typeof(InstrumentDetailsAch));

            details.ShouldNotBeNull();
            details.AccountNumber.ShouldBe("12345100");
            details.RoutingNumber.ShouldBe("026009593");
            details.AccountType.ShouldBe(InstrumentAccountType.Checking);
        }

        [Fact]
        public void ShouldRoundTripSerialize()
        {
            var original = new InstrumentDetailsAch
            {
                AccountNumber = "98765432", RoutingNumber = "123456789", AccountType = InstrumentAccountType.Savings
            };

            var deserialized = (InstrumentDetailsAch)_serializer
                .Deserialize(_serializer.Serialize(original), typeof(InstrumentDetailsAch));

            deserialized.AccountNumber.ShouldBe(original.AccountNumber);
            deserialized.RoutingNumber.ShouldBe(original.RoutingNumber);
            deserialized.AccountType.ShouldBe(original.AccountType);
        }
    }
}
