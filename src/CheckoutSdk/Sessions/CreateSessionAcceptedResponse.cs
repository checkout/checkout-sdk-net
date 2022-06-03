using Checkout.Common;
using System;
using System.Collections.Generic;

namespace Checkout.Sessions
{
    public class CreateSessionAcceptedResponse : Resource
    {
        public string Id { get; set; }

        public string SessionSecret { get; set; }

        public string TransactionId { get; set; }

        public SessionScheme? Scheme { get; set; }

        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public AuthenticationType? AuthenticationType { get; set; }

        public Category? AuthenticationCategory { get; set; }

        public SessionStatus? Status { get; set; }

        public StatusReason? StatusReason { get; set; }

        public IList<NextAction> NextActions { get; set; }

        public string ProtocolVersion { get; set; }

        public string Reference { get; set; }

        public CardInfo Card { get; set; }

        public Recurring Recurring { get; set; }

        public DateTime? AuthenticationDate { get; set; }

        public ChallengeIndicatorType? ChallengeIndicator { get; set; }
    }
}