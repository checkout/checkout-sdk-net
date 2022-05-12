using Checkout.Payments.Four.Response;
using Checkout.Payments.Four.Sender;
using Shouldly;
using Xunit;

namespace Checkout
{
    public class JsonSerializerTest : JsonTestFixture
    {
        [Fact]
        public void ShouldDeserializeFourGetPaymentResponseIndividualSender()
        {
            var fileContent = GetJsonFileContent("Sender/GetPaymentResponseIndividualSender.json");
            GetPaymentResponse getPaymentResponse =
                (GetPaymentResponse)new JsonSerializer().Deserialize(fileContent, typeof(GetPaymentResponse));
            getPaymentResponse.ShouldNotBeNull();
            getPaymentResponse.Sender.ShouldNotBeNull();
            getPaymentResponse.Sender.Type().ShouldBe(PaymentSenderType.Individual);
            getPaymentResponse.Sender.ShouldBeAssignableTo(typeof(PaymentIndividualSender));
            PaymentIndividualSender sender = (PaymentIndividualSender)getPaymentResponse.Sender;
            sender.FirstName.ShouldNotBeEmpty();
            sender.LastName.ShouldNotBeEmpty();
        }

        [Fact]
        public void ShouldDeserializeFourGetPaymentResponseCorporateSender()
        {
            var fileContent = GetJsonFileContent("Sender/GetPaymentResponseCorporateSender.json");
            GetPaymentResponse getPaymentResponse =
                (GetPaymentResponse)new JsonSerializer().Deserialize(fileContent, typeof(GetPaymentResponse));
            getPaymentResponse.ShouldNotBeNull();
            getPaymentResponse.Sender.ShouldNotBeNull();
            getPaymentResponse.Sender.Type().ShouldBe(PaymentSenderType.Corporate);
            getPaymentResponse.Sender.ShouldBeAssignableTo(typeof(PaymentCorporateSender));
            PaymentCorporateSender sender = (PaymentCorporateSender)getPaymentResponse.Sender;
            sender.CompanyName.ShouldNotBeEmpty();
        }

        [Fact]
        public void ShouldDeserializeFourGetPaymentResponseInstrumentSender()
        {
            var fileContent = GetJsonFileContent("Sender/GetPaymentResponseInstrumentSender.json");
            GetPaymentResponse getPaymentResponse =
                (GetPaymentResponse)new JsonSerializer().Deserialize(fileContent, typeof(GetPaymentResponse));
            getPaymentResponse.ShouldNotBeNull();
            getPaymentResponse.Sender.ShouldNotBeNull();
            getPaymentResponse.Sender.Type().ShouldBe(PaymentSenderType.Instrument);
            getPaymentResponse.Sender.ShouldBeAssignableTo(typeof(PaymentInstrumentSender));
        }

        [Fact]
        public void ShouldDeserializeFourGetPaymentResponseAlternativeSender()
        {
            var fileContent = GetJsonFileContent("Sender/GetPaymentResponseAlternativeSender.json");
            GetPaymentResponse getPaymentResponse =
                (GetPaymentResponse)new JsonSerializer().Deserialize(fileContent, typeof(GetPaymentResponse));
            getPaymentResponse.ShouldNotBeNull();
            getPaymentResponse.Sender.ShouldNotBeNull();
            getPaymentResponse.Sender.Type().ShouldBeNull();
            getPaymentResponse.Sender.ShouldBeAssignableTo(typeof(ResponseAlternativeSender));
            ResponseAlternativeSender sender = (ResponseAlternativeSender)getPaymentResponse.Sender;
            sender["type"].ShouldBe("xyz");
        }
    }
}