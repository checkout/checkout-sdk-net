using Checkout.Authentication;
using Checkout.Authentication.Standalone.Common.Responses;
using Checkout.Authentication.Standalone.Common.Responses.Exemption;
using Checkout.Authentication.Standalone.Common.Responses.SchemeInfo;
using Checkout.Authentication.Standalone.GETSessionsId.Responses.GetSessionDetailsResponseOk;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Source.IdSource;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Source.NetworkTokenSource;
using Checkout.Authentication.Standalone.POSTSessions.Responses.RequestASessionResponseAccepted;
using Checkout.Authentication.Standalone.POSTSessions.Responses.RequestASessionResponseCreated;
using Checkout.Authentication.Standalone.POSTSessionsIdComplete.Responses.CompleteASessionResponseNoContent;
using Checkout.Authentication.Standalone.PUTSessionsIdCollectData.Requests.AppRequest;
using Checkout.Authentication.Standalone.PUTSessionsIdCollectData.Responses.UpdateASessionResponseOk;
using Checkout.Authentication.Standalone.PUTSessionsIdIssuerFingerprint.Requests.UpdateSessionThreeDSMethodCompletionIndicatorRequest;
using Checkout.Authentication.Standalone.PUTSessionsIdIssuerFingerprint.Responses.UpdateSessionThreedsMethodCompletionIndicatorResponseOk;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Acs = Checkout.Authentication.Standalone.Common.Responses.Acs.Acs;

namespace Checkout.Authentification.Standalone
{
    public class AuthenticationClientTest : UnitTestFixture
    {
        private const string Sessions = "sessions";
        private const string CollectData = "collect-data";
        private const string Complete = "complete";
        private const string IssuerFingerprint = "issuer-fingerprint";

        private readonly SdkAuthorization _authorization = new SdkAuthorization(PlatformType.Previous, ValidPreviousPk);
        private readonly Mock<IApiClient> _apiClient = new Mock<IApiClient>();
        private readonly Mock<SdkCredentials> _sdkCredentials = new Mock<SdkCredentials>(PlatformType.Previous);
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<CheckoutConfiguration> _configuration;

        private IAuthenticationClient _authenticationClient;

        private static readonly SdkAuthorization SessionSecretAuthorization =
            new SessionSecretSdkCredentials("sessionSecret").GetSdkAuthorization(SdkAuthorizationType.Custom);

        private static readonly IDictionary<int, Type> SessionResponseMappings = new Dictionary<int, Type>();

        static AuthenticationClientTest()
        {
            SessionResponseMappings[201] = typeof(RequestASessionResponseCreated);
            SessionResponseMappings[202] = typeof(RequestASessionResponseAccepted);
        }

        public AuthenticationClientTest()
        {
            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.OAuth))
                .Returns(_authorization);

            _sdkCredentials.Setup(credentials => credentials.GetSdkAuthorization(SdkAuthorizationType.OAuth))
                .Returns(new SdkAuthorization(PlatformType.DefaultOAuth, string.Empty));

