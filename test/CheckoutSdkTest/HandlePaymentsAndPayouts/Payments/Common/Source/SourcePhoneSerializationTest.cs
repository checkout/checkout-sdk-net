using CardSourcePhone = Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.CardSource.Phone.Phone;
using KlarnaAccountHolderPhone =
    Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.PaymentGetResponseKlarnaSourceSource.AccountHolder.Phone.Phone;
using Shouldly;
using Xunit;

namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source
{
    /// <summary>
    /// Schema validation tests for Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    /// Grouped by domain; each section below covers one subject.
    /// </summary>
    public class SourcePhoneSerializationTest
    {
        // ------------------------------------------------------------------------
        // CardSourcePhone
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeWithAllPropertiesForCardSourcePhone()
        {
            var phone = new CardSourcePhone { CountryCode = "+1", Number = "415 555 2671" };

            Should.NotThrow(() => new JsonSerializer().Serialize(phone));
        }

        [Fact]
        public void ShouldSerializeWithRequiredPropertiesOnly()
        {
            var phone = new CardSourcePhone { Number = "415 555 2671" };

            Should.NotThrow(() => new JsonSerializer().Serialize(phone));
        }

        [Fact]
        public void ShouldRoundTripSerializeForCardSourcePhone()
        {
            var original = new CardSourcePhone { CountryCode = "+44", Number = "207 946 0000" };
            var serializer = new JsonSerializer();

            var json = serializer.Serialize(original);
            var deserialized = (CardSourcePhone)serializer.Deserialize(json, typeof(CardSourcePhone));

            deserialized.CountryCode.ShouldBe("+44");
            deserialized.Number.ShouldBe("207 946 0000");
        }

        [Fact]
        public void ShouldDeserializeFromSwaggerExampleForCardSourcePhone()
        {
            const string json = @"{""country_code"":""+1"",""number"":""415 555 2671""}";

            var phone = (CardSourcePhone)new JsonSerializer().Deserialize(json, typeof(CardSourcePhone));

            phone.CountryCode.ShouldBe("+1");
            phone.Number.ShouldBe("415 555 2671");
        }

        // ------------------------------------------------------------------------
        // KlarnaSourceAccountHolderPhone
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeWithAllPropertiesForKlarnaSourceAccountHolderPhone()
        {
            var phone = new KlarnaAccountHolderPhone { CountryCode = "+46", Number = "701234567" };

            Should.NotThrow(() => new JsonSerializer().Serialize(phone));
        }

        [Fact]
        public void ShouldSerializeWithNoOptionalProperties()
        {
            var phone = new KlarnaAccountHolderPhone();

            Should.NotThrow(() => new JsonSerializer().Serialize(phone));
        }

        [Fact]
        public void ShouldRoundTripSerializeForKlarnaSourceAccountHolderPhone()
        {
            var original = new KlarnaAccountHolderPhone { CountryCode = "+46", Number = "701234567" };
            var serializer = new JsonSerializer();

            var json = serializer.Serialize(original);
            var deserialized = (KlarnaAccountHolderPhone)serializer.Deserialize(json, typeof(KlarnaAccountHolderPhone));

            deserialized.CountryCode.ShouldBe("+46");
            deserialized.Number.ShouldBe("701234567");
        }

        [Fact]
        public void ShouldDeserializeFromSwaggerExampleForKlarnaSourceAccountHolderPhone()
        {
            const string json = @"{""country_code"":""+1"",""number"":""415 555 2671""}";

            var phone = (KlarnaAccountHolderPhone)new JsonSerializer().Deserialize(json, typeof(KlarnaAccountHolderPhone));

            phone.CountryCode.ShouldBe("+1");
            phone.Number.ShouldBe("415 555 2671");
        }
    }
}
