using Checkout.Inventory.Requests;
using Checkout.Inventory.Responses;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Inventory
{
    public class InventoryClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.DefaultOAuth, ValidDefaultSk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.DefaultOAuth);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public InventoryClientTest()
        {
            _sdkCredentials.Setup(c => c.GetSdkAuthorization(SdkAuthorizationType.OAuth))
                .Returns(_authorization);
            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        public async Task AdjustInventory_WhenRequestIsValid_ShouldCallApiClientPost()
        {
            var request = new InventoryAdjustmentRequest { VariantId = "var_123", Delta = -1, Reason = "damaged" };
            var expectedResponse = new InventoryLevels { VariantId = "var_123", OnHand = 9 };

            _apiClient.Setup(c => c.Post<InventoryLevels>(
                    "inventory/adjustments",
                    _authorization,
                    request,
                    CancellationToken.None,
                    null))
                .ReturnsAsync(expectedResponse);

            IInventoryClient client = new InventoryClient(_apiClient.Object, _configuration.Object);
            var response = await client.AdjustInventory(request);

            response.ShouldNotBeNull();
            response.VariantId.ShouldBe("var_123");
        }

        [Fact]
        public async Task AdjustInventory_WhenRequestIsNull_ShouldThrowCheckoutArgumentException()
        {
            IInventoryClient client = new InventoryClient(_apiClient.Object, _configuration.Object);
            await Should.ThrowAsync<CheckoutArgumentException>(
                async () => await client.AdjustInventory(null));
        }

        [Fact]
        public async Task CreateInventoryReservation_WhenRequestIsValid_ShouldCallApiClientPost()
        {
            var request = new InventoryReservationRequest
            {
                OwnerType = "order",
                OwnerReference = "order_123",
                Items = new System.Collections.Generic.List<Checkout.Inventory.Entities.InventoryReservationItem>
                {
                    new Checkout.Inventory.Entities.InventoryReservationItem { VariantId = "var_123", Quantity = 1 }
                }
            };
            var expectedResponse = new InventoryReservation { Id = "rsv_abc" };

            _apiClient.Setup(c => c.Post<InventoryReservation>(
                    "inventory/reservations",
                    _authorization,
                    request,
                    CancellationToken.None,
                    null))
                .ReturnsAsync(expectedResponse);

            IInventoryClient client = new InventoryClient(_apiClient.Object, _configuration.Object);
            var response = await client.CreateInventoryReservation(request);

            response.ShouldNotBeNull();
            response.Id.ShouldBe("rsv_abc");
        }

        [Fact]
        public async Task GetInventoryReservation_ShouldCallApiClientGet()
        {
            var expectedResponse = new InventoryReservation { Id = "rsv_abc" };

            _apiClient.Setup(c => c.Get<InventoryReservation>(
                    "inventory/reservations/rsv_abc",
                    _authorization,
                    CancellationToken.None))
                .ReturnsAsync(expectedResponse);

            IInventoryClient client = new InventoryClient(_apiClient.Object, _configuration.Object);
            var response = await client.GetInventoryReservation("rsv_abc");

            response.Id.ShouldBe("rsv_abc");
        }

        [Fact]
        public async Task CommitInventoryReservation_ShouldCallApiClientPostWithNoBody()
        {
            var expectedResponse = new InventoryReservation { Id = "rsv_abc", State = Checkout.Inventory.Entities.InventoryReservationState.Committed };

            _apiClient.Setup(c => c.Post<InventoryReservation>(
                    "inventory/reservations/rsv_abc/commit",
                    _authorization,
                    null,
                    CancellationToken.None,
                    null))
                .ReturnsAsync(expectedResponse);

            IInventoryClient client = new InventoryClient(_apiClient.Object, _configuration.Object);
            var response = await client.CommitInventoryReservation("rsv_abc");

            response.State.ShouldBe(Checkout.Inventory.Entities.InventoryReservationState.Committed);
        }

        [Fact]
        public async Task ReleaseInventoryReservation_ShouldCallApiClientPostWithNoBody()
        {
            var expectedResponse = new InventoryReservation { Id = "rsv_abc", State = Checkout.Inventory.Entities.InventoryReservationState.Released };

            _apiClient.Setup(c => c.Post<InventoryReservation>(
                    "inventory/reservations/rsv_abc/release",
                    _authorization,
                    null,
                    CancellationToken.None,
                    null))
                .ReturnsAsync(expectedResponse);

            IInventoryClient client = new InventoryClient(_apiClient.Object, _configuration.Object);
            var response = await client.ReleaseInventoryReservation("rsv_abc");

            response.State.ShouldBe(Checkout.Inventory.Entities.InventoryReservationState.Released);
        }

        [Fact]
        public async Task GetInventoryLevels_ShouldCallApiClientQuery()
        {
            var queryFilter = new InventoryLevelsQueryFilter { Expand = "product" };
            var expectedResponse = new InventoryLevels { VariantId = "var_123" };

            _apiClient.Setup(c => c.Query<InventoryLevels>(
                    "inventory/var_123",
                    _authorization,
                    queryFilter,
                    CancellationToken.None))
                .ReturnsAsync(expectedResponse);

            IInventoryClient client = new InventoryClient(_apiClient.Object, _configuration.Object);
            var response = await client.GetInventoryLevels("var_123", queryFilter);

            response.VariantId.ShouldBe("var_123");
        }

        [Fact]
        public async Task SetInventoryLevels_ShouldCallApiClientPut()
        {
            var request = new InventorySetLevelsRequest { OnHand = 50 };
            var expectedResponse = new InventoryLevels { VariantId = "var_123", OnHand = 50 };

            _apiClient.Setup(c => c.Put<InventoryLevels>(
                    "inventory/var_123",
                    _authorization,
                    request,
                    CancellationToken.None,
                    null,
                    null))
                .ReturnsAsync(expectedResponse);

            IInventoryClient client = new InventoryClient(_apiClient.Object, _configuration.Object);
            var response = await client.SetInventoryLevels("var_123", request);

            response.OnHand.ShouldBe(50);
        }

        [Fact]
        public async Task GetInventoryProduct_ShouldCallApiClientGet()
        {
            var expectedResponse = new InventoryProductKnowledge { VariantId = "var_123", Title = "Blue T-Shirt" };

            _apiClient.Setup(c => c.Get<InventoryProductKnowledge>(
                    "inventory/var_123/product",
                    _authorization,
                    CancellationToken.None))
                .ReturnsAsync(expectedResponse);

            IInventoryClient client = new InventoryClient(_apiClient.Object, _configuration.Object);
            var response = await client.GetInventoryProduct("var_123");

            response.Title.ShouldBe("Blue T-Shirt");
        }

        [Fact]
        public async Task SetInventoryProduct_ShouldCallApiClientPut()
        {
            var request = new InventorySetProductRequest
            {
                Title = "Blue T-Shirt",
                Description = "desc",
                ProductUrl = "https://shop.example.com/p",
                ImageUrl = "https://shop.example.com/i.png"
            };
            var expectedResponse = new InventoryProductKnowledge { VariantId = "var_123", Title = "Blue T-Shirt" };

            _apiClient.Setup(c => c.Put<InventoryProductKnowledge>(
                    "inventory/var_123/product",
                    _authorization,
                    request,
                    CancellationToken.None,
                    null,
                    null))
                .ReturnsAsync(expectedResponse);

            IInventoryClient client = new InventoryClient(_apiClient.Object, _configuration.Object);
            var response = await client.SetInventoryProduct("var_123", request);

            response.Title.ShouldBe("Blue T-Shirt");
        }

        [Fact]
        public async Task DeleteInventoryProduct_ShouldCallApiClientDelete()
        {
            var expectedResponse = new EmptyResponse();

            _apiClient.Setup(c => c.Delete<EmptyResponse>(
                    "inventory/var_123/product",
                    _authorization,
                    CancellationToken.None))
                .ReturnsAsync(expectedResponse);

            IInventoryClient client = new InventoryClient(_apiClient.Object, _configuration.Object);
            var response = await client.DeleteInventoryProduct("var_123");

            response.ShouldNotBeNull();
        }
    }
}
