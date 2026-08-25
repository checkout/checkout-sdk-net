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
    public class CheckoutApi : ICheckoutApi
    {
        private readonly ITokensClient _tokensClient;
        private readonly ICustomersClient _customersClient;
        private readonly IPaymentsClient _paymentsClient;
        private readonly IInstrumentsClient _instrumentsClient;
        private readonly IDisputesClient _disputesClient;
        private readonly IRiskClient _riskClient;
        private readonly IForexClient _forexClient;
        private readonly IWorkflowsClient _workflowsClient;
        private readonly IAuthenticationClient _authenticationClient;
        private readonly IAccountsClient _accountsClient;
        private readonly IPaymentLinksClient _paymentLinksClient;
        private readonly IHostedPaymentsClient _hostedPaymentsClient;
        private readonly IBalancesClient _balancesClient;
        private readonly ITransfersClient _transfersClient;
        private readonly IReportsClient _reportsClient;
        private readonly IMetadataClient _metadataClient;
        private readonly IFinancialClient _financialClient;
        private readonly IIssuingClient _issuingClient;
        private readonly IPaymentContextsClient _paymentContextsClient;
        private readonly IForwardClient _forwardClient;
        private readonly IFlowClient _flowClient;
        private readonly IApplicantsClient _applicantsClient;
        private readonly IAmlScreeningClient _amlScreeningClient;
        private readonly IFaceAuthenticationClient _faceAuthenticationClient;
        private readonly IIdDocumentVerificationClient _idDocumentVerificationClient;
        private readonly IAddressDocumentVerificationClient _addressDocumentVerificationClient;
        private readonly IIdentityVerificationClient _identityVerificationClient;
        private readonly INetworkTokensClient _networkTokensClient;
        private readonly IPaymentSetupsClient _paymentSetupsClient;
        private readonly IApplePayClient _applePayClient;
        private readonly IPaymentMethodsClient _paymentMethodsClient;
        private readonly IStandaloneAccountUpdaterClient _standaloneAccountUpdaterClient;
        private readonly IGooglePayClient _googlePayClient;
        private readonly IComplianceRequestsClient _complianceRequestsClient;
        private readonly IAgenticCommerceClient _agenticCommerceClient;
        private readonly IOnboardingSimulatorClient _onboardingSimulatorClient;
        private readonly IBacsClient _bacsClient;

        public CheckoutApi(CheckoutConfiguration configuration)
        {
            var baseApiClient = BaseApiClient(configuration);
            _tokensClient = new TokensClient(baseApiClient, configuration);
            _customersClient = new CustomersClient(baseApiClient, configuration);
            _paymentsClient = new PaymentsClient(baseApiClient, configuration);
            _instrumentsClient = new InstrumentsClient(baseApiClient, configuration);
            _disputesClient = new DisputesClient(baseApiClient, configuration);
            _riskClient = new RiskClient(baseApiClient, configuration);
            _forexClient = new ForexClient(baseApiClient, configuration);
            _workflowsClient = new WorkflowsClient(baseApiClient, configuration);
            _authenticationClient = new AuthenticationClient(baseApiClient, configuration);
            _accountsClient = new AccountsClient(
                baseApiClient,
                FilesApiClient(configuration),
                configuration);
            _paymentLinksClient = new PaymentLinksClient(baseApiClient, configuration);
            _hostedPaymentsClient = new HostedPaymentsClient(baseApiClient, configuration);
            _balancesClient = new BalancesClient(BalancesApiClient(configuration),
                configuration);
            _transfersClient = new TransfersClient(TransfersApiClient(configuration),
                configuration);
            _reportsClient = new ReportsClient(baseApiClient, configuration);
            _metadataClient = new MetadataClient(baseApiClient, configuration);
            _financialClient = new FinancialClient(baseApiClient, configuration);
            _issuingClient = new IssuingClient(baseApiClient, configuration);
            _paymentContextsClient = new PaymentContextsClient(baseApiClient, configuration);
            _forwardClient = new ForwardClient(ForwardApiClient(configuration), configuration);
            _flowClient = new FlowClient(baseApiClient, configuration);
            var identityApiClient = IdentityApiClient(configuration);
            _applicantsClient = new ApplicantsClient(identityApiClient, configuration);
            _amlScreeningClient = new AmlScreeningClient(identityApiClient, configuration);
            _faceAuthenticationClient = new FaceAuthenticationClient(identityApiClient, configuration);
            _idDocumentVerificationClient = new IdDocumentVerificationClient(identityApiClient, configuration);
            _addressDocumentVerificationClient = new AddressDocumentVerificationClient(identityApiClient, configuration);
            _identityVerificationClient = new IdentityVerificationClient(identityApiClient, configuration);
            _networkTokensClient = new NetworkTokensClient(baseApiClient, configuration);
            _paymentSetupsClient = new PaymentSetupsClient(baseApiClient, configuration);
            _applePayClient = new ApplePayClient(baseApiClient, configuration);
            _paymentMethodsClient = new PaymentMethodsClient(baseApiClient, configuration);
            _standaloneAccountUpdaterClient = new StandaloneAccountUpdaterClient(baseApiClient, configuration);
            _googlePayClient = new GooglePayClient(baseApiClient, configuration);
            _complianceRequestsClient = new ComplianceRequestsClient(baseApiClient, configuration);
            _agenticCommerceClient = new AgenticCommerceClient(baseApiClient, configuration);
            _onboardingSimulatorClient = new OnboardingSimulatorClient(baseApiClient, configuration);
            _bacsClient = new BacsClient(baseApiClient, configuration);
        }

        private static ApiClient BaseApiClient(CheckoutConfiguration configuration)
        {
            return new ApiClient(configuration.HttpClientFactory,
                configuration.EnvironmentSubdomain != null
                    ? configuration.EnvironmentSubdomain.ApiUri
                    : configuration.Environment.GetAttribute<EnvironmentAttribute>().ApiUri,
                    configuration.RecordTelemetry);
        }

        private static ApiClient FilesApiClient(CheckoutConfiguration configuration)
        {
            return new ApiClient(configuration.HttpClientFactory,
                configuration.Environment.GetAttribute<EnvironmentAttribute>().FilesApiUri,
                configuration.RecordTelemetry);
        }

        private static ApiClient TransfersApiClient(CheckoutConfiguration configuration)
        {
            return new ApiClient(configuration.HttpClientFactory,
                configuration.Environment.GetAttribute<EnvironmentAttribute>().TransfersApiUri,
                configuration.RecordTelemetry);
        }

        private static ApiClient BalancesApiClient(CheckoutConfiguration configuration)
        {
            return new ApiClient(configuration.HttpClientFactory,
                configuration.Environment.GetAttribute<EnvironmentAttribute>().BalancesApiUri,
                configuration.RecordTelemetry);
        }

        private static ApiClient ForwardApiClient(CheckoutConfiguration configuration)
        {
            return new ApiClient(configuration.HttpClientFactory,
                configuration.Environment.GetAttribute<EnvironmentAttribute>().ForwardApiUri,
                configuration.RecordTelemetry);
        }

        private static ApiClient IdentityApiClient(CheckoutConfiguration configuration)
        {
            return new ApiClient(configuration.HttpClientFactory,
                configuration.Environment.GetAttribute<EnvironmentAttribute>().IdentityApiUri,
                configuration.RecordTelemetry);
        }


        /// <summary>
        /// Creates single-use tokens from card details, wallet payloads or Apple Pay and Google Pay data.
        /// </summary>
        public ITokensClient TokensClient()
        {
            return _tokensClient;
        }

        /// <summary>
        /// Creates, retrieves, updates and deletes customers and their stored instruments.
        /// </summary>
        public ICustomersClient CustomersClient()
        {
            return _customersClient;
        }

        /// <summary>
        /// Requests payments and payouts, and retrieves, captures, refunds and voids them.
        /// </summary>
        public IPaymentsClient PaymentsClient()
        {
            return _paymentsClient;
        }

        /// <summary>
        /// Stores, retrieves, updates and deletes payment instruments.
        /// </summary>
        public IInstrumentsClient InstrumentsClient()
        {
            return _instrumentsClient;
        }

        /// <summary>
        /// Retrieves disputes and submits evidence against them.
        /// </summary>
        public IDisputesClient DisputesClient()
        {
            return _disputesClient;
        }

        /// <summary>
        /// Runs pre-authentication and pre-capture risk assessments.
        /// </summary>
        public IRiskClient RiskClient()
        {
            return _riskClient;
        }

        /// <summary>
        /// Retrieves foreign exchange quotes and rates.
        /// </summary>
        public IForexClient ForexClient()
        {
            return _forexClient;
        }

        /// <summary>
        /// Creates and manages workflows, their conditions and their actions.
        /// </summary>
        public IWorkflowsClient WorkflowsClient()
        {
            return _workflowsClient;
        }

        /// <summary>
        /// Runs standalone 3D Secure authentication sessions.
        /// </summary>
        public IAuthenticationClient AuthenticationClient()
        {
            return _authenticationClient;
        }

        /// <summary>
        /// Onboards and manages sub-entities, their instruments, payout schedules and files.
        /// </summary>
        public IAccountsClient AccountsClient()
        {
            return _accountsClient;
        }

        /// <summary>
        /// Creates and retrieves payment links.
        /// </summary>
        public IPaymentLinksClient PaymentLinksClient()
        {
            return _paymentLinksClient;
        }

        /// <summary>
        /// Creates and retrieves hosted payment pages.
        /// </summary>
        public IHostedPaymentsClient HostedPaymentsClient()
        {
            return _hostedPaymentsClient;
        }

        /// <summary>
        /// Retrieves entity balances.
        /// </summary>
        public IBalancesClient BalancesClient()
        {
            return _balancesClient;
        }

        /// <summary>
        /// Initiates and retrieves transfers between entities.
        /// </summary>
        public ITransfersClient TransfersClient()
        {
            return _transfersClient;
        }

        /// <summary>
        /// Retrieves reports and downloads their files.
        /// </summary>
        public IReportsClient ReportsClient()
        {
            return _reportsClient;
        }

        /// <summary>
        /// Retrieves card and bank account metadata.
        /// </summary>
        public IMetadataClient MetadataClient()
        {
            return _metadataClient;
        }

        /// <summary>
        /// Retrieves financial actions.
        /// </summary>
        public IFinancialClient FinancialClient()
        {
            return _financialClient;
        }

        /// <summary>
        /// Manages issued cardholders, cards, controls, transactions and issuing disputes.
        /// </summary>
        public IIssuingClient IssuingClient()
        {
            return _issuingClient;
        }

        /// <summary>
        /// Creates and retrieves payment contexts for alternative payment methods.
        /// </summary>
        public IPaymentContextsClient PaymentContextsClient()
        {
            return _paymentContextsClient;
        }

        /// <summary>
        /// Forwards requests to third-party endpoints and manages forwarding secrets.
        /// </summary>
        public IForwardClient ForwardClient()
        {
            return _forwardClient;
        }

        /// <summary>
        /// Retrieves and manages Flow payment sessions.
        /// </summary>
        public IFlowClient FlowClient()
        {
            return _flowClient;
        }

        /// <summary>
        /// Creates and manages identity verification applicants.
        /// </summary>
        public IApplicantsClient ApplicantsClient()
        {
            return _applicantsClient;
        }

        /// <summary>
        /// Runs anti-money-laundering screening checks.
        /// </summary>
        public IAmlScreeningClient AmlScreeningClient()
        {
            return _amlScreeningClient;
        }

        /// <summary>
        /// Runs face authentication checks.
        /// </summary>
        public IFaceAuthenticationClient FaceAuthenticationClient()
        {
            return _faceAuthenticationClient;
        }

        /// <summary>
        /// Runs identity document verification checks.
        /// </summary>
        public IIdDocumentVerificationClient IdDocumentVerificationClient()
        {
            return _idDocumentVerificationClient;
        }

        /// <summary>
        /// Runs address document verification checks.
        /// </summary>
        public IAddressDocumentVerificationClient AddressDocumentVerificationClient()
        {
            return _addressDocumentVerificationClient;
        }

        /// <summary>
        /// Runs combined identity verification checks.
        /// </summary>
        public IIdentityVerificationClient IdentityVerificationClient()
        {
            return _identityVerificationClient;
        }

        /// <summary>
        /// Provisions and manages network tokens.
        /// </summary>
        public INetworkTokensClient NetworkTokensClient()
        {
            return _networkTokensClient;
        }

        /// <summary>
        /// Creates, confirms and retrieves payment setups.
        /// </summary>
        public IPaymentSetupsClient PaymentSetupsClient()
        {
            return _paymentSetupsClient;
        }
        
        /// <summary>
        /// Manages Apple Pay merchant registration and certificates.
        /// </summary>
        public IApplePayClient ApplePayClient()
        {
            return _applePayClient;
        }
        
        /// <summary>
        /// Retrieves and configures available payment methods.
        /// </summary>
        public IPaymentMethodsClient PaymentMethodsClient()
        {
            return _paymentMethodsClient;
        }
        
        /// <summary>
        /// Runs standalone account updater requests on stored cards.
        /// </summary>
        public IStandaloneAccountUpdaterClient StandaloneAccountUpdaterClient()
        {
            return _standaloneAccountUpdaterClient;
        }

        /// <summary>
        /// Manages Google Pay enrollments.
        /// </summary>
        public IGooglePayClient GooglePayClient()
        {
            return _googlePayClient;
        }

        /// <summary>
        /// Retrieves compliance requests and submits responses to them.
        /// </summary>
        public IComplianceRequestsClient ComplianceRequestsClient()
        {
            return _complianceRequestsClient;
        }

        /// <summary>
        /// Creates delegated payment tokens for agentic commerce flows.
        /// </summary>
        public IAgenticCommerceClient AgenticCommerceClient()
        {
            return _agenticCommerceClient;
        }

        /// <summary>
        /// Drives sandbox-only onboarding simulations for sub-entities.
        /// </summary>
        public IOnboardingSimulatorClient OnboardingSimulatorClient()
        {
            return _onboardingSimulatorClient;
        }

        /// <summary>
        /// Sends Bacs Direct Debit pre-notifications.
        /// </summary>
        public IBacsClient BacsClient()
        {
            return _bacsClient;
        }
    }
}
