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
        public AgenticIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
        }

        [Fact(Skip = "This test is unsupported currently, not ready to test in the sandbox")]
        private async Task EnrollShouldEnroll()
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

        [Fact(Skip = "This test is unsupported currently, not ready to test in the sandbox")]
        private async Task EnrollShouldEnrollWithMinimalData()
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

        [Fact(Skip = "This test is unsupported currently, not ready to test in the sandbox")]
        private async Task EnrollShouldHandleDifferentCardTypes()
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

        [Fact(Skip = "This test is unsupported currently, not ready to test in the sandbox")]
        private async Task EnrollShouldHandleInternationalCustomers()
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

        [Fact(Skip = "This test is unsupported currently, not ready to test in the sandbox")]
        private async Task CreatePurchaseIntentShouldCreatePurchaseIntent()
        {
            var createPurchaseIntentRequest = new AgenticPurchaseIntentCreateRequest
            {
                NetworkTokenId = "nt_e7fjr77crbgmlhpjvuq3bj6jba",
                Device = new DeviceInfo
                {
                    IpAddress = "192.168.1.100",
                    UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36",
                    DeviceBrand = "apple",
                    DeviceType = "tablet"
                },
                CustomerPrompt = "I'm looking for running shoes in a size 10, for under $150.",
                Mandates = new[]
                {
                    new Mandate
                    {
                        Id = "mandate_123",
                        PurchaseThreshold = new PurchaseThreshold
                        {
                            Amount = 100,
                            CurrencyCode = Currency.USD
                        },
                        Description = "Purchase running shoes in size 10.",
                        ExpirationDate = DateTime.Parse("2026-08-31T23:59:59.000Z")
                    }
                }
            };

            var response = await DefaultApi.AgenticClient().CreatePurchaseIntent(createPurchaseIntentRequest);

            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNullOrEmpty();
            response.Status.ShouldNotBeNullOrEmpty();
            response.TokenId.ShouldNotBeNullOrEmpty();
            response.CustomerPrompt.ShouldNotBeNullOrEmpty();
            
            // Validate that purchase intent ID follows expected pattern
            response.Id.ShouldStartWith("pi_");
            
            // Validate status is one of expected values
            response.Status.ShouldBe("active");
            
            // Validate links are present
            response.Links.ShouldNotBeNull();
            response.Links.Self.ShouldNotBeNullOrEmpty();
            response.Links.CreateCredentials.ShouldNotBeNullOrEmpty();
        }

        [Fact(Skip = "This test is unsupported currently, not ready to test in the sandbox")]
        private async Task CreatePurchaseIntentShouldCreatePurchaseIntentWithMinimalData()
        {
            var createPurchaseIntentRequest = new AgenticPurchaseIntentCreateRequest
            {
                NetworkTokenId = "nt_e7fjr77crbgmlhpjvuq3bj6jba",
                Device = new DeviceInfo
                {
                    IpAddress = "10.0.0.1"
                },
                CustomerPrompt = "Basic purchase request"
            };

            var response = await DefaultApi.AgenticClient().CreatePurchaseIntent(createPurchaseIntentRequest);

            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNullOrEmpty();
            response.Status.ShouldBe("active");
            response.TokenId.ShouldNotBeNullOrEmpty();
        }

        [Fact(Skip = "This test is unsupported currently, not ready to test in the sandbox")]
        private async Task CreatePurchaseIntentShouldHandleDifferentMandateTypes()
        {
            // Test with single mandate
            var singleMandateRequest = new AgenticPurchaseIntentCreateRequest
            {
                NetworkTokenId = "nt_e7fjr77crbgmlhpjvuq3bj6jba",
                Device = new DeviceInfo
                {
                    IpAddress = "203.0.113.195",
                    UserAgent = "Test Agent",
                    DeviceBrand = "chrome",
                    DeviceType = "desktop"
                },
                CustomerPrompt = "Looking for electronics under $500",
                Mandates = new[]
                {
                    new Mandate
                    {
                        Id = "mandate_single",
                        PurchaseThreshold = new PurchaseThreshold
                        {
                            Amount = 500,
                            CurrencyCode = Currency.USD
                        },
                        Description = "Electronics purchase",
                        ExpirationDate = DateTime.Parse("2026-12-31T23:59:59.000Z")
                    }
                }
            };

            var singleResponse = await DefaultApi.AgenticClient().CreatePurchaseIntent(singleMandateRequest);
            singleResponse.ShouldNotBeNull();
            singleResponse.Status.ShouldBe("active");
            singleResponse.Mandates.Length.ShouldBe(1);

            // Test with multiple mandates
            var multipleMandateRequest = new AgenticPurchaseIntentCreateRequest
            {
                NetworkTokenId = "nt_e7fjr77crbgmlhpjvuq3bj6jba",
                Device = new DeviceInfo
                {
                    IpAddress = "198.51.100.42"
                },
                CustomerPrompt = "Shopping for clothing and accessories",
                Mandates = new[]
                {
                    new Mandate
                    {
                        Id = "mandate_clothing",
                        PurchaseThreshold = new PurchaseThreshold
                        {
                            Amount = 200,
                            CurrencyCode = Currency.USD
                        },
                        Description = "Clothing purchase",
                        ExpirationDate = DateTime.Parse("2026-06-30T23:59:59.000Z")
                    },
                    new Mandate
                    {
                        Id = "mandate_accessories",
                        PurchaseThreshold = new PurchaseThreshold
                        {
                            Amount = 100,
                            CurrencyCode = Currency.USD
                        },
                        Description = "Accessories purchase",
                        ExpirationDate = DateTime.Parse("2026-09-30T23:59:59.000Z")
                    }
                }
            };

            var multipleResponse = await DefaultApi.AgenticClient().CreatePurchaseIntent(multipleMandateRequest);
            multipleResponse.ShouldNotBeNull();
            multipleResponse.Status.ShouldBe("active");
            multipleResponse.Mandates.Length.ShouldBe(2);
        }

        [Fact(Skip = "This test is unsupported currently, not ready to test in the sandbox")]
        private async Task CreatePurchaseIntentShouldHandleInternationalCurrencies()
        {
            var internationalRequest = new AgenticPurchaseIntentCreateRequest
            {
                NetworkTokenId = "nt_e7fjr77crbgmlhpjvuq3bj6jba",
                Device = new DeviceInfo
                {
                    IpAddress = "80.112.12.34", // European IP
                    UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36",
                    DeviceBrand = "safari",
                    DeviceType = "mobile"
                },
                CustomerPrompt = "Buscando zapatos deportivos en talla 42",
                Mandates = new[]
                {
                    new Mandate
                    {
                        Id = "mandate_eur",
                        PurchaseThreshold = new PurchaseThreshold
                        {
                            Amount = 150,
                            CurrencyCode = Currency.EUR
                        },
                        Description = "Compra de zapatos deportivos",
                        ExpirationDate = DateTime.Parse("2026-08-31T23:59:59.000Z")
                    }
                }
            };

            var response = await DefaultApi.AgenticClient().CreatePurchaseIntent(internationalRequest);

            response.ShouldNotBeNull();
            response.Status.ShouldBe("active");
            response.Id.ShouldStartWith("pi_");
            response.Mandates.ShouldNotBeNull();
            response.Mandates[0].PurchaseThreshold.CurrencyCode.ShouldBe(Currency.EUR);
        }
    }
}