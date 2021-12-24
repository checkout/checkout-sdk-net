using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Sessions
{
    public class CompleteSessionsTestIT : AbstractSessionsTestIT
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        private async Task ShouldTryToCompleteCardSessionBrowserSession(bool usingSessionSecret)
        {
            var sessionResponse = await CreateHostedSession();
            sessionResponse.ShouldNotBeNull();
            sessionResponse.Accepted.ShouldNotBeNull();

            var created = sessionResponse.Accepted;

            created.Id.ShouldNotBeNull();
            created.SessionSecret.ShouldNotBeNull();
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

            await TryComplete(usingSessionSecret, created.Id, created.SessionSecret);
        }

        private async Task TryComplete(bool usingSessionSecret, string sessionId, string sessionSecret)
        {
            try
            {
                if (!usingSessionSecret)
                {
                    await FourApi.SessionsClient().CompleteSession(sessionId);
                }
                else
                {
                    await FourApi.SessionsClient().CompleteSession(sessionSecret, sessionId);
                }
            }
            catch (Exception ex)
            {
                ex.ShouldNotBeNull();
                ex.ShouldBeAssignableTo(typeof(CheckoutApiException));
                var checkoutApiException = ex as CheckoutApiException;
                checkoutApiException.ErrorDetails["error_type"].ToString().ShouldBe("operation_not_allowed");
                checkoutApiException.HttpStatusCode.ShouldBe(System.Net.HttpStatusCode.Forbidden);
            }
        }
    }
}