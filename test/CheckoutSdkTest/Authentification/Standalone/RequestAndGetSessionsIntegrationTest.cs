using Checkout.Authentication.Standalone.Common;
using Checkout.Authentication.Standalone.Common.Responses;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.ChannelData.AppChannelData;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using ChallengeIndicatorType = Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.ChallengeIndicatorType;

namespace Checkout.Authentification.Standalone
{
    public class RequestAndGetSessionsIntegrationTest : AbstractSessionsIntegrationTest
    {
        [Theory]
        [MemberData(nameof(SessionsTypes))]
        private async Task ShouldRequestAndGetCardSessionBrowserSession(
            AuthenticationCategoryType category,
            ChallengeIndicatorType challengeIndicator,
            TransactionType transactionType)
        {
            var browserSession = BrowserChannelData();
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
            response.Status.ShouldBe(StatusType.Challenged);
            response.NextActions.Count.ShouldBe(1);
            response.NextActions[0].ShouldBe(NextActionsType.ChallengeCardholder);
            response.TransactionType.ShouldBe(transactionType);
            response.ResponseCode.ShouldBe(ResponseCodeType.C);
            response.AuthenticationDate.ShouldNotBeNull();

            response.GetSelfLink().ShouldNotBeNull();
            response.GetLink("callback_url").ShouldNotBeNull();
            response.Completed.ShouldBe(false);

            var getSessionResponse = await DefaultApi.AuthenticationClient().GetSessionDetails(response.Id);

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
            getSessionResponse.Status.ShouldBe(StatusType.Challenged);
            getSessionResponse.NextActions.Count.ShouldBe(1);
            getSessionResponse.NextActions[0].ShouldBe(NextActionsType.ChallengeCardholder);
            getSessionResponse.TransactionType.ShouldBe(transactionType);
            getSessionResponse.ResponseCode.ShouldBe(ResponseCodeType.C);
            response.AuthenticationDate.ShouldNotBeNull();

            getSessionResponse.GetSelfLink().ShouldNotBeNull();
            getSessionResponse.GetLink("callback_url").ShouldNotBeNull();
            getSessionResponse.Completed.ShouldBe(false);

            var getSessionSecretSessionResponse =
                await DefaultApi.AuthenticationClient().GetSessionDetails(response.SessionSecret, response.Id);

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
            getSessionSecretSessionResponse.Status.ShouldBe(StatusType.Challenged);
            getSessionSecretSessionResponse.NextActions.Count.ShouldBe(1);
            getSessionSecretSessionResponse.NextActions[0].ShouldBe(NextActionsType.ChallengeCardholder);
            getSessionSecretSessionResponse.TransactionType.ShouldBe(transactionType);
            getSessionSecretSessionResponse.ResponseCode.ShouldBe(ResponseCodeType.C);

            getSessionResponse.GetSelfLink().ShouldNotBeNull();
            getSessionResponse.GetLink("callback_url").ShouldNotBeNull();
            getSessionResponse.Completed.ShouldBe(false);
        }

        [Theory(Skip = "use on demand")]
        [MemberData(nameof(SessionsTypes))]
        private async Task ShouldRequestAndGetCardSessionAppSession(AuthenticationCategoryType category,
            ChallengeIndicatorType challengeIndicator,
            TransactionType transactionType)
        {
            var appSession = new AppChannelData();

            var sessionResponse =
                await CreateNonHostedSession(appSession, category, challengeIndicator, transactionType);

            sessionResponse.ShouldNotBeNull();
            sessionResponse.Accepted.ShouldNotBeNull();

            var response = sessionResponse.Accepted;
            response.Id.ShouldNotBeNull();
            response.SessionSecret.ShouldNotBeNull();
            response.TransactionId.ShouldNotBeNull();
            response.Amount.ShouldNotBeNull();
            response.Card.ShouldNotBeNull();

            response.AuthenticationType.ShouldBe(AuthenticationType.Regular);
            response.AuthenticationCategory.ShouldBe(category);
            response.Status.ShouldBe(StatusType.Pending);
            response.NextActions.Count.ShouldBe(1);
            response.NextActions[0].ShouldBe(NextActionsType.CollectChannelData);

            response.GetSelfLink().ShouldNotBeNull();
            response.GetLink("callback_url").ShouldNotBeNull();
            response.Completed.ShouldBe(false);

            var getSessionResponse = await DefaultApi.AuthenticationClient().GetSessionDetails(response.Id);

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
            getSessionResponse.Status.ShouldBe(StatusType.Pending);
            getSessionResponse.NextActions.Count.ShouldBe(1);
            getSessionResponse.NextActions[0].ShouldBe(NextActionsType.CollectChannelData);
            getSessionResponse.TransactionType.ShouldBe(transactionType);
            getSessionResponse.ResponseCode.ShouldBeNull();
            getSessionResponse.AuthenticationDate.ShouldNotBeNull();

            getSessionResponse.GetSelfLink().ShouldNotBeNull();
            getSessionResponse.GetLink("callback_url").ShouldNotBeNull();
            getSessionResponse.Completed.ShouldBe(false);
        }
        
        [Theory(Skip = "use on demand")]
        [MemberData(nameof(SessionsTypes))]
        private async Task ShouldRequestAndGetCardSessionMerchantInitiatedSession(AuthenticationCategoryType category,
            ChallengeIndicatorType challengeIndicator,
            TransactionType transactionType)
        {
            var merchantInitiatedSession = MerchantInitiatedChannelData();

            var sessionResponse =
                await CreateNonHostedSession(merchantInitiatedSession, category, challengeIndicator, transactionType);

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
            response.Status.ShouldBe(StatusType.Unavailable);
            response.NextActions.Count.ShouldBe(1);
            response.NextActions[0].ShouldBe(NextActionsType.Complete);
            response.TransactionType.ShouldBe(transactionType);

            response.GetSelfLink().ShouldNotBeNull();
            response.GetLink("callback_url").ShouldNotBeNull();
            response.Completed.ShouldBe(false);

            var getSessionResponse = await DefaultApi.AuthenticationClient().GetSessionDetails(response.Id);

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
            getSessionResponse.Status.ShouldBe(StatusType.Unavailable);
            getSessionResponse.NextActions.Count.ShouldBe(1);
            getSessionResponse.NextActions[0].ShouldBe(NextActionsType.Complete);
            getSessionResponse.TransactionType.ShouldBe(transactionType);
            getSessionResponse.ResponseCode.ShouldBe(ResponseCodeType.U);
            getSessionResponse.AuthenticationDate.ShouldNotBeNull();

            getSessionResponse.GetSelfLink().ShouldNotBeNull();
            getSessionResponse.GetLink("callback_url").ShouldNotBeNull();
            getSessionResponse.Completed.ShouldBe(false);
        }

        public static IEnumerable<object[]> SessionsTypes =>
            new List<object[]>
            {
                new object[] {AuthenticationCategoryType.Payment, ChallengeIndicatorType.NoPreference, TransactionType.GoodsService},
                new object[]
                {
                    AuthenticationCategoryType.NonPayment, ChallengeIndicatorType.ChallengeRequested,
                    TransactionType.CheckAcceptance
                },
                new object[]
                {
                    AuthenticationCategoryType.NonPayment, ChallengeIndicatorType.ChallengeRequestedMandate,
                    TransactionType.AccountFunding
                }
            };
    }
}