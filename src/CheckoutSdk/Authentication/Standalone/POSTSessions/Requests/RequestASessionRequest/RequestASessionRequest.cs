using Checkout.Authentication.Standalone.Common;
using Checkout.Authentication.Standalone.Common.AccountInfo;
using Checkout.Authentication.Standalone.Common.InitialTransaction;
using Checkout.Authentication.Standalone.Common.Installment;
using Checkout.Authentication.Standalone.Common.MerchantRiskInfo;
using Checkout.Authentication.Standalone.Common.Recurring;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.ChannelData;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.ChannelData.BrowserChannelData;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Completion;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Source;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Source.CardSource;
using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest
{
    /// <summary>
    /// Request a session
    /// Create a payment session to authenticate a cardholder before requesting a payment.
    /// Payment sessions can be linked to one or more payments (in the case of recurring and other merchant-initiated
    /// payments).
    /// The next_actions object in the response tells you which actions can be performed next.
    /// </summary>
    public class RequestASessionRequest
    {
        /// <summary>
        /// The three-letter ISO currency code
        /// [Required]
        /// 3 characters
        /// </summary>
        public Currency? Currency { get; set; }

        /// <summary>
        /// The source of the authentication.
        /// [Required]
        /// </summary>
        public AbstractSource Source { get; set; } = new CardSource();

        /// <summary>
        /// The redirect information needed for callbacks or redirects after the payment is completed
        /// [Required]
        /// </summary>
        public AbstractCompletion Completion { get; set; }

        /// <summary>
        /// The payment amount in the minor currency unit.
        /// For recurring and installment payment types, this value is required and must be greater than zero.
        /// Omitting this value will set authentication_category to non_payment.
        /// [Optional]
        /// <= 48
        /// >= 0
        /// </summary>
        public long? Amount { get; set; }

        /// <summary>
        /// The processing channel to be used for the session. Required if this was not set in the request for the OAuth
        /// token.
        /// [Optional]
        /// ^(pc)_(\w{26})$
        /// </summary>
        public string ProcessingChannelId { get; set; }

        /// <summary>
        /// Information related to authentication for payfac payments
        /// [Optional]
        /// </summary>
        public Marketplace.Marketplace Marketplace { get; set; }

        /// <summary>
        /// Default: "regular" Indicates the type of payment this session is for. Please note the spelling of
        /// installment consists of two ls.
        /// [Optional]
        /// </summary>
        public AuthenticationType? AuthenticationType { get; set; } = Common.AuthenticationType.Regular;

        /// <summary>
        /// Default: "payment" Indicates the category of the authentication request
        /// [Optional]
        /// </summary>
        public AuthenticationCategoryType? AuthenticationCategory { get; set; } = AuthenticationCategoryType.Payment;

        /// <summary>
        /// Additional information about the Cardholder's account.
        /// [Optional]
        /// </summary>
        public AccountInfo AccountInfo { get; set; }

        /// <summary>
        /// Default: "no_preference" Indicates whether a challenge is requested for this session.  The following are
        /// requests for exemption: • low_value • trusted_listing • trusted_listing_prompt •
        /// transaction_risk_assessment
        /// If an exemption cannot be applied, then the value no_challenge_requested will be used instead.
        /// [Optional]
        /// <= 50
        /// </summary>
        public ChallengeIndicatorType? ChallengeIndicator { get; set; } = ChallengeIndicatorType.NoPreference;

        /// <summary>
        /// An optional dynamic billing descriptor.
        /// [Optional]
        /// </summary>
        public BillingDescriptor.BillingDescriptor BillingDescriptor { get; set; }

        /// <summary>
        /// A reference you can later use to identify this payment, such as an order number. Do not pass sensitive
        /// information in this field e.g. card details
        /// [Optional]
        /// <= 100
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Additional information about the cardholder's purchase.
        /// [Optional]
        /// </summary>
        public MerchantRiskInfo MerchantRiskInfo { get; set; }

        /// <summary>
        /// Default: "goods_service" Identifies the type of transaction being authenticated
        /// <= 50
        /// </summary>
        public TransactionType? TransactionType { get; set; } = Common.TransactionType.GoodsService;

        /// <summary>
        /// The shipping address. Any special characters will be replaced.
        /// [Optional]
        /// </summary>
        public ShippingAddress.ShippingAddress ShippingAddress { get; set; }

        /// <summary>
        /// Indicates whether the cardholder shipping address and billing address are the same.
        /// [Optional]
        /// </summary>
        public bool? ShippingAddressMatchesBilling { get; set; }

        /// <summary>
        /// The information gathered from the environment used to initiate the session
        /// [Optional]
        /// </summary>
        public AbstractChannelData ChannelData { get; set; }

        /// <summary>
        /// Details of a recurring authentication. This property is needed only for a recurring authentication type.
        /// Value will be ignored in any other cases.
        /// [Optional]
        /// </summary>
        public Recurring Recurring { get; set; }

        /// <summary>
        /// Details of an installment authentication. This property is needed only for an installment authentication
        /// type. Value will be ignored in any other cases.
        /// [Optional]
        /// </summary>
        public Installment Installment { get; set; }

        /// <summary>
        /// Optionally opt into request optimization
        /// [Optional]
        /// </summary>
        public Optimization.Optimization Optimization { get; set; }

        /// <summary>
        /// Details of a previous transaction
        /// [Optional]
        /// </summary>
        public InitialTransaction InitialTransaction { get; set; }

        /// <summary>
        /// This object contains the Google SPA properties (non-hosted only)
        /// [Optional]
        /// </summary>
        public GoogleSpa.GoogleSpa GoogleSpa { get; set; }

        /// <summary>
        /// Indicates the chosen experience(s) for this session.  Available experiences include: • 3ds • google_spa
        /// [Optional]
        /// </summary>
        public IList<PreferredExperiencesType> PreferredExperiences { get; set; }
    }
}