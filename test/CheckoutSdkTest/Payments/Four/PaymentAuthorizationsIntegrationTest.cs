using Checkout.Common;
using Checkout.Payments.Four.Request;
using Checkout.Payments.Four.Request.Source;
using Checkout.Payments.Four.Response;
using Checkout.Payments.Four.Sender;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Payments.Four
{
    public class PaymentAuthorizationsIntegrationTest : AbstractPaymentsIntegrationTest
    {
        [Fact]
        private async Task ShouldIncrementPaymentAuthorization()
        {
            PaymentResponse paymentResponse = await MakeAuthorizationEstimatedPayment();

            AuthorizationRequest authorizationRequest = new AuthorizationRequest
            {
                Amount = 10, Reference = "payment_reference"
            };

            AuthorizationResponse authorizationResponse = await FourApi.PaymentsClient()
                .IncrementPaymentAuthorization(paymentResponse.Id, authorizationRequest);

            authorizationResponse.ShouldNotBeNull();
            authorizationResponse.Amount.ShouldNotBeNull();
            authorizationResponse.ActionId.ShouldNotBeNull();
            authorizationResponse.Currency.ShouldNotBeNull();
            authorizationResponse.Approved.ShouldNotBeNull();
            authorizationResponse.ResponseCode.ShouldNotBeNull();
            authorizationResponse.ResponseSummary.ShouldNotBeNull();
            authorizationResponse.ExpiresOn.ShouldNotBeNull();
            authorizationResponse.ProcessedOn.ShouldNotBeNull();
            authorizationResponse.Balances.ShouldNotBeNull();
            authorizationResponse.Links.ShouldNotBeNull();
            authorizationResponse.Risk.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldIncrementPaymentAuthorization_Idempotently()
        {
            PaymentResponse paymentResponse = await MakeAuthorizationEstimatedPayment();

            AuthorizationRequest authorizationRequest = new AuthorizationRequest
            {
                Amount = 10, Reference = "payment_reference"
            };

            AuthorizationResponse authorizationResponse1 = await FourApi.PaymentsClient()
                .IncrementPaymentAuthorization(paymentResponse.Id, authorizationRequest, "idempotency_key");
            authorizationResponse1.ShouldNotBeNull();

            AuthorizationResponse authorizationResponse2 = await FourApi.PaymentsClient()
                .IncrementPaymentAuthorization(paymentResponse.Id, authorizationRequest, "idempotency_key");
            authorizationResponse2.ShouldNotBeNull();

            authorizationResponse1.ActionId.ShouldBe(authorizationResponse2.ActionId);
        }

        private async Task<PaymentResponse> MakeAuthorizationEstimatedPayment()
        {
            var requestCardSource = new RequestCardSource
            {
                Name = TestCardSource.Visa.Name,
                Number = "4556447238607884",
                ExpiryYear = TestCardSource.Visa.ExpiryYear,
                ExpiryMonth = TestCardSource.Visa.ExpiryMonth,
                Cvv = TestCardSource.Visa.Cvv
            };

            var address = new Address
            {
                AddressLine1 = "CheckoutSdk.com",
                AddressLine2 = "90 Tottenham Court Road",
                City = "London",
                State = "London",
                Zip = "W1T 4TJ",
                Country = CountryCode.GB
            };

            var paymentIndividualSender = new PaymentIndividualSender
            {
                FirstName = "Mr", LastName = "Test", Address = address
            };

            var paymentRequest = new PaymentRequest
            {
                Source = requestCardSource,
                Capture = false,
                Reference = Guid.NewGuid().ToString(),
                Amount = 10L,
                Currency = Currency.USD,
                Sender = paymentIndividualSender,
                AuthorizationType = AuthorizationType.Estimated
            };

            var paymentResponse = await FourApi.PaymentsClient().RequestPayment(paymentRequest);
            paymentResponse.ShouldNotBeNull();
            return paymentResponse;
        }
    }
}