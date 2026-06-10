using Checkout.Authentication.Standalone.Common.Responses;
using Checkout.Common;
using Newtonsoft.Json;
using System;
using ChallengeIndicatorType = Checkout.Common.ChallengeIndicatorType;

namespace Checkout.Payments
{
    public class ThreeDsRequest
    {
        public bool? Enabled { get; set; } = true;

        [JsonProperty(PropertyName = "attempt_n3d")]
        public bool? AttemptN3D { get; set; }

        public string Eci { get; set; }

        public string Cryptogram { get; set; }

        public string Xid { get; set; }

        public string Version { get; set; }

        public Exemption? Exemption { get; set; }

        public ChallengeIndicatorType? ChallengeIndicator { get; set; }
        
        public bool? AllowUpgrade { get; set; }
        
        public string Status { get; set; }
        
        public DateTime? AuthenticationDate { get; set; }

        public long? AuthenticationAmount { get; set; }
        
        public FlowType? FlowType { get; set; }
        
        public string StatusReasonCode { get; set; }
        
        public string ChallengeCancelReason { get; set; }
        
        public string Score { get; set; }
        
        public string CryptogramAlgorithm { get; set; }
        
        public string AuthenticationId { get; set; }

        /// <summary>
        /// The information about how the 3DS Requestor authenticated the cardholder
        /// before or during the transaction.
        /// [Optional]
        /// </summary>
        public MerchantAuthenticationInfo MerchantAuthenticationInfo { get; set; }

        /// <summary>
        /// Additional information about the cardholder's account.
        /// [Optional]
        /// </summary>
        public AccountInfo AccountInfo { get; set; }

        /// <summary>
        /// The details of a previous 3DS authenticated transaction.
        /// Required when 3DS authentication was performed by a third-party provider.
        /// [Optional]
        /// </summary>
        public InitialAuthentication InitialAuthentication { get; set; }
    }
}