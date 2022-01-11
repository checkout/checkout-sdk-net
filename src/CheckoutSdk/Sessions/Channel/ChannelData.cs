namespace Checkout.Sessions.Channel
{
    public abstract class ChannelData
    {
        public ChannelType? Channel { get; set; }

        protected ChannelData(ChannelType channel)
        {
            Channel = channel;
        }
    }
}