using Checkout.Common;
using Checkout.Sessions.Channel;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Sessions
{
    public class SessionsClientTest
    {
        private const string Sessions = "sessions";
        private const string CollectData = "collect-data";
        private const string Complete = "complete";
        private const string IssuerFingerprint = "issuer-fingerprint";

        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<CheckoutConfiguration> _configuration;
        private readonly Mock<SdkCredentials> _credentials = new Mock<SdkCredentials>(PlatformType.Default);
        private readonly SdkAuthorization _authorization;
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();

        private SessionsClient _sessionsClient;

        private static SdkAuthorization SessionSecretAuthorization =
            new SessionSecretSdkCredentials("sessionSecret").GetSdkAuthorization(SdkAuthorizationType.Custom);

        private static readonly IDictionary<int, Type> SessionResponseMappings = new Dictionary<int, Type>();

        static SessionsClientTest()
        {
            SessionResponseMappings[201] = typeof(CreateSessionOkResponse);
            SessionResponseMappings[202] = typeof(CreateSessionAcceptedResponse);
        }

        public SessionsClientTest()
        {
            _credentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.OAuth))
                .Returns(_authorization);

            _credentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth))
                .Returns(new SdkAuthorization(PlatformType.FourOAuth, string.Empty));

            _configuration = new Mock<CheckoutConfiguration>(_credentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        private async Task ShouldRequestSessionCreateSessionOkResponse()
        {
            var request = new Mock<SessionRequest>();
            var response = new Mock<CreateSessionOkResponse>();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<Resource>(Sessions, _credentials.Object.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth), SessionResponseMappings, request.Object,
                        CancellationToken.None, null))
                .ReturnsAsync(() => response.Object);

            _sessionsClient = new SessionsClient(_apiClient.Object, _configuration.Object);

            var getSessionResponse = await _sessionsClient.RequestSession(request.Object, CancellationToken.None);

            getSessionResponse.ShouldNotBeNull();
            getSessionResponse.Created.ShouldNotBeNull();
            getSessionResponse.Accepted.ShouldBeNull();
        }

        [Fact]
        private async Task ShouldRequestSessionCreateSessionAcceptedResponse()
        {
            var request = new Mock<SessionRequest>();
            var response = new Mock<CreateSessionAcceptedResponse>();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<Resource>(Sessions, _credentials.Object.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth), SessionResponseMappings, request.Object,
                        CancellationToken.None, null))
                .ReturnsAsync(() => response.Object);

            _sessionsClient = new SessionsClient(_apiClient.Object, _configuration.Object);

            var getSessionResponse = await _sessionsClient.RequestSession(request.Object, CancellationToken.None);

            getSessionResponse.ShouldNotBeNull();
            getSessionResponse.Accepted.ShouldNotBeNull();
            getSessionResponse.Created.ShouldBeNull();
        }

        [Fact]
        private async Task RequestSessionShouldThrowOnNullRequest()
        {
            try
            {
                _sessionsClient = new SessionsClient(_apiClient.Object, _configuration.Object);
                await _sessionsClient.RequestSession(null, CancellationToken.None);
            }
            catch (Exception ex)
            {
                ex.ShouldNotBeNull();
                ex.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("sessionRequest cannot be null");
            }
        }

        [Fact]
        private async Task ShouldGetSessionDetails()
        {
            var response = new Mock<GetSessionResponse>();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<GetSessionResponse>($"{Sessions}/id", _credentials.Object.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth), CancellationToken.None))
                .ReturnsAsync(() => response.Object);

            _sessionsClient = new SessionsClient(_apiClient.Object, _configuration.Object);

            var getSessionResponse = await _sessionsClient.GetSessionDetails("id", CancellationToken.None);

            getSessionResponse.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldGetSessionDetailsSessionSecret()
        {
            var response = new Mock<GetSessionResponse>();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<GetSessionResponse>($"{Sessions}/id", SessionSecretAuthorization, CancellationToken.None))
                .ReturnsAsync(() => response.Object);

            _sessionsClient = new SessionsClient(_apiClient.Object, _configuration.Object);

            var getSessionResponse = await _sessionsClient.GetSessionDetails("sessionSecret", "id", CancellationToken.None);

            getSessionResponse.ShouldNotBeNull();
        }

        [Fact]
        private async Task GetSessionDetailsShouldThrowOnNullRequest()
        {
            try
            {
                _sessionsClient = new SessionsClient(_apiClient.Object, _configuration.Object);
                await _sessionsClient.GetSessionDetails(null, CancellationToken.None);
            }
            catch (Exception ex)
            {
                ex.ShouldNotBeNull();
                ex.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("sessionId cannot be null");
            }
        }

        [Fact]
        private async Task GetSessionDetailsShouldThrowOnSessionSecret()
        {
            try
            {
                _sessionsClient = new SessionsClient(_apiClient.Object, _configuration.Object);
                await _sessionsClient.GetSessionDetails(null, null, CancellationToken.None);
            }
            catch (Exception ex)
            {
                ex.ShouldNotBeNull();
                ex.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("sessionSecret cannot be null");
            }
        }

        [Fact]
        private async Task ShouldUpdateSessionDetails()
        {
            var request = new Mock<AppSession>();
            var response = new Mock<GetSessionResponse>();

            _apiClient.Setup(apiClient =>
                    apiClient.Put<GetSessionResponse>($"{Sessions}/id/{CollectData}", _credentials.Object.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth), request.Object,
                        CancellationToken.None, null))
                .ReturnsAsync(() => response.Object);

            _sessionsClient = new SessionsClient(_apiClient.Object, _configuration.Object);

            var getSessionResponse = await _sessionsClient.UpdateSession("id", request.Object, CancellationToken.None);

            getSessionResponse.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldUpdateSessionDetailsSessionSecret()
        {
            var request = new Mock<AppSession>();
            var response = new Mock<GetSessionResponse>();

            _apiClient.Setup(apiClient =>
                    apiClient.Put<GetSessionResponse>($"{Sessions}/id/{CollectData}", SessionSecretAuthorization, request.Object,
                        CancellationToken.None, null))
                .ReturnsAsync(() => response.Object);

            _sessionsClient = new SessionsClient(_apiClient.Object, _configuration.Object);

            var getSessionResponse = await _sessionsClient.UpdateSession("sessionSecret", "id", request.Object, CancellationToken.None);

            getSessionResponse.ShouldNotBeNull();
        }

        [Fact]
        private async Task UpdateSessionShouldThrowOnNullRequest()
        {
            try
            {
                _sessionsClient = new SessionsClient(_apiClient.Object, _configuration.Object);
                await _sessionsClient.UpdateSession(null, null, CancellationToken.None);
            }
            catch (Exception ex)
            {
                ex.ShouldNotBeNull();
                ex.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("sessionId cannot be null");
            }

            try
            {
                _sessionsClient = new SessionsClient(_apiClient.Object, _configuration.Object);
                await _sessionsClient.UpdateSession("id", null, CancellationToken.None);
            }
            catch (Exception ex)
            {
                ex.ShouldNotBeNull();
                ex.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("channelData cannot be null");
            }
        }

        [Fact]
        private async Task UpdateSessionShouldThrowOnNullRequestSessionSecret()
        {
            try
            {
                _sessionsClient = new SessionsClient(_apiClient.Object, _configuration.Object);
                await _sessionsClient.UpdateSession("secret", null, null, CancellationToken.None);
            }
            catch (Exception ex)
            {
                ex.ShouldNotBeNull();
                ex.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("sessionId cannot be null");
            }

            try
            {
                _sessionsClient = new SessionsClient(_apiClient.Object, _configuration.Object);
                await _sessionsClient.UpdateSession("secret", "id", null, CancellationToken.None);
            }
            catch (Exception ex)
            {
                ex.ShouldNotBeNull();
                ex.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("channelData cannot be null");
            }

            try
            {
                _sessionsClient = new SessionsClient(_apiClient.Object, _configuration.Object);
                await _sessionsClient.UpdateSession(null, "id", new AppSession(), CancellationToken.None);
            }
            catch (Exception ex)
            {
                ex.ShouldNotBeNull();
                ex.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("sessionSecret cannot be null");
            }
        }

        [Fact]
        private async Task ShouldCompleteSession()
        {
            var response = new Mock<GetSessionResponse>();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<object>($"{Sessions}/id/{Complete}", _authorization, null,
                        CancellationToken.None, null))
                .ReturnsAsync(() => response.Object);

            _sessionsClient = new SessionsClient(_apiClient.Object, _configuration.Object);

            var completeSessionResponse = _sessionsClient.CompleteSession("id", CancellationToken.None);
            await completeSessionResponse;
            completeSessionResponse.IsCompleted.ShouldBe(true);
        }

        [Fact]
        private async Task ShouldCompleteSessionSessionSecret()
        {
            var response = new Mock<GetSessionResponse>();

            _apiClient.Setup(apiClient =>
                    apiClient.Post<object>($"{Sessions}/id/{Complete}", SessionSecretAuthorization, null,
                        CancellationToken.None, null))
                .ReturnsAsync(() => response.Object);

            _sessionsClient = new SessionsClient(_apiClient.Object, _configuration.Object);

            var completeSessionResponse = _sessionsClient.CompleteSession("sessionSecret", "id", CancellationToken.None);
            await completeSessionResponse;
            completeSessionResponse.IsCompleted.ShouldBe(true);
        }

        [Fact]
        private async Task CompleteSessionShouldThrowOnNullRequest()
        {
            try
            {
                _sessionsClient = new SessionsClient(_apiClient.Object, _configuration.Object);
                await _sessionsClient.CompleteSession(null, CancellationToken.None);
            }
            catch (Exception ex)
            {
                ex.ShouldNotBeNull();
                ex.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("sessionId cannot be null");
            }
        }

        [Fact]
        private async Task CompleteSessionShouldThrowOnNullRequestSessionSecret()
        {
            try
            {
                _sessionsClient = new SessionsClient(_apiClient.Object, _configuration.Object);
                await _sessionsClient.CompleteSession(null, "sessionSecret", CancellationToken.None);
            }
            catch (Exception ex)
            {
                ex.ShouldNotBeNull();
                ex.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("sessionSecret cannot be null");
            }
        }

        [Fact]
        private async Task ShouldUpdate3dsMethodCompletionIndicator()
        {
            var request = new Mock<ThreeDsMethodCompletionRequest>();
            var response = new Mock<GetSessionResponseAfterChannelDataSupplied>();

            _apiClient.Setup(apiClient =>
                    apiClient.Put<GetSessionResponseAfterChannelDataSupplied>($"{Sessions}/id/{IssuerFingerprint}", _credentials.Object.GetSdkAuthorization(SdkAuthorizationType.SecretKeyOrOAuth), request.Object,
                        CancellationToken.None, null))
                .ReturnsAsync(() => response.Object);

            _sessionsClient = new SessionsClient(_apiClient.Object, _configuration.Object);

            var getSessionResponse = await _sessionsClient.Update3dsMethodCompletionIndicator("id", request.Object, CancellationToken.None);

            getSessionResponse.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldUpdate3dsMethodCompletionIndicatorSessionSecret()
        {
            var request = new Mock<ThreeDsMethodCompletionRequest>();
            var response = new Mock<GetSessionResponseAfterChannelDataSupplied>();

            _apiClient.Setup(apiClient =>
                    apiClient.Put<GetSessionResponseAfterChannelDataSupplied>($"{Sessions}/id/{IssuerFingerprint}", SessionSecretAuthorization, request.Object,
                        CancellationToken.None, null))
                .ReturnsAsync(() => response.Object);

            _sessionsClient = new SessionsClient(_apiClient.Object, _configuration.Object);

            var getSessionResponse = await _sessionsClient.Update3dsMethodCompletionIndicator("sessionSecret", "id", request.Object,
                CancellationToken.None);

            getSessionResponse.ShouldNotBeNull();
        }

        [Fact]
        private async Task Update3dsMethodCompletionIndicatorShouldThrowOnNullRequest()
        {
            try
            {
                _sessionsClient = new SessionsClient(_apiClient.Object, _configuration.Object);
                await _sessionsClient.Update3dsMethodCompletionIndicator(null, null, CancellationToken.None);
            }
            catch (Exception ex)
            {
                ex.ShouldNotBeNull();
                ex.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("sessionId cannot be null");
            }

            try
            {
                _sessionsClient = new SessionsClient(_apiClient.Object, _configuration.Object);
                await _sessionsClient.Update3dsMethodCompletionIndicator("id", null, CancellationToken.None);
            }
            catch (Exception ex)
            {
                ex.ShouldNotBeNull();
                ex.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("threeDsMethodCompletionRequest cannot be null");
            }
        }

        [Fact]
        private async Task Update3dsMethodCompletionIndicatorShouldThrowOnNullRequestSessionSecret()
        {
            try
            {
                _sessionsClient = new SessionsClient(_apiClient.Object, _configuration.Object);
                await _sessionsClient.Update3dsMethodCompletionIndicator("sessionSecret", null, null, CancellationToken.None);
            }
            catch (Exception ex)
            {
                ex.ShouldNotBeNull();
                ex.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("sessionId cannot be null");
            }

            try
            {
                _sessionsClient = new SessionsClient(_apiClient.Object, _configuration.Object);
                await _sessionsClient.Update3dsMethodCompletionIndicator("sessionSecret", "id", null, CancellationToken.None);
            }
            catch (Exception ex)
            {
                ex.ShouldNotBeNull();
                ex.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("threeDsMethodCompletionRequest cannot be null");
            }

            try
            {
                _sessionsClient = new SessionsClient(_apiClient.Object, _configuration.Object);
                await _sessionsClient.Update3dsMethodCompletionIndicator(null, "id", new ThreeDsMethodCompletionRequest(),
                    CancellationToken.None);
            }
            catch (Exception ex)
            {
                ex.ShouldNotBeNull();
                ex.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("sessionSecret cannot be null");
            }
        }
    }
}