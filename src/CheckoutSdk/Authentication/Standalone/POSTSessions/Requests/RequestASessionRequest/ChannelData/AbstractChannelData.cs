namespace Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.ChannelData
{
    /// <summary>
    /// Abstract channel_data Class
    /// The information gathered from the environment used to initiate the session
    /// </summary>
    public abstract class AbstractChannelData
    {
        /// <summary>
        /// Default: "browser" Indicates the type of channel interface being used to initiate the transaction.
        /// [Required]
        /// </summary>
        public ChannelDataType? Channel;

        protected AbstractChannelData(ChannelDataType channel)
        {
            Channel = channel;
        }
    }
}