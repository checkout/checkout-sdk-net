using Checkout.Issuing.CardholderAccessTokens.Requests;
using Checkout.Issuing.CardholderAccessTokens.Responses;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Issuing
{
    public class RequestCardholderAccessTokenIntegrationTest : IssuingCommon
    {
        [Fact(Skip = "This test requires issuing access key credentials and a valid cardholder id")]
        private async Task ShouldRequestCardholderAccessToken()
        {
            var request = new CardholderAccessTokenRequest
            {
                GrantType = "client_credentials",
                ClientId = System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_OAUTH_ISSUING_CLIENT_ID"),
                ClientSecret = System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_OAUTH_ISSUING_CLIENT_SECRET"),
                CardholderId = "crh_xxxxxxxxxxxxxxxxxxx", // Replace with a valid cardholder ID
                SingleUse = true
            };

            CardholderAccessTokenResponse response = await Api.IssuingClient().RequestCardholderAccessToken(request);

            response.ShouldNotBeNull();
            response.AccessToken.ShouldNotBeNull();
            response.TokenType.ShouldNotBeNull();
            response.ExpiresIn.ShouldNotBeNull();
        }
    }
}
