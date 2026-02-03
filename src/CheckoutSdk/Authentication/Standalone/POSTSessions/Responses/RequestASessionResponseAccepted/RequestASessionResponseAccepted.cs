using Checkout.Authentication.Standalone.Common;
using Checkout.Authentication.Standalone.Common.AccountInfo;
using Checkout.Authentication.Standalone.Common.InitialTransaction;
using Checkout.Authentication.Standalone.Common.Installment;
using Checkout.Authentication.Standalone.Common.MerchantRiskInfo;
using Checkout.Authentication.Standalone.Common.Recurring;
using Checkout.Authentication.Standalone.Common.Responses;
using Checkout.Authentication.Standalone.Common.Responses.Card;
using Checkout.Authentication.Standalone.Common.Responses.Certificates;
using Checkout.Authentication.Standalone.Common.Responses.Ds;
using Checkout.Authentication.Standalone.Common.Responses.GoogleSpa;
using Checkout.Authentication.Standalone.Common.Responses.Optimization;
using Checkout.Authentication.Standalone.Common.Responses.PreferredExperiences;
using Checkout.Common;
using System;
using System.Collections.Generic;
using ChallengeIndicatorType = Checkout.Authentication.Standalone.Common.Responses.ChallengeIndicatorType;

namespace Checkout.Authentication.Standalone.POSTSessions.Responses.RequestASessionResponseAccepted
{
    /// <summary>
    /// Request a session Response 202
    /// Session accepted and further action required
    /// </summary>
    public class RequestASessionResponseAccepted : Resource
    {
        /// <summary>
        /// A base64 encoded value prefixed with sek_ that gives access to client-side operations for a single
        /// authentication within the Sessions API. This value is returned as the session_secret when requesting a
        /// session.
        /// [Required]
        /// ^(sek)_(.{44})$
        /// 48 characters
        /// </summary>
        public string SessionSecret { get; set; }

        /// <summary>
        /// Session unique identifier
        /// [Required]
        /// ^(sid)_(\w{26})$
        /// 30 characters
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The transaction identifier that needs to be provided when communicating directly with the Access Control
        /// Server (ACS)
        /// [Required]
        /// 36 characters
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// Indicates the scheme this authentication is carried out against
        /// [Required]
        /// </summary>
        public SchemeType? Scheme { get; set; }

        /// <summary>
        /// The amount in the minor currency.
        /// [Required]
        /// [ 0 .. 9223372036854776000 ]
        /// </summary>
        public long Amount { get; set; }

        /// <summary>
        /// The three-letter ISO currency code
        /// [Required]
        /// </summary>
        public Currency? Currency { get; set; }

        /// <summary>
        /// Default: "regular" Indicates the type of payment this session is for. Please note the spelling of
        /// installment consists of two ls.
        /// [Required]
        /// </summary>
        public AuthenticationType AuthenticationType { get; set; } = AuthenticationType.Regular;

        /// <summary>
        /// Default: "payment" Indicates the category of the authentication request
        /// [Required]
        /// </summary>
        public AuthenticationCategoryType AuthenticationCategory { get; set; } = AuthenticationCategoryType.Payment;

        /// <summary>
        /// The status of the session.
        /// [Required]
        /// </summary>
        public StatusType? Status { get; set; }

        /// <summary>
        /// The protocol version number of the specification used by the API for authentication
        /// [Required]
        /// &lt;= 50
        /// </summary>
        public string ProtocolVersion { get; set; }

        /// <summary>
        /// Default: "no_preference" Indicates the preference for whether or not a 3DS challenge should be performed.
        /// The customer’s bank has the final say on whether or not the customer receives the challenge
        /// [Required]
        /// </summary>
        public ChallengeIndicatorType ChallengeIndicator { get; set; } = ChallengeIndicatorType.NoPreference;

        /// <summary>
        /// Authentication date and time
        /// [Required]
        /// <date-time>
        /// </summary>
        public DateTime AuthenticationDate { get; set; }

        /// <summary>
        /// Specifies which action to take in order to complete the session.
        /// The redirect_cardholder action is only applicable for hosted sessions.
        /// [Required]
        /// </summary>
        public IList<NextActionsType> NextActions { get; set; }

        /// <summary>
        /// Specifies if the session was completed.
        /// [Optional]
        /// </summary>
        public bool? Completed { get; set; }

        /// <summary>
        /// Additional information about the Cardholder's account.
        /// [Optional]
        /// </summary>
        public AccountInfo AccountInfo { get; set; }

        /// <summary>
        /// Additional information about the cardholder's purchase.
        /// [Optional]
        /// </summary>
        public MerchantRiskInfo MerchantRiskInfo { get; set; }

        /// <summary>
        /// A reference you can later use to identify this payment, such as an order number. Do not pass sensitive
        /// information in this field e.g. card details
        /// [Optional]
        /// &lt;= 100
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Default: "goods_service" Identifies the type of transaction being authenticated
        /// [Optional]
        /// &lt;= 50
        /// </summary>
        public TransactionType? TransactionType { get; set; } = Common.TransactionType.GoodsService;

        /// <summary>
        /// Details related to the Session source. This property should always be in the response, unless a card source
        /// was used and communication with Checkout.com's Vault was not possible.
        /// [Optional]
        /// </summary>
        public Card Card { get; set; }

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
        /// Details of a previous transaction
        /// [Optional]
        /// </summary>
        public InitialTransaction InitialTransaction { get; set; }

        /// <summary>
        /// The information about the optimization options selected
        /// [Optional]
        /// </summary>
        public Optimization Optimization { get; set; }

        /// <summary>
        /// Preferred Experiences
        /// [Optional]
        /// </summary>
        public PreferredExperiences PreferredExperiences { get; set; }

        /// <summary>
        /// The authentication experience that was used for processing
        /// [Optional]
        /// </summary>
        public ExperienceType? Experience { get; set; }

        /// <summary>
        /// Details of Google SPA (Secure Payment Authentication)
        /// [Optional]
        /// </summary>
        public GoogleSpa GoogleSpa { get; set; }

        /// <summary>
        /// The directory server (DS) information. Can be empty if the session is pending or communication with the DS
        /// failed
        /// [Optional]
        /// </summary>
        public Ds Ds { get; set; }

        /// <summary>
        /// Public certificates specific to a Directory Server (DS) for encrypting device data and verifying ACS signed
        /// content. Required when channel is app.
        /// [Optional]
        /// </summary>
        public Certificates Certificates { get; set; }
    }
}