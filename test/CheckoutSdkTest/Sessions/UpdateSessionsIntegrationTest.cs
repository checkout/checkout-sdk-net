using Checkout.Sessions.Channel;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Sessions
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
            created.AuthenticationCategory.ShouldBe(Category.Payment);
            created.NextActions.Count.ShouldBe(1);
            created.NextActions[0].ShouldBe(NextAction.RedirectCardholder);
            created.GetSelfLink().ShouldNotBeNull();
            created.GetLink("success_url").ShouldNotBeNull();
            created.GetLink("failure_url").ShouldNotBeNull();
            created.GetLink("redirect_url").ShouldNotBeNull();

            GetSessionResponse updated = usingSessionSecret
                ? await FourApi.SessionsClient().UpdateSession(created.SessionSecret, created.Id, BrowserSession(),
                    CancellationToken.None)
                : await FourApi.SessionsClient().UpdateSession(created.Id, BrowserSession(), CancellationToken.None);

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
            updated.AuthenticationCategory.ShouldBe(Category.Payment);
            updated.Status.ShouldBe(SessionStatus.Approved);
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
            created.AuthenticationCategory.ShouldBe(Category.Payment);
            created.Status.ShouldBe(SessionStatus.Pending);
            created.NextActions.Count.ShouldBe(1);
            created.NextActions[0].ShouldBe(NextAction.RedirectCardholder);
            created.GetSelfLink().ShouldNotBeNull();
            created.GetLink("success_url").ShouldNotBeNull();
            created.GetLink("failure_url").ShouldNotBeNull();
            created.GetLink("redirect_url").ShouldNotBeNull();

            var updated = await FourApi.SessionsClient()
                .UpdateSession(created.Id, AppSession(), CancellationToken.None);

            updated.ShouldNotBeNull();
            updated.Id.ShouldNotBeNull();
            updated.TransactionId.ShouldNotBeNull();
            updated.Amount.ShouldNotBeNull();
            updated.Card.ShouldNotBeNull();
            updated.AuthenticationType.ShouldBe(AuthenticationType.Regular);
            updated.AuthenticationCategory.ShouldBe(Category.Payment);
            updated.Status.ShouldBe(SessionStatus.Challenged);
            updated.NextActions.Count.ShouldBe(1);
            updated.NextActions[0].ShouldBe(NextAction.RedirectCardholder);
            updated.GetSelfLink().ShouldNotBeNull();
            updated.GetLink("success_url").ShouldNotBeNull();
            updated.GetLink("failure_url").ShouldNotBeNull();
            updated.GetLink("redirect_url").ShouldNotBeNull();
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

            var threeDsMethodCompletionRequest = new ThreeDsMethodCompletionRequest()
            {
                ThreeDsMethodCompletion = ThreeDsMethodCompletion.Y
            };

            GetSessionResponseAfterChannelDataSupplied updated = usingSessionSecret
                ? await FourApi.SessionsClient().Update3dsMethodCompletionIndicator(created.Id,
                    threeDsMethodCompletionRequest, CancellationToken.None)
                : await FourApi.SessionsClient().Update3dsMethodCompletionIndicator(created.SessionSecret, created.Id,
                    threeDsMethodCompletionRequest, CancellationToken.None);

            updated.ShouldNotBeNull();
            updated.HttpStatusCode.ShouldNotBeNull();
            updated.ResponseHeaders.ShouldNotBeNull();
        }
    }
}