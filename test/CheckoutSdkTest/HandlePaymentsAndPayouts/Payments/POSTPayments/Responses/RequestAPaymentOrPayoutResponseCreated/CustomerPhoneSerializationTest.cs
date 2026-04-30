using Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.Customer.Phone;
using Shouldly;
using Xunit;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated
{
    public class CustomerPhoneSerializationTest
    {
        [Fact]
        public void ShouldSerializeWithAllProperties()
        {
            var phone = new Phone { CountryCode = "+1", Number = "415 555 2671" };

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
            var original = new Phone { CountryCode = "+44", Number = "207 946 0000" };
            var serializer = new JsonSerializer();

            var json = serializer.Serialize(original);
            var deserialized = (Phone)serializer.Deserialize(json, typeof(Phone));

            deserialized.CountryCode.ShouldBe("+44");
            deserialized.Number.ShouldBe("207 946 0000");
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
