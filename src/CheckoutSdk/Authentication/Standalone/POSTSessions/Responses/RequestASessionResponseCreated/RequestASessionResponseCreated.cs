using Checkout.Authentication.Standalone.Common;
using Checkout.Authentication.Standalone.Common.AccountInfo;
using Checkout.Authentication.Standalone.Common.InitialTransaction;
using Checkout.Authentication.Standalone.Common.Installment;
using Checkout.Authentication.Standalone.Common.MerchantRiskInfo;
using Checkout.Authentication.Standalone.Common.Recurring;
using Checkout.Authentication.Standalone.Common.Responses;
using Checkout.Authentication.Standalone.Common.Responses.Acs;
using Checkout.Authentication.Standalone.Common.Responses.Card;
using Checkout.Authentication.Standalone.Common.Responses.Certificates;
using Checkout.Authentication.Standalone.Common.Responses.Ds;
using Checkout.Authentication.Standalone.Common.Responses.GoogleSpa;
using Checkout.Authentication.Standalone.Common.Responses.Optimization;
using Checkout.Authentication.Standalone.Common.Responses.PreferredExperiences;
using Checkout.Authentication.Standalone.Common.Responses.SchemeInfo;
using Checkout.Authentication.Standalone.Common.Responses.Threeds;
using Checkout.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using ChallengeIndicatorType = Checkout.Authentication.Standalone.Common.Responses.ChallengeIndicatorType;
using Exemption = Checkout.Authentication.Standalone.Common.Responses.Exemption.Exemption;

namespace Checkout.Authentication.Standalone.POSTSessions.Responses.RequestASessionResponseCreated
{
    /// <summary>
    /// Request a session Response 201
    /// Session processed successfully
    /// </summary>
    public class RequestASessionResponseCreated : Resource
    {
        /// <summary>
        /// A base64 encoded value prefixed with sek_ that gives access to client-side operations for a single
        /// authentication within the Sessions API.  This value is returned as the session_secret when requesting a
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
        public SchemeType Scheme { get; set; }

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
        /// Indicates the status of the session
        /// [Required]
        /// </summary>
        public StatusType? Status { get; set; }

        /// <summary>
        /// The protocol version number of the specification used by the API for authentication
        /// [Required]
        /// <= 50
        /// </summary>
        public string ProtocolVersion { get; set; }

        /// <summary>
        /// Default: "no_preference" Indicates the preference for whether or not a 3DS challenge should be performed.
        /// The customer’s bank has the final say on whether or not the customer receives the challenge
        /// [Required]
        /// </summary>
        public ChallengeIndicatorType ChallengeIndicator { get; set; } = ChallengeIndicatorType.NoPreference;

        /// <summary>
        /// Indicates whether this session has been completed
        /// [Optional]
        /// </summary>
        public bool? Completed { get; set; }

        /// <summary>
        /// Indicates whether this session involved a challenge. This will only be set after communication with the
        /// scheme is finished.
        /// [Optional]
        /// </summary>
        public bool? Challenged { get; set; }

        /// <summary>
        /// Public certificates specific to a Directory Server (DS) for encrypting device data and verifying ACS signed
        /// content. Required when channel is app.
        /// [Optional]
        /// </summary>
        public Certificates Certificates { get; set; }

        /// <summary>
        /// When the Session is unavailable this will point to the reason it is so.
        /// • ares_error = There was an issue in the Authentication response we got back from the Directory Server
        /// (scheme server - 3DS2) • ares_status = The status was set to the status in the Authentication response we
        /// got back from the Directory Server (scheme server - 3DS2) • rreq_error = There was an issue in the Response
        /// we got back from the Access Control Server (issuer server - 3DS2)  • rreq_status = The status was set to the
        /// status in the Response we got back from the Access Control Server (issuer server - 3DS2) • risk_declined =
        /// The status was set to declined because the Risk engine recommended we decline the authentication
        /// [Optional]
        /// </summary>
        public StatusReasonType? StatusReason { get; set; }

        /// <summary>
        /// Whether the authentication was successful. This will only be set if the Session is in a final state
        /// [Optional]
        /// </summary>
        public bool? Approved { get; set; }

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
        /// <= 100
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Default: "goods_service" Identifies the type of transaction being authenticated
        /// [Optional]
        /// <= 50
        /// </summary>
        public TransactionType? TransactionType { get; set; } = Common.TransactionType.GoodsService;

