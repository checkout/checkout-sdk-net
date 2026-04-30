using Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.PaymentGetResponseKlarnaSourceSource.AccountHolder.Phone;
using Shouldly;
using Xunit;

namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source
{
    public class KlarnaSourceAccountHolderPhoneSerializationTest
    {
        [Fact]
        public void ShouldSerializeWithAllProperties()
        {
            var phone = new Phone { CountryCode = "+46", Number = "701234567" };

            Should.NotThrow(() => new JsonSerializer().Serialize(phone));
        }

        [Fact]
        public void ShouldSerializeWithNoOptionalProperties()
        {
            var phone = new Phone();

            Should.NotThrow(() => new JsonSerializer().Serialize(phone));
        }

        [Fact]
        public void ShouldRoundTripSerialize()
        {
            var original = new Phone { CountryCode = "+46", Number = "701234567" };
            var serializer = new JsonSerializer();

            var json = serializer.Serialize(original);
            var deserialized = (Phone)serializer.Deserialize(json, typeof(Phone));

            deserialized.CountryCode.ShouldBe("+46");
            deserialized.Number.ShouldBe("701234567");
        }

        [Fact]
        public void ShouldDeserializeFromSwaggerExample()
        {
            const string json = @"{""country_code"":""+1"",""number"":""415 555 2671""}";

            var phone = (Phone)new JsonSerializer().Deserialize(json, typeof(Phone));

            phone.CountryCode.ShouldBe("+1");
            phone.Number.ShouldBe("415 555 2671");
        }
    }
}
