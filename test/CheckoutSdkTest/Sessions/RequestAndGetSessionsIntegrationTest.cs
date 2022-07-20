using Checkout.Common;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Sessions
{
    public class RequestAndGetSessionsIntegrationTest : AbstractSessionsIntegrationTest
    {
        [Theory]
        [MemberData(nameof(SessionsTypes))]
        private async Task ShouldRequestAndGetCardSessionBrowserSession(Category category,
            ChallengeIndicatorType challengeIndicator,
            TransactionType transactionType)
        {
            var browserSession = BrowserSession();
            var sessionResponse =
                await CreateNonHostedSession(browserSession, category, challengeIndicator, transactionType);

            sessionResponse.ShouldNotBeNull();
            sessionResponse.Created.ShouldNotBeNull();

            var response = sessionResponse.Created;
            response.Id.ShouldNotBeNull();
            response.SessionSecret.ShouldNotBeNull();
            response.TransactionId.ShouldNotBeNull();
            response.Amount.ShouldNotBeNull();
            response.Certificates.ShouldNotBeNull();
            response.Ds.ShouldNotBeNull();
            response.Acs.ShouldNotBeNull();
            response.Card.ShouldNotBeNull();

            response.AuthenticationType.ShouldBe(AuthenticationType.Regular);
            response.AuthenticationCategory.ShouldBe(category);
            response.Status.ShouldBe(SessionStatus.Challenged);
            response.NextActions.Count.ShouldBe(1);
            response.NextActions[0].ShouldBe(NextAction.ChallengeCardHolder);
            response.TransactionType.ShouldBe(transactionType);
            response.ResponseCode.ShouldBe(ResponseCode.C);
            response.AuthenticationDate.ShouldNotBeNull();

            response.GetSelfLink().ShouldNotBeNull();
            response.GetLink("callback_url").ShouldNotBeNull();
            response.Completed.ShouldBe(false);

            var getSessionResponse = await DefaultApi.SessionsClient().GetSessionDetails(response.Id);

            getSessionResponse.ShouldNotBeNull();

            getSessionResponse.Id.ShouldNotBeNull();
            getSessionResponse.SessionSecret.ShouldNotBeNull();
            getSessionResponse.TransactionId.ShouldNotBeNull();
            getSessionResponse.Amount.ShouldNotBeNull();
            getSessionResponse.Certificates.ShouldNotBeNull();
            getSessionResponse.Ds.ShouldNotBeNull();
            getSessionResponse.Card.ShouldNotBeNull();

            getSessionResponse.AuthenticationType.ShouldBe(AuthenticationType.Regular);
            getSessionResponse.AuthenticationCategory.ShouldBe(category);
            getSessionResponse.Status.ShouldBe(SessionStatus.Challenged);
            getSessionResponse.NextActions.Count.ShouldBe(1);
            getSessionResponse.NextActions[0].ShouldBe(NextAction.ChallengeCardHolder);
            getSessionResponse.TransactionType.ShouldBe(transactionType);
            getSessionResponse.ResponseCode.ShouldBe(ResponseCode.C);
            response.AuthenticationDate.ShouldNotBeNull();

            getSessionResponse.GetSelfLink().ShouldNotBeNull();
            getSessionResponse.GetLink("callback_url").ShouldNotBeNull();
            getSessionResponse.Completed.ShouldBe(false);

            var getSessionSecretSessionResponse =
                await DefaultApi.SessionsClient().GetSessionDetails(response.SessionSecret, response.Id);

            getSessionSecretSessionResponse.Certificates.ShouldBeNull();
            getSessionSecretSessionResponse.SessionSecret.ShouldBeNull();

            getSessionSecretSessionResponse.Id.ShouldNotBeNull();
            getSessionSecretSessionResponse.TransactionId.ShouldNotBeNull();
            getSessionSecretSessionResponse.Amount.ShouldNotBeNull();
            getSessionSecretSessionResponse.Ds.ShouldNotBeNull();
            getSessionSecretSessionResponse.Acs.ShouldNotBeNull();
            getSessionSecretSessionResponse.Card.ShouldNotBeNull();

            getSessionSecretSessionResponse.AuthenticationType.ShouldBe(AuthenticationType.Regular);
            getSessionSecretSessionResponse.AuthenticationCategory.ShouldBe(category);
            getSessionSecretSessionResponse.Status.ShouldBe(SessionStatus.Challenged);
            getSessionSecretSessionResponse.NextActions.Count.ShouldBe(1);
            getSessionSecretSessionResponse.NextActions[0].ShouldBe(NextAction.ChallengeCardHolder);
            getSessionSecretSessionResponse.TransactionType.ShouldBe(transactionType);
            getSessionSecretSessionResponse.ResponseCode.ShouldBe(ResponseCode.C);

            getSessionResponse.GetSelfLink().ShouldNotBeNull();
            getSessionResponse.GetLink("callback_url").ShouldNotBeNull();
            getSessionResponse.Completed.ShouldBe(false);
        }

        [Theory]
        [MemberData(nameof(SessionsTypes))]
        private async Task ShouldRequestAndGetCardSessionAppSession(Category category,
            ChallengeIndicatorType challengeIndicator,
            TransactionType transactionType)
        {
            var appSession = AppSession();

            var sessionResponse =
                await CreateNonHostedSession(appSession, category, challengeIndicator, transactionType);

            sessionResponse.ShouldNotBeNull();
            sessionResponse.Created.ShouldNotBeNull();

            var response = sessionResponse.Created;
            response.Id.ShouldNotBeNull();
            response.SessionSecret.ShouldNotBeNull();
            response.TransactionId.ShouldNotBeNull();
            response.Amount.ShouldNotBeNull();
            response.Certificates.ShouldNotBeNull();
            response.Ds.ShouldNotBeNull();
            response.Card.ShouldNotBeNull();

            response.AuthenticationType.ShouldBe(AuthenticationType.Regular);
            response.AuthenticationCategory.ShouldBe(category);
            response.Status.ShouldBe(SessionStatus.Challenged);
            response.NextActions.Count.ShouldBe(1);
            response.NextActions[0].ShouldBe(NextAction.Authenticate);
            response.TransactionType.ShouldBe(transactionType);

            response.GetSelfLink().ShouldNotBeNull();
            response.GetLink("callback_url").ShouldNotBeNull();
            response.Completed.ShouldBe(false);

            var getSessionResponse = await DefaultApi.SessionsClient().GetSessionDetails(response.Id);

            getSessionResponse.ShouldNotBeNull();

            getSessionResponse.Id.ShouldNotBeNull();
            getSessionResponse.SessionSecret.ShouldNotBeNull();
            getSessionResponse.TransactionId.ShouldNotBeNull();
            getSessionResponse.Amount.ShouldNotBeNull();
            getSessionResponse.Certificates.ShouldNotBeNull();
            getSessionResponse.Ds.ShouldNotBeNull();
            getSessionResponse.Acs.ShouldBeNull();
            getSessionResponse.Card.ShouldNotBeNull();

            getSessionResponse.AuthenticationType.ShouldBe(AuthenticationType.Regular);
            getSessionResponse.AuthenticationCategory.ShouldBe(category);
            getSessionResponse.Status.ShouldBe(SessionStatus.Challenged);
            getSessionResponse.NextActions.Count.ShouldBe(1);
            getSessionResponse.NextActions[0].ShouldBe(NextAction.Authenticate);
            getSessionResponse.TransactionType.ShouldBe(transactionType);
            getSessionResponse.ResponseCode.ShouldBeNull();
            getSessionResponse.AuthenticationDate.ShouldNotBeNull();

            getSessionResponse.GetSelfLink().ShouldNotBeNull();
            getSessionResponse.GetLink("callback_url").ShouldNotBeNull();
            getSessionResponse.Completed.ShouldBe(false);

            var getSessionSecretSessionResponse =
                await DefaultApi.SessionsClient().GetSessionDetails(response.SessionSecret, response.Id);

            getSessionSecretSessionResponse.Certificates.ShouldBeNull();
            getSessionSecretSessionResponse.SessionSecret.ShouldBeNull();

            getSessionSecretSessionResponse.Id.ShouldNotBeNull();
            getSessionSecretSessionResponse.TransactionId.ShouldNotBeNull();
            getSessionSecretSessionResponse.Amount.ShouldNotBeNull();
            getSessionSecretSessionResponse.Ds.ShouldNotBeNull();
            getSessionSecretSessionResponse.Acs.ShouldBeNull();
            getSessionSecretSessionResponse.Card.ShouldNotBeNull();

            getSessionSecretSessionResponse.AuthenticationType.ShouldBe(AuthenticationType.Regular);
            getSessionSecretSessionResponse.AuthenticationCategory.ShouldBe(category);
            getSessionSecretSessionResponse.Status.ShouldBe(SessionStatus.Challenged);
            getSessionSecretSessionResponse.NextActions.Count.ShouldBe(1);
            getSessionSecretSessionResponse.NextActions[0].ShouldBe(NextAction.Authenticate);
            getSessionSecretSessionResponse.TransactionType.ShouldBe(transactionType);
            getSessionSecretSessionResponse.ResponseCode.ShouldBeNull();

            getSessionSecretSessionResponse.GetSelfLink().ShouldNotBeNull();
            getSessionSecretSessionResponse.GetLink("callback_url").ShouldNotBeNull();
            getSessionSecretSessionResponse.Completed.ShouldBe(false);
        }

        public static IEnumerable<object[]> SessionsTypes =>
            new List<object[]>
            {
                new object[] {Category.Payment, ChallengeIndicatorType.NoPreference, TransactionType.GoodsService},
                new object[]
                {
                    Category.NonPayment, ChallengeIndicatorType.ChallengeRequested,
                    TransactionType.CheckAcceptance
                },
                new object[]
                {
                    Category.NonPayment, ChallengeIndicatorType.ChallengeRequestedMandate,
                    TransactionType.AccountFunding
                }
            };
    }
}