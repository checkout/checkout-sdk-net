using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Source.Common.HomePhone;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Source.Common.MobilePhone;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Source.Common.WorkPhone;
using Shouldly;
using Xunit;

namespace Checkout.Authentification.Standalone
{
    public class SessionPhoneSerializationTest
    {
        [Fact]
        public void ShouldSerializeHomePhoneWithAllProperties()
        {
            var phone = new HomePhone { CountryCode = "44", Number = "2079460000" };

            Should.NotThrow(() => new JsonSerializer().Serialize(phone));
        }

        [Fact]
        public void ShouldRoundTripHomePhone()
        {
            var original = new HomePhone { CountryCode = "44", Number = "2079460000" };
            var serializer = new JsonSerializer();

            var json = serializer.Serialize(original);
            var deserialized = (HomePhone)serializer.Deserialize(json, typeof(HomePhone));

            deserialized.CountryCode.ShouldBe("44");
            deserialized.Number.ShouldBe("2079460000");
        }

        /// <summary>
        /// Regression test for GitHub issue #549 — CountryCode enum serialised as ISO country code (e.g. "GB"),
        /// causing country_code_invalid when the API expects a numeric ITU-E.164 dialing code (e.g. "44").
        /// </summary>
        [Fact]
        public void Issue549_HomePhone_CountryCodeShouldSerializeAsNumericDialingCode()
        {
            var phone = new HomePhone { CountryCode = "44", Number = "2079460000" };

            var json = new JsonSerializer().Serialize(phone);

            json.ShouldContain("\"country_code\":\"44\"");
            json.ShouldNotContain("\"country_code\":\"GB\"");
        }

        [Fact]
        public void ShouldDeserializeHomePhoneFromSwaggerExample()
        {
            const string json = @"{""country_code"":""234"",""number"":""0204567895""}";

            var phone = (HomePhone)new JsonSerializer().Deserialize(json, typeof(HomePhone));

            phone.CountryCode.ShouldBe("234");
            phone.Number.ShouldBe("0204567895");
        }

        [Fact]
        public void ShouldSerializeMobilePhoneWithAllProperties()
        {
            var phone = new MobilePhone { CountryCode = "1", Number = "4155552671" };

            Should.NotThrow(() => new JsonSerializer().Serialize(phone));
        }

        [Fact]
        public void ShouldRoundTripMobilePhone()
        {
            var original = new MobilePhone { CountryCode = "1", Number = "4155552671" };
            var serializer = new JsonSerializer();

            var json = serializer.Serialize(original);
            var deserialized = (MobilePhone)serializer.Deserialize(json, typeof(MobilePhone));

            deserialized.CountryCode.ShouldBe("1");
            deserialized.Number.ShouldBe("4155552671");
        }

        /// <summary>
        /// Regression test for GitHub issue #549.
        /// </summary>
        [Fact]
        public void Issue549_MobilePhone_CountryCodeShouldSerializeAsNumericDialingCode()
        {
            var phone = new MobilePhone { CountryCode = "1", Number = "4155552671" };

            var json = new JsonSerializer().Serialize(phone);

            json.ShouldContain("\"country_code\":\"1\"");
            json.ShouldNotContain("\"country_code\":\"US\"");
        }

        [Fact]
        public void ShouldSerializeWorkPhoneWithAllProperties()
        {
            var phone = new WorkPhone { CountryCode = "49", Number = "3012345678" };

            Should.NotThrow(() => new JsonSerializer().Serialize(phone));
        }

        [Fact]
        public void ShouldRoundTripWorkPhone()
        {
            var original = new WorkPhone { CountryCode = "49", Number = "3012345678" };
            var serializer = new JsonSerializer();

            var json = serializer.Serialize(original);
            var deserialized = (WorkPhone)serializer.Deserialize(json, typeof(WorkPhone));

            deserialized.CountryCode.ShouldBe("49");
            deserialized.Number.ShouldBe("3012345678");
        }

        /// <summary>
        /// Regression test for GitHub issue #549.
        /// </summary>
        [Fact]
        public void Issue549_WorkPhone_CountryCodeShouldSerializeAsNumericDialingCode()
        {
            var phone = new WorkPhone { CountryCode = "49", Number = "3012345678" };

            var json = new JsonSerializer().Serialize(phone);

            json.ShouldContain("\"country_code\":\"49\"");
            json.ShouldNotContain("\"country_code\":\"DE\"");
        }
    }
}