            _configuration = new Mock<CheckoutConfiguration>(_sdkCredentials.Object,
                Environment.Sandbox, _httpClientFactory.Object);
        }

        [Fact]
        private async Task ShouldRequestSessionCreateSessionOkResponse()
        {
            var request = new RequestASessionRequest
            {
                Source = new NetworkTokenSource
                {
                    Token = "token", ExpiryMonth = 12, ExpiryYear = 2030, Name = "name"
                }
            };
            var response = new RequestASessionResponseCreated
            {
                Acs = new Acs { ChallengeCancelReasonCode = "code" },
                SchemeInfo = new Authentication.Standalone.Common.Responses.SchemeInfo.SchemeInfo()
                {
                    Name = NameType.CartesBancaires, Score = "score", Avalgo = "avalgo"
                }
            };

            _apiClient.Setup(apiClient =>
                    apiClient.Post<HttpMetadata>(Sessions,
                        _sdkCredentials.Object.GetSdkAuthorization(SdkAuthorizationType.OAuth),
                        SessionResponseMappings, request,
                        CancellationToken.None, null))
                .ReturnsAsync(() => response);

            _authenticationClient = new AuthenticationClient(_apiClient.Object, _configuration.Object);

            var getSessionResponse = await _authenticationClient.RequestASession(request, CancellationToken.None);

            getSessionResponse.ShouldNotBeNull();
            getSessionResponse.Accepted.ShouldBeNull();
            getSessionResponse.Created.ShouldNotBeNull();
            getSessionResponse.Created.Acs.ChallengeCancelReasonCode.ShouldNotBeNull();
            getSessionResponse.Created.SchemeInfo.ShouldNotBeNull();
            getSessionResponse.Created.SchemeInfo.Name.ShouldNotBeNull();
            getSessionResponse.Created.SchemeInfo.Score.ShouldNotBeNull();
            getSessionResponse.Created.SchemeInfo.Avalgo.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldRequestSessionCreateSessionAcceptedResponse()
        {
            var request = new RequestASessionRequest() { Source = new IdSource { Id = "id" } };
            var response = new RequestASessionResponseAccepted
            {
                AuthenticationDate = DateTime.Now,
                ChallengeIndicator = Authentication.Standalone.Common.Responses.ChallengeIndicatorType
                    .ChallengeRequestedMandate
            };

            _apiClient.Setup(apiClient =>
                    apiClient.Post<HttpMetadata>(Sessions,
                        _sdkCredentials.Object.GetSdkAuthorization(SdkAuthorizationType.OAuth),
                        SessionResponseMappings, request,
                        CancellationToken.None, null))
                .ReturnsAsync(() => response);

            _authenticationClient = new AuthenticationClient(_apiClient.Object, _configuration.Object);

            var getSessionResponse = await _authenticationClient.RequestASession(request, CancellationToken.None);

            getSessionResponse.ShouldNotBeNull();
            getSessionResponse.Accepted.ShouldNotBeNull();
            getSessionResponse.Accepted.AuthenticationDate.ShouldNotBeNull();
            getSessionResponse.Accepted.ChallengeIndicator.ShouldNotBeNull();
            getSessionResponse.Created.ShouldBeNull();
        }

        [Fact]
        private async Task RequestSessionShouldThrowOnNullRequest()
        {
            try
            {
                _authenticationClient = new AuthenticationClient(_apiClient.Object, _configuration.Object);
                await _authenticationClient.RequestASession(null, CancellationToken.None);
            }
            catch (Exception ex)
            {
                ex.ShouldNotBeNull();
                ex.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("requestASessionRequest cannot be null");
            }
        }

        [Fact]
        private async Task ShouldGetSessionDetails()
        {
            var response = new GetSessionDetailsResponseOk
            {
                Exemption =
                    new Authentication.Standalone.Common.Responses.Exemption.Exemption()
                    {
                        Requested = "requested", Applied = AppliedType.None, Code = "code"
                    },
                AuthenticationDate = DateTime.Now,
                FlowType = FlowType.Frictionless,
                ChallengeIndicator =
                    Authentication.Standalone.Common.Responses.ChallengeIndicatorType.ChallengeRequested,
                SchemeInfo = new Authentication.Standalone.Common.Responses.SchemeInfo.SchemeInfo()
                {
                    Name = NameType.CartesBancaires, Score = "score", Avalgo = "avalgo"
                }
            };

            _apiClient.Setup(apiClient =>
                    apiClient.Get<GetSessionDetailsResponseOk>($"{Sessions}/id",
                        _sdkCredentials.Object.GetSdkAuthorization(SdkAuthorizationType.OAuth),
                        CancellationToken.None))
                .ReturnsAsync(() => response);

            _authenticationClient = new AuthenticationClient(_apiClient.Object, _configuration.Object);

            var getSessionResponse = await _authenticationClient.GetSessionDetails("id", CancellationToken.None);

            getSessionResponse.ShouldNotBeNull();
            getSessionResponse.Exemption.ShouldNotBeNull();
            getSessionResponse.AuthenticationDate.ShouldNotBeNull();
            getSessionResponse.FlowType.ShouldNotBeNull();
            getSessionResponse.ChallengeIndicator.ShouldNotBeNull();
            getSessionResponse.SchemeInfo.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldGetSessionDetailsSessionSecret()
        {
            var response = new Mock<GetSessionDetailsResponseOk>();

            _apiClient.Setup(apiClient =>
                    apiClient.Get<GetSessionDetailsResponseOk>($"{Sessions}/id", SessionSecretAuthorization,
                        CancellationToken.None))
                .ReturnsAsync(() => response.Object);

            _authenticationClient = new AuthenticationClient(_apiClient.Object, _configuration.Object);

            var getSessionResponse =
                await _authenticationClient.GetSessionDetails("sessionSecret", "id", CancellationToken.None);

            getSessionResponse.ShouldNotBeNull();
        }

        [Fact]
        private async Task GetSessionDetailsShouldThrowOnNullRequest()
        {
            try
            {
                _authenticationClient = new AuthenticationClient(_apiClient.Object, _configuration.Object);
                await _authenticationClient.GetSessionDetails(null, CancellationToken.None);
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
                _authenticationClient = new AuthenticationClient(_apiClient.Object, _configuration.Object);
                await _authenticationClient.GetSessionDetails(null, null, CancellationToken.None);
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
            var request = new Mock<AppRequest>();
            var response = new Mock<UpdateASessionResponseOk>();

            _apiClient.Setup(apiClient =>
                    apiClient.Put<UpdateASessionResponseOk>($"{Sessions}/id/{CollectData}",
                        _sdkCredentials.Object.GetSdkAuthorization(SdkAuthorizationType.OAuth),
                        request.Object,
                        CancellationToken.None, null))
                .ReturnsAsync(() => response.Object);

            _authenticationClient = new AuthenticationClient(_apiClient.Object, _configuration.Object);

            var getSessionResponse =
                await _authenticationClient.UpdateASession("id", request.Object, CancellationToken.None);

            getSessionResponse.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldUpdateSessionDetailsSessionSecret()
        {
            var request = new Mock<AppRequest>();
            var response = new Mock<UpdateASessionResponseOk>();

            _apiClient.Setup(apiClient =>
                    apiClient.Put<UpdateASessionResponseOk>($"{Sessions}/id/{CollectData}", SessionSecretAuthorization,
                        request.Object,
                        CancellationToken.None, null))
                .ReturnsAsync(() => response.Object);

            _authenticationClient = new AuthenticationClient(_apiClient.Object, _configuration.Object);

            var getSessionResponse =
                await _authenticationClient.UpdateASession("sessionSecret", "id", request.Object,
                    CancellationToken.None);

            getSessionResponse.ShouldNotBeNull();
        }

        [Fact]
        private async Task UpdateSessionShouldThrowOnNullRequest()
        {
            try
            {
                _authenticationClient = new AuthenticationClient(_apiClient.Object, _configuration.Object);
                await _authenticationClient.UpdateASession(null, null, CancellationToken.None);
            }
            catch (Exception ex)
            {
                ex.ShouldNotBeNull();
                ex.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("sessionId cannot be null");
            }

            try
            {
                _authenticationClient = new AuthenticationClient(_apiClient.Object, _configuration.Object);
                await _authenticationClient.UpdateASession("id", null, CancellationToken.None);
            }
            catch (Exception ex)
            {
                ex.ShouldNotBeNull();
                ex.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("updateASessionRequest cannot be null");
            }
        }

        [Fact]
        private async Task UpdateSessionShouldThrowOnNullRequestSessionSecret()
        {
            try
            {
                _authenticationClient = new AuthenticationClient(_apiClient.Object, _configuration.Object);
                await _authenticationClient.UpdateASession("secret", null, null, CancellationToken.None);
            }
            catch (Exception ex)
            {
                ex.ShouldNotBeNull();
                ex.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("sessionId cannot be null");
            }

            try
            {
                _authenticationClient = new AuthenticationClient(_apiClient.Object, _configuration.Object);
                await _authenticationClient.UpdateASession("secret", "id", null, CancellationToken.None);
            }
            catch (Exception ex)
            {
                ex.ShouldNotBeNull();
                ex.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("updateASessionRequest cannot be null");
            }

            try
            {
                _authenticationClient = new AuthenticationClient(_apiClient.Object, _configuration.Object);
                await _authenticationClient.UpdateASession(null, "id", new AppRequest(), CancellationToken.None);
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
            _apiClient.Setup(apiClient =>
                    apiClient.Post<CompleteASessionResponseNoContent>($"{Sessions}/id/{Complete}", _authorization, null,
                        CancellationToken.None, null))
                .ReturnsAsync(() => new CompleteASessionResponseNoContent());

            _authenticationClient = new AuthenticationClient(_apiClient.Object, _configuration.Object);

            var completeSessionResponse = _authenticationClient.CompleteASession("id", CancellationToken.None);
            await completeSessionResponse;
            completeSessionResponse.IsCompleted.ShouldBe(true);
        }

        [Fact]
        private async Task ShouldCompleteSessionSessionSecret()
        {
            _apiClient.Setup(apiClient =>
                    apiClient.Post<CompleteASessionResponseNoContent>($"{Sessions}/id/{Complete}", SessionSecretAuthorization, null,
                        CancellationToken.None, null))
                .ReturnsAsync(() => new CompleteASessionResponseNoContent());

            _authenticationClient = new AuthenticationClient(_apiClient.Object, _configuration.Object);

            var completeSessionResponse =
                _authenticationClient.CompleteASession("sessionSecret", "id", CancellationToken.None);
            await completeSessionResponse;
            completeSessionResponse.IsCompleted.ShouldBe(true);
        }

        [Fact]
        private async Task CompleteSessionShouldThrowOnNullRequest()
        {
            try
            {
                _authenticationClient = new AuthenticationClient(_apiClient.Object, _configuration.Object);
                await _authenticationClient.CompleteASession(null, CancellationToken.None);
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
                _authenticationClient = new AuthenticationClient(_apiClient.Object, _configuration.Object);
                await _authenticationClient.CompleteASession(null, "sessionSecret", CancellationToken.None);
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
            var request = new Mock<UpdateSessionThreedsMethodCompletionIndicatorRequest>();
            var response = new Mock<UpdateSessionThreedsMethodCompletionIndicatorResponseOk>();

            _apiClient.Setup(apiClient =>
                    apiClient.Put<UpdateSessionThreedsMethodCompletionIndicatorResponseOk>($"{Sessions}/id/{IssuerFingerprint}",
                        _sdkCredentials.Object.GetSdkAuthorization(SdkAuthorizationType.OAuth),
                        request.Object,
                        CancellationToken.None, null))
                .ReturnsAsync(() => response.Object);

            _authenticationClient = new AuthenticationClient(_apiClient.Object, _configuration.Object);

            var getSessionResponse =
                await _authenticationClient.UpdateSessionThreedsMethodCompletionIndicator("id", request.Object,
                    CancellationToken.None);

            getSessionResponse.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldUpdate3dsMethodCompletionIndicatorSessionSecret()
        {
            var request = new Mock<UpdateSessionThreedsMethodCompletionIndicatorRequest>();
            var response = new Mock<UpdateSessionThreedsMethodCompletionIndicatorResponseOk>();

            _apiClient.Setup(apiClient =>
                    apiClient.Put<UpdateSessionThreedsMethodCompletionIndicatorResponseOk>($"{Sessions}/id/{IssuerFingerprint}",
                        SessionSecretAuthorization, request.Object,
                        CancellationToken.None, null))
                .ReturnsAsync(() => response.Object);

            _authenticationClient = new AuthenticationClient(_apiClient.Object, _configuration.Object);

            var getSessionResponse = await _authenticationClient.UpdateSessionThreedsMethodCompletionIndicator("sessionSecret",
                "id",
                request.Object,
                CancellationToken.None);

            getSessionResponse.ShouldNotBeNull();
        }

        [Fact]
        private async Task Update3dsMethodCompletionIndicatorShouldThrowOnNullRequest()
        {
            try
            {
                _authenticationClient = new AuthenticationClient(_apiClient.Object, _configuration.Object);
                await _authenticationClient.UpdateSessionThreedsMethodCompletionIndicator(null, null, CancellationToken.None);
            }
            catch (Exception ex)
            {
                ex.ShouldNotBeNull();
                ex.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("sessionId cannot be null");
            }

            try
            {
                _authenticationClient = new AuthenticationClient(_apiClient.Object, _configuration.Object);
                await _authenticationClient.UpdateSessionThreedsMethodCompletionIndicator("id", null, CancellationToken.None);
            }
            catch (Exception ex)
            {
                ex.ShouldNotBeNull();
                ex.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("updateSessionThreedsMethodCompletionIndicatorRequest cannot be null");
            }
        }

        [Fact]
        private async Task Update3dsMethodCompletionIndicatorShouldThrowOnNullRequestSessionSecret()
        {
            try
            {
                _authenticationClient = new AuthenticationClient(_apiClient.Object, _configuration.Object);
                await _authenticationClient.UpdateSessionThreedsMethodCompletionIndicator("sessionSecret", null, null,
                    CancellationToken.None);
            }
            catch (Exception ex)
            {
                ex.ShouldNotBeNull();
                ex.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("sessionId cannot be null");
            }

            try
            {
                _authenticationClient = new AuthenticationClient(_apiClient.Object, _configuration.Object);
                await _authenticationClient.UpdateSessionThreedsMethodCompletionIndicator("sessionSecret", "id", null,
                    CancellationToken.None);
            }
            catch (Exception ex)
            {
                ex.ShouldNotBeNull();
                ex.ShouldBeAssignableTo(typeof(CheckoutArgumentException));
                ex.Message.ShouldBe("updateSessionThreedsMethodCompletionIndicatorRequest cannot be null");
            }

            try
            {
                _authenticationClient = new AuthenticationClient(_apiClient.Object, _configuration.Object);
                await _authenticationClient.UpdateSessionThreedsMethodCompletionIndicator(null, "id",
                    new UpdateSessionThreedsMethodCompletionIndicatorRequest(),
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