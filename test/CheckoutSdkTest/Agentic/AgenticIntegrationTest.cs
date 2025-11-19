using System;
using System.Net;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

using Checkout.Agentic.Entities;
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
            response.Links["self"].Href.ShouldNotBeNullOrEmpty();
            response.Links["create-credentials"].Href.ShouldNotBeNullOrEmpty();
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

        [Fact(Skip = "This test is unsupported currently, not ready to test in the sandbox")]
        private async Task CreatePurchaseIntentCredentialsShouldCreateCredentials()
        {
            // First create a purchase intent to get an ID
            var createPurchaseIntentRequest = new AgenticPurchaseIntentCreateRequest
            {
                NetworkTokenId = "nt_e7fjr77crbgmlhpjvuq3bj6jba",
                Device = new DeviceInfo
                {
                    IpAddress = "192.168.1.100",
                    UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36"
                },
                CustomerPrompt = "I need running shoes in size 10, under $150.",
                Mandates = new[]
                {
                    new Mandate
                    {
                        Id = "mandate_credentials_test",
                        PurchaseThreshold = new PurchaseThreshold
                        {
                            Amount = 15000,
                            CurrencyCode = Currency.USD
                        },
                        Description = "Purchase running shoes for credentials test.",
                        ExpirationDate = DateTime.Parse("2026-12-31T23:59:59.000Z")
                    }
                }
            };

            var purchaseIntentResponse = await DefaultApi.AgenticClient().CreatePurchaseIntent(createPurchaseIntentRequest);
            purchaseIntentResponse.ShouldNotBeNull();
            purchaseIntentResponse.Id.ShouldNotBeNullOrEmpty();

            // Now create credentials for this purchase intent  
            var createCredentialsRequest = new AgenticPurchaseIntentCredentialsCreateRequest
            {
                TransactionData = new[]
                {
                    new TransactionData
                    {
                        MerchantCountryCode = CountryCode.US,
                        MerchantName = "Nike Store",
                        MerchantCategoryCode = "5661",
                        MerchantUrl = "https://www.nike.com",
                        TransactionAmount = new TransactionAmount
                        {
                            Amount = 12999,
                            CurrencyCode = Currency.USD
                        }
                    }
                }
            };

            var credentialsResponse = await DefaultApi.AgenticClient().CreatePurchaseIntentCredentials(
                purchaseIntentResponse.Id, createCredentialsRequest);

            credentialsResponse.ShouldNotBeNull();
            credentialsResponse.Id.ShouldBe(purchaseIntentResponse.Id);
            credentialsResponse.Status.ShouldNotBeNullOrEmpty();
            credentialsResponse.TokenId.ShouldNotBeNullOrEmpty();
            
            // Validate that the purchase intent ID follows expected pattern
            credentialsResponse.Id.ShouldStartWith("pi_");
            
            // Validate links are present and updated
            credentialsResponse.Links.ShouldNotBeNull();
            credentialsResponse.Links["self"].Href.ShouldNotBeNullOrEmpty();
            credentialsResponse.Links["create-credentials"].Href.ShouldNotBeNullOrEmpty();
        }

        [Fact(Skip = "This test is unsupported currently, not ready to test in the sandbox")]
        private async Task CreatePurchaseIntentCredentialsShouldHandleMultipleTransactionData()
        {
            // First create a purchase intent
            var createPurchaseIntentRequest = new AgenticPurchaseIntentCreateRequest
            {
                NetworkTokenId = "nt_e7fjr77crbgmlhpjvuq3bj6jba",
                Device = new DeviceInfo
                {
                    IpAddress = "203.0.113.195",
                    UserAgent = "Test Agent Multi Transaction"
                },
                CustomerPrompt = "I need multiple items: electronics and clothing.",
                Mandates = new[]
                {
                    new Mandate
                    {
                        Id = "mandate_multi_transaction",
                        PurchaseThreshold = new PurchaseThreshold
                        {
                            Amount = 100000,
                            CurrencyCode = Currency.USD
                        },
                        Description = "Purchase multiple items across different categories.",
                        ExpirationDate = DateTime.Parse("2026-12-31T23:59:59.000Z")
                    }
                }
            };

            var purchaseIntentResponse = await DefaultApi.AgenticClient().CreatePurchaseIntent(createPurchaseIntentRequest);
            purchaseIntentResponse.ShouldNotBeNull();

            // Create credentials with multiple transaction data
            var createCredentialsRequest = new AgenticPurchaseIntentCredentialsCreateRequest
            {
                TransactionData = new[]
                {
                    new TransactionData
                    {
                        MerchantCountryCode = CountryCode.US,
                        MerchantName = "Best Electronics",
                        MerchantCategoryCode = "5732",
                        MerchantUrl = "https://bestelectronics.com",
                        TransactionAmount = new TransactionAmount
                        {
                            Amount = 79999,
                            CurrencyCode = Currency.USD
                        }
                    },
                    new TransactionData
                    {
                        MerchantCountryCode = CountryCode.US,
                        MerchantName = "Fashion Central",
                        MerchantCategoryCode = "5651", 
                        MerchantUrl = "https://fashioncentral.com",
                        TransactionAmount = new TransactionAmount
                        {
                            Amount = 15999,
                            CurrencyCode = Currency.USD
                        }
                    }
                }
            };

            var credentialsResponse = await DefaultApi.AgenticClient().CreatePurchaseIntentCredentials(
                purchaseIntentResponse.Id, createCredentialsRequest);

            credentialsResponse.ShouldNotBeNull();
            credentialsResponse.Id.ShouldBe(purchaseIntentResponse.Id);
            credentialsResponse.Status.ShouldNotBeNullOrEmpty();
            credentialsResponse.TokenId.ShouldNotBeNullOrEmpty();
            
            // Validate response structure
            credentialsResponse.Id.ShouldStartWith("pi_");
            credentialsResponse.Links.ShouldNotBeNull();
        }

        [Fact(Skip = "This test is unsupported currently, not ready to test in the sandbox")]
        private async Task CreatePurchaseIntentCredentialsShouldHandleInternationalTransactions()
        {
            // Create purchase intent
            var createPurchaseIntentRequest = new AgenticPurchaseIntentCreateRequest
            {
                NetworkTokenId = "nt_e7fjr77crbgmlhpjvuq3bj6jba",
                Device = new DeviceInfo
                {
                    IpAddress = "80.112.12.34", // European IP
                    UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36"
                },
                CustomerPrompt = "Looking for luxury items in different currencies.",
                Mandates = new[]
                {
                    new Mandate
                    {
                        Id = "mandate_international",
                        PurchaseThreshold = new PurchaseThreshold
                        {
                            Amount = 200000,
                            CurrencyCode = Currency.EUR
                        },
                        Description = "International luxury purchases.",
                        ExpirationDate = DateTime.Parse("2026-12-31T23:59:59.000Z")
                    }
                }
            };

            var purchaseIntentResponse = await DefaultApi.AgenticClient().CreatePurchaseIntent(createPurchaseIntentRequest);
            purchaseIntentResponse.ShouldNotBeNull();

            // Create credentials with international transaction data
            var createCredentialsRequest = new AgenticPurchaseIntentCredentialsCreateRequest
            {
                TransactionData = new[]
                {
                    new TransactionData
                    {
                        MerchantCountryCode = CountryCode.FR,
                        MerchantName = "Luxury Paris Boutique",
                        MerchantCategoryCode = "5944",
                        MerchantUrl = "https://luxuryparis.fr",
                        TransactionAmount = new TransactionAmount
                        {
                            Amount = 89999,
                            CurrencyCode = Currency.EUR
                        }
                    },
                    new TransactionData
                    {
                        MerchantCountryCode = CountryCode.GB,
                        MerchantName = "London Fashion House",
                        MerchantCategoryCode = "5651",
                        MerchantUrl = "https://londonfashion.co.uk",
                        TransactionAmount = new TransactionAmount
                        {
                            Amount = 45000,
                            CurrencyCode = Currency.GBP
                        }
                    }
                }
            };

            var credentialsResponse = await DefaultApi.AgenticClient().CreatePurchaseIntentCredentials(
                purchaseIntentResponse.Id, createCredentialsRequest);

            credentialsResponse.ShouldNotBeNull();
            credentialsResponse.Id.ShouldBe(purchaseIntentResponse.Id);
            credentialsResponse.Status.ShouldNotBeNullOrEmpty();
            credentialsResponse.TokenId.ShouldNotBeNullOrEmpty();
            
            // Validate international handling
            credentialsResponse.Id.ShouldStartWith("pi_");
            credentialsResponse.Links.ShouldNotBeNull();
            credentialsResponse.Links["self"].Href.ShouldContain(purchaseIntentResponse.Id);
        }

        [Fact(Skip = "This test is unsupported currently, not ready to test in the sandbox")]
        private async Task CreatePurchaseIntentCredentialsShouldHandleMinimalTransactionData()
        {
            // Create purchase intent first
            var createPurchaseIntentRequest = new AgenticPurchaseIntentCreateRequest
            {
                NetworkTokenId = "nt_e7fjr77crbgmlhpjvuq3bj6jba",
                Device = new DeviceInfo
                {
                    IpAddress = "10.0.0.1"
                },
                CustomerPrompt = "Basic purchase for minimal test"
            };

            var purchaseIntentResponse = await DefaultApi.AgenticClient().CreatePurchaseIntent(createPurchaseIntentRequest);
            purchaseIntentResponse.ShouldNotBeNull();

            // Create credentials with minimal transaction data
            var createCredentialsRequest = new AgenticPurchaseIntentCredentialsCreateRequest
            {
                TransactionData = new[]
                {
                    new TransactionData
                    {
                        MerchantName = "Basic Store",
                        TransactionAmount = new TransactionAmount
                        {
                            Amount = 1000,
                            CurrencyCode = Currency.USD
                        }
                    }
                }
            };

            var credentialsResponse = await DefaultApi.AgenticClient().CreatePurchaseIntentCredentials(
                purchaseIntentResponse.Id, createCredentialsRequest);

            credentialsResponse.ShouldNotBeNull();
            credentialsResponse.Id.ShouldBe(purchaseIntentResponse.Id);
            credentialsResponse.Status.ShouldNotBeNullOrEmpty();
            credentialsResponse.TokenId.ShouldNotBeNullOrEmpty();
            credentialsResponse.Id.ShouldStartWith("pi_");
        }

        [Fact(Skip = "This test is unsupported currently, not ready to test in the sandbox")]
        private async Task UpdatePurchaseIntentShouldUpdatePurchaseIntent()
        {
            // First create a purchase intent to update
            var createPurchaseIntentRequest = new AgenticPurchaseIntentCreateRequest
            {
                NetworkTokenId = "nt_e7fjr77crbgmlhpjvuq3bj6jba",
                Device = new DeviceInfo
                {
                    IpAddress = "192.168.1.100",
                    UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36",
                    DeviceBrand = "apple",
                    DeviceType = "tablet"
                },
                CustomerPrompt = "I'm looking for running shoes in a size 10, for under $150.",
                Mandates = new[]
                {
                    new Mandate
                    {
                        Id = "mandate_original_123",
                        PurchaseThreshold = new PurchaseThreshold
                        {
                            Amount = 15000,
                            CurrencyCode = Currency.USD
                        },
                        Description = "Purchase running shoes in size 10.",
                        ExpirationDate = System.DateTime.Parse("2026-08-31T23:59:59.000Z")
                    }
                }
            };

            var purchaseIntentResponse = await DefaultApi.AgenticClient().CreatePurchaseIntent(createPurchaseIntentRequest);

            purchaseIntentResponse.ShouldNotBeNull();
            purchaseIntentResponse.Id.ShouldNotBeNullOrEmpty();

            // Now update the purchase intent
            var updatePurchaseIntentRequest = new AgenticPurchaseIntentUpdateRequest
            {
                CustomerPrompt = "Updated: I'm looking for Nike running shoes in size 10.5, for under $200.",
                Mandates = new[]
                {
                    new Mandate
                    {
                        Id = "mandate_updated_123",
                        PurchaseThreshold = new PurchaseThreshold
                        {
                            Amount = 20000,
                            CurrencyCode = Currency.USD
                        },
                        Description = "Updated: Purchase Nike running shoes in size 10.5.",
                        ExpirationDate = System.DateTime.Parse("2026-12-31T23:59:59.000Z")
                    }
                }
            };

            var updateResponse = await DefaultApi.AgenticClient().UpdatePurchaseIntent(purchaseIntentResponse.Id, updatePurchaseIntentRequest);

            updateResponse.ShouldNotBeNull();
            updateResponse.Id.ShouldBe(purchaseIntentResponse.Id);
            updateResponse.CustomerPrompt.ShouldBe(updatePurchaseIntentRequest.CustomerPrompt);
            updateResponse.Status.ShouldNotBeNullOrEmpty();
        }

        [Fact(Skip = "This test is unsupported currently, not ready to test in the sandbox")]
        private async Task UpdatePurchaseIntentShouldHandleNonExistentPurchaseIntent()
        {
            var nonExistentId = "pi_nonexistent_123456";
            var updatePurchaseIntentRequest = new AgenticPurchaseIntentUpdateRequest
            {
                CustomerPrompt = "This should fail"
            };

            var exception = await Should.ThrowAsync<CheckoutApiException>(
                async () => await DefaultApi.AgenticClient().UpdatePurchaseIntent(nonExistentId, updatePurchaseIntentRequest));

            exception.HttpStatusCode.ShouldBeOneOf(HttpStatusCode.NotFound, HttpStatusCode.BadRequest);
        }

        [Fact(Skip = "This test is unsupported currently, not ready to test in the sandbox")]
        private async Task UpdatePurchaseIntentShouldValidateInputParameters()
        {
            // Test with null ID
            var updateRequest = new AgenticPurchaseIntentUpdateRequest
            {
                CustomerPrompt = "Valid prompt"
            };

            var exception = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await DefaultApi.AgenticClient().UpdatePurchaseIntent(null, updateRequest));

            exception.Message.ShouldContain("id");

            // Test with null request
            var validId = "pi_valid_123";
            var nullRequestException = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await DefaultApi.AgenticClient().UpdatePurchaseIntent(validId, null));

            nullRequestException.Message.ShouldContain("agenticPurchaseIntentUpdateRequest");
        }

        [Fact(Skip = "This test is unsupported currently, not ready to test in the sandbox")]
        private async Task UpdatePurchaseIntentShouldHandlePartialUpdates()
        {
            // First create a purchase intent
            var createPurchaseIntentRequest = new AgenticPurchaseIntentCreateRequest
            {
                NetworkTokenId = "nt_e7fjr77crbgmlhpjvuq3bj6jba",
                Device = new DeviceInfo
                {
                    IpAddress = "192.168.1.100",
                    UserAgent = "Mozilla/5.0 Test Browser"
                },
                CustomerPrompt = "Original prompt",
                Mandates = new[]
                {
                    new Mandate
                    {
                        Id = "mandate_original",
                        Description = "Original mandate"
                    }
                }
            };

            var purchaseIntentResponse = await DefaultApi.AgenticClient().CreatePurchaseIntent(createPurchaseIntentRequest);

            // Update only the customer prompt (partial update)
            var partialUpdateRequest = new AgenticPurchaseIntentUpdateRequest
            {
                CustomerPrompt = "Updated prompt only"
                // Mandates is null - testing partial update
            };

            var updateResponse = await DefaultApi.AgenticClient().UpdatePurchaseIntent(
                purchaseIntentResponse.Id, partialUpdateRequest);

            updateResponse.ShouldNotBeNull();
            updateResponse.Id.ShouldBe(purchaseIntentResponse.Id);
            updateResponse.CustomerPrompt.ShouldBe(partialUpdateRequest.CustomerPrompt);
        }

        [Fact(Skip = "This test is unsupported currently, not ready to test in the sandbox")]
        private async Task UpdatePurchaseIntentShouldHandleDifferentPurchaseIntentStates()
        {
            // Create purchase intent
            var createRequest = new AgenticPurchaseIntentCreateRequest
            {
                NetworkTokenId = "nt_e7fjr77crbgmlhpjvuq3bj6jba",
                Device = new DeviceInfo
                {
                    IpAddress = "192.168.1.100",
                    UserAgent = "Mozilla/5.0 State Test"
                },
                CustomerPrompt = "Test different states",
                Mandates = new[]
                {
                    new Mandate
                    {
                        Id = "mandate_state_test",
                        PurchaseThreshold = new PurchaseThreshold
                        {
                            Amount = 10000,
                            CurrencyCode = Currency.USD
                        },
                        Description = "State test mandate"
                    }
                }
            };

            var purchaseIntentResponse = await DefaultApi.AgenticClient().CreatePurchaseIntent(createRequest);

            // Try to update the purchase intent regardless of its current state
            var updateRequest = new AgenticPurchaseIntentUpdateRequest
            {
                CustomerPrompt = "Updated for state testing",
                Mandates = new[]
                {
                    new Mandate
                    {
                        Id = "mandate_updated_state",
                        PurchaseThreshold = new PurchaseThreshold
                        {
                            Amount = 12000,
                            CurrencyCode = Currency.USD
                        },
                        Description = "Updated state test mandate"
                    }
                }
            };

            // This should work regardless of the purchase intent's current state
            var updateResponse = await DefaultApi.AgenticClient().UpdatePurchaseIntent(
                purchaseIntentResponse.Id, updateRequest);

            updateResponse.ShouldNotBeNull();
            updateResponse.Id.ShouldBe(purchaseIntentResponse.Id);
        }

        [Fact(Skip = "This test is unsupported currently, not ready to test in the sandbox")]
        private async Task DeletePurchaseIntentShouldDeletePurchaseIntent()
        {
            // First create a purchase intent to delete
            var createPurchaseIntentRequest = new AgenticPurchaseIntentCreateRequest
            {
                NetworkTokenId = "nt_e7fjr77crbgmlhpjvuq3bj6jba",
                Device = new DeviceInfo
                {
                    IpAddress = "192.168.1.100",
                    UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36"
                },
                CustomerPrompt = "I need running shoes for deletion test.",
                Mandates = new[]
                {
                    new Mandate
                    {
                        Id = "mandate_delete_test",
                        PurchaseThreshold = new PurchaseThreshold
                        {
                            Amount = 10000,
                            CurrencyCode = Currency.USD
                        },
                        Description = "Purchase intent for deletion test.",
                        ExpirationDate = DateTime.Parse("2026-12-31T23:59:59.000Z")
                    }
                }
            };

            var purchaseIntentResponse = await DefaultApi.AgenticClient().CreatePurchaseIntent(createPurchaseIntentRequest);
            purchaseIntentResponse.ShouldNotBeNull();
            purchaseIntentResponse.Id.ShouldNotBeNullOrEmpty();
            purchaseIntentResponse.Id.ShouldStartWith("pi_");

            // Now delete the purchase intent
            var deleteResponse = await DefaultApi.AgenticClient().DeletePurchaseIntent(purchaseIntentResponse.Id);

            deleteResponse.ShouldNotBeNull();
            
            // Validate successful deletion (typically 200 or 204)
            deleteResponse.HttpStatusCode.ShouldBeOneOf(200, 204);
        }

        [Fact(Skip = "This test is unsupported currently, not ready to test in the sandbox")]
        private async Task DeletePurchaseIntentShouldHandleNonExistentPurchaseIntent()
        {
            var nonExistentId = "pi_non_existent_123456789";

            // Attempt to delete non-existent purchase intent should handle gracefully
            var exception = await Should.ThrowAsync<CheckoutApiException>(
                async () => await DefaultApi.AgenticClient().DeletePurchaseIntent(nonExistentId));

            // Should return 404 Not Found for non-existent purchase intent
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact(Skip = "This test is unsupported currently, not ready to test in the sandbox")]
        private async Task DeletePurchaseIntentShouldHandleAlreadyDeletedPurchaseIntent()
        {
            // First create a purchase intent
            var createPurchaseIntentRequest = new AgenticPurchaseIntentCreateRequest
            {
                NetworkTokenId = "nt_e7fjr77crbgmlhpjvuq3bj6jba",
                Device = new DeviceInfo
                {
                    IpAddress = "203.0.113.195",
                    UserAgent = "Test Agent Double Delete"
                },
                CustomerPrompt = "Purchase intent for double deletion test.",
                Mandates = new[]
                {
                    new Mandate
                    {
                        Id = "mandate_double_delete",
                        PurchaseThreshold = new PurchaseThreshold
                        {
                            Amount = 5000,
                            CurrencyCode = Currency.USD
                        },
                        Description = "Double deletion test mandate.",
                        ExpirationDate = DateTime.Parse("2026-12-31T23:59:59.000Z")
                    }
                }
            };

            var purchaseIntentResponse = await DefaultApi.AgenticClient().CreatePurchaseIntent(createPurchaseIntentRequest);
            purchaseIntentResponse.ShouldNotBeNull();

            // Delete it once
            var firstDeleteResponse = await DefaultApi.AgenticClient().DeletePurchaseIntent(purchaseIntentResponse.Id);
            firstDeleteResponse.ShouldNotBeNull();

            // Try to delete it again - should handle gracefully (404 or other appropriate response)
            var exception = await Should.ThrowAsync<CheckoutApiException>(
                async () => await DefaultApi.AgenticClient().DeletePurchaseIntent(purchaseIntentResponse.Id));

            // Should return 404 Not Found for already deleted purchase intent
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact(Skip = "This test is unsupported currently, not ready to test in the sandbox")]
        private async Task DeletePurchaseIntentShouldHandleDifferentPurchaseIntentStates()
        {
            // Test deleting purchase intents in different states
            var testCases = new[]
            {
                new { Description = "Basic purchase intent", CustomerPrompt = "Basic deletion test" },
                new { Description = "Complex purchase intent", CustomerPrompt = "Complex purchase intent with multiple mandates for deletion test" }
            };

            foreach (var testCase in testCases)
            {
                // Create purchase intent
                var createRequest = new AgenticPurchaseIntentCreateRequest
                {
                    NetworkTokenId = "nt_e7fjr77crbgmlhpjvuq3bj6jba",
                    Device = new DeviceInfo
                    {
                        IpAddress = "10.0.0.1",
                        UserAgent = "Test Agent States"
                    },
                    CustomerPrompt = testCase.CustomerPrompt,
                    Mandates = new[]
                    {
                        new Mandate
                        {
                            Id = $"mandate_states_{testCase.Description.Replace(" ", "_").ToLower()}",
                            PurchaseThreshold = new PurchaseThreshold
                            {
                                Amount = 7500,
                                CurrencyCode = Currency.USD
                            },
                            Description = $"Mandate for {testCase.Description}",
                            ExpirationDate = DateTime.Parse("2026-12-31T23:59:59.000Z")
                        }
                    }
                };

                var purchaseIntentResponse = await DefaultApi.AgenticClient().CreatePurchaseIntent(createRequest);
                purchaseIntentResponse.ShouldNotBeNull();
                purchaseIntentResponse.Id.ShouldStartWith("pi_");

                // Delete the purchase intent
                var deleteResponse = await DefaultApi.AgenticClient().DeletePurchaseIntent(purchaseIntentResponse.Id);
                
                deleteResponse.ShouldNotBeNull();
                deleteResponse.HttpStatusCode.ShouldBeOneOf(200, 204);
            }
        }

        [Fact(Skip = "This test is unsupported currently, not ready to test in the sandbox")]
        private async Task DeletePurchaseIntentShouldHandleInvalidIdFormat()
        {
            var invalidIds = new[]
            {
                "invalid_id_format",
                "pi_",
                "not_a_purchase_intent_id",
                "123456789"
            };

            foreach (var invalidId in invalidIds)
            {
                var exception = await Should.ThrowAsync<CheckoutApiException>(
                    async () => await DefaultApi.AgenticClient().DeletePurchaseIntent(invalidId));

                // Should return 400 Bad Request or 404 Not Found for invalid ID format
                exception.HttpStatusCode.ShouldBeOneOf(HttpStatusCode.BadRequest, HttpStatusCode.NotFound);
            }
        }
    }
}