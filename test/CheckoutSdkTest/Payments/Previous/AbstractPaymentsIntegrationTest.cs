using Checkout.Common;
using Checkout.Payments.Hosted;
using Checkout.Payments.Previous.Request;
using Checkout.Payments.Previous.Request.Source;
using Checkout.Payments.Previous.Response;
using Checkout.Tokens;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace Checkout.Payments.Previous
{
    public abstract class AbstractPaymentsIntegrationTest : SandboxTestFixture
    {
        protected const string IdempotencyKey = "test.net";

        protected AbstractPaymentsIntegrationTest() : base(PlatformType.Previous)
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

            var paymentRequest = new PaymentRequest
            {
                Source = requestCardSource,
                Capture = shouldCapture,
                Reference = Guid.NewGuid().ToString(),
                Amount = amount,
                Currency = Currency.GBP,
                CaptureOn = captureOn
            };

            var paymentResponse = await PreviousApi.PaymentsClient().RequestPayment(paymentRequest);
            paymentResponse.ShouldNotBeNull();
            return paymentResponse;
        }

        protected async Task<PaymentResponse> MakeTokenPayment()
        {
            var phone = GetPhone();
            var billingAddress = GetAddress();

            var cardTokenRequest = new CardTokenRequest
            {
                Name = TestCardSource.Visa.Name,
                Number = TestCardSource.Visa.Number,
                ExpiryYear = TestCardSource.Visa.ExpiryYear,
                ExpiryMonth = TestCardSource.Visa.ExpiryMonth,
                Cvv = TestCardSource.Visa.Cvv,
                BillingAddress = billingAddress,
                Phone = phone
            };

            var cardTokenResponse = await PreviousApi.TokensClient().Request(cardTokenRequest);
            cardTokenResponse.ShouldNotBeNull();

            var paymentRequest = new PaymentRequest
            {
                Source = new RequestTokenSource {Token = cardTokenResponse.Token},
                Capture = true,
                Reference = Guid.NewGuid().ToString(),
                Amount = 10L,
                Currency = Currency.USD,
                Customer = new CustomerRequest {Email = GenerateRandomEmail()},
                BillingDescriptor = new BillingDescriptor {Name = "name", City = "London", Reference = "reference"}
            };

            var paymentResponse = await PreviousApi.PaymentsClient().RequestPayment(paymentRequest);
            paymentResponse.ShouldNotBeNull();
            return paymentResponse;
        }

        protected async Task<PaymentResponse> Make3dsCardPayment(bool attemptN3d = false)
        {
            var phone = GetPhone();
            var billingAddress = GetAddress();

            var requestCardSource = new RequestCardSource
            {
                Name = TestCardSource.Visa.Name,
                Number = TestCardSource.Visa.Number,
                ExpiryYear = TestCardSource.Visa.ExpiryYear,
                ExpiryMonth = TestCardSource.Visa.ExpiryMonth,
                Cvv = TestCardSource.Visa.Cvv,
                BillingAddress = billingAddress,
                Phone = phone
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

            var customerRequest = new CustomerRequest {Email = GenerateRandomEmail()};

            var paymentRequest = new PaymentRequest
            {
                Source = requestCardSource,
                Capture = false,
                Reference = Guid.NewGuid().ToString(),
                Amount = 10L,
                Currency = Currency.USD,
                Customer = customerRequest,
                ThreeDs = threeDsRequest
            };

            var paymentResponse = await PreviousApi.PaymentsClient().RequestPayment(paymentRequest);
            paymentResponse.ShouldNotBeNull();
            return paymentResponse;
        }

        protected static HostedPaymentRequest CreateHostedPaymentRequest(string reference)
        {
            var customer = new CustomerRequest {Name = "Jack Napier", Email = GenerateRandomEmail()};
            var shippingDetails = new ShippingDetails {Address = GetAddress(), Phone = GetPhone()};
            var billing = new BillingInformation {Address = GetAddress(), Phone = GetPhone()};

            var recipient = new PaymentRecipient
            {
                AccountNumber = "1234567",
                Country = CountryCode.ES,
                DateOfBirth = "1985-05-15",
                FirstName = "IT",
                LastName = "TESTING",
                Zip = "12345"
            };

            return new HostedPaymentRequest
            {
                Amount = 1000L,
                Reference = reference,
                Currency = Currency.GBP,
                Description = "Payment for Gold Necklace",
                Customer = customer,
                Shipping = shippingDetails,
                Billing = billing,
                Recipient = recipient,
                Processing = new ProcessingSettings {Aft = true},
                Products = new List<Product> {new Product {Name = "Gold Necklace", Quantity = 1L, Price = 1000L}},
                Risk = new RiskRequest {Enabled = false},
                SuccessUrl = "https://example.com/payments/success",
                CancelUrl = "https://example.com/payments/success",
                FailureUrl = "https://example.com/payments/success",
                Locale = "en-GB",
                ThreeDs = new ThreeDsRequest {Enabled = false, AttemptN3D = false},
                Capture = true,
                CaptureOn = DateTime.UtcNow
            };
        }
    }
}