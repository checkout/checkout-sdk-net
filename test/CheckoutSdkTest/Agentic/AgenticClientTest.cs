using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Shouldly;
using Xunit;
using Checkout.Agentic.Requests;
using Checkout.Agentic.Responses;
using Checkout.Common;

namespace Checkout.Agentic
{
    public class AgenticClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Default, ValidDefaultSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Default);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public AgenticClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.OAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
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
                    IpAddress = "192.168.1.1",
                    UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36"
                },
                Customer = new AgenticCustomer
                {
                    Email = "test@example.com",
                    CountryCode = CountryCode.US,
                    LanguageCode = "en"
                }
            };

            var expectedResponse = new AgenticEnrollResponse
            {
                TokenId = "nt_e7fjr77crbgmlhpjvuq3bj6jba",
                Status = "enrolled",
                CreatedAt = System.DateTime.UtcNow
            };

            _apiClient.Setup(apiClient =>
                    apiClient.Post<AgenticEnrollResponse>("agentic/enroll", _authorization, agenticEnrollRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => expectedResponse);

            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            var response = await client.Enroll(agenticEnrollRequest, CancellationToken.None);

            response.ShouldNotBeNull();
            response.TokenId.ShouldBe(expectedResponse.TokenId);
            response.Status.ShouldBe(expectedResponse.Status);
            response.CreatedAt.ShouldBe(expectedResponse.CreatedAt);
        }

        [Fact]
        private async Task EnrollShouldThrowExceptionWhenEnrollRequestIsNull()
        {
            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            var exception = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.Enroll(null, CancellationToken.None));

            exception.Message.ShouldContain("agenticEnrollRequest");
        }

        [Fact]
        private async Task EnrollShouldCallCorrectEnrollEndpoint()
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
                    IpAddress = "192.168.1.1",
                    UserAgent = "Mozilla/5.0 Test"
                },
                Customer = new AgenticCustomer
                {
                    Email = "test@example.com",
                    CountryCode = CountryCode.US,
                    LanguageCode = "en"
                }
            };

            _apiClient.Setup(apiClient =>
                    apiClient.Post<AgenticEnrollResponse>("agentic/enroll", _authorization, agenticEnrollRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => new AgenticEnrollResponse());

            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            await client.Enroll(agenticEnrollRequest, CancellationToken.None);

            _apiClient.Verify(apiClient =>
                apiClient.Post<AgenticEnrollResponse>("agentic/enroll", _authorization, agenticEnrollRequest,
                    CancellationToken.None, null), Times.Once);
        }

        [Fact]
        private async Task EnrollShouldUseCorrectAuthorizationForEnroll()
        {
            var agenticEnrollRequest = new AgenticEnrollRequest
            {
                Source = new PaymentSource { Type = PaymentSourceType.Card },
                Device = new DeviceInfo(),
                Customer = new AgenticCustomer { Email = "test@example.com", CountryCode = CountryCode.US, LanguageCode = "en" }
            };

            _apiClient.Setup(apiClient =>
                    apiClient.Post<AgenticEnrollResponse>(It.IsAny<string>(), It.IsAny<SdkAuthorization>(), 
                        It.IsAny<AgenticEnrollRequest>(), It.IsAny<CancellationToken>(), It.IsAny<string>()))
                .ReturnsAsync(() => new AgenticEnrollResponse());

            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            await client.Enroll(agenticEnrollRequest, CancellationToken.None);

            _apiClient.Verify(apiClient =>
                apiClient.Post<AgenticEnrollResponse>(It.IsAny<string>(), _authorization, 
                    It.IsAny<AgenticEnrollRequest>(), It.IsAny<CancellationToken>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        private async Task CreatePurchaseIntentShouldCreatePurchaseIntent()
        {
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
                        Id = "mandate_123",
                        PurchaseThreshold = new PurchaseThreshold
                        {
                            Amount = 100,
                            CurrencyCode = Currency.USD
                        },
                        Description = "Purchase running shoes in size 10.",
                        ExpirationDate = System.DateTime.Parse("2026-08-31T23:59:59.000Z")
                    }
                }
            };

            var expectedResponse = new AgenticPurchaseIntentResponse
            {
                Id = "pi_f3egwppx6rde3hg6itlqzp3h7e",
                Scheme = "visa",
                Status = "active",
                TokenId = "nt_e7fjr77crbgmlhpjvuq3bj6jba",
                DeviceData = new DeviceInfo
                {
                    IpAddress = "192.168.1.100",
                    UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36",
                    DeviceBrand = "apple",
                    DeviceType = "tablet"
                },
                CustomerPrompt = "Hey AI, I need Nike running shoes in size 10 under $130.00",
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
                        Description = "Purchase Nike Air Max 270 running shoes in size 10",
                        ExpirationDate = System.DateTime.Parse("2026-08-31T23:59:59.000Z")
                    }
                },
                Links = new Dictionary<string, Link>
                {
                    { "self", new Link { Href = "https://api.example.com/agentic/purchase-intents/intent_789" } },
                    { "create-credentials", new Link { Href = "https://api.example.com/agentic/purchase-intents/intent_789/credentials" } },
                    { "update", new Link { Href = "https://api.example.com/agentic/purchase-intents/intent_789" } },
                    { "cancel", new Link { Href = "https://api.example.com/agentic/purchase-intents/intent_789/cancel" } }
                }
            };

            _apiClient.Setup(apiClient =>
                    apiClient.Post<AgenticPurchaseIntentResponse>("agentic/purchase-intent", _authorization, createPurchaseIntentRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => expectedResponse);

            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            var response = await client.CreatePurchaseIntent(createPurchaseIntentRequest, CancellationToken.None);

            response.ShouldNotBeNull();
            response.Id.ShouldBe(expectedResponse.Id);
            response.Scheme.ShouldBe(expectedResponse.Scheme);
            response.Status.ShouldBe(expectedResponse.Status);
            response.TokenId.ShouldBe(expectedResponse.TokenId);
            response.CustomerPrompt.ShouldBe(expectedResponse.CustomerPrompt);
            response.Links.ShouldNotBeNull();
            response.Links["self"].Href.ShouldBe(expectedResponse.Links["self"].Href);
        }

        [Fact]
        private async Task CreatePurchaseIntentShouldThrowExceptionWhenCreatePurchaseIntentRequestIsNull()
        {
            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            var exception = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.CreatePurchaseIntent(null, CancellationToken.None));

            exception.Message.ShouldContain("agenticPurchaseIntentCreateRequest");
        }

        [Fact]
        private async Task CreatePurchaseIntentShouldCallCorrectCreatePurchaseIntentEndpoint()
        {
            var createPurchaseIntentRequest = new AgenticPurchaseIntentCreateRequest
            {
                NetworkTokenId = "nt_test_123",
                Device = new DeviceInfo
                {
                    IpAddress = "192.168.1.100",
                    UserAgent = "Mozilla/5.0 Test"
                },
                CustomerPrompt = "Test prompt",
                Mandates = new[]
                {
                    new Mandate
                    {
                        Id = "mandate_test",
                        Description = "Test mandate"
                    }
                }
            };

            _apiClient.Setup(apiClient =>
                    apiClient.Post<AgenticPurchaseIntentResponse>("agentic/purchase-intent", _authorization, createPurchaseIntentRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => new AgenticPurchaseIntentResponse());

            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            await client.CreatePurchaseIntent(createPurchaseIntentRequest, CancellationToken.None);

            _apiClient.Verify(apiClient =>
                apiClient.Post<AgenticPurchaseIntentResponse>("agentic/purchase-intent", _authorization, createPurchaseIntentRequest,
                    CancellationToken.None, null), Times.Once);
        }

        [Fact]
        private async Task CreatePurchaseIntentShouldUseCorrectAuthorizationForCreatePurchaseIntent()
        {
            var createPurchaseIntentRequest = new AgenticPurchaseIntentCreateRequest
            {
                NetworkTokenId = "nt_test_123",
                Device = new DeviceInfo(),
                CustomerPrompt = "Test prompt"
            };

            _apiClient.Setup(apiClient =>
                    apiClient.Post<AgenticPurchaseIntentResponse>(It.IsAny<string>(), It.IsAny<SdkAuthorization>(), 
                        It.IsAny<AgenticPurchaseIntentCreateRequest>(), It.IsAny<CancellationToken>(), It.IsAny<string>()))
                .ReturnsAsync(() => new AgenticPurchaseIntentResponse());

            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            await client.CreatePurchaseIntent(createPurchaseIntentRequest, CancellationToken.None);

            _apiClient.Verify(apiClient =>
                apiClient.Post<AgenticPurchaseIntentResponse>(It.IsAny<string>(), _authorization, 
                    It.IsAny<AgenticPurchaseIntentCreateRequest>(), It.IsAny<CancellationToken>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        private async Task CreatePurchaseIntentCredentialsShouldCreatePurchaseIntentCredentials()
        {
            var purchaseIntentId = "pi_f3egwppx6rde3hg6itlqzp3h7e";
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

            var expectedResponse = new AgenticPurchaseIntentResponse
            {
                Id = purchaseIntentId,
                Scheme = "visa",
                Status = "credentials_created",
                TokenId = "nt_e7fjr77crbgmlhpjvuq3bj6jba",
                DeviceData = new DeviceInfo
                {
                    IpAddress = "192.168.1.100",
                    UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36",
                    DeviceBrand = "apple",
                    DeviceType = "tablet"
                },
                CustomerPrompt = "Hey AI, I need Nike running shoes in size 10 under $130.00",
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
                        Description = "Purchase Nike Air Max 270 running shoes in size 10",
                        ExpirationDate = System.DateTime.Parse("2026-08-31T23:59:59.000Z")
                    }
                },
                Links = new Dictionary<string, Link>
                {
                    { "self", new Link { Href = "https://api.example.com/agentic/purchase-intents/pi_f3egwppx6rde3hg6itlqzp3h7e" } },
                    { "create-credentials", new Link { Href = "https://api.example.com/agentic/purchase-intents/pi_f3egwppx6rde3hg6itlqzp3h7e/credentials" } },
                    { "update", new Link { Href = "https://api.example.com/agentic/purchase-intents/pi_f3egwppx6rde3hg6itlqzp3h7e" } },
                    { "cancel", new Link { Href = "https://api.example.com/agentic/purchase-intents/pi_f3egwppx6rde3hg6itlqzp3h7e/cancel" } }
                }
            };

            _apiClient.Setup(apiClient =>
                    apiClient.Post<AgenticPurchaseIntentResponse>("agentic/purchase-intent/pi_f3egwppx6rde3hg6itlqzp3h7e/credentials", _authorization, createCredentialsRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => expectedResponse);

            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            var response = await client.CreatePurchaseIntentCredentials(purchaseIntentId, createCredentialsRequest, CancellationToken.None);

            response.ShouldNotBeNull();
            response.Id.ShouldBe(expectedResponse.Id);
            response.Scheme.ShouldBe(expectedResponse.Scheme);
            response.Status.ShouldBe(expectedResponse.Status);
            response.TokenId.ShouldBe(expectedResponse.TokenId);
            response.CustomerPrompt.ShouldBe(expectedResponse.CustomerPrompt);
            response.Links.ShouldNotBeNull();
            response.Links["self"].Href.ShouldBe(expectedResponse.Links["self"].Href);
        }

        [Fact]
        private async Task CreatePurchaseIntentCredentialsShouldThrowExceptionWhenIdIsNull()
        {
            var createCredentialsRequest = new AgenticPurchaseIntentCredentialsCreateRequest
            {
                TransactionData = new[]
                {
                    new TransactionData
                    {
                        MerchantName = "Test Store",
                        TransactionAmount = new TransactionAmount { Amount = 1000, CurrencyCode = Currency.USD }
                    }
                }
            };

            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            var exception = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.CreatePurchaseIntentCredentials(null, createCredentialsRequest, CancellationToken.None));

            exception.Message.ShouldContain("id");
        }

        [Fact]
        private async Task CreatePurchaseIntentCredentialsShouldThrowExceptionWhenRequestIsNull()
        {
            var purchaseIntentId = "pi_test_123";

            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            var exception = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.CreatePurchaseIntentCredentials(purchaseIntentId, null, CancellationToken.None));

            exception.Message.ShouldContain("agenticPurchaseIntentCredentialsCreateRequest");
        }

        [Fact]
        private async Task CreatePurchaseIntentCredentialsShouldCallCorrectEndpoint()
        {
            var purchaseIntentId = "pi_test_endpoint_123";
            var createCredentialsRequest = new AgenticPurchaseIntentCredentialsCreateRequest
            {
                TransactionData = new[]
                {
                    new TransactionData
                    {
                        MerchantName = "Test Merchant",
                        MerchantCountryCode = CountryCode.US,
                        MerchantCategoryCode = "5411",
                        TransactionAmount = new TransactionAmount
                        {
                            Amount = 5000,
                            CurrencyCode = Currency.USD
                        }
                    }
                }
            };

            _apiClient.Setup(apiClient =>
                    apiClient.Post<AgenticPurchaseIntentResponse>($"agentic/purchase-intent/{purchaseIntentId}/credentials", _authorization, createCredentialsRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => new AgenticPurchaseIntentResponse());

            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            await client.CreatePurchaseIntentCredentials(purchaseIntentId, createCredentialsRequest, CancellationToken.None);

            _apiClient.Verify(apiClient =>
                apiClient.Post<AgenticPurchaseIntentResponse>($"agentic/purchase-intent/{purchaseIntentId}/credentials", _authorization, createCredentialsRequest,
                    CancellationToken.None, null), Times.Once);
        }

        [Fact]
        private async Task CreatePurchaseIntentCredentialsShouldUseCorrectAuthorization()
        {
            var purchaseIntentId = "pi_auth_test_123";
            var createCredentialsRequest = new AgenticPurchaseIntentCredentialsCreateRequest
            {
                TransactionData = new[]
                {
                    new TransactionData
                    {
                        MerchantName = "Auth Test Store",
                        TransactionAmount = new TransactionAmount { Amount = 2500, CurrencyCode = Currency.EUR }
                    }
                }
            };

            _apiClient.Setup(apiClient =>
                    apiClient.Post<AgenticPurchaseIntentResponse>(It.IsAny<string>(), It.IsAny<SdkAuthorization>(), 
                        It.IsAny<AgenticPurchaseIntentCredentialsCreateRequest>(), It.IsAny<CancellationToken>(), It.IsAny<string>()))
                .ReturnsAsync(() => new AgenticPurchaseIntentResponse());

            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            await client.CreatePurchaseIntentCredentials(purchaseIntentId, createCredentialsRequest, CancellationToken.None);

            _apiClient.Verify(apiClient =>
                apiClient.Post<AgenticPurchaseIntentResponse>(It.IsAny<string>(), _authorization, 
                    It.IsAny<AgenticPurchaseIntentCredentialsCreateRequest>(), It.IsAny<CancellationToken>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        private async Task CreatePurchaseIntentCredentialsShouldHandleMultipleTransactionData()
        {
            var purchaseIntentId = "pi_multi_test_123";
            var createCredentialsRequest = new AgenticPurchaseIntentCredentialsCreateRequest
            {
                TransactionData = new[]
                {
                    new TransactionData
                    {
                        MerchantCountryCode = CountryCode.US,
                        MerchantName = "Electronics Store",
                        MerchantCategoryCode = "5732",
                        MerchantUrl = "https://electronics.example.com",
                        TransactionAmount = new TransactionAmount
                        {
                            Amount = 79999,
                            CurrencyCode = Currency.USD
                        }
                    },
                    new TransactionData
                    {
                        MerchantCountryCode = CountryCode.GB,
                        MerchantName = "UK Fashion Store",
                        MerchantCategoryCode = "5651",
                        MerchantUrl = "https://fashion.co.uk",
                        TransactionAmount = new TransactionAmount
                        {
                            Amount = 4500,
                            CurrencyCode = Currency.GBP
                        }
                    }
                }
            };

            var expectedResponse = new AgenticPurchaseIntentResponse
            {
                Id = purchaseIntentId,
                Status = "credentials_created",
                TokenId = "nt_multi_test_token"
            };

            _apiClient.Setup(apiClient =>
                    apiClient.Post<AgenticPurchaseIntentResponse>($"agentic/purchase-intent/{purchaseIntentId}/credentials", _authorization, createCredentialsRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => expectedResponse);

            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            var response = await client.CreatePurchaseIntentCredentials(purchaseIntentId, createCredentialsRequest, CancellationToken.None);

            response.ShouldNotBeNull();
            response.Id.ShouldBe(expectedResponse.Id);
            response.Status.ShouldBe(expectedResponse.Status);
            response.TokenId.ShouldBe(expectedResponse.TokenId);
        }

        [Fact]
        private async Task UpdatePurchaseIntentShouldUpdatePurchaseIntent()
        {
            var purchaseIntentId = "pi_f3egwppx6rde3hg6itlqzp3h7e";
            var updatePurchaseIntentRequest = new AgenticPurchaseIntentUpdateRequest
            {
                CustomerPrompt = "Updated prompt: I'm looking for Nike running shoes in size 10.5, for under $200.",
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

            var expectedResponse = new AgenticPurchaseIntentResponse
            {
                Id = purchaseIntentId,
                Scheme = "visa",
                Status = "updated",
                TokenId = "nt_e7fjr77crbgmlhpjvuq3bj6jba",
                DeviceData = new DeviceInfo
                {
                    IpAddress = "192.168.1.100",
                    UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36",
                    DeviceBrand = "apple",
                    DeviceType = "tablet"
                },
                CustomerPrompt = "Updated prompt: I'm looking for Nike running shoes in size 10.5, for under $200.",
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
                },
                Links = new Dictionary<string, Link>
                {
                    { "self", new Link { Href = "https://api.example.com/agentic/purchase-intents/pi_f3egwppx6rde3hg6itlqzp3h7e" } },
                    { "create-credentials", new Link { Href = "https://api.example.com/agentic/purchase-intents/pi_f3egwppx6rde3hg6itlqzp3h7e/credentials" } },
                    { "update", new Link { Href = "https://api.example.com/agentic/purchase-intents/pi_f3egwppx6rde3hg6itlqzp3h7e" } },
                    { "cancel", new Link { Href = "https://api.example.com/agentic/purchase-intents/pi_f3egwppx6rde3hg6itlqzp3h7e/cancel" } }
                }
            };

            _apiClient.Setup(apiClient =>
                    apiClient.Put<AgenticPurchaseIntentResponse>($"agentic/purchase-intent/{purchaseIntentId}", _authorization, updatePurchaseIntentRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => expectedResponse);

            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            var response = await client.UpdatePurchaseIntent(purchaseIntentId, updatePurchaseIntentRequest, CancellationToken.None);

            response.ShouldNotBeNull();
            response.Id.ShouldBe(expectedResponse.Id);
            response.Scheme.ShouldBe(expectedResponse.Scheme);
            response.Status.ShouldBe(expectedResponse.Status);
            response.TokenId.ShouldBe(expectedResponse.TokenId);
            response.CustomerPrompt.ShouldBe(expectedResponse.CustomerPrompt);
            response.Links.ShouldNotBeNull();
            response.Links["self"].Href.ShouldBe(expectedResponse.Links["self"].Href);
        }

        [Fact]
        private async Task UpdatePurchaseIntentShouldThrowExceptionWhenIdIsNull()
        {
            var updatePurchaseIntentRequest = new AgenticPurchaseIntentUpdateRequest
            {
                CustomerPrompt = "Test prompt",
                Mandates = new[]
                {
                    new Mandate
                    {
                        Id = "mandate_test",
                        Description = "Test mandate"
                    }
                }
            };

            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            var exception = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.UpdatePurchaseIntent(null, updatePurchaseIntentRequest, CancellationToken.None));

            exception.Message.ShouldContain("id");
        }

        [Fact]
        private async Task UpdatePurchaseIntentShouldThrowExceptionWhenRequestIsNull()
        {
            var purchaseIntentId = "pi_test_123";

            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            var exception = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.UpdatePurchaseIntent(purchaseIntentId, null, CancellationToken.None));

            exception.Message.ShouldContain("agenticPurchaseIntentUpdateRequest");
        }

        [Fact]
        private async Task UpdatePurchaseIntentShouldThrowExceptionWhenIdIsEmpty()
        {
            var updatePurchaseIntentRequest = new AgenticPurchaseIntentUpdateRequest
            {
                CustomerPrompt = "Test prompt"
            };

            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            var exception = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.UpdatePurchaseIntent("", updatePurchaseIntentRequest, CancellationToken.None));

            exception.Message.ShouldContain("id");
        }

        [Fact]
        private async Task UpdatePurchaseIntentShouldCallCorrectEndpoint()
        {
            var purchaseIntentId = "pi_test_update_123";
            var updatePurchaseIntentRequest = new AgenticPurchaseIntentUpdateRequest
            {
                CustomerPrompt = "Test update prompt",
                Mandates = new[]
                {
                    new Mandate
                    {
                        Id = "mandate_test_update",
                        PurchaseThreshold = new PurchaseThreshold
                        {
                            Amount = 15000,
                            CurrencyCode = Currency.EUR
                        },
                        Description = "Test update mandate"
                    }
                }
            };

            _apiClient.Setup(apiClient =>
                    apiClient.Put<AgenticPurchaseIntentResponse>($"agentic/purchase-intent/{purchaseIntentId}", _authorization, updatePurchaseIntentRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => new AgenticPurchaseIntentResponse());

            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            await client.UpdatePurchaseIntent(purchaseIntentId, updatePurchaseIntentRequest, CancellationToken.None);

            _apiClient.Verify(apiClient =>
                apiClient.Put<AgenticPurchaseIntentResponse>($"agentic/purchase-intent/{purchaseIntentId}", _authorization, updatePurchaseIntentRequest,
                    CancellationToken.None, null), Times.Once);
        }

        [Fact]
        private async Task UpdatePurchaseIntentShouldUseCorrectAuthorization()
        {
            var purchaseIntentId = "pi_auth_test_123";
            var updatePurchaseIntentRequest = new AgenticPurchaseIntentUpdateRequest
            {
                CustomerPrompt = "Auth test prompt",
                Mandates = new[]
                {
                    new Mandate
                    {
                        Id = "mandate_auth_test",
                        Description = "Auth test mandate"
                    }
                }
            };

            _apiClient.Setup(apiClient =>
                    apiClient.Put<AgenticPurchaseIntentResponse>(It.IsAny<string>(), It.IsAny<SdkAuthorization>(), 
                        It.IsAny<AgenticPurchaseIntentUpdateRequest>(), It.IsAny<CancellationToken>(), It.IsAny<string>()))
                .ReturnsAsync(() => new AgenticPurchaseIntentResponse());

            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            await client.UpdatePurchaseIntent(purchaseIntentId, updatePurchaseIntentRequest, CancellationToken.None);

            _apiClient.Verify(apiClient =>
                apiClient.Put<AgenticPurchaseIntentResponse>(It.IsAny<string>(), _authorization, 
                    It.IsAny<AgenticPurchaseIntentUpdateRequest>(), It.IsAny<CancellationToken>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        private async Task UpdatePurchaseIntentShouldHandlePartialUpdates()
        {
            var purchaseIntentId = "pi_partial_update_123";
            var updatePurchaseIntentRequest = new AgenticPurchaseIntentUpdateRequest
            {
                CustomerPrompt = "Only updating the customer prompt"
                // Mandates is null - partial update
            };

            var expectedResponse = new AgenticPurchaseIntentResponse
            {
                Id = purchaseIntentId,
                Status = "updated",
                CustomerPrompt = "Only updating the customer prompt",
                TokenId = "nt_partial_update_token"
            };

            _apiClient.Setup(apiClient =>
                    apiClient.Put<AgenticPurchaseIntentResponse>($"agentic/purchase-intent/{purchaseIntentId}", _authorization, updatePurchaseIntentRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => expectedResponse);

            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            var response = await client.UpdatePurchaseIntent(purchaseIntentId, updatePurchaseIntentRequest, CancellationToken.None);

            response.ShouldNotBeNull();
            response.Id.ShouldBe(expectedResponse.Id);
            response.Status.ShouldBe(expectedResponse.Status);
            response.CustomerPrompt.ShouldBe(expectedResponse.CustomerPrompt);
            response.TokenId.ShouldBe(expectedResponse.TokenId);
        }

        [Fact]
        private async Task DeletePurchaseIntentShouldDeletePurchaseIntent()
        {
            var purchaseIntentId = "pi_f3egwppx6rde3hg6itlqzp3h7e";

            var expectedResponse = new EmptyResponse
            {
                HttpStatusCode = 200
            };

            _apiClient.Setup(apiClient =>
                    apiClient.Delete<EmptyResponse>($"agentic/purchase-intent/{purchaseIntentId}", _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => expectedResponse);

            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            var response = await client.DeletePurchaseIntent(purchaseIntentId, CancellationToken.None);

            response.ShouldNotBeNull();
            response.HttpStatusCode.ShouldBe(200);
        }

        [Fact]
        private async Task DeletePurchaseIntentShouldThrowExceptionWhenIdIsNull()
        {
            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            var exception = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.DeletePurchaseIntent(null, CancellationToken.None));

            exception.Message.ShouldContain("id");
        }

        [Fact]
        private async Task DeletePurchaseIntentShouldThrowExceptionWhenIdIsEmpty()
        {
            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            var exception = await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.DeletePurchaseIntent("", CancellationToken.None));

            exception.Message.ShouldContain("id");
        }

        [Fact]
        private async Task DeletePurchaseIntentShouldCallCorrectEndpoint()
        {
            var purchaseIntentId = "pi_test_delete_123";

            _apiClient.Setup(apiClient =>
                    apiClient.Delete<EmptyResponse>($"agentic/purchase-intent/{purchaseIntentId}", _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => new EmptyResponse());

            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            await client.DeletePurchaseIntent(purchaseIntentId, CancellationToken.None);

            _apiClient.Verify(apiClient =>
                apiClient.Delete<EmptyResponse>($"agentic/purchase-intent/{purchaseIntentId}", _authorization,
                    CancellationToken.None), Times.Once);
        }

        [Fact]
        private async Task DeletePurchaseIntentShouldUseCorrectAuthorization()
        {
            var purchaseIntentId = "pi_auth_test_123";

            _apiClient.Setup(apiClient =>
                    apiClient.Delete<EmptyResponse>(It.IsAny<string>(), It.IsAny<SdkAuthorization>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => new EmptyResponse());

            IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

            await client.DeletePurchaseIntent(purchaseIntentId, CancellationToken.None);

            _apiClient.Verify(apiClient =>
                apiClient.Delete<EmptyResponse>(It.IsAny<string>(), _authorization,
                    It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        private async Task DeletePurchaseIntentShouldHandleDifferentPurchaseIntentIdFormats()
        {
            var testIds = new[]
            {
                "pi_short123",
                "pi_f3egwppx6rde3hg6itlqzp3h7e",
                "pi_very_long_purchase_intent_id_with_underscores_123456789"
            };

            foreach (var purchaseIntentId in testIds)
            {
                _apiClient.Setup(apiClient =>
                        apiClient.Delete<EmptyResponse>($"agentic/purchase-intent/{purchaseIntentId}", _authorization,
                            CancellationToken.None))
                    .ReturnsAsync(() => new EmptyResponse());

                IAgenticClient client = new AgenticClient(_apiClient.Object, _configuration.Object);

                var response = await client.DeletePurchaseIntent(purchaseIntentId, CancellationToken.None);

                response.ShouldNotBeNull();
                
                _apiClient.Verify(apiClient =>
                    apiClient.Delete<EmptyResponse>($"agentic/purchase-intent/{purchaseIntentId}", _authorization,
                        CancellationToken.None), Times.Once);
            }
        }
    }
}