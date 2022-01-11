using Checkout.Common;
using Checkout.Sessions.Channel;
using Checkout.Sessions.Completion;
using Checkout.Sessions.Source;

namespace Checkout.Sessions
{
    public class SessionRequest
    {
        public SessionSource Source { get; set; }

        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public string ProcessingChannelId { get; set; }

        public MarketplaceData Marketplace { get; set; }

        public AuthenticationType? AuthenticationType { get; set; }

        public Category? AuthenticationCategory { get; set; }

        public ChallengeIndicatorType? ChallengeIndicator { get; set; }

        public SessionsBillingDescriptor BillingDescriptor { get; set; }

        public string Reference { get; set; }

        public TransactionType? TransactionType { get; set; }

        public SessionAddress ShippingAddress { get; set; }

        public CompletionInfo Completion { get; set; }

        public ChannelData ChannelData { get; set; }
    }
}