using Checkout.Common;
using Checkout.Issuing.ControlProfiles.Requests;
using Checkout.Issuing.ControlProfiles.Responses;
using Checkout.Payments;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Issuing.ControlProfiles
{
    public class ControlProfilesClientTest : UnitTestFixture
    {
        private readonly SdkAuthorization _authorization =
            new SdkAuthorization(PlatformType.DefaultOAuth, ValidDefaultSk);

        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.DefaultOAuth);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        public ControlProfilesClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.OAuth))
                .Returns(_authorization);

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        private async Task ShouldCreateControlProfile()
        {
            ControlProfileRequest controlProfileRequest = new ControlProfileRequest();
            ControlProfileResponse controlProfileResponse = new ControlProfileResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<ControlProfileResponse>("issuing/controls/control-profiles", _authorization,
                        controlProfileRequest,
                        CancellationToken.None, null))
                .ReturnsAsync(() => controlProfileResponse);

            IIssuingClient client =
                new IssuingClient(_apiClient.Object, _configuration.Object);

            ControlProfileResponse response = await client.CreateControlProfile(controlProfileRequest);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(controlProfileResponse);
        }

        [Fact]
        private async Task ShouldGetAllControlProfiles()
        {
            ControlProfilesResponse controlProfilesResponse = new ControlProfilesResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<ControlProfilesResponse>("issuing/controls/control-profiles", _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => controlProfilesResponse);

            IIssuingClient client =
                new IssuingClient(_apiClient.Object, _configuration.Object);

            ControlProfilesResponse response = await client.GetAllControlProfiles("target_id");

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(controlProfilesResponse);
        }

        [Fact]
        private async Task ShouldGetControlProfileDetails()
        {
            ControlProfileResponse controlProfileResponse = new ControlProfileResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<ControlProfileResponse>("issuing/controls/control-profiles/control_profile_id",
                        _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => controlProfileResponse);

            IIssuingClient client =
                new IssuingClient(_apiClient.Object, _configuration.Object);

            ControlProfileResponse response = await client.GetControlProfileDetails("control_profile_id");

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(controlProfileResponse);
        }

        [Fact]
        private async Task ShouldUpdateControlProfile()
        {
            ControlProfileRequest controlProfileRequest = new ControlProfileRequest();
            ControlProfileResponse controlProfileResponse = new ControlProfileResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Patch<ControlProfileResponse>("issuing/controls/control-profiles/control_profile_id",
                        _authorization,
                        controlProfileRequest,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(() => controlProfileResponse);

            IIssuingClient client =
                new IssuingClient(_apiClient.Object, _configuration.Object);

            ControlProfileResponse response =
                await client.UpdateControlProfile("control_profile_id", controlProfileRequest);

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(controlProfileResponse);
        }

        [Fact]
        private async Task ShouldDeleteControlProfile()
        {
            VoidResponse removeControlProfileResponse = new VoidResponse();

            _apiClient.Setup(apiClient =>
                    apiClient.Delete<VoidResponse>("issuing/controls/control-profiles/control_profile_id",
                        _authorization,
                        CancellationToken.None))
                .ReturnsAsync(() => removeControlProfileResponse);

            IIssuingClient client =
                new IssuingClient(_apiClient.Object, _configuration.Object);

            VoidResponse response = await client.RemoveControlProfile("control_profile_id");

            response.ShouldNotBeNull();
            response.ShouldBeSameAs(removeControlProfileResponse);
        }

        [Fact]
        private async Task ShouldAddTargetToControlProfile()
        {
            Resource response = new Resource();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<Resource>("issuing/controls/control-profiles/control_profile_id/add/target_id",
                        _authorization,
                        null,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(() => response);

            IIssuingClient client =
                new IssuingClient(_apiClient.Object, _configuration.Object);

            Resource result = await client.AddTargetToControlProfile("control_profile_id", "target_id");

            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
        }

        [Fact]
        private async Task ShouldRemoveTargetFromControlProfile()
        {
            Resource response = new Resource();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<Resource>("issuing/controls/control-profiles/control_profile_id/remove/target_id",
                        _authorization,
                        null,
                        CancellationToken.None,
                        null))
                .ReturnsAsync(() => response);

            IIssuingClient client =
                new IssuingClient(_apiClient.Object, _configuration.Object);

            Resource result = await client.RemoveTargetFromControlProfile("control_profile_id", "target_id");

            result.ShouldNotBeNull();
            result.ShouldBeSameAs(response);
        }
    }
}