using Checkout.Common;
using Checkout.Payments.Four.Request;
using Checkout.Payments.Four.Request.Source;
using Checkout.Payments.Four.Response;
using Checkout.Payments.Four.Sender;
using Checkout.Tokens;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace Checkout.Payments.Four
{
    public abstract class AbstractPaymentsIntegrationTest : SandboxTestFixture
    {
        protected const string IdempotencyKey = "test.net";

        protected AbstractPaymentsIntegrationTest() : base(PlatformType.Four)
        {
        }

        protected async Task<PaymentResponse> MakeCardPayment(bool shouldCapture = false, long amount = 10L,
            DateTime? captureOn = null)
        {
            if (!shouldCapture && captureOn != null)
            {
                throw new XunitException("CaptureOn was provided but the payment is not set for capture");
            }

            var requestCardSource = new RequestCardSource
            {
                Name = TestCardSource.Visa.Name,
                Number = TestCardSource.Visa.Number,
                ExpiryYear = TestCardSource.Visa.ExpiryYear,
                ExpiryMonth = TestCardSource.Visa.ExpiryMonth,
                Cvv = TestCardSource.Visa.Cvv,
                BillingAddress = GetAddress(),
                Phone = GetPhone()
            };

            var customerRequest = new CustomerRequest {Email = GenerateRandomEmail(), Name = "Customer"};

            var paymentIndividualSender = new PaymentIndividualSender
            {
                FirstName = "Mr",
                LastName = "Test",
                Address = GetAddress(),
                Identification = new Identification
                {
                    IssuingCountry = CountryCode.GT, Number = "1234", Type = IdentificationType.DrivingLicence
                }
            };

            var paymentRequest = new PaymentRequest
            {
                Source = requestCardSource,
                Capture = shouldCapture,
                Reference = Guid.NewGuid().ToString(),
                Amount = amount,
                Currency = Currency.USD,
                Customer = customerRequest,
                Sender = paymentIndividualSender,
                CaptureOn = captureOn
            };

            var paymentResponse = await FourApi.PaymentsClient().RequestPayment(paymentRequest);
            paymentResponse.ShouldNotBeNull();
            return paymentResponse;
        }

        protected async Task<PaymentResponse> MakeTokenPayment()
        {
            var cardTokenResponse = await RequestToken();

            var requestTokenSource = new RequestTokenSource {Token = cardTokenResponse.Token};

            var customerRequest = new CustomerRequest {Email = GenerateRandomEmail()};

            var paymentInstrument = new PaymentInstrumentSender();

            var paymentRequest = new PaymentRequest
            {
                Source = requestTokenSource,
                Capture = true,
                Reference = Guid.NewGuid().ToString(),
                Amount = 10L,
                Currency = Currency.USD,
                Customer = customerRequest,
                Sender = paymentInstrument
            };

            var paymentResponse = await FourApi.PaymentsClient().RequestPayment(paymentRequest);
            paymentResponse.ShouldNotBeNull();
            return paymentResponse;
        }

        protected async Task<PaymentResponse> Make3dsCardPayment(bool attemptN3d = false)
        {
            var requestCardSource = new RequestCardSource
            {
                Name = TestCardSource.Visa.Name,
                Number = TestCardSource.Visa.Number,
                ExpiryYear = TestCardSource.Visa.ExpiryYear,
                ExpiryMonth = TestCardSource.Visa.ExpiryMonth,
                Cvv = TestCardSource.Visa.Cvv,
                BillingAddress = GetAddress(),
                Phone = GetPhone()
            };

            var threeDsRequest = new ThreeDsRequest
            {
                Enabled = true,
                AttemptN3D = attemptN3d,
                Eci = attemptN3d ? "05" : null,
                Cryptogram = attemptN3d ? "AgAAAAAAAIR8CQrXcIhbQAAAAAA" : null,
                Xid = attemptN3d ? "MDAwMDAwMDAwMDAwMDAwMzIyNzY=" : null,
                Version = "2.0.1"
            };

            var customerRequest = new CustomerRequest {Email = GenerateRandomEmail(), Name = "Customer"};

            var paymentIndividualSender =
                new PaymentCorporateSender {CompanyName = "Testing Inc.", Address = GetAddress()};

            var paymentRequest = new PaymentRequest
            {
                Source = requestCardSource,
                Capture = false,
                Reference = Guid.NewGuid().ToString(),
                Amount = 10L,
                Currency = Currency.USD,
                Customer = customerRequest,
                ThreeDs = threeDsRequest,
                Sender = paymentIndividualSender,
                SuccessUrl = "https://test.checkout.com/success",
                FailureUrl = "https://test.checkout.com/failure"
            };

            var paymentResponse = await FourApi.PaymentsClient().RequestPayment(paymentRequest);
            paymentResponse.ShouldNotBeNull();
            return paymentResponse;
        }

        protected async Task<CardTokenResponse> RequestToken()
        {
            var cardTokenRequest = new CardTokenRequest
            {
                Name = TestCardSource.Visa.Name,
                Number = TestCardSource.Visa.Number,
                ExpiryYear = TestCardSource.Visa.ExpiryYear,
                ExpiryMonth = TestCardSource.Visa.ExpiryMonth,
                Cvv = TestCardSource.Visa.Cvv,
                BillingAddress = GetAddress(),
                Phone = GetPhone()
            };

            var cardTokenResponse = await FourApi.TokensClient().Request(cardTokenRequest);
            cardTokenResponse.ShouldNotBeNull();
            return cardTokenResponse;
        }
    }
}