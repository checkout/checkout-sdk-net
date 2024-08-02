namespace Checkout.Sessions.Channel
{
    public class MerchantInitiatedSession : ChannelData
    {
        public RequestType RequestType { get; set; }

        public MerchantInitiatedSession() : base(ChannelType.MerchantInitiated)
        {
        }
    }
}