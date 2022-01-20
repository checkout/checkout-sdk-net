using System;
using System.Threading.Tasks;
using Checkout.Common;
using Checkout.Payments.Hosted;
using Checkout.Payments.Request;
using Checkout.Payments.Request.Source;
using Checkout.Payments.Response;
using Checkout.Tokens;
using Shouldly;
using Xunit.Sdk;

namespace Checkout.Payments
{
    public abstract class AbstractPaymentsIntegrationTest : SandboxTestFixture
    {
        protected const string IdempotencyKey = "test.net";

        protected AbstractPaymentsIntegrationTest() : base(PlatformType.Default)
        {
        }

        protected async Task<PaymentResponse> MakeCardPayment(bool shouldCapture = false, long amount = 10L,
            DateTime? captureOn = null)
        {
            if (!shouldCapture && captureOn != null)
            {
                throw new XunitException("CaptureOn was provided but the payment is not set for capture");
            }

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

            var paymentRequest = new PaymentRequest
            {
                Source = requestCardSource,
                Capture = shouldCapture,
                Reference = Guid.NewGuid().ToString(),
                Amount = amount,
                Currency = Currency.GBP,
                CaptureOn = captureOn
            };

            var paymentResponse = await DefaultApi.PaymentsClient().RequestPayment(paymentRequest);
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

            var cardTokenResponse = await DefaultApi.TokensClient().Request(cardTokenRequest);
            cardTokenResponse.ShouldNotBeNull();

            var requestTokenSource = new RequestTokenSource {Token = cardTokenResponse.Token};

            var customerRequest = new CustomerRequest {Email = GenerateRandomEmail()};

            var paymentRequest = new PaymentRequest
            {
                Source = requestTokenSource,
                Capture = true,
                Reference = Guid.NewGuid().ToString(),
                Amount = 10L,
                Currency = Currency.USD,
                Customer = customerRequest
            };

            var paymentResponse = await DefaultApi.PaymentsClient().RequestPayment(paymentRequest);
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

            var paymentResponse = await DefaultApi.PaymentsClient().RequestPayment(paymentRequest);
            paymentResponse.ShouldNotBeNull();
            return paymentResponse;
        }

        protected static Phone GetPhone()
        {
            return new Phone() {CountryCode = "1", Number = "4155552671"};
        }

        protected static Address GetAddress()
        {
            return new Address()
            {
                AddressLine1 = "CheckoutSdk.com",
                AddressLine2 = "90 Tottenham Court Road",
                City = "London",
                State = "London",
                Zip = "W1T 4TJ",
                Country = CountryCode.GB
            };
        }

        protected static HostedPaymentRequest CreateHostedPaymentRequest(string reference)
        {
            var customer = new CustomerRequest {Name = "Jack Napier", Email = GenerateRandomEmail()};

            var shippingDetails = new ShippingDetails {Address = GetAddress(), Phone = GetPhone()};

            var billing = new BillingInformation() {Address = GetAddress(), Phone = GetPhone()};


            var recipient = new PaymentRecipient()
            {
                AccountNumber = "1234567",
                Country = CountryCode.ES,
                DateOfBirth = "1985-05-15",
                FirstName = "IT",
                LastName = "TESTING",
                Zip = "12345"
            };

            var products = new Product[] {new Product() {Name = "Gold Necklace", Quantity = 1L, Price = 200L}};

            var threeDs = new ThreeDsRequest() {Enabled = false, AttemptN3D = false};

            var processing = new ProcessingSettings {Aft = true};

            var risk = new RiskRequest {Enabled = false};

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
                Processing = processing,
                Products = products,
                Risk = risk,
                SuccessUrl = "https://example.com/payments/success",
                CancelUrl = "https://example.com/payments/success",
                FailureUrl = "https://example.com/payments/success",
                Locale = "en-GB",
                ThreeDs = threeDs,
                Capture = true,
                CaptureOn = DateTime.UtcNow
            };
        }
    }
}