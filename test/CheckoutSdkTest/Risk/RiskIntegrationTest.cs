using Checkout.Common;
using Checkout.Risk.PreAuthentication;
using Checkout.Risk.PreCapture;
using Checkout.Risk.source;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using CustomerRequest = Checkout.Customers.CustomerRequest;

namespace Checkout.Risk
{
    public class RiskIntegrationTest : SandboxTestFixture
    {
        public RiskIntegrationTest() : base(PlatformType.Default)
        {
        }

        [Fact(Skip = "unavailable")]
        private async Task ShouldRiskCardSource()
        {
            var cardSourcePrism = new CardSourcePrism()
            {
                BillingAddress = new Address()
                {
                    AddressLine1 = "123 Street",
                    AddressLine2 = "Hollywood Avenue",
                    City = "Los Angeles",
                    State = "CA",
                    Zip = "91001",
                    Country = CountryCode.US
                },
                ExpiryMonth = TestCardSource.Visa.ExpiryMonth,
                ExpiryYear = TestCardSource.Visa.ExpiryYear,
                Number = TestCardSource.Visa.Number,
                Phone = new Phone()
                {
                    CountryCode = "1",
                    Number = "987654321"
                }
            };

            await TestRequestPreAuthenticationRiskScan(cardSourcePrism);
            await TestPreCaptureAssessmentRequest(cardSourcePrism);
        }

        [Fact(Skip = "unavailable")]
        private async Task ShouldRiskCustomerSource()
        {
            var customerRequest = new CustomerRequest()
            {
                Email = GenerateRandomEmail(),
                Name = "Customer"
            };

            var customerResponse = await DefaultApi.CustomersClient().Create(customerRequest);
            customerRequest.ShouldNotBeNull();

            var customerSourcePrism = new CustomerSourcePrism()
            {
                Id = customerResponse.Id
            };

            await TestRequestPreAuthenticationRiskScan(customerSourcePrism);
            await TestPreCaptureAssessmentRequest(customerSourcePrism);
        }

        private async Task TestRequestPreAuthenticationRiskScan(RiskPaymentRequestSource requestSource)
        {
            var preAuthenticationAssessmentRequest = new PreAuthenticationAssessmentRequest()
            {
                Date = DateTime.Now,
                Source = requestSource,
                Customer = new Common.CustomerRequest()
                {
                    Email = GenerateRandomEmail(),
                    Name = "name"
                },
                Payment = new RiskPayment()
                {
                    Psp = "CheckoutSdk.com",
                    Id = "78453878"
                },
                Shipping = new RiskShippingDetails()
                {
                    Address = new Address()
                    {
                        AddressLine1 = "CheckoutSdk.com",
                        AddressLine2 = "90 Tottenham Court Road",
                        City = "London",
                        State = "London",
                        Zip = "W1T 4TJ",
                        Country = CountryCode.GB
                    }
                },
                Reference = "ORD-1011-87AH",
                Description = "Set of 3 masks",
                Amount = 6540,
                Device = new Device()
                {
                    Ip = "90.197.169.245",
                    Location = new Location()
                    {
                        Longitude = "0.1313",
                        Latitude = "51.5107"
                    },
                    Type = "Phone",
                    Os = "ISO",
                    Model = "iPhone X",
                    Date = DateTime.Now,
                }
            };

            var response = await DefaultApi.RiskClient()
                .RequestPreAuthenticationRiskScan(preAuthenticationAssessmentRequest);

            response.ShouldNotBeNull();
            response.AssessmentId.ShouldNotBeNull();
            response.Result.ShouldNotBeNull();
            response.Result.Decision.ShouldNotBeNull();
        }

        private async Task TestPreCaptureAssessmentRequest(RiskPaymentRequestSource requestSource)
        {
            var preCaptureAssessmentRequest = new PreCaptureAssessmentRequest()
            {
                Date = DateTime.Now,
                Source = requestSource,
                Customer = new Common.CustomerRequest()
                {
                    Email = GenerateRandomEmail(),
                    Name = "name"
                },
                Payment = new RiskPayment()
                {
                    Psp = "CheckoutSdk.com",
                    Id = "78453878"
                },
                Shipping = new RiskShippingDetails()
                {
                    Address = new Address()
                    {
                        AddressLine1 = "CheckoutSdk.com",
                        AddressLine2 = "90 Tottenham Court Road",
                        City = "London",
                        State = "London",
                        Zip = "W1T 4TJ",
                        Country = CountryCode.GB
                    }
                },
                Amount = 6540,
                Currency = Currency.GBP,
                Device = new Device()
                {
                    Ip = "90.197.169.245",
                    Location = new Location()
                    {
                        Longitude = "0.1313",
                        Latitude = "51.5107"
                    },
                    Type = "Phone",
                    Os = "ISO",
                    Model = "iPhone X",
                    Date = DateTime.Now,
                    UserAgent =
                        "Mozilla/5.0 (iPhone; CPU iPhone OS 11_0 like Mac OS X) AppleWebKit/604.1.38 (KHTML, like Gecko) Version/11.0 Mobile/15A372 Safari/604.1",
                    Fingerprint = "34304a9e3fg09302"
                },
                Metadata = new Dictionary<string, string>()
                {
                    {"VoucherCode", "loyalty_10"},
                    {"discountApplied", "10"},
                    {"customer_id", "2190EF321"},
                },
                AuthenticationResult = new AuthenticationResult()
                {
                    Attempted = true,
                    Challenged = true,
                    LiabilityShifted = true,
                    Method = "3ds",
                    Succeeded = true,
                    Version = "2.0"
                },
                AuthorizationResult = new AuthorizationResult()
                {
                    AvsCode = "V",
                    CvvResult = "N"
                }
            };

            var response = await DefaultApi.RiskClient().RequestPreCaptureRiskScan(preCaptureAssessmentRequest);

            response.ShouldNotBeNull();
            response.AssessmentId.ShouldNotBeNull();
            response.Result.ShouldNotBeNull();
            response.Result.Decision.ShouldNotBeNull();
        }
    }
}