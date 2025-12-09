using Checkout.Common;
using Checkout.HandlePaymentsAndPayouts.Common;
using Checkout.HandlePaymentsAndPayouts.Flow.Entities;
using Checkout.HandlePaymentsAndPayouts.Flow.Requests;
using Checkout.HandlePaymentsAndPayouts.Flow.Responses;
using Customer = Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses.RequestAPaymentOrPayoutResponseCreated.Customer;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.HandlePaymentsAndPayouts.Flow
{
    public class FlowIntegrationTest : SandboxTestFixture
    {
        public FlowIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
        }

        [Fact(Skip = "This test requires a valid merchant configuration for Flow")]
        public async Task ShouldCreatePaymentSession()
        {
            // Arrange
            var request = CreatePaymentSessionCreateRequest();

            // Act
            var response = await DefaultApi.FlowClient().CreatePaymentSession(request);

            // Assert
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNull();
            response.PaymentSessionToken.ShouldNotBeNull();
            response.PaymentSessionSecret.ShouldNotBeNull();
        }

        [Fact(Skip = "This test requires a valid merchant configuration for Flow and a valid payment session")]
        public async Task ShouldSubmitPaymentSession()
        {
            // Arrange
            var createRequest = CreatePaymentSessionCreateRequest();
            var createResponse = await DefaultApi.FlowClient().CreatePaymentSession(createRequest);
            
            var submitRequest = CreatePaymentSessionSubmitRequest();

            // Act
            var response = await DefaultApi.FlowClient().SubmitPaymentSession(createResponse.Id, submitRequest);

            // Assert
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNull();
            response.Status.ShouldNotBeNull();
        }

        [Fact(Skip = "This test requires a valid merchant configuration for Flow")]
        public async Task ShouldCompletePaymentSession()
        {
            // Arrange
            var request = CreatePaymentSessionCompleteRequest();

            // Act
            var response = await DefaultApi.FlowClient().CompletePaymentSession(request);

            // Assert
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNull();
            response.Status.ShouldNotBeNull();
        }

        private PaymentSessionCreateRequest CreatePaymentSessionCreateRequest()
        {
            return new PaymentSessionCreateRequest
            {
                Amount = 1000,
                Currency = Currency.GBP,
                Reference = "ORD-5023-4E89",
                Customer = new Customer.Customer
                {
                    Email = "johndoe@email.com",
                    Name = "John Doe"
                },
                SuccessUrl = "https://example.com/payments/success",
                FailureUrl = "https://example.com/payments/fail",
                PaymentMethodConfiguration = new PaymentMethodConfiguration
                {
                    Card = new CardConfiguration
                    {
                        StorePaymentDetails = StorePaymentDetailsType.Enabled
                    }
                },
                EnabledPaymentMethods = new List<PaymentMethod>
                {
                    PaymentMethod.Card
                }
            };
        }

        private PaymentSessionSubmitRequest CreatePaymentSessionSubmitRequest()
        {
            return new PaymentSessionSubmitRequest
            {
                SessionData = "encrypted_session_data",
                IpAddress = "192.168.1.1"
            };
        }

        private PaymentSessionCompleteRequest CreatePaymentSessionCompleteRequest()
        {
            return new PaymentSessionCompleteRequest
            {
                Amount = 1000,
                Currency = Currency.USD,
                Reference = "ORD-5023-4E89",
                Customer = new Customer.Customer
                {
                    Email = "johndoe@email.com",
                    Name = "John Doe"
                },
                SuccessUrl = "https://example.com/payments/success",
                FailureUrl = "https://example.com/payments/fail",
                SessionData = "encrypted_session_data"
            };
        }
    }
}