using Shouldly;
using Xunit;

namespace Checkout
{
    public class OAuthScopeTest
    {
        // The [OAuthScope] attribute value is the only place a scope's wire value is written down,
        // and OAuthSdkCredentials resolves it by reflection (OAuthSdkCredentials.cs:109) rather
        // than from the member name. A typo is therefore invisible at compile time and surfaces at
        // the token endpoint, which rejects the whole request when one requested scope is
        // undefined -- so a caller would lose every scope it asked for alongside the bad one.
        //
        // Values come from components.securitySchemes.OAuth.flows.clientCredentials.scopes in
        // shared/swagger-latest.json. Asserted through GetAttribute so the test exercises the same
        // path OAuthSdkCredentials uses to build the token request.
        [Theory]
        [InlineData(OAuthScope.Balances, "balances")]
        [InlineData(OAuthScope.BalancesView, "balances:view")]
        [InlineData(OAuthScope.BalancesTopUpInstructions, "balances:top-up-instructions")]
        private void ShouldExposeDocumentedBalancesScopeValues(OAuthScope scope, string expected)
        {
            scope.GetAttribute<OAuthScopeAttribute>().Scope.ShouldBe(expected);
        }
    }
}
