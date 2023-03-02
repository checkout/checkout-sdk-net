using Checkout.Common;
using Checkout.Payments.Request;
using Checkout.Payments.Request.Source;
using Checkout.Payments.Sender;
using Shouldly;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace Checkout
{
    public class OAuthIntegrationTest : SandboxTestFixture
    {
        public OAuthIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
        }

        [Fact]
        public async Task ShouldMakeOAuthCall()
        {
            var requestCardSource = new RequestCardSource
            {
                Name = TestCardSource.Visa.Name,
                Number = TestCardSource.Visa.Number,
                ExpiryYear = TestCardSource.Visa.ExpiryYear,
                ExpiryMonth = TestCardSource.Visa.ExpiryMonth,
                Cvv = TestCardSource.Visa.Cvv,
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
                Currency = Currency.EUR,
                ProcessingChannelId = "pc_a6dabcfa2o3ejghb3sjuotdzzy",
                Marketplace = new MarketplaceData {SubEntityId = "ent_ocw5i74vowfg2edpy66izhts2u"},
                Sender = paymentIndividualSender
            };

            var paymentResponse = await DefaultApi.PaymentsClient().RequestPayment(paymentRequest);
            paymentResponse.ShouldNotBeNull();
        }

        [Fact]
        public void ShouldFailInitAuthorization_InvalidCredentials()
        {
            try
            {
                CheckoutSdk.Builder()
                    .OAuth()
                    .ClientCredentials("fake", "fake")
                    .Environment(Environment.Sandbox)
                    .Build();
                throw new XunitException();
            }
            catch (Exception e)
            {
                e.Message.ShouldBe("OAuth client_credentials authentication failed with error: invalid_client");
            }
        }

        [Fact]
        public void ShouldFailInitAuthorization_CustomFakeAuthorizationUri()
        {
            try
            {
                CheckoutSdk.Builder()
                    .OAuth()
                    .ClientCredentials(System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_OAUTH_CLIENT_ID"),
                        System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_OAUTH_CLIENT_SECRET"))
                    .AuthorizationUri(new Uri("https://test.checkout.com"))
                    .HttpClientFactory(new DefaultHttpClientFactory())
                    .Build();
                throw new XunitException();
            }
            catch (Exception e)
            {
                e.Message.ShouldBe("OAuth client_credentials authentication failed with error: invalid_client");
            }
        }
    }
}