using Checkout.Accounts.Entities.Common.Company;
using Checkout.Accounts.Entities.Response;
using Checkout.Financial;
using Checkout.Issuing.Cards.Requests.Create;
using Checkout.Issuing.Common;
using Checkout.Issuing.Common.Responses;
using Checkout.Issuing.Controls.Requests.Create;
using Checkout.Issuing.Controls.Responses.Create;
using Checkout.Issuing.Transactions.Responses;
using Checkout.Payments.Contexts;
using Checkout.Payments.Response;
using Checkout.Payments.Response.Source;
using Checkout.Payments.Response.Source.Contexts;
using Checkout.Payments.Sender;
using Shouldly;
using System;
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
            VelocityCardControlRequest abstractCardControlRequest =
                (VelocityCardControlRequest)new JsonSerializer().Deserialize(fileContent,
                    typeof(VelocityCardControlRequest));
            abstractCardControlRequest.ShouldNotBeNull();
            abstractCardControlRequest.ControlType.ShouldBe(IssuingControlType.VelocityLimit);
        }

        [Fact]
        public void ShouldDeserializeDefaultCardControlTypeResponse()
        {
            var fileContent = GetJsonFileContent("./Resources/CardControlTypeResponse.json");
            VelocityCardControlResponse cardControlResponse =
                (VelocityCardControlResponse)new JsonSerializer().Deserialize(fileContent,
                    typeof(VelocityCardControlResponse));
            cardControlResponse.ShouldNotBeNull();
            cardControlResponse.ControlType.ShouldBe(IssuingControlType.VelocityLimit);
        }

        [Fact]
        public void ShouldDeserializeDefaultCardCTypeRequest()
        {
            var fileContent = GetJsonFileContent("./Resources/CardTypeRequest.json");
            AbstractCardCreateRequest abstractCardCreateRequest =
                (AbstractCardCreateRequest)new JsonSerializer().Deserialize(fileContent,
                    typeof(VirtualCardCreateRequest));
            abstractCardCreateRequest.ShouldNotBeNull();
            abstractCardCreateRequest.Type.ShouldBe(IssuingCardType.Virtual);
        }

        [Fact]
        public void ShouldDeserializeDefaultCardCTypeResponse()
        {
            var fileContent = GetJsonFileContent("./Resources/CardTypeResponse.json");
            AbstractCardResponse cardDetailsResponse =
                (AbstractCardResponse)new JsonSerializer().Deserialize(fileContent,
                    typeof(AbstractCardResponse));
            cardDetailsResponse.ShouldNotBeNull();
            cardDetailsResponse.Type.ShouldBe(IssuingCardType.Physical);
        }
        
        [Fact]
        public void ShouldDeserializeIssuingTransactionsDeclinedReasonResponse()
        {
            var fileContent = GetJsonFileContent("./Resources/IssuingTransactionsDeclinedReason.json");
            Messages messages =
                (Messages)new JsonSerializer().Deserialize(fileContent,
                    typeof(Messages));
            messages.ShouldNotBeNull();
            messages.DeclineReason.ShouldBe("velocity_reached");
        }
        
        [Fact]
        public void ShouldDeserializeKnetResponse()
        {
            var fileContent = GetJsonFileContent("./Resources/KnetResponse.json");
            var paymentResponse =
                (PaymentResponse)new JsonSerializer().Deserialize(fileContent,
                    typeof(PaymentResponse));
            paymentResponse.ShouldNotBeNull();
            paymentResponse.Source.ShouldBeOfType(typeof(KnetResponseSource));
        }

        [Fact]
        public void ShouldDeserializeDateTimeFormatsFromJson()
        {
            var fileContent = GetJsonFileContent("./Resources/get_financial_actions_response.json");
            FinancialActionsQueryResponse financialActionsQueryResponse =
                (FinancialActionsQueryResponse)new JsonSerializer().Deserialize(fileContent,
                    typeof(FinancialActionsQueryResponse));
            financialActionsQueryResponse.ShouldNotBeNull();
            financialActionsQueryResponse.Data[0].ProcessedOn.ShouldNotBeNull();
        }
        
        [Fact]
        public void ShouldDeserializeOnBoardSubEntityCompanyFromJson()
        {
            var fileContent = GetJsonFileContent("./Resources/OnBoardSubEntityCompanyResponse.json");
            OnboardEntityDetailsResponse onboardEntityDetailsResponse =
                (OnboardEntityDetailsResponse)new JsonSerializer().Deserialize(fileContent,
                    typeof(OnboardEntityDetailsResponse));
            onboardEntityDetailsResponse.ShouldNotBeNull();
            onboardEntityDetailsResponse.Company.BusinessType.ShouldBeOfType<BusinessType>();
        }
        
        [Fact]
        public void ShouldDeserializeOnBoardSubEntityGBCompany30FromJson()
        {
            var fileContent = GetJsonFileContent("./Resources/OnBoardSubEntityGBCompany30Response.json");
            OnboardEntityDetailsResponse onboardEntityDetailsResponse =
                (OnboardEntityDetailsResponse)new JsonSerializer().Deserialize(fileContent,
                    typeof(OnboardEntityDetailsResponse));
            onboardEntityDetailsResponse.ShouldNotBeNull();
            onboardEntityDetailsResponse.Company.BusinessType.ShouldBeOfType<BusinessType>();
        }
        
        [Fact]
        public void ShouldDeserializeOnBoardSubEntityEEACompany30FromJson()
        {
            var fileContent = GetJsonFileContent("./Resources/OnBoardSubEntityEEACompany30Response.json");
            OnboardEntityDetailsResponse onboardEntityDetailsResponse =
                (OnboardEntityDetailsResponse)new JsonSerializer().Deserialize(fileContent,
                    typeof(OnboardEntityDetailsResponse));
            onboardEntityDetailsResponse.ShouldNotBeNull();
            onboardEntityDetailsResponse.Company.BusinessType.ShouldBeOfType<BusinessType>();
        }
        
        [Fact]
        public void ShouldDeserializePaymentContextsPayPalDetailsResponseFromJson()
        {
            var fileContent = GetJsonFileContent("./Resources/PaymentContextsPayPalDetailsResponse.json");
            PaymentContextDetailsResponse paymentContextsPayPalResponseSource =
                (PaymentContextDetailsResponse)new JsonSerializer().Deserialize(fileContent,
                    typeof(PaymentContextDetailsResponse));
            paymentContextsPayPalResponseSource.ShouldNotBeNull();
            paymentContextsPayPalResponseSource.PaymentRequest.Source.ShouldBeOfType<PaymentContextsPayPalResponseSource>();
        }

        [Fact]
        public void ShouldDeserializeDateTimeFormatsFromStrings()
        {
            string[] dates =
            {
                "{\"processed_on\":\"2021-06-08T12:25:01.000Z\"}",
                "{\"processed_on\":\"2021-06-08T12:25:01Z\"}",
                "{\"processed_on\":\"2021-06-08T12:25:01\"}",
                "{\"processed_on\":\"2021-06-08T12:25:01.1234567\"}",
                "{\"processed_on\":\"2021-06-08T12:25:01.1234560\"}",
                "{\"processed_on\":\"2021-06-08T12:25:01.1234500\"}",
                "{\"processed_on\":\"2021-06-08T12:25:01.1234000\"}",
                "{\"processed_on\":\"2021-06-08T12:25:01.1230000\"}",
                "{\"processed_on\":\"2021-06-08T12:25:01.1200000\"}",
                "{\"processed_on\":\"2021-06-08T12:25:01.1000000\"}",
                "{\"processed_on\":\"2021-06-08T12:25:01.0000000\"}",
            };
            
            var dateTime = DateTime.Parse("2021-06-08T12:25:01").ToUniversalTime();

            foreach (var date in dates)
            {
                PaymentResponse paymentResponse =
                    (PaymentResponse)new JsonSerializer().Deserialize(date, typeof(PaymentResponse));

                paymentResponse.ShouldNotBeNull();
                paymentResponse.ProcessedOn.ShouldNotBeNull();
                DateTime.Parse(paymentResponse.ProcessedOn.ToString()).ToUniversalTime().ShouldBe(dateTime);
            }
        }

        [Fact]
        public void ShouldDeserializeDateTimeFormatsFromStringsUtc()
        {
            string[] dates =
            {
                "{\"processed_on\":\"2021-06-08T12:25:01+00:00\"}",
                "{\"processed_on\":\"2021-06-08T12:25:01+0000\"}", 
                "{\"processed_on\":\"2021-06-08T12:25:01+00\"}",
            };
            
            var dateTime = DateTime.Parse("2021-06-08T12:25:01Z");

            foreach (var date in dates)
            {
                PaymentResponse paymentResponse =
                    (PaymentResponse)new JsonSerializer().Deserialize(date, typeof(PaymentResponse));

                paymentResponse.ShouldNotBeNull();
                paymentResponse.ProcessedOn.ShouldNotBeNull();
                paymentResponse.ProcessedOn.ShouldBe(dateTime);
            }
        }
    }
}