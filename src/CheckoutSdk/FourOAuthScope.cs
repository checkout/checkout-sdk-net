namespace Checkout
{
    public enum FourOAuthScope
    {
        [FourOAuthScope("vault")] Vault,
        [FourOAuthScope("vault:instruments")] VaultInstruments,
        [FourOAuthScope("vault:tokenization")] VaultTokenization,
        [FourOAuthScope("gateway")] Gateway,
        [FourOAuthScope("gateway:payment")] GatewayPayment,

        [FourOAuthScope("gateway:payment-details")]
        GatewayPaymentDetails,

        [FourOAuthScope("gateway:payment-authorizations")]
        GatewayPaymentAuthorization,

        [FourOAuthScope("gateway:payment-voids")]
        GatewayPaymentVoids,

        [FourOAuthScope("gateway:payment-captures")]
        GatewayPaymentCaptures,

        [FourOAuthScope("gateway:payment-refunds")]
        GatewayPaymentRefunds,
        [FourOAuthScope("fx")] Fx,

        [FourOAuthScope("payouts:bank-details")]
        PayoutsBankDetails,
        [FourOAuthScope("sessions")] Sessions,
        [FourOAuthScope("sessions:app")] SessionsApp,
        [FourOAuthScope("sessions:browser")] SessionsBrowser,
        [FourOAuthScope("disputes")] Disputes,
        [FourOAuthScope("disputes:view")] DisputesView,

        [FourOAuthScope("disputes:provide-evidence")]
        DisputesProvideEvidence,
        [FourOAuthScope("disputes:accept")] DisputesAccept,
        [FourOAuthScope("marketplace")] Marketplace,
        [FourOAuthScope("flow")] Flow,
        [FourOAuthScope("flow:workflows")] FlowWorkflows,
        [FourOAuthScope("flow:events")] FlowEvents,
        [FourOAuthScope("files")] Files,
        [FourOAuthScope("files:retrieve")] FilesRetrieve,
        [FourOAuthScope("files:upload")] FilesUpload,
        [FourOAuthScope("issuing:client")] IssuingClient,
        [FourOAuthScope("issuing:partner")] IssuingPartner,
        [FourOAuthScope("risk")] Risk,
        [FourOAuthScope("risk:assessment")] RiskAssessment,
        [FourOAuthScope("risk:settings")] RiskSettings
    }
}