namespace Checkout.Authentication.Standalone.PUTSessionsIdCollectData.Requests
{
    public abstract class AbstractUpdateASessionRequest
    {
        /// <summary>
        /// Default: "browser" Indicates the type of channel interface being used to initiate the transaction.
        /// [Required]
        /// </summary>
        public ChannelType? Channel;

        protected AbstractUpdateASessionRequest(ChannelType channel)
        {
            Channel = channel;
        }
    }
}