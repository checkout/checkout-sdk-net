using System;
using System.Collections.Generic;
using Checkout.Payments;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace Checkout.Tests
{
    public class JsonSerializerTests
    {
        [Fact]
        public void ConstructorAcceptsNullJsonSerializerSettings()
        {
            var sut = new JsonSerializer(null);
            sut.ShouldNotBeNull();
        }

        [Fact]
        public void SerializeGuardsInputAgainstNull()
        {
            var sut = new JsonSerializer();
            Should.Throw<ArgumentNullException>(() => sut.Serialize(null));
        }

        [Fact]
        public void SerializesProcessingObjectOnPaymentRequest()
        {
            var tpaSettings = new
            {
                Setting1 = 1,
                Setting2 = "abc",
                SnakeCaseSetting = "hiss"
            };
            var input = new PaymentRequest<CardSource>(
                new CardSource("123", 12, 99), "USD", 1)
            {
                Processing = new Dictionary<string, object> {{"tpa", tpaSettings}}
            };

            var sut = new JsonSerializer();

            var result = sut.Serialize(input);
            dynamic output = JsonConvert.DeserializeObject(result);

            Assert.Equal(tpaSettings.Setting1, output.processing.tpa.setting1.Value);
            Assert.Equal(tpaSettings.Setting2, output.processing.tpa.setting2.Value);
            Assert.Equal(tpaSettings.SnakeCaseSetting, output.processing.tpa.snake_case_setting.Value);
        }
    }
}
