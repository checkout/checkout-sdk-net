namespace Checkout
{
    /// <summary>
    /// OAuth 2.0 client credentials scopes.
    ///
    /// <para>Mirrors <c>components.securitySchemes.OAuth.flows.clientCredentials.scopes</c>
    /// in the Checkout.com API specification, plus the scopes that appear only in
    /// per-operation <c>security</c> requirements and are never declared in that map:
    /// <c>compliance-requests</c>, <c>compliance-requests:read</c>,
    /// <c>compliance-requests:respond</c>, <c>vault:gpayme-enrollment</c> and
    /// <c>vault:tokens-metadata</c>.</para>
    ///
    /// <para>Members are ordered alphabetically. The wire value comes from the
    /// <see cref="OAuthScopeAttribute"/>, never from the member name, so a member may be
    /// moved freely -- but note that <c>CheckoutOptions.Scopes</c> is bound from
    /// <c>IConfiguration</c>, which accepts numeric enum values, so reordering changes the
    /// meaning of a numerically-specified scope in <c>appsettings.json</c>.</para>
    /// </summary>
    public enum OAuthScope
    {
        [OAuthScope("accounts")] Accounts,
        [OAuthScope("balances")] Balances,
        [OAuthScope("balances:top-up-instructions")] BalancesTopUpInstructions,
        [OAuthScope("balances:view")] BalancesView,
        [OAuthScope("card-management")] CardManagement,
        [OAuthScope("compliance-requests")] ComplianceRequests,
        [OAuthScope("compliance-requests:read")] ComplianceRequestsRead,
        [OAuthScope("compliance-requests:respond")] ComplianceRequestsRespond,
        [OAuthScope("disputes")] Disputes,
        [OAuthScope("disputes:accept")] DisputesAccept,
        [OAuthScope("disputes:provide-evidence")] DisputesProvideEvidence,
        [OAuthScope("disputes:scheme-files")] DisputesSchemeFiles,
        [OAuthScope("disputes:view")] DisputesView,
        [OAuthScope("files")] Files,
        [OAuthScope("files:download")] FilesDownload,
        [OAuthScope("files:retrieve")] FilesRetrieve,
        [OAuthScope("files:upload")] FilesUpload,
        [OAuthScope("financial-actions")] FinancialActions,
        [OAuthScope("financial-actions:view")] FinancialActionsView,
        [OAuthScope("flow")] Flow,
        [OAuthScope("flow:events")] FlowEvents,
        [OAuthScope("flow:reflow")] FlowReflow,
        [OAuthScope("flow:workflows")] FlowWorkflows,
        [OAuthScope("forward")] Forward,
        [OAuthScope("forward:secrets")] ForwardSecrets,
        [OAuthScope("fx")] Fx,
        [OAuthScope("gateway")] Gateway,
        [OAuthScope("gateway:payment")] GatewayPayment,
        [OAuthScope("gateway:payment-authorizations")] GatewayPaymentAuthorization,
        [OAuthScope("gateway:payment-cancellations")] GatewayPaymentCancellations,
        [OAuthScope("gateway:payment-captures")] GatewayPaymentCaptures,
        [OAuthScope("gateway:payment-contexts")] GatewayPaymentContexts,
        [OAuthScope("gateway:payment-details")] GatewayPaymentDetails,
        [OAuthScope("gateway:payment-refunds")] GatewayPaymentRefunds,
        [OAuthScope("gateway:payment-voids")] GatewayPaymentVoids,
        [OAuthScope("identity-verification")] IdentityVerification,
        [OAuthScope("issuing:card-management-read")] IssuingCardManagementRead,
        [OAuthScope("issuing:card-management-write")] IssuingCardManagementWrite,
        [OAuthScope("issuing:controls-read")] IssuingControlRead,
        [OAuthScope("issuing:controls-write")] IssuingControlWrite,
        [OAuthScope("issuing-disputes")] IssuingDisputes,
        [OAuthScope("issuing:disputes-read")] IssuingDisputesRead,
        [OAuthScope("issuing:disputes-write")] IssuingDisputesWrite,
        [OAuthScope("issuing:transactions-read")] IssuingTransactionsRead,
        [OAuthScope("issuing:transactions-write")] IssuingTransactionsWrite,
        [OAuthScope("middleware")] Middleware,
        [OAuthScope("middleware:merchants-public")] MiddlewareMerchantsPublic,
        [OAuthScope("middleware:merchants-secret")] MiddlewareMerchantsSecret,
        [OAuthScope("Payment Context")] PaymentContext,
        [OAuthScope("payment-sessions")] PaymentSessions,
        [OAuthScope("payments:search")] PaymentsSearch,
        [OAuthScope("payouts:bank-details")] PayoutsBankDetails,
        [OAuthScope("reports")] Reports,
        [OAuthScope("reports:view")] ReportsView,
        [OAuthScope("sessions:app")] SessionsApp,
        [OAuthScope("sessions:browser")] SessionsBrowser,
        [OAuthScope("transactions")] Transactions,
        [OAuthScope("transfers")] Transfers,
        [OAuthScope("transfers:create")] TransfersCreate,
        [OAuthScope("transfers:view")] TransfersView,
        [OAuthScope("vault")] Vault,
        [OAuthScope("vault:apme-enrollment")] VaultApmeEnrollment,
        [OAuthScope("vault:card-metadata")] VaultCardMetadata,
        [OAuthScope("vault:customers")] VaultCustomers,
        [OAuthScope("vault:gpayme-enrollment")] VaultGpaymeEnrollment,
        [OAuthScope("vault:instruments")] VaultInstruments,
        [OAuthScope("vault:network-tokens")] VaultNetworkTokens,
        [OAuthScope("vault:real-time-account-updater")] VaultRealTimeAccountUpdater,
        [OAuthScope("vault:tokenization")] VaultTokenization,
        [OAuthScope("vault:tokens-metadata")] VaultTokensMetadata
    }
}
