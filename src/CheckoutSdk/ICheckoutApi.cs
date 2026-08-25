using Checkout.Accounts;
using Checkout.AgenticCommerce;
using Checkout.Apm.Bacs;
using Checkout.Authentication;
using Checkout.Balances;
using Checkout.ComplianceRequests;
using Checkout.Issuing;
using Checkout.Customers;
using Checkout.Disputes;
using Checkout.Financial;
using Checkout.HandlePaymentsAndPayouts.ApplePay;
using Checkout.HandlePaymentsAndPayouts.Flow;
using Checkout.HandlePaymentsAndPayouts.GooglePay;
using Checkout.Forex;
using Checkout.Forward;
using Checkout.Identities.Applicants;
using Checkout.Identities.AmlScreening;
using Checkout.Identities.FaceAuthentication;
using Checkout.Identities.IdDocumentVerification;
using Checkout.Identities.AddressDocumentVerification;
using Checkout.Identities.IdentityVerification;
using Checkout.Instruments;
using Checkout.Metadata;
using Checkout.NetworkTokens;
using Checkout.OnboardingSimulator;
using Checkout.PaymentMethods;
using Checkout.Payments;
using Checkout.Payments.Contexts;
using Checkout.Payments.Hosted;
using Checkout.Payments.Links;
using Checkout.Payments.Setups;
using Checkout.Reports;
using Checkout.Risk;
using Checkout.StandaloneAccountUpdater;
using Checkout.Tokens;
using Checkout.Transfers;
using Checkout.Workflows;

namespace Checkout
{
    public interface ICheckoutApi : ICheckoutApiClient
    {
        /// <summary>
        /// Creates single-use tokens from card details, wallet payloads or Apple Pay and Google Pay data.
        /// </summary>
        ITokensClient TokensClient();

        /// <summary>
        /// Creates, retrieves, updates and deletes customers and their stored instruments.
        /// </summary>
        ICustomersClient CustomersClient();

        /// <summary>
        /// Requests payments and payouts, and retrieves, captures, refunds and voids them.
        /// </summary>
        IPaymentsClient PaymentsClient();

        /// <summary>
        /// Stores, retrieves, updates and deletes payment instruments.
        /// </summary>
        IInstrumentsClient InstrumentsClient();

        /// <summary>
        /// Retrieves disputes and submits evidence against them.
        /// </summary>
        IDisputesClient DisputesClient();

        /// <summary>
        /// Runs pre-authentication and pre-capture risk assessments.
        /// </summary>
        IRiskClient RiskClient();

        /// <summary>
        /// Retrieves foreign exchange quotes and rates.
        /// </summary>
        IForexClient ForexClient();

        /// <summary>
        /// Creates and manages workflows, their conditions and their actions.
        /// </summary>
        IWorkflowsClient WorkflowsClient();

        /// <summary>
        /// Runs standalone 3D Secure authentication sessions.
        /// </summary>
        IAuthenticationClient AuthenticationClient();

        /// <summary>
        /// Onboards and manages sub-entities, their instruments, payout schedules and files.
        /// </summary>
        IAccountsClient AccountsClient();

        /// <summary>
        /// Creates and retrieves payment links.
        /// </summary>
        IPaymentLinksClient PaymentLinksClient();

        /// <summary>
        /// Creates and retrieves hosted payment pages.
        /// </summary>
        IHostedPaymentsClient HostedPaymentsClient();

        /// <summary>
        /// Retrieves entity balances.
        /// </summary>
        IBalancesClient BalancesClient();

        /// <summary>
        /// Initiates and retrieves transfers between entities.
        /// </summary>
        ITransfersClient TransfersClient();

        /// <summary>
        /// Retrieves reports and downloads their files.
        /// </summary>
        IReportsClient ReportsClient();

        /// <summary>
        /// Retrieves card and bank account metadata.
        /// </summary>
        IMetadataClient MetadataClient();

        /// <summary>
        /// Retrieves financial actions.
        /// </summary>
        IFinancialClient FinancialClient();

        /// <summary>
        /// Manages issued cardholders, cards, controls, transactions and issuing disputes.
        /// </summary>
        IIssuingClient IssuingClient();

        /// <summary>
        /// Creates and retrieves payment contexts for alternative payment methods.
        /// </summary>
        IPaymentContextsClient PaymentContextsClient();
        
        /// <summary>
        /// Forwards requests to third-party endpoints and manages forwarding secrets.
        /// </summary>
        IForwardClient ForwardClient();
        
        /// <summary>
        /// Retrieves and manages Flow payment sessions.
        /// </summary>
        IFlowClient FlowClient();
        
        /// <summary>
        /// Creates and manages identity verification applicants.
        /// </summary>
        IApplicantsClient ApplicantsClient();
        
        /// <summary>
        /// Runs anti-money-laundering screening checks.
        /// </summary>
        IAmlScreeningClient AmlScreeningClient();
        
        /// <summary>
        /// Runs face authentication checks.
        /// </summary>
        IFaceAuthenticationClient FaceAuthenticationClient();
        
        /// <summary>
        /// Runs identity document verification checks.
        /// </summary>
        IIdDocumentVerificationClient IdDocumentVerificationClient();

        /// <summary>
        /// Runs address document verification checks.
        /// </summary>
        IAddressDocumentVerificationClient AddressDocumentVerificationClient();
        
        /// <summary>
        /// Runs combined identity verification checks.
        /// </summary>
        IIdentityVerificationClient IdentityVerificationClient();
        
        /// <summary>
        /// Provisions and manages network tokens.
        /// </summary>
        INetworkTokensClient NetworkTokensClient();

        /// <summary>
        /// Creates, confirms and retrieves payment setups.
        /// </summary>
        IPaymentSetupsClient PaymentSetupsClient();
        
        /// <summary>
        /// Manages Apple Pay merchant registration and certificates.
        /// </summary>
        IApplePayClient ApplePayClient();
        
        /// <summary>
        /// Retrieves and configures available payment methods.
        /// </summary>
        IPaymentMethodsClient PaymentMethodsClient();
        
        /// <summary>
        /// Runs standalone account updater requests on stored cards.
        /// </summary>
        IStandaloneAccountUpdaterClient StandaloneAccountUpdaterClient();

        /// <summary>
        /// Manages Google Pay enrollments.
        /// </summary>
        IGooglePayClient GooglePayClient();

        /// <summary>
        /// Retrieves compliance requests and submits responses to them.
        /// </summary>
        IComplianceRequestsClient ComplianceRequestsClient();

        /// <summary>
        /// Creates delegated payment tokens for agentic commerce flows.
        /// </summary>
        IAgenticCommerceClient AgenticCommerceClient();

        /// <summary>
        /// Drives sandbox-only onboarding simulations for sub-entities.
        /// </summary>
        IOnboardingSimulatorClient OnboardingSimulatorClient();

        /// <summary>
        /// Sends Bacs Direct Debit pre-notifications.
        /// </summary>
        IBacsClient BacsClient();
    }
}
