using Checkout.Common;
using Checkout.Payments;
using Shouldly;
using System;
using Xunit;

namespace Checkout.Instruments.Create
{
    /// <summary>
    /// Schema validation tests for InstrumentData (SEPA instrument_data).
    /// The type field carries the SEPA mandate type and must serialize with the
    /// exact casing the API expects (Core, B2B), and stay out of the body when unset.
    /// </summary>
    public class InstrumentDataSerializationTest
    {
        private readonly JsonSerializer _serializer = new JsonSerializer();

        [Fact]
        private void ShouldSerializeWithAllFields()
        {
            var data = new InstrumentData
            {
                Type = SepaMandateType.B2B,
                AccountNumber = "FR2810096000509685512959O86",
                Country = CountryCode.FR,
                Currency = Currency.EUR,
                PaymentType = PaymentType.Recurring,
                MandateId = "1234567890",
                DateOfSignature = new DateTime(2021, 1, 1)
            };

            var json = _serializer.Serialize(data);

            json.ShouldContain("\"type\":\"B2B\"");
            json.ShouldContain("\"account_number\":\"FR2810096000509685512959O86\"");
            json.ShouldContain("\"mandate_id\":\"1234567890\"");
        }

        [Fact]
        private void ShouldSerializeCoreMandateType()
        {
            var data = new InstrumentData { Type = SepaMandateType.Core };

            var json = _serializer.Serialize(data);

            json.ShouldContain("\"type\":\"Core\"");
        }

        [Fact]
        private void ShouldOmitTypeWhenNotSet()
        {
            var data = new InstrumentData { AccountNumber = "FR2810096000509685512959O86" };

            var json = _serializer.Serialize(data);

            json.ShouldNotContain("\"type\"");
        }

        [Fact]
        private void ShouldDeserializeMandateType()
        {
            const string json = "{\"type\":\"B2B\",\"account_number\":\"FR2810096000509685512959O86\"}";

            var data = (InstrumentData)_serializer.Deserialize(json, typeof(InstrumentData));

            data.Type.ShouldBe(SepaMandateType.B2B);
            data.AccountNumber.ShouldBe("FR2810096000509685512959O86");
        }
    }
}
