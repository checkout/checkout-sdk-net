using System;
using System.Threading.Tasks;
using Shouldly;
using Xunit;
using Checkout.Agentic;
using Checkout.Agentic.Requests;
using Checkout.Common;

namespace Checkout.Agentic
{
    public class AgenticIntegrationTest : SandboxTestFixture
    {
        public AgenticIntegrationTest() : base(PlatformType.Default)
        {
        }

        [Fact]
        private async Task ShouldEnrollInAgenticServices()
        {
            var agenticEnrollRequest = new AgenticEnrollRequest
            {
                Source = new PaymentSource
                {
                    Number = "4242424242424242",
                    ExpiryMonth = 12,
                    ExpiryYear = 2025,
                    Cvv = "123",
                    Type = PaymentSourceType.Card
                },
                Device = new DeviceInfo
                {
                    IpAddress = "192.168.1.100",
                    UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36",
                    DeviceBrand = "Chrome",
                    DeviceType = "desktop"
                },
                Customer = new AgenticCustomer
                {
                    Email = GenerateRandomEmail(),
                    CountryCode = CountryCode.US,
                    LanguageCode = "en"
                }
            };

            var response = await DefaultApi.AgenticClient().Enroll(agenticEnrollRequest);

            response.ShouldNotBeNull();
            response.TokenId.ShouldNotBeNullOrEmpty();
            response.Status.ShouldNotBeNullOrEmpty();
            response.CreatedAt.ShouldNotBe(default(DateTime));
            
            // Validate that token_id follows expected pattern
            response.TokenId.ShouldStartWith("nt_");
            
            // Validate status is one of expected values
            response.Status.ShouldBe("enrolled");
        }

        [Fact]
        private async Task ShouldEnrollWithMinimalData()
        {
            var agenticEnrollRequest = new AgenticEnrollRequest
            {
                Source = new PaymentSource
                {
                    Number = "4242424242424242",
                    ExpiryMonth = 6,
                    ExpiryYear = 2026,
                    Cvv = "100",
                    Type = PaymentSourceType.Card
                },
                Device = new DeviceInfo
                {
                    IpAddress = "10.0.0.1"
                },
                Customer = new AgenticCustomer
                {
                    Email = GenerateRandomEmail(),
                    CountryCode = CountryCode.US,
                    LanguageCode = "en"
                }
            };

            var response = await DefaultApi.AgenticClient().Enroll(agenticEnrollRequest);

            response.ShouldNotBeNull();
            response.TokenId.ShouldNotBeNullOrEmpty();
            response.Status.ShouldBe("enrolled");
        }

        [Fact]
        private async Task ShouldHandleDifferentCardTypes()
        {
            // Test with Visa card
            var visaRequest = new AgenticEnrollRequest
            {
                Source = new PaymentSource
                {
                    Number = "4242424242424242", // Visa test card
                    ExpiryMonth = 8,
                    ExpiryYear = 2027,
                    Cvv = "888",
                    Type = PaymentSourceType.Card
                },
                Device = new DeviceInfo
                {
                    IpAddress = "203.0.113.195",
                    UserAgent = "Test Agent"
                },
                Customer = new AgenticCustomer
                {
                    Email = GenerateRandomEmail(),
                    CountryCode = CountryCode.US,
                    LanguageCode = "en"
                }
            };

            var visaResponse = await DefaultApi.AgenticClient().Enroll(visaRequest);
            visaResponse.ShouldNotBeNull();
            visaResponse.Status.ShouldBe("enrolled");

            // Test with Mastercard
            var mastercardRequest = new AgenticEnrollRequest
            {
                Source = new PaymentSource
                {
                    Number = "5555555555554444", // Mastercard test card
                    ExpiryMonth = 9,
                    ExpiryYear = 2028,
                    Cvv = "999",
                    Type = PaymentSourceType.Card
                },
                Device = new DeviceInfo
                {
                    IpAddress = "198.51.100.42"
                },
                Customer = new AgenticCustomer
                {
                    Email = GenerateRandomEmail(),
                    CountryCode = CountryCode.US,
                    LanguageCode = "en"
                }
            };

            var mastercardResponse = await DefaultApi.AgenticClient().Enroll(mastercardRequest);
            mastercardResponse.ShouldNotBeNull();
            mastercardResponse.Status.ShouldBe("enrolled");
        }

        [Fact]
        private async Task ShouldHandleInternationalCustomers()
        {
            var internationalRequest = new AgenticEnrollRequest
            {
                Source = new PaymentSource
                {
                    Number = "4242424242424242",
                    ExpiryMonth = 3,
                    ExpiryYear = 2029,
                    Cvv = "314",
                    Type = PaymentSourceType.Card
                },
                Device = new DeviceInfo
                {
                    IpAddress = "80.112.12.34", // European IP
                    UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36",
                    DeviceBrand = "Safari",
                    DeviceType = "mobile"
                },
                Customer = new AgenticCustomer
                {
                    Email = GenerateRandomEmail(),
                    CountryCode = CountryCode.ES,
                    LanguageCode = "es"
                }
            };

            var response = await DefaultApi.AgenticClient().Enroll(internationalRequest);

            response.ShouldNotBeNull();
            response.Status.ShouldBe("enrolled");
            response.TokenId.ShouldStartWith("nt_");
        }
    }
}