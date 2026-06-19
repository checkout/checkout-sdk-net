using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.ChannelData.BrowserChannelData;
using Shouldly;
using Xunit;

namespace Checkout.Authentification.Standalone
{
    public class BrowserChannelDataSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        [Fact]
        public void ShouldSerializeExplicitFalseFlags()
        {
            var data = new BrowserChannelData
            {
                JavaEnabled = false,
                JavascriptEnabled = false
            };

            var json = Serializer.Serialize(data);

            json.ShouldContain("\"java_enabled\":false");
            json.ShouldContain("\"javascript_enabled\":false");
        }

        [Fact]
        public void ShouldSerializeExplicitTrueFlags()
        {
            var data = new BrowserChannelData
            {
                JavaEnabled = true,
                JavascriptEnabled = true
            };

            var json = Serializer.Serialize(data);

            json.ShouldContain("\"java_enabled\":true");
            json.ShouldContain("\"javascript_enabled\":true");
        }

        [Fact]
        public void ShouldOmitUnsetFlags()
        {
            var data = new BrowserChannelData();

            var json = Serializer.Serialize(data);

            json.ShouldNotContain("java_enabled");
            json.ShouldNotContain("javascript_enabled");
        }

        [Fact]
        public void ShouldRoundTripExplicitFalse()
        {
            var original = new BrowserChannelData { JavaEnabled = false, JavascriptEnabled = false };

            var json = Serializer.Serialize(original);
            var deserialized = (BrowserChannelData)Serializer.Deserialize(json, typeof(BrowserChannelData));

            deserialized.JavaEnabled.ShouldBe(false);
            deserialized.JavascriptEnabled.ShouldBe(false);
        }
    }
}
