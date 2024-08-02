using Checkout.Common;
using Checkout.Sessions.Channel;
using Checkout.Sessions.Completion;
using Checkout.Sessions.Source;

namespace Checkout.Sessions
{
    public class SessionRequest
    {
        public SessionSource Source { get; set; } = new SessionCardSource();

        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public string ProcessingChannelId { get; set; }

        public SessionMarketplaceData Marketplace { get; set; }

        public AuthenticationType? AuthenticationType { get; set; } = Sessions.AuthenticationType.Regular;

        public Category? AuthenticationCategory { get; set; } = Category.Payment;

        public CardholderAccountInfo AccountInfo { get; set; }

        public ChallengeIndicatorType? ChallengeIndicator { get; set; } = ChallengeIndicatorType.NoPreference;

        public SessionsBillingDescriptor BillingDescriptor { get; set; }

        public string Reference { get; set; }

        public MerchantRiskInfo MerchantRiskInfo { get; set; }

        public string PriorTransactionReference { get; set; }

        public TransactionType? TransactionType { get; set; } = Sessions.TransactionType.GoodsService;

        public SessionAddress ShippingAddress { get; set; }

        public bool? ShippingAddressMatchesBilling { get; set; }

        public CompletionInfo Completion { get; set; }

        public ChannelData ChannelData { get; set; }

        public Recurring Recurring { get; set; }
        
        public Installment Installment { get; set; }

        public Optimization Optimization { get; set; }

        public InitialTransaction InitialTransaction { get; set; }
    }
}