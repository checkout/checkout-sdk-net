using Checkout.Common;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Payments.Hosted
{
    public class HostedPaymentsClientTest : UnitTestFixture
    {
        private const string HostedPayments = "hosted-payments";
        private const string Reference = "ORD-1234";

        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<CheckoutConfiguration> _configuration;
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Previous);
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Previous, ValidPreviousSk);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly HostedPaymentResponse _hostedPaymentResponse = new HostedPaymentResponse();

        public HostedPaymentsClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKey))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object, null);

            _hostedPaymentResponse.Reference = Reference;
            _hostedPaymentResponse.Links = new Dictionary<string, Link>();
        }

        [Fact]
        private async Task ShouldGetHostedPayments()
        {
            var hostedPaymentResponse = new HostedPaymentDetailsResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<HostedPaymentDetailsResponse>(HostedPayments + "/id", _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => hostedPaymentResponse);

            var client = new HostedPaymentsClient(_apiClient.Object, _configuration.Object);

            var response = await client.GetHostedPaymentsPageDetails("id", CancellationToken.None);

            response.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldCreateHostedPayments()
        {
            var hostedPaymentRequest = CreateHostedPaymentRequest(Reference);

            _apiClient.Setup(apiClient =>
                    apiClient.Post<HostedPaymentResponse>(HostedPayments, _authorization, hostedPaymentRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => _hostedPaymentResponse);

            var client = new HostedPaymentsClient(_apiClient.Object, _configuration.Object);

            var response = await client.CreateHostedPaymentsPageSession(hostedPaymentRequest, CancellationToken.None);

            response.ShouldNotBeNull();
            response.Reference.ShouldBe(Reference);
            response.Links.ShouldNotBeNull();
        }

        private static string GenerateRandomEmail()
        {
            return $"{Guid.NewGuid()}@checkout-sdk-net.com";
        }

        private static Phone GetPhone()
        {
            return new Phone {CountryCode = "1", Number = "4155552671"};
        }

        private static Address GetAddress()
        {
            return new Address
            {
                AddressLine1 = "CheckoutSdk.com",
                AddressLine2 = "90 Tottenham Court Road",
                City = "London",
                State = "London",
                Zip = "W1T 4TJ",
                Country = CountryCode.GB
            };
        }

        private static HostedPaymentRequest CreateHostedPaymentRequest(string reference)
        {
            return new HostedPaymentRequest
            {
                Amount = 1000L,
                Reference = reference,
                Currency = Currency.GBP,
                Description = "Payment for Gold Necklace",
                Customer = new CustomerRequest {Name = "Jack Napier", Email = GenerateRandomEmail()},
                Shipping = new ShippingDetails {Address = GetAddress(), Phone = GetPhone()},
                Billing = new BillingInformation {Address = GetAddress(), Phone = GetPhone()},
                Recipient =
                    new PaymentRecipient
                    {
                        AccountNumber = "1234567",
                        Country = CountryCode.ES,
                        DateOfBirth = "1985-05-15",
                        FirstName = "IT",
                        LastName = "TESTING",
                        Zip = "12345"
                    },
                Processing = new ProcessingSettings {Aft = true},
                Products = new Product[] {new Product {Name = "Gold Necklace", Quantity = 1L, Price = 200L}},
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