        /// <summary>
        /// Specifies which action to take in order to complete the session.
        /// The redirect_cardholder action is only applicable for hosted sessions.
        /// [Optional]
        /// </summary>
        public IList<NextActionsType> NextActions { get; set; }

        /// <summary>
        /// The directory server (DS) information. Can be empty if the session is pending or communication with the DS
        /// failed
        /// [Optional]
        /// </summary>
        public Ds Ds { get; set; }

        /// <summary>
        /// The access control server (ACS) information. Can be empty if the session is still pending or if
        /// communication with the ACS failed. This will be available when the channel data and issuer fingerprint
        /// result have been provided.
        /// [Optional]
        /// </summary>
        public Acs Acs { get; set; }

        /// <summary>
        /// Only available as a result of a 3DS2 authentication.
        /// The response from the DS or ACS which indicates whether a transaction qualifies as an authenticated
        /// transaction or account verification.  Only available if communication with the scheme was successful and the
        /// Session is in a final state. • Y = Authentication Verification Successful. • N = Not Authenticated
        /// /Account Not Verified; Transaction denied.  • U = Authentication/ Account Verification Could Not Be
        /// Performed; Technical or other problem, as indicated in ARes or RReq. • A = Attempts Processing Performed;
        /// Not Authenticated/Verified, but a proof of attempted authentication/verification is provided. • C =
        /// Challenge Required; Additional authentication is required using the CReq/CRes. • D = Challenge Required;
        /// Decoupled Authentication confirmed. • R = Authentication/ Account Verification Rejected; Issuer is
        /// rejecting authentication/verification and request that authorization not be attempted. • I = Informational
        /// Only; 3DS Requestor challenge preference acknowledged.
        /// [Optional]
        /// </summary>
        public ResponseCodeType? ResponseCode { get; set; }

        /// <summary>
        /// Only available as a result of a 3DS2 authentication.
        /// The response from the DS or ACS which provides information on why the response_code field has the specified
        /// value. Only available when response_code is not Y. Learn more about the reasons for authentication
        /// failures.
        /// [Optional]
        /// </summary>
        public string ResponseStatusReason { get; set; }

        /// <summary>
        /// Payment system-specific value provided as part of the ACS registration for each supported DS. Please be
        /// advised that this field will only be included in responses when authenticating with a valid OAuth token and
        /// not when authenticating with session_secret. Cryptogram can only be retrieved for up to 24 hours after the
        /// session is created.
        /// [Optional]
        /// 28 characters
        /// </summary>
        public string Cryptogram { get; set; }

        /// <summary>
        /// Electronic Commerce Indicator.  Please be advised that this field will only be included in responses when
        /// authenticating with a valid OAuth token and not when authenticating with session_secret.
        /// [Optional]
        /// 2 characters
        /// </summary>
        public string Eci { get; set; }

        /// <summary>
        /// The xid value to use for authorization
        /// [Optional]
        /// </summary>
        public string Xid { get; set; }

        /// <summary>
        /// May provide cardholder information from the DS to be presented to the cardholder
        /// [Optional]
        /// </summary>
        public string CardholderInfo { get; set; }

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
        /// Indicates the card holder's IP address. Only available when the scheme selected is Cartes Bancaires.
        /// [Optional]
        /// </summary>
        public string CustomerIp { get; set; }

        /// <summary>
        /// Authentication date and time
        /// [Optional]
        /// <date-time>
        /// </summary>
        public DateTime? AuthenticationDate { get; set; }

        /// <summary>
        /// Details related to exemption present in 3DS flow
        /// [Optional]
        /// </summary>
        public Exemption Exemption { get; set; }

        /// <summary>
        /// Indicates whether the 3D Secure 2 authentication was challenged or frictionless
        /// [Optional]
        /// </summary>
        public FlowType? FlowType { get; set; }

        /// <summary>
        /// The information about the optimization options selected
        /// [Optional]
        /// </summary>
        public Optimization Optimization { get; set; }

        /// <summary>
        /// Indicates scheme-specific information
        /// [Optional]
        /// </summary>
        public SchemeInfo SchemeInfo { get; set; }

        /// <summary>
        /// This object provides more information about the 3DS experience
        /// [Optional]
        /// </summary>
        [JsonProperty(PropertyName = "3ds")] 
        public Threeds Threeds { get; set; }

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
    }
}