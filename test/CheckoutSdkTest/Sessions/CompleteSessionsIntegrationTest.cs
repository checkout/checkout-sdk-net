using Shouldly;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace Checkout.Sessions
{
    public class CompleteSessionsIntegrationTest : AbstractSessionsIntegrationTest
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
                    await DefaultApi.SessionsClient().CompleteSession(sessionId);
                }
                else
                {
                    await DefaultApi.SessionsClient().CompleteSession(sessionSecret, sessionId);
                }

                throw new XunitException();
            }
            catch (Exception ex)
            {
                ex.ShouldNotBeNull();
                ex.ShouldBeAssignableTo(typeof(CheckoutApiException));
                var checkoutApiException = ex as CheckoutApiException;
                checkoutApiException.ShouldNotBeNull();
                checkoutApiException.ErrorDetails["error_type"].ShouldBe("operation_not_allowed");
                checkoutApiException.HttpStatusCode.ShouldBe(HttpStatusCode.Forbidden);
            }
        }
    }
}