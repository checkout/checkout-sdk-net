using System;
using System.Linq;
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
        public void ShouldExposeDocumentedBalancesScopeValues(OAuthScope scope, string expected)
        {
            scope.GetAttribute<OAuthScopeAttribute>().Scope.ShouldBe(expected);
        }

        // The scopes added when this enum was synced against the spec. Kept as explicit cases
        // rather than folded into the sweep below, because the sweep only proves that a value
        // exists and is unique -- it cannot tell a correct wire value from a plausible typo.
        //
        // The last five are not declared in clientCredentials.scopes at all: they appear only in
        // the per-operation security requirements of GET/POST /compliance-requests/{payment_id},
        // the /googlepay/enrollments operations and GET /tokens/{tokenId}/metadata. A client built
        // from the declared map alone would be missing them.
        [Theory]
        [InlineData(OAuthScope.CardManagement, "card-management")]
        [InlineData(OAuthScope.FlowReflow, "flow:reflow")]
        [InlineData(OAuthScope.GatewayPaymentContexts, "gateway:payment-contexts")]
        [InlineData(OAuthScope.IssuingCardManagementRead, "issuing:card-management-read")]
        [InlineData(OAuthScope.IssuingCardManagementWrite, "issuing:card-management-write")]
        [InlineData(OAuthScope.IssuingDisputes, "issuing-disputes")]
        [InlineData(OAuthScope.IssuingTransactionsWrite, "issuing:transactions-write")]
        [InlineData(OAuthScope.PaymentSessions, "payment-sessions")]
        [InlineData(OAuthScope.Transactions, "transactions")]
        [InlineData(OAuthScope.ComplianceRequests, "compliance-requests")]
        [InlineData(OAuthScope.ComplianceRequestsRead, "compliance-requests:read")]
        [InlineData(OAuthScope.ComplianceRequestsRespond, "compliance-requests:respond")]
        [InlineData(OAuthScope.VaultGpaymeEnrollment, "vault:gpayme-enrollment")]
        [InlineData(OAuthScope.VaultTokensMetadata, "vault:tokens-metadata")]
        public void ShouldExposeDocumentedValuesForScopesAddedInSpecSync(OAuthScope scope, string expected)
        {
            scope.GetAttribute<OAuthScopeAttribute>().Scope.ShouldBe(expected);
        }

        // These five scopes appear nowhere in the specification -- neither in the clientCredentials
        // scope map nor in any operation's security requirement -- so a sweep driven by the spec
        // alone would delete them. They are kept deliberately: the authorization server still
        // grants them and callers still request them. marketplace is the proof: the sandbox payouts
        // client is provisioned for it and answers a request for accounts with invalid_scope.
        //
        // IssuingCard rather than IssuingCardMgmt: that is the member name this SDK has always
        // exposed for issuing:card-mgmt, and renaming it would defeat the point of keeping it.
        [Theory]
        [InlineData(OAuthScope.IssuingCard, "issuing:card-mgmt")]
        [InlineData(OAuthScope.IssuingClient, "issuing:client")]
        [InlineData(OAuthScope.Marketplace, "marketplace")]
        [InlineData(OAuthScope.MiddlewareGateway, "middleware:gateway")]
        [InlineData(OAuthScope.MiddlewarePaymentContext, "middleware:payment-context")]
        public void ShouldRetainTheLegacyScopesTheSpecificationOmits(OAuthScope scope, string expected)
        {
            scope.GetAttribute<OAuthScopeAttribute>().Scope.ShouldBe(expected);
        }

        // A member with no [OAuthScope] attribute is not a compile error and not a visible defect:
        // GetAttribute returns null (EnvironmentExtension.cs:14), so the first OAuth token request
        // that includes the member dies with a NullReferenceException from inside GetScopes. This
        // sweep is what makes adding a member to the enum safe without remembering the attribute.
        [Fact]
        public void ShouldResolveAWireValueForEveryMember()
        {
            foreach (OAuthScope scope in Enum.GetValues(typeof(OAuthScope)))
            {
                var attribute = scope.GetAttribute<OAuthScopeAttribute>();
                attribute.ShouldNotBeNull($"{scope} is missing its [OAuthScope] attribute");
                attribute.Scope.ShouldNotBeNullOrWhiteSpace();
            }
        }

        // Two members sharing a wire value means one of them is a copy-paste error, and it cannot
        // be caught by the per-scope assertions above, which only ever read the member they name.
        // The consequence is silent in both directions: a caller selecting the mistyped member
        // requests a scope it did not ask for, and the scope that member was supposed to carry is
        // left with no member at all, so it becomes unreachable through this enum.
        [Fact]
        public void ShouldNotReuseAWireValueAcrossMembers()
        {
            var duplicates = Enum.GetValues(typeof(OAuthScope))
                .Cast<OAuthScope>()
                .GroupBy(scope => scope.GetAttribute<OAuthScopeAttribute>().Scope)
                .Where(group => group.Count() > 1)
                .Select(group => $"{group.Key}: {string.Join(", ", group)}")
                .ToList();

            duplicates.ShouldBeEmpty();
        }
    }
}
