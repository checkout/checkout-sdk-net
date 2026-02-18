using Checkout.Common;
using Checkout.Issuing.ControlGroups.Common;
using Checkout.Issuing.ControlGroups.Requests;
using Checkout.Issuing.ControlGroups.Responses;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Issuing.ControlGroups
{
    public class ControlGroupsClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization =
            new SdkAuthorization(PlatformType.DefaultOAuth, ValidDefaultSk);

        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.DefaultOAuth);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public ControlGroupsClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.OAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        public async Task CreateControlGroup_WhenRequestIsValid_ShouldSucceed()
        {
            // Arrange
            var request = CreateValidControlGroupRequest();
            var expectedResponse = new ControlGroupResponse { Id = "cgr_test_12345" };

            _apiClient.Setup(apiClient =>
                    apiClient.Post<ControlGroupResponse>("issuing/controls/control-groups", _authorization,
                        request, CancellationToken.None, null))
                .ReturnsAsync(() => expectedResponse);

            var client = new IssuingClient(_apiClient.Object, _configuration.Object);

            // Act
            var response = await client.CreateControlGroup(request);

            // Assert
            AssertControlGroupResponse(response, expectedResponse);
        }

        [Fact]
        public async Task GetTargetControlGroups_WhenQueryIsValid_ShouldSucceed()
        {
            // Arrange
            var query = new ControlGroupQueryTarget { TargetId = "crd_test_12345" };
            var expectedResponse = new ControlGroupsResponse 
            { 
                ControlGroups = new List<ControlGroupResponse>
                {
                    new ControlGroupResponse { Id = "cgr_test_12345" }
                }
            };

            _apiClient.Setup(apiClient =>
                    apiClient.Query<ControlGroupsResponse>("issuing/controls/control-groups", _authorization,
                        query, CancellationToken.None))
                .ReturnsAsync(() => expectedResponse);

            var client = new IssuingClient(_apiClient.Object, _configuration.Object);

            // Act
            var response = await client.GetTargetControlGroups(query);

            // Assert
            AssertControlGroupsResponse(response, expectedResponse);
        }

        [Fact]
        public async Task GetControlGroupDetails_WhenIdIsValid_ShouldSucceed()
        {
            // Arrange
            var controlGroupId = "cgr_test_12345";
            var expectedResponse = new ControlGroupResponse { Id = controlGroupId };

            _apiClient.Setup(apiClient =>
                    apiClient.Get<ControlGroupResponse>($"issuing/controls/control-groups/{controlGroupId}",
                        _authorization, CancellationToken.None))
                .ReturnsAsync(() => expectedResponse);

            var client = new IssuingClient(_apiClient.Object, _configuration.Object);

            // Act
            var response = await client.GetControlGroupDetails(controlGroupId);

            // Assert
            AssertControlGroupResponse(response, expectedResponse);
        }

        [Fact]
        public async Task RemoveControlGroup_WhenIdIsValid_ShouldSucceed()
        {
            // Arrange
            var controlGroupId = "cgr_test_12345";
            var expectedResponse = new IdResponse { Id = controlGroupId };

            _apiClient.Setup(apiClient =>
                    apiClient.Delete<IdResponse>($"issuing/controls/control-groups/{controlGroupId}",
                        _authorization, CancellationToken.None))
                .ReturnsAsync(() => expectedResponse);

            var client = new IssuingClient(_apiClient.Object, _configuration.Object);

            // Act
            var response = await client.RemoveControlGroup(controlGroupId);

            // Assert
            AssertIdResponse(response, expectedResponse);
        }

        private static CreateControlGroupRequest CreateValidControlGroupRequest()
        {
            return new CreateControlGroupRequest
            {
                TargetId = "crd_test_12345",
                FailIf = FailIfType.AllFail,
                Description = "Test control group",
                Controls = new List<ControlGroupControl>
                {
                    new MccControlGroupControl
                    {
                        Description = "Test MCC control",
                        MccLimit = new Checkout.Issuing.Common.MccLimit
                        {
                            Type = Checkout.Issuing.Common.LimitControlType.Block,
                            MccList = new List<string> { "5411", "5422" }
                        }
                    }
                }
            };
        }

        private static void AssertControlGroupResponse(ControlGroupResponse actual, ControlGroupResponse expected)
        {
            actual.ShouldNotBeNull();
            actual.ShouldBeSameAs(expected);
            actual.Id.ShouldBe(expected.Id);
        }

        private static void AssertControlGroupsResponse(ControlGroupsResponse actual, ControlGroupsResponse expected)
        {
            actual.ShouldNotBeNull();
            actual.ShouldBeSameAs(expected);
            actual.ControlGroups.ShouldNotBeNull();
        }

        private static void AssertIdResponse(IdResponse actual, IdResponse expected)
        {
            actual.ShouldNotBeNull();
            actual.ShouldBeSameAs(expected);
            actual.Id.ShouldBe(expected.Id);
        }
    }
}