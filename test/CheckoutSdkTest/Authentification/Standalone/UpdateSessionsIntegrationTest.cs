using Checkout.Authentication.Standalone.Common;
using Checkout.Authentication.Standalone.Common.Responses;
using Checkout.Authentication.Standalone.PUTSessionsIdCollectData.Responses.UpdateASessionResponseOk;
using Checkout.Authentication.Standalone.PUTSessionsIdIssuerFingerprint.Requests.
    UpdateSessionThreeDSMethodCompletionIndicatorRequest;
using Checkout.Authentication.Standalone.PUTSessionsIdIssuerFingerprint.Responses.
    UpdateSessionThreedsMethodCompletionIndicatorResponseOk;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AuthenticationType = Checkout.Authentication.Standalone.Common.AuthenticationType;
using ThreeDsMethodCompletionType =
    Checkout.Authentication.Standalone.PUTSessionsIdIssuerFingerprint.Requests.
    UpdateSessionThreeDSMethodCompletionIndicatorRequest.ThreeDsMethodCompletionType;

namespace Checkout.Authentification.Standalone
{
    public class UpdateSessionsIntegrationTest : AbstractSessionsIntegrationTest
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        private async Task ShouldUpdateCardSessionUsingSessionSecret(bool usingSessionSecret)
        {
            var createSessionResponse = await CreateHostedSession();

            createSessionResponse.ShouldNotBeNull();
            createSessionResponse.Accepted.ShouldNotBeNull();

            var created = createSessionResponse.Accepted;

            created.Id.ShouldNotBeNull();
            created.SessionSecret.ShouldNotBeNull();
            created.TransactionId.ShouldNotBeNull();
            created.Amount.ShouldNotBeNull();
            created.AuthenticationType.ShouldBe(AuthenticationType.Regular);
            created.AuthenticationCategory.ShouldBe(AuthenticationCategoryType.Payment);
            created.NextActions.Count.ShouldBe(1);
            created.NextActions[0].ShouldBe(NextActionsType.RedirectCardholder);
            created.GetSelfLink().ShouldNotBeNull();
            created.GetLink("success_url").ShouldNotBeNull();
            created.GetLink("failure_url").ShouldNotBeNull();
            created.GetLink("redirect_url").ShouldNotBeNull();

            UpdateASessionResponseOk updated = usingSessionSecret
                ? await DefaultApi.AuthenticationClient().UpdateASession(created.SessionSecret, created.Id,
                    BrowserRequest(),
                    CancellationToken.None)
                : await DefaultApi.AuthenticationClient()
                    .UpdateASession(created.Id, BrowserRequest(), CancellationToken.None);

            updated.ShouldNotBeNull();
            updated.HttpStatusCode.ShouldNotBeNull();
            updated.ResponseHeaders.ShouldNotBeNull();
            updated.Id.ShouldNotBeNull();

            if (usingSessionSecret)
                updated.SessionSecret.ShouldBeNull();
            else
                updated.SessionSecret.ShouldNotBeNull();

            updated.Id.ShouldNotBeNull();
            updated.Amount.ShouldNotBeNull();
            updated.Card.ShouldNotBeNull();
            updated.AuthenticationType.ShouldBe(AuthenticationType.Regular);
            updated.AuthenticationCategory.ShouldBe(AuthenticationCategoryType.Payment);
            updated.Status.ShouldBe(StatusType.Approved);
            updated.NextActions.Count.ShouldBe(0);
            updated.GetSelfLink().ShouldNotBeNull();
            updated.GetLink("success_url").ShouldNotBeNull();
            updated.GetLink("failure_url").ShouldNotBeNull();
            updated.GetLink("redirect_url").ShouldBeNull();
        }

        [Fact]
        private async Task ShouldUpdateCardSession()
        {
            var createSessionResponse = await CreateHostedSession();

            createSessionResponse.ShouldNotBeNull();
            createSessionResponse.Accepted.ShouldNotBeNull();

            var created = createSessionResponse.Accepted;

            created.Id.ShouldNotBeNull();
            created.SessionSecret.ShouldNotBeNull();
            created.TransactionId.ShouldNotBeNull();
            created.Amount.ShouldNotBeNull();
            created.Card.ShouldNotBeNull();
            created.AuthenticationType.ShouldBe(AuthenticationType.Regular);
            created.AuthenticationCategory.ShouldBe(AuthenticationCategoryType.Payment);
            created.Status.ShouldBe(StatusType.Pending);
            created.NextActions.Count.ShouldBe(1);
            created.NextActions[0].ShouldBe(NextActionsType.RedirectCardholder);
            created.GetSelfLink().ShouldNotBeNull();
            created.GetLink("success_url").ShouldNotBeNull();
            created.GetLink("failure_url").ShouldNotBeNull();
            created.GetLink("redirect_url").ShouldNotBeNull();

            var updated = await DefaultApi.AuthenticationClient()
                .UpdateASession(created.Id, AppRequest(), CancellationToken.None);

            updated.ShouldNotBeNull();
            updated.Id.ShouldNotBeNull();
            updated.TransactionId.ShouldNotBeNull();
            updated.Amount.ShouldNotBeNull();
            updated.Card.ShouldNotBeNull();
            updated.AuthenticationType.ShouldBe(AuthenticationType.Regular);
            updated.AuthenticationCategory.ShouldBe(AuthenticationCategoryType.Payment);
            updated.Status.ShouldBe(StatusType.Unavailable);
            updated.NextActions.Count.ShouldBe(0);
            updated.GetSelfLink().ShouldNotBeNull();
            updated.GetLink("success_url").ShouldNotBeNull();
            updated.GetLink("failure_url").ShouldNotBeNull();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        private async Task ShouldUpdate3dsMethodCompletionIndicator(bool usingSessionSecret)
        {
            var createSessionResponse = await CreateHostedSession();

            createSessionResponse.ShouldNotBeNull();
            createSessionResponse.Accepted.ShouldNotBeNull();

            var created = createSessionResponse.Accepted;

            var threeDsMethodCompletionRequest = new UpdateSessionThreedsMethodCompletionIndicatorRequest()
            {
                ThreeDsMethodCompletion = ThreeDsMethodCompletionType.Y
            };

            UpdateSessionThreedsMethodCompletionIndicatorResponseOk updated = usingSessionSecret
                ? await DefaultApi.AuthenticationClient().UpdateSessionThreedsMethodCompletionIndicator(created.Id,
                    threeDsMethodCompletionRequest, CancellationToken.None)
                : await DefaultApi.AuthenticationClient().UpdateSessionThreedsMethodCompletionIndicator(
                    created.SessionSecret, created.Id,
                    threeDsMethodCompletionRequest, CancellationToken.None);

            updated.ShouldNotBeNull();
            updated.HttpStatusCode.ShouldNotBeNull();
            updated.ResponseHeaders.ShouldNotBeNull();
        }
    }
}