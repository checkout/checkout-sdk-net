using Checkout.Common;
using System;
using System.Collections.Generic;

namespace Checkout.Sessions
{
    public class GetSessionResponse : Resource
    {
        public string Id { get; set; }

        public string SessionSecret { get; set; }

        public string TransactionId { get; set; }

        public SessionScheme? Scheme { get; set; }

        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public bool? Completed { get; set; }

        public bool? Challenged { get; set; }

        public AuthenticationType? AuthenticationType { get; set; }

        public Category? AuthenticationCategory { get; set; }

        public DsPublicKeys Certificates { get; set; }

        public SessionStatus? Status { get; set; }

        public StatusReason? StatusReason { get; set; }

        public bool? Approved { get; set; }

        public string ProtocolVersion { get; set; }

        public string Reference { get; set; }

        public TransactionType? TransactionType { get; set; }

        public IList<NextAction> NextActions { get; set; }

        public Ds Ds { get; set; }

        public Acs Acs { get; set; }

        public ResponseCode? ResponseCode { get; set; }

        public string ResponseStatusReason { get; set; }

        public string Pareq { get; set; }

        public string Cryptogram { get; set; }

        public string Eci { get; set; }

        public string Xid { get; set; }

        public string CardholderInfo { get; set; }

        public CardInfo Card { get; set; }

        public Recurring Recurring { get; set; }

        public Installment Installment { get; set; }

        public string CustomerIp { get; set; }

        public DateTime? AuthenticationDate { get; set; }

        public ThreeDsExemption Exemption { get; set; }

        public ThreeDSFlowType? FlowType { get; set; }

        public ChallengeIndicatorType? ChallengeIndicator { get; set; }

        public SchemeInfo SchemeInfo { get; set; }
    }
}