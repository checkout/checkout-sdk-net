using Checkout.Issuing.Cards;
using Checkout.Issuing.Cards.Requests.Create;
using Checkout.Issuing.Cards.Responses;
using Checkout.Issuing.Controls;
using Checkout.Issuing.Controls.Requests;
using Checkout.Issuing.Controls.Requests.Create;
using Checkout.Issuing.Controls.Responses.Create;
using Checkout.Payments.Response;
using Checkout.Payments.Sender;
using Shouldly;
using Xunit;

namespace Checkout
{
    public class JsonSerializerTest : JsonTestFixture
    {
        [Fact]
        public void ShouldDeserializeDefaultGetPaymentResponseIndividualSender()
        {
            var fileContent = GetJsonFileContent("./Resources/Sender/GetPaymentResponseIndividualSender.json");
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
        public void ShouldDeserializeDefaultGetPaymentResponseCorporateSender()
        {
            var fileContent = GetJsonFileContent("./Resources/Sender/GetPaymentResponseCorporateSender.json");
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
        public void ShouldDeserializeDefaultGetPaymentResponseInstrumentSender()
        {
            var fileContent = GetJsonFileContent("./Resources/Sender/GetPaymentResponseInstrumentSender.json");
            GetPaymentResponse getPaymentResponse =
                (GetPaymentResponse)new JsonSerializer().Deserialize(fileContent, typeof(GetPaymentResponse));
            getPaymentResponse.ShouldNotBeNull();
            getPaymentResponse.Sender.ShouldNotBeNull();
            getPaymentResponse.Sender.Type().ShouldBe(PaymentSenderType.Instrument);
            getPaymentResponse.Sender.ShouldBeAssignableTo(typeof(PaymentInstrumentSender));
        }

        [Fact]
        public void ShouldDeserializeDefaultGetPaymentResponseAlternativeSender()
        {
            var fileContent = GetJsonFileContent("./Resources/Sender/GetPaymentResponseAlternativeSender.json");
            GetPaymentResponse getPaymentResponse =
                (GetPaymentResponse)new JsonSerializer().Deserialize(fileContent, typeof(GetPaymentResponse));
            getPaymentResponse.ShouldNotBeNull();
            getPaymentResponse.Sender.ShouldNotBeNull();
            getPaymentResponse.Sender.Type().ShouldBeNull();
            getPaymentResponse.Sender.ShouldBeAssignableTo(typeof(ResponseAlternativeSender));
            ResponseAlternativeSender sender = (ResponseAlternativeSender)getPaymentResponse.Sender;
            sender["type"].ShouldBe("xyz");
        }
        
        [Fact]
        public void ShouldDeserializeDefaultCardControlTypeRequest()
        {
            var fileContent = GetJsonFileContent("./Resources/CardControlTypeRequest.json");
            CardControlRequest cardControlRequest =
                (CardControlRequest)new JsonSerializer().Deserialize(fileContent,
                    typeof(CardControlRequest));
            cardControlRequest.ShouldNotBeNull();
            cardControlRequest.ControlType.ShouldBe(ControlType.VelocityLimit);
        }

        [Fact]
        public void ShouldDeserializeDefaultCardControlTypeResponse()
        {
            var fileContent = GetJsonFileContent("./Resources/CardControlTypeResponse.json");
            VelocityCardControlResponse cardControlResponse =
                (VelocityCardControlResponse)new JsonSerializer().Deserialize(fileContent,
                    typeof(VelocityCardControlResponse));
            cardControlResponse.ShouldNotBeNull();
            cardControlResponse.ControlType.ShouldBe(ControlType.VelocityLimit);
        }
        
        [Fact]
        public void ShouldDeserializeDefaultCardCTypeRequest()
        {
            var fileContent = GetJsonFileContent("./Resources/CardTypeRequest.json");
            CardRequest cardRequest =
                (CardRequest)new JsonSerializer().Deserialize(fileContent,
                    typeof(CardRequest));
            cardRequest.ShouldNotBeNull();
            cardRequest.Type.ShouldBe(CardType.Virtual);
        }
        
        [Fact]
        public void ShouldDeserializeDefaultCardCTypeResponse()
        {
            var fileContent = GetJsonFileContent("./Resources/CardTypeResponse.json");
            CardDetailsResponse cardDetailsResponse =
                (CardDetailsResponse)new JsonSerializer().Deserialize(fileContent,
                    typeof(CardDetailsResponse));
            cardDetailsResponse.ShouldNotBeNull();
            cardDetailsResponse.Type.ShouldBe(CardType.Physical);
        }
    }
}